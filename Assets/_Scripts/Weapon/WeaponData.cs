using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SantaProject
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/New Weapon")]
    public class WeaponData : ScriptableObject
    {
        [Header("Info")]
        public new string name;
        public int id;

        [Header("Shooting")]
        public int damage;
        public float maxDistance;

        [Header("Reloading")]
        public int currentAmmo;
        public int magSize;
        public float fireRate;
        public float reloadTime;
        [HideInInspector]
        public bool reloading;
    }
}

