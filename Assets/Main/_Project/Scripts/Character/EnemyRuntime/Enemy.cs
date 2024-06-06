using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    public Transform Transform => transform;

    private bool _isDead;
    public bool IsDead { get => _isDead; set => _isDead = value; }
}
