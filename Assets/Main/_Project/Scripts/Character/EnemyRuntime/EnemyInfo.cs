
using UnityEngine;


public interface IEnemy
{
    public Transform Transform { get; }
    public bool IsDead { get; set; }
}
