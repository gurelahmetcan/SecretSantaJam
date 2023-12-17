using System.Collections;
using UnityEngine;

namespace SantaProject
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed = 20f;
        
        private int _damage;
        
        void Update()
        {
            transform.Translate(transform.forward * _speed * Time.deltaTime, Space.World);
        }

        public void SetDamage(int dmg)
        {
            _damage = dmg;
        }

        IEnumerator DestroyBulletAfterTimeCoroutine()
        {
            yield return new WaitForSeconds(1f);
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
                //other.gameObject.GetComponent<Enemy>().Damage(_damage);
                Destroy(gameObject);
            }
        }
    }
    
}
