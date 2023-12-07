using System.Collections;
using UnityEngine;

namespace SantaProject
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed = 12f;
        [SerializeField] private int _damage = 2;

        void Update()
        {
            transform.Translate(transform.forward * _speed * Time.deltaTime, Space.World);
        }

        IEnumerator DestroyBulletAfterTimeCoroutine()
        {
            yield return new WaitForSeconds(3f);
            Destroy(gameObject);
        }
        
        private void OnEnable() 
        {
            StartCoroutine(DestroyBulletAfterTimeCoroutine());
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<Enemy>().TakeDamage(_damage);
                Destroy(gameObject);
            }
        }
    }
    
}
