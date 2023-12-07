using System;
using UnityEngine;

namespace SantaProject
{
    public class EventManager : MonoBehaviour
    {
        public static EventManager Instance;

        public Action<int> onExperienceChange;
        public Action<int> onLevelUp;
        public Action<int, int> onHealthChange;
        public Action<int, int> onExpChange;
        public Action<Transform> enemyDeadEvent;
        public Action onPresentCollected;
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

        public void AddExperience(int amount)
        {
            onExperienceChange?.Invoke(amount);
        }
    }
}

