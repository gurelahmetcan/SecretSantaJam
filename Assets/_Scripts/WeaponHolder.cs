using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SantaProject
{
    public class WeaponHolder : MonoBehaviour
    {
        [SerializeField] private GameObject[] weapons;
        [SerializeField] private WeaponData[] data;
        [SerializeField] private ParticleSystem[] particles;
        [SerializeField] private Animator animator;
        [SerializeField] private AutoShoot autoShoot;
        
        private int selectedWeapon = 0;

        private void Start()
        {
            // TODO: Is this the best way to start ? This is for event invoke check again
            StartCoroutine(LateStart(.01f));
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                UpgradeWeapon();
            }
        }

        public void UpgradeWeapon()
        {
            selectedWeapon = 1;
            SelectWeapon();
        }

        public int GetSelectedWeapon()
        {
            return selectedWeapon;
        }
        
        IEnumerator LateStart(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            SelectWeapon();
        }

        private void SelectWeapon()
        {
            int i = 0;
            autoShoot.ChangeWeapon(data[selectedWeapon], particles[selectedWeapon]);
            EventManager.Instance.onAmmoChanged?.Invoke(data[selectedWeapon].currentAmmo, data[selectedWeapon].magSize, false);
            animator.SetInteger("weapon", selectedWeapon);
            foreach (var weapon in weapons)
            {
                weapon.gameObject.SetActive(i == selectedWeapon);
                i++;
            }
        }
    }
}