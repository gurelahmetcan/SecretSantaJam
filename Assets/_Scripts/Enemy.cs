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

        [HideInInspector] public double weight;
        
        public GameObject prefab;
        [Range(0f, 100f)] public float chance;

        #region Variables
        
        private Transform _playerTransform;
        private bool _canDamage = true;
        private int _baseHp;
        
        private Animator _animator;
        private bool _isMoving;

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

        private void Update()
        {
            ChasePlayer();
        }

        #endregion
        
        public void Damage(int damageTaken)
        {
            hp -= damageTaken;
            
            if (hp <= 0)
            {
                EventManager.Instance.enemyDeadEvent.Invoke(gameObject.transform);
                Destroy(gameObject);
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
        
        private void AnimateMove()
        {
            var animName = type == Constants.EnemyType.Normal ? "isWalking" : "isAggroWalking";
            _animator.SetBool(animName, !_agent.isStopped);
        }
    }
    
}
