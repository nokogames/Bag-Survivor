using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Character.Craft;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Character.Bot.Gun
{
    public class BotGunBase : MonoBehaviour
    {
        [SerializeField] private ParticleSystem shootParticle;
        [SerializeField] private Transform shootPoint;
        private float _rotationSpeed = 100f;
        private float _shootTimeRate = .5f;
        private int _bulletPoolIndex = 1;
        private float _damege = .5f;
        private ITargetable _target;
        [Inject] private BotAnimationController _botAnimController;
        [Inject] private ICharacter _character;

        internal void FixedTick()
        {
            _target = _character.Target;
            if (_character.Target == null) return;

            RotateToTarget();
            Shooting();

        }

        float _crrTime = 0;
        private void Shooting()
        {
            _crrTime += Time.fixedDeltaTime;
            if (_crrTime < _shootTimeRate) return;
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
            GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject(_bulletPoolIndex);
            bullet.transform.position = shootPoint.position;
            bullet.transform.forward = shootPoint.forward;
            bullet.SetActive(true);
            bullet.GetComponent<BulletBehaviour>().Initialise(_damege);
            //   _character.OnGunShooted();
        }

        internal void RotateToTarget()
        {
            shootPoint.LookAt(_character.Target.Transform.position);


        }
        public void SetFireRate(float fireRate)
        {
            _shootTimeRate = fireRate;
        }
        public void SetDamage(float damage){
            _damege=damage;
        }
    }


}
