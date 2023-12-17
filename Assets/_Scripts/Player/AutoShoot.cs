using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SantaProject
{
    public class AutoShoot : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform _bulletDirection;
        [SerializeField] private Transform _bulletContainer;

        #endregion

        #region Variables

        private bool canShoot = true;
        private GameObject target;

        private WeaponData weaponData;

        private Transform nearestEnemy;
        private float nearestEnemyDistance;
        
        private ParticleSystem _particle;

        #endregion
        
        #region Unity Methods
        
        void Start()
        {
            nearestEnemyDistance = Mathf.Infinity;
            weaponData.currentAmmo = weaponData.magSize;
            weaponData.reloading = false;
        }

        void Update () {
            target = FindClosestEnemy();

            if (weaponData.currentAmmo == 0 && !weaponData.reloading)
            {
                StartReloading();
            }
            
            if (target!=null)
            {
                transform.LookAt(target.transform);

                if (canShoot && !weaponData.reloading)
                {
                    Shoot();
                }
            }
        }

        #endregion

        #region Public Methods

        public void ChangeWeapon(WeaponData data, ParticleSystem particle)
        {
            weaponData = data;
            _particle = particle;
        }

        #endregion

        #region Private Methods

        private void Shoot()
        {
            for (int i = 0; i < weaponData.shootAmount; i++)
            {
                if (Vector3.Distance(target.transform.position, transform.position)<= weaponData.maxDistance)
                {
                    canShoot = false;

                    StartCoroutine("AllowToShoot");

                    GameObject bullet = Instantiate(bulletPrefab, transform.position, _bulletDirection.rotation, _bulletContainer);
                    bullet.SetActive(true);
                    target.GetComponent<Enemy>().Damage(weaponData.damage);
                    
                    if (!_particle.gameObject.activeSelf)
                    {
                        _particle.gameObject.SetActive(true);
                    }
                    else
                    {
                        _particle.Play();
                    }
                    EventManager.Instance.onShoot?.Invoke();
                    weaponData.currentAmmo--;
                }
            }

            EventManager.Instance.onShoot?.Invoke();
        }

        private GameObject FindClosestEnemy()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            // If no enemies found at all directly return nothing
            // This happens if there simply is no object tagged "Enemy" in the scene
            if(enemies.Length == 0)
            {
                Debug.LogWarning("No enemies found!", this);
                return null;
            }

            GameObject closest;

            // If there is only exactly one anyway skip the rest and return it directly
            if(enemies.Length == 1)
            {
                closest = enemies[0];
                return closest;
            }


            // Otherwise: Take the enemies
            closest = enemies
                // Order them by distance (ascending) => smallest distance is first element
                .OrderBy(go => (transform.position - go.transform.position).sqrMagnitude)
                // Get the first element
                .First();
            
            return closest;
        }
        
        private void StartReloading()
        {
            if (!weaponData.reloading)
            {
                StartCoroutine(Reload());
            }
        }

        #endregion

        #region Coroutines

        IEnumerator Reload()
        {
            weaponData.reloading = true;
            var anim = FindObjectOfType<PlayerStats>().GetComponent<Animator>();
            anim.SetTrigger("isReloading");
            yield return new WaitForSeconds(weaponData.reloadTime);
            weaponData.currentAmmo = weaponData.magSize;
            weaponData.reloading = false;
        }
        
        IEnumerator AllowToShoot ()
        {
            yield return new WaitForSeconds(1);
            canShoot = true;
        } 

        #endregion
    }
}

