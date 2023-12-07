using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SantaProject
{ 
    public class PlayerStats : MonoBehaviour
    {
        [SerializeField] private int 
            currentHealth, maxHealth, 
            currentExperience, maxExperience, 
            currentLevel ;
    
        public int MaxHealth
        {
            get => maxHealth;

            set => maxHealth = value;
        }
        private void OnEnable()
        {
            if (EventManager.Instance)
            {
                EventManager.Instance.onExperienceChange += HandleExperienceChange;
            }
        }
    
        private void OnDisable()
        {
            EventManager.Instance.onExperienceChange -= HandleExperienceChange;
        }
    
        private void HandleExperienceChange(int experienceAmount)
        {
            currentExperience += experienceAmount;

            EventManager.Instance.onExpChange?.Invoke(currentExperience, maxExperience);
    
            if (currentExperience >= maxExperience)
            {
                LevelUp();
            }
        }
    
        public void TakeHeal(int healAmount)
        {
            currentHealth += healAmount;
            
            if (currentHealth >= maxHealth)
            {
                currentHealth = maxHealth;
            }
            
            EventManager.Instance.onHealthChange?.Invoke(currentHealth, maxHealth);
        }
        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            
            EventManager.Instance.onHealthChange?.Invoke(currentHealth, maxHealth);

            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    
        private void LevelUp()
        {
            maxHealth += 10;
            currentHealth = maxHealth;
    
            currentLevel++;
            currentExperience = 0;
            maxExperience += 100;
            
            EventManager.Instance.onHealthChange?.Invoke(currentHealth, maxHealth);
            EventManager.Instance.onLevelUp?.Invoke(currentLevel);
            EventManager.Instance.onExpChange?.Invoke(currentExperience, maxExperience);
        }
    }
}

