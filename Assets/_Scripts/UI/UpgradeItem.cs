using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SantaProject
{
    public class UpgradeItem : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI desc;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI stats;
        [SerializeField] private Image levelBar;

        private Constants.UpgradeType upgradeType;
        private LevelItem data;
        private int level;

        public void OnCardSelection()
        {
            GameManager.Instance.UpgradePlayer(upgradeType);
            MainUI.Instance.OpenCloseLevelUp(false);
            data.Level++;
            if (data.Level >= data.MaxLevel)
            {
                LevelUpPanel.Instance.RemoveFromPool(upgradeType);
            }
        }

        public void SetUpgradeCard(LevelItem _data)
        {
            data = _data;
            title.text = data.Title;
            desc.text = data.Description;
            icon.sprite = data.Icon;
            upgradeType = data.UpgradeType;
            levelBar.fillAmount = (float)data.Level / data.MaxLevel;
            levelText.text = $"LEVEL {data.Level + 1}";
        }
    } 
}

