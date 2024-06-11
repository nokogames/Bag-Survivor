using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public abstract class BaseCharacterGraphics : MonoBehaviour
{

    private readonly int ANIMATION_SHOT_HASH = Animator.StringToHash("Shot");
    private readonly int ANIMATION_HIT_HASH = Animator.StringToHash("Hit");

    private readonly int JUMP_ANIMATION_HASH = Animator.StringToHash("Jump");
    private readonly int GRUNT_ANIMATION_HASH = Animator.StringToHash("Grunt");



    [SerializeField]
    protected Animator characterAnimator;
    [SerializeField] SkinnedMeshRenderer meshRenderer;
    public SkinnedMeshRenderer MeshRenderer => meshRenderer;

    [Header("Movement")]
    [SerializeField] MovementSettings movementSettings;
    public MovementSettings MovementSettings => movementSettings;

    [SerializeField] MovementSettings movementAimingSettings;
    public MovementSettings MovementAimingSettings => movementAimingSettings;

    [Header("Hands Rig")]
    [SerializeField] TwoBoneIKConstraint leftHandRig;
    public TwoBoneIKConstraint LeftHandRig => leftHandRig;

    [SerializeField] Vector3 leftHandExtraRotation;
    public Vector3 LeftHandExtraRotation => leftHandExtraRotation;

    [SerializeField] TwoBoneIKConstraint rightHandRig;
    public TwoBoneIKConstraint RightHandRig => rightHandRig;

    [SerializeField] Vector3 rightHandExtraRotation;
    public Vector3 RightHandExtraRotation => rightHandExtraRotation;

    [Header("Weapon")]
    [SerializeField] Transform weaponsTransform;

    [SerializeField] Transform shootGunHolderTransform;
    public Transform ShootGunHolderTransform => shootGunHolderTransform;


    [Space]
    [SerializeField] protected Rig mainRig;
    [SerializeField] Transform leftHandController;
    [SerializeField] Transform rightHandController;

    protected ICharacter character;
    private AnimatorOverrideController animatorOverrideController;
    private int animatorShootingLayerIndex;


    public virtual void Initialise(ICharacter character)
    {
        this.character = character;
        animatorOverrideController = new AnimatorOverrideController(characterAnimator.runtimeAnimatorController);
        characterAnimator.runtimeAnimatorController = animatorOverrideController;
        animatorShootingLayerIndex = characterAnimator.GetLayerIndex("Shooting");
    }

    // When Starting to move
    public abstract void OnMovingStarted();
    //Strop moving
    public abstract void OnMovingStoped();
    //public abstract void OnMoving(float speedPercent, Vector3 direction, bool isTargetFound);
    public abstract void OnMoving(float speedPercent, Vector3 direction, bool isTargetFound = false);

    public virtual void DisableIK()
    {
        rightHandRig.weight = 0;
        leftHandRig.weight = 0;
    }
    public virtual void EnableIK()
    {
        rightHandRig.weight = 1;
        leftHandRig.weight = 1;
    }

#if UNITY_EDITOR
    [ExecuteWithButton("Execute Function")]
    public void PrepareModel()
    {
        Debug.Log($"PrepareModel");
        // Get animator component
        Animator tempAnimator = characterAnimator;

        if (tempAnimator != null)
        {
            if (tempAnimator.avatar != null && tempAnimator.avatar.isHuman)
            {
                // Initialise rig
                RigBuilder rigBuilder = tempAnimator.GetComponent<RigBuilder>();
                if (rigBuilder == null)
                {
                    rigBuilder = tempAnimator.gameObject.AddComponent<RigBuilder>();

                    GameObject rigObject = new GameObject("Main Rig");
                    rigObject.transform.SetParent(tempAnimator.transform);
                    rigObject.transform.ResetLocal();

                    Rig rig = rigObject.AddComponent<Rig>();

                    mainRig = rig;

                    rigBuilder.layers.Add(new RigLayer(rig, true));

                    // Left hand rig
                    GameObject leftHandRigObject = new GameObject("Left Hand Rig");
                    leftHandRigObject.transform.SetParent(rigObject.transform);
                    leftHandRigObject.transform.ResetLocal();

                    GameObject leftHandControllerObject = new GameObject("Controller");
                    leftHandControllerObject.transform.SetParent(leftHandRigObject.transform);
                    leftHandControllerObject.transform.ResetLocal();

                    leftHandController = leftHandControllerObject.transform;

                    Transform leftHandBone = tempAnimator.GetBoneTransform(HumanBodyBones.LeftHand);
                    leftHandControllerObject.transform.position = leftHandBone.position;
                    leftHandControllerObject.transform.rotation = leftHandBone.rotation;

                    TwoBoneIKConstraint leftHandRig = leftHandRigObject.AddComponent<TwoBoneIKConstraint>();
                    leftHandRig.data.target = leftHandControllerObject.transform;
                    leftHandRig.data.root = tempAnimator.GetBoneTransform(HumanBodyBones.LeftUpperArm);
                    leftHandRig.data.mid = tempAnimator.GetBoneTransform(HumanBodyBones.LeftLowerArm);
                    leftHandRig.data.tip = leftHandBone;

                    // Right hand rig
                    GameObject rightHandRigObject = new GameObject("Right Hand Rig");
                    rightHandRigObject.transform.SetParent(rigObject.transform);
                    rightHandRigObject.transform.ResetLocal();

                    GameObject rightHandControllerObject = new GameObject("Controller");
                    rightHandControllerObject.transform.SetParent(rightHandRigObject.transform);
                    rightHandControllerObject.transform.ResetLocal();

                    rightHandController = rightHandControllerObject.transform;

                    Transform rightHandBone = tempAnimator.GetBoneTransform(HumanBodyBones.RightHand);
                    rightHandControllerObject.transform.position = rightHandBone.position;
                    rightHandControllerObject.transform.rotation = rightHandBone.rotation;

                    TwoBoneIKConstraint rightHandRig = rightHandRigObject.AddComponent<TwoBoneIKConstraint>();
                    rightHandRig.data.target = rightHandControllerObject.transform;
                    rightHandRig.data.root = tempAnimator.GetBoneTransform(HumanBodyBones.RightUpperArm);
                    rightHandRig.data.mid = tempAnimator.GetBoneTransform(HumanBodyBones.RightLowerArm);
                    rightHandRig.data.tip = rightHandBone;

                    this.leftHandRig = leftHandRig;
                    this.rightHandRig = rightHandRig;
                }

                // Prepare ragdoll
                //   RagdollHelper.CreateRagdoll(tempAnimator, 60, 1, LayerMask.NameToLayer("Ragdoll"));

                movementSettings.RotationSpeed = 8;
                movementSettings.MoveSpeed = 32;
                movementSettings.Acceleration = 5000;
                movementSettings.AnimationMultiplier = 1f;

                movementAimingSettings.RotationSpeed = 8;
                movementAimingSettings.MoveSpeed = 28;
                movementAimingSettings.Acceleration = 5000;
                movementAimingSettings.AnimationMultiplier = 1f;

                // CharacterAnimationHandler tempAnimationHandler = tempAnimator.GetComponent<CharacterAnimationHandler>();
                // if (tempAnimationHandler == null)
                //     tempAnimator.gameObject.AddComponent<CharacterAnimationHandler>();

                // Create weapon holders
                GameObject weaponHolderObject = new GameObject("Weapons");
                weaponHolderObject.transform.SetParent(tempAnimator.transform);
                weaponHolderObject.transform.ResetLocal();

                weaponsTransform = weaponHolderObject.transform;

                // // Minigun
                // GameObject miniGunHolderObject = new GameObject("Minigun Holder");
                // miniGunHolderObject.transform.SetParent(weaponsTransform);
                // miniGunHolderObject.transform.ResetLocal();
                // miniGunHolderObject.transform.localPosition = new Vector3(1.36f, 4.67f, 2.5f);

                // minigunHolderTransform = miniGunHolderObject.transform;

                // Shotgun
                GameObject shotgunHolderObject = new GameObject("Shotgun Holder");
                shotgunHolderObject.transform.SetParent(weaponsTransform);
                shotgunHolderObject.transform.ResetLocal();
                shotgunHolderObject.transform.localPosition = new Vector3(1.12f, 4.49f, 1.09f);

                shootGunHolderTransform = shotgunHolderObject.transform;

                // // Rocket
                // GameObject rocketHolderObject = new GameObject("Rocket Holder");
                // rocketHolderObject.transform.SetParent(weaponsTransform);
                // rocketHolderObject.transform.ResetLocal();
                // rocketHolderObject.transform.localPosition = new Vector3(1.09f, 4.23f, 1.8f);
                // rocketHolderObject.transform.localRotation = Quaternion.Euler(-15, 0, 0);

                // rocketHolderTransform = rocketHolderObject.transform;

                // // Tesla
                // GameObject teslaHolderObject = new GameObject("Tesla Holder");
                // teslaHolderObject.transform.SetParent(weaponsTransform);
                // teslaHolderObject.transform.ResetLocal();
                // teslaHolderObject.transform.localPosition = new Vector3(1.42f, 5.22f, 2.38f);

                // teslaHolderTransform = teslaHolderObject.transform;

                // // Initialise mesh renderer
                meshRenderer = tempAnimator.transform.GetComponentInChildren<SkinnedMeshRenderer>();

#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(this);
#endif
            }
            else
            {
                Debug.LogError("Avatar is missing or type isn't humanoid!");
            }
        }
        else
        {
            Debug.LogWarning("Animator component can't be found!");
        }

    }
#endif

}
