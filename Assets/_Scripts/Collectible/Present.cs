using DG.Tweening;
using UnityEngine;

namespace SantaProject
{
    public class Present : MonoBehaviour
    {
        public void DestroyGameObject()
        {
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(transform.DOMove(FindObjectOfType<PlayerStats>().transform.position, 0.2f));
            mySequence.AppendCallback((() => Destroy(gameObject)));
        }
    }
}

