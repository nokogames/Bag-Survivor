using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Reusable;
using Unity.VisualScripting;
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
        private void OnEnable()
        {
            _isDead = false;
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
        }

        private void FixedUpdate()
        {
            if (_playerTransform == null) return;
            var distance = Vector3.Distance(transform.position, _playerTransform.position);
            Quaternion lookAt = Quaternion.LookRotation(_playerTransform.position - transform.position);
            if (distance < _attackDistance)
            {
                Attack();
                return;
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, lookAt, Time.fixedDeltaTime * 5);
            transform.Translate(Vector3.forward * Time.fixedDeltaTime);

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



