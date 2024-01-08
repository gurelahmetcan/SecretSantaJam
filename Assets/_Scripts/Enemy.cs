using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace SantaProject
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        [SerializeField] private int hp;
        [SerializeField] private int damage;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Constants.EnemyType type;
        [SerializeField] private SimpleFlash flashEffect;

        [HideInInspector] public double weight;
        
        public GameObject prefab;
        [Range(0f, 100f)] public float chance;

        #region Variables
        
        private Transform _playerTransform;
        private bool _canDamage = true;
        private int _baseHp;
        
        private Animator _animator;
        private bool _isMoving;
        private bool _isAlive = true;

        private bool _knockback;
        private Vector3 direction;
        
        #endregion

        #region Unity Methods

        private void Awake()
        {
            _baseHp = hp;
            
            try
            {
                _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            }
            catch (Exception e)
            {
                _playerTransform = null;
            }
            
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _knockback = false;
        }

        private void Update()
        {
            ChasePlayer();
        }

        private void FixedUpdate()
        {
            if (_knockback)
            {
                _agent.velocity = transform.forward * -3;
            }
        }

        #endregion

        IEnumerator KnockBack()
        {
            _knockback = true;

            yield return new WaitForSeconds(0.2f);
            
            _knockback = false;
        }
        
        public void Damage(int damageTaken, Transform hitPos, bool canKnockBack)
        {
            if (_isAlive)
            {
                flashEffect.Flash();
                hp -= damageTaken;
                direction = hitPos.transform.forward;
                
                if (canKnockBack)
                {
                    StartCoroutine(KnockBack());
                }
                
                if (hp <= 0)
                {
                    _isAlive = false;
                    StartCoroutine(WaitBeforeDie());
                }
            }
        }

        public int GiveDamage()
        {
            _animator.SetTrigger("isAttacking");
            return _canDamage ? damage : 0;
        }

        private void ChasePlayer()
        {
            if (_playerTransform)
            {
                _agent.SetDestination(_playerTransform.position);
                AnimateMove();
            }
        }
        
        IEnumerator CanDamageCoroutine()
        {
            _canDamage = false;
            yield return new WaitForSeconds(.5f);
            _canDamage = true;
        }

        IEnumerator WaitBeforeDie()
        {
            yield return new WaitForSeconds(.2f);
            EventManager.Instance.enemyDeadEvent.Invoke(gameObject);
            Destroy(gameObject);
        }
        
        private void AnimateMove()
        {
            var animName = type == Constants.EnemyType.Normal ? "isWalking" : "isAggroWalking";
            _animator.SetBool(animName, !_agent.isStopped);
        }
        
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _playerTransform.GetComponent<PlayerStats>().TakeDamage(GiveDamage());
            }
        }
    }
    
}
