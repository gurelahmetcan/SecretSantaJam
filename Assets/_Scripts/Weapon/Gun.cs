using System;
using System.Collections;
using System.Collections.Generic;
using SantaProject;
using UnityEngine;

namespace SantaProject
{
    public class Gun : MonoBehaviour
    {
        #region Fields

        [SerializeField] private WeaponData weaponData;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _bulletDirection;
        [SerializeField] private Transform _bulletContainer;

        #endregion

        #region Variables

        private float timeSinceLastShot;
        private bool _canShoot => !weaponData.reloading && timeSinceLastShot > 1f / (weaponData.fireRate / 60f);
        
        #endregion

        #region Unity Functions

        private void Start()
        {
            EventManager.Instance.onShootPressed += Shoot;
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

        private void OnDestroy()
        {
            EventManager.Instance.onShootPressed -= Shoot;
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

        private void Shoot()
        {
            if (weaponData.currentAmmo > 0 && _canShoot && weaponData.id == GameManager.Instance.weaponHolder.GetSelectedWeapon())
            {
                GameObject bullet = Instantiate(_bulletPrefab, _bulletDirection.position, _bulletDirection.rotation, _bulletContainer);
                bullet.GetComponent<Bullet>().SetDamage(weaponData.damage);
                bullet.SetActive(true);
                EventManager.Instance.onShoot?.Invoke();
                weaponData.currentAmmo--;
                timeSinceLastShot = 0;
            }
        }
    }
}
