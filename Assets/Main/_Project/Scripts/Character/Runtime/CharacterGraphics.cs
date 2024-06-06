using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGraphics : BaseCharacterGraphics
{
    private static readonly int ANIMATOR_MOVEMENT_SPEED = Animator.StringToHash("Speed");

    private static readonly int ANIMATOR_RUNNING_HASH = Animator.StringToHash("IsRunning");
    private static readonly int ANIMATOR_MOVEMENT_X_HASH = Animator.StringToHash("MovementX");
    private static readonly int ANIMATOR_MOVEMENT_Y_HASH = Animator.StringToHash("MovementY");

    public override void OnMovingStarted()
    {
        characterAnimator.SetBool(ANIMATOR_RUNNING_HASH, true);
    }

    public override void OnMovingStoped()
    {
        characterAnimator.SetBool(ANIMATOR_RUNNING_HASH, false);
    }
    public override void OnMoving(float speedPercent, Vector3 direction, bool isTargetFound = false)
    {
        if (isTargetFound)
        {

            var enemyPosition = character.TargetEnemy.Transform.position;

            var angle = Mathf.Atan2(enemyPosition.x - transform.position.x, enemyPosition.z - transform.position.z) * 180 / Mathf.PI;

            var rotatedInput = Quaternion.Euler(0, 0, angle) * new Vector2(direction.x, direction.z);

            characterAnimator.SetFloat(ANIMATOR_MOVEMENT_X_HASH, rotatedInput.x);
            characterAnimator.SetFloat(ANIMATOR_MOVEMENT_Y_HASH, rotatedInput.y);
            return;
        }
        characterAnimator.SetFloat(ANIMATOR_MOVEMENT_SPEED, speedPercent);
        characterAnimator.SetFloat(ANIMATOR_MOVEMENT_X_HASH, 0);
        characterAnimator.SetFloat(ANIMATOR_MOVEMENT_Y_HASH, 1);

    }
}
