using System;
using UnityEngine;

namespace SantaProject
{
    public class EventManager : MonoBehaviour
    {
        public static EventManager Instance;

        [Header("Pick Up Events")]
        public Action<int> onExperienceChange;
        public Action<int> onLevelUp;
        public Action<int, int> onHealthChange;
        public Action<int, int> onExpChange;
        public Action onPresentCollected;

        public Action<Transform> enemyDeadEvent;

        [Header("Player Events")] 
        public Action onShootPressed;
        public Action onShoot;
        public Action<float> onDashPressed;

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

