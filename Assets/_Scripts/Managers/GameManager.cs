using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SantaProject
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject expPrefab;
        [SerializeField] private GameObject player;
        [SerializeField] private Magnet magnet;
        public WeaponHolder weaponHolder;

        private static GameManager _instance;
        public int bonusDmg;
        public int bonusExp;
        public bool shotgunUpgraded;
        public int killedEnemyCount;

        public static GameManager Instance
        {
            get
            {
                if (_instance is null)
                {
                    Debug.Log("Game Manager is NULL");
                }

                return _instance;
            }
        }
        
        private void Awake()
        {
            _instance = this;
            bonusDmg = 0;
            bonusExp = 0;
        }

        private void Start()
        {
            EventManager.Instance.enemyDeadEvent += OnEnemyDead;
        }

        private void OnDestroy()
        {
            EventManager.Instance.enemyDeadEvent -= OnEnemyDead;
        }

        private void OnEnemyDead(GameObject pos)
        {
            var position = pos.transform.position;
            killedEnemyCount++;
            Instantiate(expPrefab, new Vector3(position.x, 0.5f, position.z), pos.transform.rotation);
        }

        public void HealPlayer(int healAmount)
        {
            player.GetComponent<PlayerStats>().TakeHeal(healAmount);
        }

        public void UpgradePlayer(LevelItem _data)
        {
            Debug.Log($"Upgrade Type: {_data.UpgradeType}");
            switch (_data.UpgradeType)
            {
                case Constants.UpgradeType.Hp:
                    player.GetComponent<PlayerStats>().MaxHealth += (int)_data.IncreaseAmount;
                    player.GetComponent<PlayerStats>().ChangeHealth();
                    break;
                case Constants.UpgradeType.Damage:
                    bonusDmg += (int)_data.IncreaseAmount;
                    break;
                case Constants.UpgradeType.FireRate:
                    // TODO: FireRate Upgrade
                    /*
                    if (player.GetComponent<PlayerController>().FireRate <= 0)
                    {
                        break;
                    }
                    player.GetComponent<PlayerController>().FireRate -= .1f;
                    */
                    break;
                case Constants.UpgradeType.CollectRange:
                    magnet.UpgradeMagnetRange();
                    break;
                case Constants.UpgradeType.ExpModifier:
                    bonusExp += (int)_data.IncreaseAmount;
                    break;
                case Constants.UpgradeType.GunUpgrade:
                    weaponHolder.UpgradeWeapon();
                    shotgunUpgraded = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
    
}
