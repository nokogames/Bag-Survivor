using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Reusable;
using Unity.VisualScripting;
using UnityEngine;


namespace _Project.Scripts.Character.EnemyRuntime
{

    public class Enemy : StateMachineMB, IEnemy
    {
        public Transform Transform => transform;

        private bool _isDead;
        public bool IsDead { get => _isDead; set => _isDead = value; }
        private float _healt = 100;

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
            {
                _isDead = true;
                gameObject.SetActive(false);
            }
        }
    }
}


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Enemy : MonoBehaviour
// {
//     public EnemyInfo enemyInfo;
//     private void Awake()
//     {
//         enemyInfo = new(transform);
//     }
//     public new bool TryGetComponent<T>(out T component)
//     {
//         if (typeof(T) == typeof(IEnemy))
//         {

//             Debug.Log("ddd");
//             component = (T)(object)enemyInfo;
//             return true;
//         }

//         return gameObject.TryGetComponent(out component);
//     }
// }


// public class EnemyInfo : IEnemy
// {
//     public Transform Transform { get; set; }

//     private bool _isDead;

//     public EnemyInfo(Transform transform)
//     {
//         Transform = transform;
//     }

//     public bool IsDead { get => _isDead; set => _isDead = value; }
// }