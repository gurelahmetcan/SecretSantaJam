using DG.Tweening;
using UnityEngine;

namespace SantaProject
{
    public class Present : MonoBehaviour
    {
        public void Collect()
        {
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(transform.DOMove(FindObjectOfType<PlayerStats>().transform.position, 0.2f))
                .AppendCallback(() => EventManager.Instance.onPresentCollected.Invoke())
                .AppendCallback(() => Destroy(gameObject));
        }
    }
}

