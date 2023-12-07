using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SantaProject
{
    public class Magnet : MonoBehaviour
    {
        private CapsuleCollider _collider;

        private void Awake()
        {
            _collider = GetComponent<CapsuleCollider>();
        }

        public void UpgradeMagnetRange()
        {
            _collider.radius = .5f;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Experience"))
            {
                var item = other.gameObject.GetComponent<Exp>();
                EventManager.Instance.AddExperience(item.experience);
                item.DestroyGameObject();
            }

            if (other.gameObject.CompareTag("HealthCollectible"))
            {
                var item = other.gameObject.GetComponent<HPCollectible>();
                GameManager.Instance.HealPlayer(item.hpAmount);
                item.DestroyGameObject();
            }

            if (other.gameObject.CompareTag("Present"))
            {
                var item = other.gameObject.GetComponent<Present>();
                EventManager.Instance.onPresentCollected.Invoke();
                item.DestroyGameObject();
            }
        }
    }
}