using System.Collections.Generic;
using SantaProject;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SantaProject
{
    public class LevelUpPanel : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Transform parent;
        [SerializeField] private List<LevelItem> allLevelItems;
        [SerializeField] private List<UpgradeItem> upgradeItems;

        #endregion

        #region Variables

        private List<LevelItem> levelPool = new();
        private static LevelUpPanel _instance;

        #endregion

        public static LevelUpPanel Instance
        {
            get
            {
                if (_instance is null)
                {
                    Debug.Log("LevelUpPanel is NULL");
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

        #region Public Methods

        public void CreateUpgrades()
        {
            int selectedUpgrade1 = SelectUpgrade();
            int selectedUpgrade2 = SelectUpgrade();
            int selectedUpgrade3 = SelectUpgrade();

            if (levelPool.Count >= 3)
            {
                while (selectedUpgrade1 == selectedUpgrade2 || selectedUpgrade1 == selectedUpgrade3 || selectedUpgrade2 == selectedUpgrade3)
                {
                    while (selectedUpgrade1.Equals(selectedUpgrade2))
                    {
                        selectedUpgrade2 = SelectUpgrade();
                    }
                    while (selectedUpgrade1.Equals(selectedUpgrade3))
                    {
                        selectedUpgrade3 = SelectUpgrade();
                    }
                    while (selectedUpgrade2.Equals(selectedUpgrade3))
                    {
                        selectedUpgrade3 = SelectUpgrade();
                    }
                }
            }
        
            //TODO: If levelPool.Count < 3, we need to add something
        
            upgradeItems[0].SetUpgradeCard(levelPool[selectedUpgrade1]);
            upgradeItems[1].SetUpgradeCard(levelPool[selectedUpgrade2]);
            upgradeItems[2].SetUpgradeCard(levelPool[selectedUpgrade3]);
        }
    
        public void RemoveFromPool(Constants.UpgradeType remove)
        {
            var removeObject = levelPool.Find(x => x.UpgradeType == remove);
            if (removeObject != null)
            {
                Debug.Log($"Removed object {removeObject}");
                levelPool.Remove(removeObject);
            }
            else
            {
                Debug.Log("There is no removeObject in pool");
            }
        }

        public void InitializeUpgradePool()
        {
            foreach (var levelItem in allLevelItems)
            {
                levelPool.Add(levelItem);
            }
        }

        #endregion

        #region Private Methods

        private int SelectUpgrade()
        {
            if (GameManager.Instance.shotgunUpgraded)
            {
                var shotgunUpgrade = levelPool.Find(x => x.Title == "Shotgun");
                levelPool.Remove(shotgunUpgrade);
            }
        
            int number = Random.Range(0, levelPool.Count);

            return number;
        }

        #endregion
    }
}
