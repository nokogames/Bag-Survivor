using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBehaviour : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private GameObject model;
    [SerializeField] private float damageRange;
    [SerializeField] private float damage;
    private Collider _collider;
    private Rigidbody _rigidbody;
    private LayerMask _layerMask;
    private Transform _playerTransform;
    private void Awake()

    {
        _layerMask = LayerMask.GetMask("Ground");
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
    }
    public void Initialise(Transform playerTransform)
    {
        SetPhysicSatatus(true);
        model.SetActive(true);
        _playerTransform = playerTransform;
        transform.position = playerTransform.position + Vector3.up;
        // transform.position = playerTransform.position.GetRandomPositionAroundObjectIgnorRadius(5, 90) + Vector3.up * 10f;
        _rigidbody.velocity = Vector3.up * 5 + playerTransform.forward * 5;

    }
    public void Initialise()
    {

        transform.position = Vector3.zero.GetRandomPositionAroundObjectIgnorRadius(10, 90) + Vector3.up * 5f;


    }
    private void OnEnable()
    {
        SetPhysicSatatus(true);

    }

    private void SetPhysicSatatus(bool status)
    {
        _collider.enabled = status;
        _rigidbody.isKinematic = !status;
    }

    private void OnTriggerEnter(Collider other)
    {


        if ((_layerMask.value & (1 << other.gameObject.layer)) > 0)
        {
            Explosion();
        }
    }

    private void Explosion()
    {
        SetPhysicSatatus(false);
        explosionParticle.Play();
        model.SetActive(false);
        ApplyDamageToEnemys();
        StartCoroutine(DisableSelf());
        HapticManager.PlayHaptic(HapticType.SoftImpact);
    }

    private void ApplyDamageToEnemys()
    {
        var colliders = Physics.OverlapSphere(transform.position, damageRange);
        List<IEnemy> enemies = new List<IEnemy>();
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent<IEnemy>(out IEnemy enemy)) enemies.Add(enemy);
        }
        enemies.ForEach(enemy => enemy.GetDamage(damage));
    }

    IEnumerator DisableSelf()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
