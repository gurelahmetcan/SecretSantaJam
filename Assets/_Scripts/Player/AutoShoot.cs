using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SantaProject
{
    public class AutoShoot : MonoBehaviour
    {
        #region Variables

        private bool canShoot = true;
        private GameObject target;

        private WeaponData weaponData;

        private Transform nearestEnemy;
        
        private ParticleSystem _particle;
        private AudioSource audioSource;
        private bool isOutOfRange=true;

        #endregion
        
        #region Unity Methods

        void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        void Update () {
            target = FindClosestEnemy();

            if (weaponData != null)
            {
                if (CheckIfReload())
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
        }

        #endregion

        #region Public Methods

        public void ChangeWeapon(WeaponData data, ParticleSystem particle)
        {
            weaponData = data;
            weaponData.currentAmmo = weaponData.magSize;
            weaponData.reloading = false;
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
                    isOutOfRange = false;
                    canShoot = false;

                    StartCoroutine(AllowToShoot());

                    //Check Enemy Type
                    if (target.GetComponent<Enemy>() == null)
                    {
                        target.GetComponent<Yeti>().Damage(weaponData.damage + GameManager.Instance.bonusDmg);
                    }
                    else
                    {
                        target.GetComponent<Enemy>().Damage(weaponData.damage + GameManager.Instance.bonusDmg, transform, weaponData.canKnockBack);
                    }

                    if (!_particle.gameObject.activeSelf)
                    {
                        _particle.gameObject.SetActive(true);
                    }
                    else
                    {
                        _particle.Play();
                    }

                    //Shoot sound
                    audioSource.clip = weaponData.shootSound;
                    audioSource.Play();
                    
                    //Camera Shake when shoot
                    CinemachineShake.Instance.ShakeCamera(1.25f, .1f);
                    
                    EventManager.Instance.onShoot?.Invoke();
                    weaponData.currentAmmo--;
                    EventManager.Instance.onAmmoChanged?.Invoke(weaponData.currentAmmo, weaponData.magSize, false);
                }
                else
                {
                    isOutOfRange = true;
                }
            }
        }

        private GameObject FindClosestEnemy()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            // If no enemies found at all directly return nothing
            // This happens if there simply is no object tagged "Enemy" in the scene
            if(enemies.Length == 0)
            {
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

        private bool CheckIfReload()
        {
            return ((weaponData.currentAmmo == 0) ||
                    (weaponData.currentAmmo != weaponData.magSize && isOutOfRange) ||
                    (weaponData.currentAmmo != weaponData.magSize && target == null)) && !weaponData.reloading;
        }

        #endregion

        #region Coroutines

        IEnumerator Reload()
        {
            weaponData.reloading = true;
            EventManager.Instance.onAmmoChanged?.Invoke(weaponData.currentAmmo, weaponData.magSize, true);
            var anim = FindObjectOfType<PlayerStats>().GetComponent<Animator>();
            anim.SetTrigger("isReloading");
            yield return new WaitForSeconds(weaponData.reloadTime);
            weaponData.currentAmmo = weaponData.magSize;
            EventManager.Instance.onAmmoChanged?.Invoke(weaponData.currentAmmo, weaponData.magSize, false);
            weaponData.reloading = false;
        }
        
        IEnumerator AllowToShoot()
        {
            yield return new WaitForSeconds(.4f);
            canShoot = true;
        } 

        #endregion
    }
}

