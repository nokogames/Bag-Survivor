using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{


    private List<IEnemy> _detectedEnemies = new List<IEnemy>();

    private int detectedEnemiesCount;
    private IEnemy closestEnemy;

    private IEnemyDetector _enemyDetector;
    public void Initialise(IEnemyDetector enemyDetector)
    {
        _enemyDetector = enemyDetector;
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

    private readonly string ENEMY_TAG = "Enemy";
}
