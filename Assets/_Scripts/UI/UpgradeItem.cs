using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SantaProject
{
    public class UpgradeItem : MonoBehaviour
    {
        [SerializeField] private Constants.UpgradeType upgradeType;
        
        public void OnCardSelection()
        {
            GameManager.Instance.UpgradePlayer(upgradeType);
            MainUI.Instance.OpenCloseLevelUp(false);
        }
    } 
}

