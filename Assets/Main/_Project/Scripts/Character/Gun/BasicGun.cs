using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts;
using UnityEngine;

public class BasicGun : BaseGunBehavior
{
    [SerializeField] private ParticleSystem shootParticle;
    private float _crrTime;
    private void Start()
    {
        _crrTime = gunData.spawnTimeRate;

    }
    public override void GunFixedUpdate()
    {
        _crrTime += Time.fixedDeltaTime;
        if (!_character.IsEnemyFound || _crrTime < gunData.spawnTimeRate) return;
        _crrTime = 0;
        Shoot();


    }

    private void Shoot()
    {
        CreateBullet();
        PlayParticle();
    }

    private void PlayParticle()
    {
        if (shootParticle != null) shootParticle.Play();
    }

    private void CreateBullet()
    {
        GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject(gunData.bulletPoolIndex);
        bullet.transform.position = shootPoint.position;
        bullet.transform.forward = shootPoint.forward;
        bullet.SetActive(true);
        _character.OnGunShooted();
    }
}
