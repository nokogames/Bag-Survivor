using System;
using System.Collections;
using System.Collections.Generic;


using UnityEngine;


namespace _Project.Scripts.Character.EnemyRuntime
{

    public class Enemy : MonoBehaviour, IEnemy
    {
        [SerializeField] private float _damageAmount = 1;

        private EnemyManager _enemyManger;
        private IDamagableByEnemy _damageableByEnemy;
        public Transform Transform => transform;

        private bool _isDead;
        public bool IsDead { get => _isDead; set => _isDead = value; }
        private float _healt = 5;
        private float _attackDistance = 1;
        private Transform _playerTransform;
        private CharacterController _characterController;
        private void Awake()
        {
            // _characterController = GetComponent<CharacterController>();
        }
        private void OnEnable()
        {
            _isDead = false;
            _healt=5;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<ICharacter>(out ICharacter character))
            {
                Debug.Log("Icharacter triggered");
            }
        }

        public void GetDamage(float damage)
        {
            if (IsDead) return;
            _healt -= damage;
            if (_healt <= 0)
                Dead();
        }

        private void Dead()
        {
            _isDead = true;
            gameObject.SetActive(false);
            var obj = ObjectPooler.SharedInstance.GetPooledObject(3);

            obj.transform.position = transform.position.SetY(1f);
            obj.SetActive(true);
           _enemyManger.EnmeyDead(this);
        }

        private void FixedUpdate()
        {
            if (_playerTransform == null || IsDead) return;

            var distance = Vector3.Distance(transform.position, _playerTransform.position);
            Quaternion lookAt = Quaternion.LookRotation(_playerTransform.position - transform.position);
            if (distance < _attackDistance)
            {
                Attack();
                return;
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, lookAt, Time.fixedDeltaTime * 5);
            transform.Translate(Vector3.forward * Time.fixedDeltaTime);
            // var dest = transform.forward * Time.fixedDeltaTime;
            // if (!_characterController.isGrounded) dest += Vector3.down;

            // _characterController.Move(dest);

        }

        private void Attack()
        {
            _damageableByEnemy.GetDamage(_damageAmount);
        }

        internal void Initialize(Transform playerTransform, EnemyManager enemyManager, IDamagableByEnemy damagableByEnemy)
        {
            _playerTransform = playerTransform;
            _enemyManger = enemyManager;
            _damageableByEnemy = damagableByEnemy;
        }
    }
}



