using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "InputData", menuName = "Datas/InputData", order = 1)]
    public class InputDataSO : ScriptableObject
    {
        public bool onDown = false;
        public float Horizontal = 0;
        public float Vertical = 0;
        public Vector3 MovementInput;
        public UpTouchDelegate UpTouch;

        public delegate void UpTouchDelegate();
        public bool IsInputValid()
        {
            return onDown && (Mathf.Abs(Horizontal + Vertical)) > .1f;
        }
        public bool IsValid => onDown && (Magnitude) > .1f;

        public float Magnitude => Mathf.Abs(Horizontal) + Mathf.Abs(Vertical);


    }
}