using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SantaProject
{
    public class WeaponHolder : MonoBehaviour
    {
        [SerializeField] private GameObject[] weapons;
        [SerializeField] private Animator animator;
        
        private int selectedWeapon = 0;

        private void Start()
        {
            SelectWeapon();
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
            animator.SetInteger("weapon", selectedWeapon);
            foreach (var weapon in weapons)
            {
                weapon.SetActive(i == selectedWeapon);
                i++;
            }
        }
    }
}