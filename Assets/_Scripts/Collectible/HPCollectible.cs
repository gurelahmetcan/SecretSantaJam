using DG.Tweening;
using UnityEngine;

namespace SantaProject
{
    public class HPCollectible : MonoBehaviour
    {
        public int hpAmount;

        public void DestroyGameObject()
        {
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(transform.DOMove(FindObjectOfType<PlayerStats>().transform.position, 0.2f));
            mySequence.AppendCallback((() => Destroy(gameObject)));
        }
    }    
}

