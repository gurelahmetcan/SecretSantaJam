using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SantaProject
{
    [CreateAssetMenu(menuName = "UpgradeItem")]
    public class LevelItem : ScriptableObject
    {
        [Header("UI")] 
        public Sprite Icon;
        public string Title;
        public string Description;

        [Header("Type")] public Constants.UpgradeType UpgradeType;

        [Header("Stats")] public float IncreaseAmount;
    }
}
