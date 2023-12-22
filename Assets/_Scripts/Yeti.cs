using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SantaProject
{
    public class Yeti : MonoBehaviour
    {
        #region Fields

        [SerializeField] private int hp;
        [SerializeField] private int damage;
        
        #endregion
        
        #region Variables
        
        private NavMeshAgent _agent;
        private SimpleFlash flashEffect;

        private Transform _playerTransform;
        private int _baseHp;
        private Animator _animator;
        
        private bool _canDamage = true;
        private bool _isMoving;
        private bool _isAlive = true;
        
        #endregion
        
        #region Unity Methods

        private void Awake()
        {
            _baseHp = hp;
            
            try
            {
                _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            }
            catch (Exception _)
            {
                _playerTransform = null;
            }
            
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            flashEffect = GetComponent<SimpleFlash>();
        }

        private void Update()
        {
            ChasePlayer();
        }

        #endregion

        #region Public Methods

        public void Damage(int damageTaken)
        {
            if (_isAlive)
            {
                flashEffect.Flash();
                hp -= damageTaken;
                if (hp <= 0)
                {
                    _isAlive = false;
                    StartCoroutine(WaitBeforeDie());
                }
            }
        }

        public int GiveDamage()
        {
            _animator.SetTrigger("AttackClose");
            return _canDamage ? damage : 0;
        }

        #endregion

        #region Private Methods

        private void ChasePlayer()
        {
            if (_playerTransform)
            {
                _agent.SetDestination(_playerTransform.position);
                AnimateMove();
            }
        }
        
        private void AnimateMove()
        {
            var animName = "isWalking";
            _animator.SetBool(animName, !_agent.isStopped);
        }
        
        IEnumerator WaitBeforeDie()
        {
            yield return new WaitForSeconds(.5f);
            var animName = "isDead";
            _animator.SetBool(animName, true);
            _agent.isStopped = true;
            yield return new WaitForSeconds(2f);
            Destroy(gameObject);
        }
        
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _playerTransform.GetComponent<PlayerStats>().TakeDamage(GiveDamage());
            }
        }

        #endregion
    }
}

