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

        private int newLevel;
        private Sequence mySequence;
        void Start()
        {
            healthBar.fillAmount = 1f;
            experienceBar.fillAmount = 0f;
            EventManager.Instance.onHealthChange += OnHealthChanged;
            EventManager.Instance.onExpChange += OnExperienceChanged;
        }

        private void OnDestroy()
        {
            EventManager.Instance.onHealthChange -= OnHealthChanged;
            EventManager.Instance.onExpChange -= OnExperienceChanged;
        }

        private void OnHealthChanged(int currentHealth, int maxHealth)
        {
            float targetVal = (float)currentHealth / maxHealth;
            healthBar.DOFillAmount(targetVal, 1f);
        }
        
        private void OnExperienceChanged(int currentExp, int maxExp)
        {
            //TODO: There is a problem in here that makes game crash probably when you get two XP at the same time

            if (mySequence != null)
            {
                mySequence.Kill();
                mySequence = null;
            }
            
            mySequence = DOTween.Sequence();
            float targetVal = (float)currentExp / maxExp;
            mySequence.Append(experienceBar.DOFillAmount(targetVal, 1f));

            if (currentExp >= maxExp)
            {
                mySequence.AppendCallback(() =>
                {
                    Debug.Log("Here");
                    newLevel = int.Parse(levelText.text) + 1;
                    levelText.text = newLevel.ToString();
                    EventManager.Instance.onLevelUp?.Invoke();
                    mySequence.Append(experienceBar.DOFillAmount(0f, 0.5f));
                });
            }

        }
    }
}

