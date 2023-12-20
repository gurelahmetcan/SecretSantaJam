using System;
using System.Collections;
using System.Collections.Generic;
using SantaProject;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelUpPanel : MonoBehaviour
{
    #region Fields

    [SerializeField] private Transform parent;
    [SerializeField] private List<LevelItem> allLevelItems;
    [SerializeField] private List<UpgradeItem> upgradeItems;

    #endregion

    #region Variables

    private List<LevelItem> levelPool = new();

    #endregion
    
    public void CreateUpgrades()
    {
        int selectedUpgrade1 = SelectUpgrade();
        int selectedUpgrade2 = SelectUpgrade();
        int selectedUpgrade3 = SelectUpgrade();

        while (selectedUpgrade1 == selectedUpgrade2 || selectedUpgrade1 == selectedUpgrade3 || selectedUpgrade2 == selectedUpgrade3)
        {
            while (selectedUpgrade1.Equals(selectedUpgrade2))
            {
                selectedUpgrade2 = SelectUpgrade();
            }
            while (selectedUpgrade1.Equals(selectedUpgrade2))
            {
                selectedUpgrade3 = SelectUpgrade();
            }
            while (selectedUpgrade1.Equals(selectedUpgrade2))
            {
                selectedUpgrade3 = SelectUpgrade();
            }
        }
        
        upgradeItems[0].SetUpgradeCard(allLevelItems[selectedUpgrade1]);
        upgradeItems[1].SetUpgradeCard(allLevelItems[selectedUpgrade2]);
        upgradeItems[2].SetUpgradeCard(allLevelItems[selectedUpgrade3]);

    }

    private int SelectUpgrade()
    {
        int number = Random.Range(0, allLevelItems.Count-1);

        while (levelPool[number]==null)
        {
            number = Random.Range(0, allLevelItems.Count-1);
        }

        return number;
    }

    public void RemoveFromPool(int remove)
    {
        levelPool[remove] = null;
    }

    public void InitializeUpgradePool()
    {
        foreach (var levelItem in allLevelItems)
        {
            levelPool.Add(levelItem);
        }
    }
}
