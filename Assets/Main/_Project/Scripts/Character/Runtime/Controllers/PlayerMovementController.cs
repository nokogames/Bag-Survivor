using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Character.Craft;
using ScriptableObjects;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Character.Runtime.Controllers
{
    public class PlayerMovementController
    {
        [Inject]
        private InputDataSO _inputData;
        [Inject]
        private CharacterGraphics _characterGraphics;
        [Inject] private Transform _playerTransform;
        [Inject] private CharacterController _characterController;
        public bool IsCloseEnemyFound
        {
            get => _isCloseEnemyFound; set
            {
                _isCloseEnemyFound = value;
                SetMovementSettings();
            }
        }



        public MovementSettings ActiveMovementSettings { set => _activeMovementSettings = value; }
        public ITargetable Target { get => _target; set => _target = value; }

        public bool IsMoving { get => _isMoving; set => _isMoving = value; }

        private ITargetable _target;
        private MovementSettings _activeMovementSettings;
        private bool _isCloseEnemyFound = false;
        private bool _isMoving = false;
        private float _speed = 0;
        private Vector3 _movementVelocity;

        private bool _isDeath = false;
        public bool PlayerIsDeath
        {
            get => _isDeath; set
            {
                _isDeath = value;
                if (value) MovingStoped();
            }
        }
        private MovementSettings _crrMovementSettings = new();
        private MovementSettings _crrAimingMovementSettings = new();

        public void Initialise()
        {
            _crrMovementSettings = _characterGraphics.MovementSettings.Clone();
            _crrAimingMovementSettings = _characterGraphics.MovementAimingSettings.Clone();

            // _activeMovementSettings = _characterGraphics.MovementSettings;
            _activeMovementSettings = _crrMovementSettings;
            CustomExtentions.ColorLog($"Speed  iNITIALISE{_crrAimingMovementSettings.MoveSpeed} cc{_crrMovementSettings.MoveSpeed}", Color.red);
        }
        public void SetSpeed(float speed)
        {
            _crrMovementSettings.MoveSpeed = _characterGraphics.MovementSettings.MoveSpeed + speed;
            _crrAimingMovementSettings.MoveSpeed = _characterGraphics.MovementAimingSettings.MoveSpeed + speed - 0.5f;
            CustomExtentions.ColorLog($"Speed {_crrAimingMovementSettings.MoveSpeed} cc{_crrMovementSettings.MoveSpeed}", Color.red);
        }
        private void Move()
        {
            CustomExtentions.ColorLog($"Speed {_crrAimingMovementSettings.MoveSpeed} cc{_crrMovementSettings.MoveSpeed}", Color.yellow);

            if (_isDeath) return;

            Rotate();
            if (!_inputData.IsValid && _isMoving) MovingStoped();

            if (!_inputData.IsValid && _target == null) return;

            if (!_isMoving) MovingStarted();

            float maxAlowedSpeed = Mathf.Clamp01(_inputData.Magnitude) * _activeMovementSettings.MoveSpeed;
            _speed += _activeMovementSettings.Acceleration * Time.deltaTime;
            if (_speed > maxAlowedSpeed) _speed = maxAlowedSpeed;

            _movementVelocity = _playerTransform.forward * _speed;

            //  _playerTransform.position += _inputData.MovementInput * Time.deltaTime * _speed;
            var movement = _inputData.MovementInput * Time.deltaTime * _speed;
            if (!_characterController.isGrounded) movement += Vector3.down * Time.deltaTime * 5;


            _characterController.Move(movement);
            _characterGraphics.OnMoving(Mathf.InverseLerp(0, _activeMovementSettings.MoveSpeed, _speed),
                 _inputData.MovementInput, _isCloseEnemyFound);
            //If not found enemy 
            //  Rotate();

        }
        public void Update()
        {
            Move();

        }

        private void MovingStoped()
        {
            _isMoving = false;
            _characterGraphics.OnMovingStoped();
        }

        private void Rotate()
        {

            if (!_isCloseEnemyFound)
            {
                if (_inputData.MovementInput == Vector3.zero) return;
                _playerTransform.rotation =
             Quaternion.Lerp(_playerTransform.rotation, Quaternion.LookRotation(_inputData.MovementInput), Time.deltaTime * _activeMovementSettings.RotationSpeed);
            }
            else
            {
                Vector3 newTargetPoint = _target.Transform.position;
                newTargetPoint.y = _playerTransform.position.y;
                Quaternion targetRotation = Quaternion.LookRotation(newTargetPoint - _playerTransform.position);
                _playerTransform.rotation = Quaternion.Lerp(_playerTransform.rotation, targetRotation, Time.deltaTime * _activeMovementSettings.RotationSpeed);
                //   _playerTransform.LookAt(new Vector3(_targetEnemy.Transform.position.x, _playerTransform.position.y, _targetEnemy.Transform.position.z));
            }
        }

        private void MovingStarted()
        {
            _speed = 0;
            _isMoving = true;
            _characterGraphics.OnMovingStarted();
        }

        private void SetMovementSettings()
        {
            _activeMovementSettings = IsCloseEnemyFound ? _crrAimingMovementSettings : _crrMovementSettings;

        }
    }
}