using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SantaProject
{
    public class StatBar : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private Image healthBar;
        [SerializeField] private Image experienceBar;
        
        void Start()
        {
            EventManager.Instance.onLevelUp += OnLevelChanged;
            EventManager.Instance.onHealthChange += OnHealthChanged;
            EventManager.Instance.onExpChange += OnExperienceChanged;
        }

        private void OnDestroy()
        {
            EventManager.Instance.onLevelUp -= OnLevelChanged;
            EventManager.Instance.onHealthChange -= OnHealthChanged;
            EventManager.Instance.onExpChange -= OnExperienceChanged;
        }

        private void OnLevelChanged(int newLevel)
        {
            levelText.text = newLevel.ToString();
        }

        private void OnHealthChanged(int currentHealth, int maxHealth)
        {
            float targetVal = (float)currentHealth / maxHealth;
            healthBar.DOFillAmount(targetVal, 1f);
        }
        
        private void OnExperienceChanged(int currentExp, int maxExp)
        {
            Sequence mySequence = DOTween.Sequence();
            float targetVal = (float)currentExp / maxExp;
            mySequence.Append(experienceBar.DOFillAmount(targetVal, 1f));
        }
    }
}

