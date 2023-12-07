using UnityEngine;

namespace SantaProject
{
    public class HPCollectible : MonoBehaviour
    {
        public int hpAmount;

        public void DestroyGameObject()
        {
            Destroy(gameObject);
        }
    }    
}

