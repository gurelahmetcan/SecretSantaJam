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
            SelectWeapon();
        }

        private void OnDestroy()
        {
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

        private void SelectWeapon()
        {
            int i = 0;
            autoShoot.ChangeWeapon(data[selectedWeapon], particles[selectedWeapon]);
            animator.SetInteger("weapon", selectedWeapon);
            foreach (var weapon in weapons)
            {
                weapon.gameObject.SetActive(i == selectedWeapon);
                i++;
            }
        }
    }
}