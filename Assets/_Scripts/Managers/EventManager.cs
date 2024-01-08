using System;
using UnityEngine;
using UnityEngine.Events;

namespace SantaProject
{
    public class EventManager : MonoBehaviour
    {
        public static EventManager Instance;

        [Header("Pick Up Events")]
        public Action<int> onExperienceChange;
        public Action onLevelUp;
        public Action<int, int> onHealthChange;
        public Action<int, int> onExpChange;
        public Action onPresentCollected;

        public Action<GameObject> enemyDeadEvent;

        [Header("Player Events")] 
        public Action onShootPressed;
        public Action onShoot;
        public Action<float> onDashPressed;
        public Action<GameObject> onEnemySpawned;
        public Action<int> onWaveEnd;
        public Action onWaveBreakEnds;
        public Action onGameOver;
        public Action<int,int,bool> onAmmoChanged;

        // Singleton Check
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }
    }
}

