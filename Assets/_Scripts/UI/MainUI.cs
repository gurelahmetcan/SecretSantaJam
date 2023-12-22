using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SantaProject
{
    public class MainUI : MonoBehaviour
    {
        [SerializeField] private GameObject statBar;
        [SerializeField] private LevelUpPanel levelUpPanel;
        [SerializeField] private GameOverPanel gameOverPanel;

        private static MainUI _instance;
        private int _presentAmount;

        public static MainUI Instance
        {
            get
            {
                if (_instance is null)
                {
                    Debug.Log("MainUI is NULL");
                }

                return _instance;
            }
        }
        
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this);
            }
            else
            {
                _instance = this;
            }
        }
        
        private void Start()
        {
            EventManager.Instance.onLevelUp += OnLevelChanged;
            EventManager.Instance.onPresentCollected += OnPresentCollected;
            EventManager.Instance.onGameOver += OnGameOver;
            
            gameOverPanel.gameObject.SetActive(false);
            levelUpPanel.InitializeUpgradePool();
        }

        private void OnDestroy()
        {
            EventManager.Instance.onLevelUp -= OnLevelChanged;
            EventManager.Instance.onPresentCollected -= OnPresentCollected;
            EventManager.Instance.onGameOver -= OnGameOver;
        }

        private void OnLevelChanged()
        {
            levelUpPanel.CreateUpgrades();
            OpenCloseLevelUp(true);
        }

        public void OpenCloseLevelUp(bool isOpen)
        {
            levelUpPanel.gameObject.SetActive(isOpen);
            Time.timeScale = isOpen ? 0 : 1;
        }

        private void OnPresentCollected()
        {
            _presentAmount++;
            Debug.Log($"Present Amount {_presentAmount}");
        }

        private void OnGameOver()
        {
            gameOverPanel.gameObject.SetActive(true);
        }
    }
}

