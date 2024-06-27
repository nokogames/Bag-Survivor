
using UnityEngine;

public abstract class BaseGunBehavior : MonoBehaviour
{
    [SerializeField] protected GunData gunData;
    

    [Header("Animations")]
    [SerializeField] AnimationClip characterShootAnimation;

    [Space]
    [SerializeField] Transform leftHandHolder;
    [SerializeField] Transform rightHandHolder;
    [Space]
    [SerializeField]
    protected Transform shootPoint;

    //ik controllers
    private Transform _leftHandRigController;
    private Transform _rightHandRigController;
    //Player
    protected ICharacter _character;

    public virtual void InitialiseCharacter(BaseCharacterGraphics characterGraphics, ICharacter character)
    {
        _leftHandRigController = characterGraphics.LeftHandRig.data.target;
        _rightHandRigController = characterGraphics.RightHandRig.data.target;
        _character = character;


    }
  
    public void UpdateHandRig()
    {
        _leftHandRigController.position = leftHandHolder.position;
        _rightHandRigController.position = rightHandHolder.position;
        _leftHandRigController.rotation = leftHandHolder.rotation;
        _rightHandRigController.rotation = rightHandHolder.rotation;

    }

    public void SetActivity(bool status)
    {
        gameObject.SetActive(status);
    }
    public virtual void GunFixedUpdate() { }
    public void Fire()
    {

    }

#if UNITY_EDITOR
    [ExecuteWithButton("PrepareWeapon")]
    public void PrepareWeapon()
    {

        if (leftHandHolder == null)
        {
            GameObject leftHandHolderObject = new GameObject("L H Holder");
            leftHandHolderObject.transform.SetParent(transform);
            leftHandHolderObject.transform.ResetLocal();
            leftHandHolderObject.transform.localPosition = new Vector3(-0.4f, 0, 0);

            GUIContent iconContent = UnityEditor.EditorGUIUtility.IconContent("sv_label_3");
            UnityEditor.EditorGUIUtility.SetIconForObject(leftHandHolderObject, (Texture2D)iconContent.image);

            leftHandHolder = leftHandHolderObject.transform;
        }

        if (rightHandHolder == null)
        {
            GameObject rightHandHolderObject = new GameObject("Right Hand Holder");
            rightHandHolderObject.transform.SetParent(transform);
            rightHandHolderObject.transform.ResetLocal();
            rightHandHolderObject.transform.localPosition = new Vector3(0.4f, 0, 0);

            GUIContent iconContent = UnityEditor.EditorGUIUtility.IconContent("sv_label_4");
            UnityEditor.EditorGUIUtility.SetIconForObject(rightHandHolderObject, (Texture2D)iconContent.image);

            rightHandHolder = rightHandHolderObject.transform;
        }

        if (shootPoint == null)
        {
            GameObject shootingPointObject = new GameObject("Shooting Point");
            shootingPointObject.transform.SetParent(transform);
            shootingPointObject.transform.ResetLocal();
            shootingPointObject.transform.localPosition = new Vector3(0, 0, 1);

            GUIContent iconContent = UnityEditor.EditorGUIUtility.IconContent("sv_label_1");
            UnityEditor.EditorGUIUtility.SetIconForObject(shootingPointObject, (Texture2D)iconContent.image);

            shootPoint = shootingPointObject.transform;
        }
    }
#endif
}
