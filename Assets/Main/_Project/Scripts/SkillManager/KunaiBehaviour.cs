using System.Collections;
using System.Collections.Generic;
using _Project.Scripts;
using UnityEngine;

public class KunaiBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject hitParticle;
    [SerializeField] private float lifeTime=1f;
    // [SerializeField] private Animator model;
    private float _speed = 7f;
    private Coroutine moveCorotine;
    private float _damege = 1;
    public void Initialise(float damage)
    {
        _damege = damage;
    }
    private void OnEnable()
    {
        _spawnedTime = 0;
        StartCoroutine(Move());
    }
    float _spawnedTime = 0;
    IEnumerator Move()
    {
        while (_spawnedTime < lifeTime)
        {
            _spawnedTime += Time.deltaTime;
            transform.position += transform.forward * Time.deltaTime * _speed;
            yield return null;
        }
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.CompareTag("Enemy")) return;

        if (other.transform.TryGetComponent<IEnemy>(out IEnemy enemy))
        {
            enemy.GetDamage(_damege);
            var particle = ParticlePool.SharedInstance.GetPooledObject(hitParticle);
            particle.transform.position = transform.position;
            particle.SetActive(true);
            StopCoroutine(Move());
            gameObject.SetActive(false);
        }
    }
}
