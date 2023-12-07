using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SantaProject
{
    [CreateAssetMenu(menuName = "UpgradeItem")]
    public class LevelItem : ScriptableObject
    {
        [Header("UI")] 
        [SerializeField] private Sprite Icon;
        [SerializeField] private string Title;
        [SerializeField] private string Description;

        [Header("Type")] [SerializeField] private Constants.UpgradeType UpgradeType;

        [Header("Stats")] [SerializeField] private float IncreaseAmount;
    }
}
