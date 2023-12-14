using DG.Tweening;
using UnityEngine;

namespace SantaProject
{
    public class Exp : MonoBehaviour
    {
        public int experience;
        public void Collect()
        {
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(transform.DOMove(FindObjectOfType<PlayerStats>().transform.position, 0.2f))
                .AppendCallback(() => EventManager.Instance.AddExperience(experience))
                .AppendCallback(() => Destroy(gameObject));
        }
    }
    
}
