using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


[System.Serializable]
public class MovementSettings
{
    public float RotationSpeed;
    public float MoveSpeed;
    public float Acceleration;
    public float AnimationMultiplier = 1f;

    public MovementSettings Clone()
    {
        return new MovementSettings
        {
            RotationSpeed = this.RotationSpeed,
            MoveSpeed = this.MoveSpeed,
            Acceleration = this.Acceleration,
            AnimationMultiplier = this.AnimationMultiplier
        };
    }
}
