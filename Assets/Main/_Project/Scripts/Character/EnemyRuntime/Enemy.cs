using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using static _Project.Scripts.Character.EnemyRuntime.EnemyManager;


namespace _Project.Scripts.Character.EnemyRuntime
{

    public class Enemy : MonoBehaviour, IEnemy
    {
        [SerializeField] private EnemyType enemyType;
        [SerializeField] private float _damageAmount = 1;
        public EnemyType EnemyType => enemyType;
        private EnemyManager _enemyManger;
        private IDamagableByEnemy _damageableByEnemy;
        public Transform Transform => transform;

        private bool _isDead;
        public bool IsDead { get => _isDead; set => _isDead = value; }
        private float _healt = 5;
        private float _attackDistance = 1;
        private Transform _playerTransform;
        //private CharacterController _characterController;
        private Rigidbody _rb;
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            // _characterController = GetComponent<CharacterController>();
        }
        private void OnEnable()
        {
            _rb.velocity = Vector3.zero;
            _isDead = false;
            _healt = 5;
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

            obj.transform.position = transform.position.SetY(.1f);
            obj.SetActive(true);
            _enemyManger.EnmeyDead(this);
        }
        public void CompletedSection()
        {
            
            _isDead = true;
            gameObject.SetActive(false);
            var obj = ObjectPooler.SharedInstance.GetPooledObject(3);

            obj.transform.position = transform.position.SetY(.1f);
            obj.SetActive(true);
        }
        private void FixedUpdate()
        {
            if (_playerTransform == null || IsDead) return;

            var distance = Vector3.Distance(transform.position, _playerTransform.position);
            var destination = new Vector3(_playerTransform.position.x, transform.position.y, _playerTransform.position.z);
            Quaternion lookAt = Quaternion.LookRotation(destination - transform.position);
            if (distance < _attackDistance)
            {
                Attack();

                return;
            }
            _rb.velocity = Vector3.zero;
            transform.rotation = Quaternion.Slerp(transform.rotation, lookAt, Time.fixedDeltaTime * 5);
            transform.Translate(Vector3.forward * Time.fixedDeltaTime);
            //_rb.velocity = transform.forward * 8;
            // var dest = transform.forward * Time.fixedDeltaTime;
            // if (!_characterController.isGrounded) dest += Vector3.down;

            // _characterController.Move(dest);

        }
        private float _crrTime = 0;
        private float _attackTimeRate = .2f;
        private void Attack()
        {
            _crrTime += Time.fixedDeltaTime;
            if (_crrTime < _attackTimeRate) return;
            _crrTime = 0;

            _damageableByEnemy.GetDamage(_damageAmount);
            MoveForwardAndBack();
        }
        void MoveForwardAndBack()
        {
            // Objeyi ileri hareket ettir
            // transform.DOMoveZ(transform.position.z + .5f, .2f).SetLoops(2, LoopType.Yoyo);
        }
        internal void Initialize(Transform playerTransform, EnemyManager enemyManager, IDamagableByEnemy damagableByEnemy)
        {
            _playerTransform = playerTransform;
            _enemyManger = enemyManager;
            _damageableByEnemy = damagableByEnemy;
        }
    }
}



