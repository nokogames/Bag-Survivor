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

        if (!_character.IsEnemyFound)
        {
            transform.localRotation = Quaternion.Euler(Vector3.zero);
            return;
        }
        if (_crrTime < gunData.spawnTimeRate) return;


        RotateToTarget();
        _crrTime = 0;
        Shoot();


    }

    private void RotateToTarget()
    {
        Vector3 lookDirection = _character.Target.Transform.position - transform.position; // Hedefe doğru vektör
        lookDirection = transform.InverseTransformDirection(lookDirection); // Dünya koordinatlarından lokal koordinatlara çevir
      

        // Lokal koordinatlarda rotasyon oluşturulur
        Quaternion localRotation = Quaternion.LookRotation(lookDirection);

        // // Açıları Euler açılarına dönüştürür ve clamp işlemi uygular
        // float clampedX = Mathf.Clamp(localRotation.eulerAngles.x, -40, 40);

        // Yerel rotasyonu günceller, burada X ve Z eksenleri sabit tutulmuş, Y ekseni sınırlandırılmıştır
        transform.localRotation = Quaternion.Euler(localRotation.x, transform.localEulerAngles.y, transform.localEulerAngles.z);


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
