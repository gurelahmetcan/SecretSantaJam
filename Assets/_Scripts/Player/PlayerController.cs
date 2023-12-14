using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SantaProject
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private WeaponHolder _weaponHolder;
        
        [Header("Dash Attributes")]
        [SerializeField] private float _dashingPower = 5f;
        [SerializeField] private float _dashingTime = 0.3f;
        [SerializeField] private float _dashingCooldown = 1f;

        #region Variables

        private bool _isDashing;

        private Vector2 _move, _mouseLook;
        private Vector3 _rotationTarget;
        private bool _isMoving;

        private PlayerStats _playerStats;
        private InputManager _controls;
        private Animator _animator;
        
        #endregion
        
        #region Unity Methods

        private void Awake()
        {
            _controls = new InputManager();
            _playerStats = gameObject.GetComponent<PlayerStats>();
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _controls.Player.Shoot.performed += _ => EventManager.Instance.onShootPressed?.Invoke();
            _controls.Player.Dash.performed += _ => Dash();
            EventManager.Instance.onShoot += () => ShootAnim();
        }

        void Update()
        {
            if (Time.timeScale == 0)
            {
                return;
            }
            
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(_mouseLook);

            if (Physics.Raycast(ray, out hit))
            {
                _rotationTarget = hit.point;
            }
            
            Move();
        }

        private void OnEnable()
        {
            _controls.Enable();
        }

        private void OnDisable()
        {
            _controls.Disable();
        }

        #endregion

        #region Public Methods

        public void OnMove(InputAction.CallbackContext callbackContext)
        {
            _move = callbackContext.ReadValue<Vector2>();
        }
        
        public void OnMouseLook(InputAction.CallbackContext callbackContext)
        {
            _mouseLook = callbackContext.ReadValue<Vector2>();
        }

        #endregion

        #region Private Methods
        
        private void ShootAnim()
        {
            _animator.SetTrigger("isAttacking");
        }
        
        private void Dash()
        {
            if (!_isDashing)
            {
                StartCoroutine(DashCoroutine());
            }
        }

        private IEnumerator DashCoroutine()
        {
            _isDashing = true;
            transform.DOMove(transform.position + (new Vector3(_move.x, 0f, _move.y) * _dashingPower), _dashingTime);
            EventManager.Instance.onDashPressed.Invoke(_dashingCooldown);
            yield return new WaitForSeconds(_dashingCooldown);
            _isDashing = false;
        }

        private void Move()
        {
            var lookPos = _rotationTarget - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);

            Vector3 aimDirection = new Vector3(_rotationTarget.x, 0f, _rotationTarget.z);

            if (aimDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.15f);
            }
            Vector3 movement = new Vector3(_move.x, 0f, _move.y);
            AnimateMove(movement);
            transform.Translate(movement * _speed * Time.deltaTime, Space.World);
        }

        private void AnimateMove(Vector3 movement)
        {
            _isMoving = (movement.x > 0.1f || movement.x < -0.1f) || (movement.z > 0.1f || movement.z < -0.1f);
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
            _animator.SetBool("isWalking", _isMoving);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                _playerStats.TakeDamage(other.gameObject.GetComponent<Enemy>().GiveDamage());
            }
        }
        #endregion
    }
    
}
