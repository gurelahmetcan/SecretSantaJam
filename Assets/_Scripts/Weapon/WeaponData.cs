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
        public AudioClip shootSound;

        [Header("Shooting")]
        public int damage;
        public float maxDistance;
        public float spread;
        public int shootAmount;
        public bool canKnockBack;

        [Header("Reloading")]
        public int currentAmmo;
        public int magSize;
        public float fireRate;
        public float reloadTime;
        [HideInInspector]
        public bool reloading;
    }
}

