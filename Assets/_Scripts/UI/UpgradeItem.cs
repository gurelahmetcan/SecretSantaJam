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
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI stats;

        private Constants.UpgradeType upgradeType;
        private LevelItem data;

        public void OnCardSelection()
        {
            GameManager.Instance.UpgradePlayer(upgradeType);
            MainUI.Instance.OpenCloseLevelUp(false);
        }

        public void SetUpgradeCard(LevelItem _data)
        {
            data = _data;
            title.text = data.Title;
            desc.text = data.Description;
            icon.sprite = data.Icon;
            upgradeType = data.UpgradeType;
        }
    } 
}

