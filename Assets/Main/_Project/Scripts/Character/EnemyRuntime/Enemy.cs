using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using static _Project.Scripts.Character.EnemyRuntime.EnemyManager;


namespace _Project.Scripts.Character.EnemyRuntime
{

    public class Enemy : MonoBehaviour, IEnemy
    {
        [SerializeField] private GameObject floatingTxt;
        [SerializeField] private int xpCount = 1;
        [SerializeField] private GameObject xpPrefab;
        [SerializeField] private GameObject deathParticle;
        [SerializeField] private EnemyType enemyType;
        [SerializeField] private float _damageAmount = 1;
        [SerializeField] private Animator animator;
        public EnemyType EnemyType => enemyType;
        private EnemyManager _enemyManger;
        private IDamagableByEnemy _damageableByEnemy;
        public Transform Transform => transform;

        private bool _isDead;
        public bool IsDead { get => _isDead; set => _isDead = value; }
        [SerializeField] private float baseHealt = 5;
        private float _healt = 5;
        [SerializeField] private float _attackDistance = 2;
        [SerializeField] private float speed = 5;
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
            if (animator != null) animator.SetBool("Dead", false);
            _rb.velocity = Vector3.zero;
            _isDead = false;
            _healt = baseHealt;
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
            transform.Translate(Vector3.back * 0.3f);
            if (IsDead) return;
            var obj = ParticlePool.SharedInstance.GetPooledObject(floatingTxt);
            var textMesh = obj.GetComponent<TextMeshPro>();
            textMesh.text = "-" + damage.ToString("F1");
            obj.transform.forward = Camera.main.transform.forward;
            obj.transform.position = transform.position;
            obj.SetActive(true);
            StaticHelper.Instance.FloatingTextAnim(obj.transform, textMesh, .8f, Vector3.up * 4f).Forget();

            _healt -= damage;
            if (_healt <= 0)
                Dead();
        }

        private void Dead()
        {
            _isDead = true;
            DropXP();
            if (animator != null)
            {
                animator.SetBool("Dead", true);
                StartCoroutine(WaitAndWork(1f, () =>
                {
                    SetFalse();
                }));
            }
            else
            {
                SetFalse();
            }

        }

        private void DropXP()
        {
            if (xpPrefab == null) return;

            for (int i = 0; i < xpCount; i++)
            {

                var obj = ParticlePool.SharedInstance.GetPooledObject(xpPrefab);
                var startPos = transform.position.SetY(1f);
                var destPos = startPos.GetRandomPositionAroundObject(1f);
                obj.transform.position = startPos;
                //  obj.transform.position = startPos.GetRandomPositionAroundObject(1f);
                obj.SetActive(true);
                JumpToPos(obj.transform, destPos).Forget();
                //await UniTask.Delay(TimeSpan.FromSeconds(.2f));
            }

        }
        private float duration = .3f;
        private float height = .6f;
        private async UniTaskVoid JumpToPos(Transform obj, Vector3 destPos)
        {
            // Height of the jump

            Vector3 startPos = obj.position;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / duration;


                Vector3 currentPos = Vector3.Lerp(startPos, destPos, t);


                currentPos.y += height * Mathf.Sin(Mathf.PI * t);

                obj.position = currentPos;

                await UniTask.Yield();
            }

            obj.position = destPos;
        }

        private void SetFalse()
        {
            if (deathParticle != null)
            {
                var go = ParticlePool.SharedInstance.GetPooledObject(deathParticle);
                go.transform.position = transform.position;
                go.SetActive(true);


            }
            transform.DOScale(Vector3.one * .1f, .5f).OnComplete(() =>
            {
                gameObject.SetActive(false);
                gameObject.transform.localScale = Vector3.one;
                _enemyManger.EnmeyDead(this);


            });
        }

        public void CompletedSection()
        {
            if (animator != null) animator.SetBool("Dead", true);
            _isDead = true;
            gameObject.SetActive(false);
            // var obj = ObjectPooler.SharedInstance.GetPooledObject(3);

            // obj.transform.position = transform.position.SetY(.1f);
            // obj.SetActive(true);
        }
        private void FixedUpdate()
        {
            // if (StaticHelper.Instance.gameStatus == GameStatus.Pause) return;
            if (_playerTransform == null || IsDead) return;

            var distance = Vector3.Distance(transform.position, _playerTransform.position);
            var destination = new Vector3(_playerTransform.position.x, transform.position.y, _playerTransform.position.z);
            Quaternion lookAt = Quaternion.LookRotation(destination - transform.position);

            if (distance < _attackDistance)
            {
                Attack();

                return;
            }
            if (animator != null) animator.SetBool("Move", true);
            _rb.velocity = Vector3.zero;
            transform.rotation = Quaternion.Slerp(transform.rotation, lookAt, Time.fixedDeltaTime * speed);
            transform.Translate(Vector3.forward * Time.fixedDeltaTime);
            //_rb.velocity = transform.forward * 8;
            // var dest = transform.forward * Time.fixedDeltaTime;
            // if (!_characterController.isGrounded) dest += Vector3.down;

            // _characterController.Move(dest);

        }
        private float _crrTime = 0;
        private float _attackTimeRate = .5f;
        private void Attack()
        {
            if (animator != null) animator.SetBool("Move", false);
            if (animator != null) animator.SetTrigger("Attack");
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

        IEnumerator WaitAndWork(float time, Action action)
        {
            yield return new WaitForSeconds(time);
            action?.Invoke();
        }


    }
}



