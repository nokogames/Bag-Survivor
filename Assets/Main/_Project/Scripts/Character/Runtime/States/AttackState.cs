

using _Project.Scripts.Character.Runtime.Controllers;
using _Project.Scripts.Reusable;
using VContainer;

namespace _Project.Scripts.Character.Runtime.States
{
    public class AttackState : IState
    {
        [Inject] private BaseGunBehavior _gunBehavior;
        [Inject] private PlayerMovementController _playerMovementController;
        [Inject] private CharacterGraphics _characterGraphics;
        [Inject] private BotController _botController;
        [Inject] private DetectionController _detectionController;
        [Inject] private UpgradeDataApplyer _upgradeDataApplyer;
        public void Initialize()
        {

        }
        public void Enter()
        {
            _characterGraphics.EnableIK();
            _gunBehavior.SetActivity(true);
            _botController.AttackBots();
        }

        public void Exit()
        {
        }

        public void FixedTick()
        {
            _gunBehavior.GunFixedUpdate();
            _upgradeDataApplyer.FixedTick();
        }

        public void Tick()
        {
            _gunBehavior.UpdateHandRig();
            _playerMovementController.Update();
        }
    }
}