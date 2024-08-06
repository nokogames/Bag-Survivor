using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [SerializeField] private GameObject circle;
    public GameObject enemy;

    private List<IEnemy> _detectedEnemies = new List<IEnemy>();

    private int detectedEnemiesCount;
    private IEnemy closestEnemy;

    private IEnemyDetector _enemyDetector;
    public void Initialise(IEnemyDetector enemyDetector)
    {
        _enemyDetector = enemyDetector;
        // circle.transform.parent = null;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag(ENEMY_TAG))
        {
            if (other.TryGetComponent(out IEnemy enemy))
            {
                if (_detectedEnemies.Contains(enemy)) return;
                detectedEnemiesCount++;
                _detectedEnemies.Add(enemy);
                UpdateClosestEnemy();
            }
        }
    }

    private float _timeRate = .1f;
    private float _crrTime = 0;

    // private void Update()
    // {
    //     circle.transform.position = transform.position;

    // }
    private void FixedUpdate()
    {

        _crrTime += Time.fixedDeltaTime;
        if (_crrTime < _timeRate) return;
        UpdateClosestEnemy();
        _crrTime = 0;

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag(ENEMY_TAG))
        {
            if (other.TryGetComponent(out IEnemy enemy) && _detectedEnemies.Contains(enemy))
            {
                detectedEnemiesCount--;
                _detectedEnemies.Remove(enemy);
                UpdateClosestEnemy();
            }

        }
    }
    private void UpdateClosestEnemy()
    {
        if (detectedEnemiesCount == 0)
        {
            if (closestEnemy != null)
                _enemyDetector.OnEnemyDetected(null);

            closestEnemy = null;

            return;
        }

        float minDistanceSqr = float.MaxValue;
        IEnemy tempEnemy = null;
        for (int i = 0; i < _detectedEnemies.Count; i++)
        {
            var enemy = _detectedEnemies[i];
            if (enemy.IsDead)
            {
                _detectedEnemies.Remove(enemy);
                detectedEnemiesCount--;
                i--;
            }

        }
        for (int i = 0; i < _detectedEnemies.Count; i++)
        {
            var enemy = _detectedEnemies[i];

            float distanceSqr = (transform.position - enemy.Transform.position).sqrMagnitude;

            if (distanceSqr < minDistanceSqr && !enemy.IsDead)
            {
                tempEnemy = enemy;
                minDistanceSqr = distanceSqr;
            }
        }

        if (closestEnemy != tempEnemy)
        {

            _enemyDetector.OnEnemyDetected(tempEnemy);
        }

        closestEnemy = tempEnemy;
    }
    public void SetRadius(float radius)
    {
        transform.localScale = new Vector3(radius, radius, radius);
       // circle.transform.localScale = transform.localScale;
    }
    public void ClearDeatected()
    {
        _detectedEnemies.Clear();
    }
    private readonly string ENEMY_TAG = "Enemy";
}
