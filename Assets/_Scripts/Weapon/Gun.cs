using System;
using System.Collections;
using System.Collections.Generic;
using SantaProject;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SantaProject
{
    public class Gun : MonoBehaviour
    {
        #region Fields

        [SerializeField] private WeaponData weaponData;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _bulletDirection;
        [SerializeField] private Transform _bulletContainer;
        [SerializeField] private ParticleSystem _particle;

        #endregion

        #region Variables

        private float timeSinceLastShot;
        private bool _canShoot => !weaponData.reloading && timeSinceLastShot > 1f / (weaponData.fireRate / 60f);

        #endregion

        #region Unity Functions

        private void Awake()
        {
            weaponData.currentAmmo = weaponData.magSize;
            weaponData.reloading = false;
        }

        private void Update()
        {
            timeSinceLastShot += Time.deltaTime;

            if (weaponData.currentAmmo == 0 && !weaponData.reloading)
            {
                StartReloading();
            }
        }

        #endregion

        private void StartReloading()
        {
            if (!weaponData.reloading)
            {
                StartCoroutine(Reload());
            }
        }

        private IEnumerator Reload()
        {
            weaponData.reloading = true;
            var anim = FindObjectOfType<PlayerStats>().GetComponent<Animator>();
            anim.SetTrigger("isReloading");
            yield return new WaitForSeconds(weaponData.reloadTime);
            weaponData.currentAmmo = weaponData.magSize;
            weaponData.reloading = false;
        }

        public void Shoot()
        {
            if (weaponData.currentAmmo > 0 && _canShoot && weaponData.id == GameManager.Instance.weaponHolder.GetSelectedWeapon())
            {
                timeSinceLastShot = 0;
                var spread = weaponData.spread;
                for (int i = 0; i < weaponData.shootAmount; i++)
                {
                    var pos = _bulletDirection.position;
                    GameObject bullet = Instantiate(_bulletPrefab, pos, _bulletDirection.rotation, _bulletContainer);
                    bullet.transform.localPosition = new Vector3(pos.x + spread, pos.y, pos.z);
                    bullet.GetComponent<Bullet>().SetDamage(weaponData.damage);
                    bullet.SetActive(true);
                    weaponData.currentAmmo--;
                    spread++;
                }

                if (!_particle.gameObject.activeSelf)
                {
                    _particle.gameObject.SetActive(true);
                }
                else
                {
                    _particle.Play();
                }
                EventManager.Instance.onShoot?.Invoke();
            }
        }
        
        public void ShootNew()
        {
            if (weaponData.currentAmmo > 0 && _canShoot)
            {
                timeSinceLastShot = 0;
                var spread = weaponData.spread;
                var shootAmount = weaponData.shootAmount;

                for (int i = 0; i < shootAmount; i++)
                {
                    float x = Random.Range(-spread, spread);

                    var transform1 = _bulletDirection.transform;
                    Vector3 direction = transform1.forward + new Vector3(x, 0, 0);
                    
                    GameObject bullet = Instantiate(_bulletPrefab, transform1.position + new Vector3(x,0,0), _bulletDirection.rotation, _bulletContainer);
                    //bullet.GetComponent<Bullet>().SetDamage(0);
                    bullet.SetActive(true);

                    if (Physics.Raycast(transform1.position, direction, out var rayHit, weaponData.maxDistance))
                    {
                        if (rayHit.collider.CompareTag("Enemy"))
                        {
                            rayHit.collider.GetComponent<Enemy>().Damage(weaponData.damage);
                        }
                    }
                    
                    weaponData.currentAmmo--;
                }

                if (!_particle.gameObject.activeSelf)
                {
                    _particle.gameObject.SetActive(true);
                }
                else
                {
                    _particle.Play();
                }
                EventManager.Instance.onShoot?.Invoke();
            }
        }
    }
}
