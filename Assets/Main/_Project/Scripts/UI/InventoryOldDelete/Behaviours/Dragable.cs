using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.SkillManagement;
using _Project.Scripts.SkillManagement.SO.Skills;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace _Project.Scripts.UI.Inventory.Behaviours
{

    public class Dragable : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] protected Image _img;
        [SerializeField] protected Image bgImg;
        protected InventoryManager _inventoryManager;
        public Transform checkPoint;
        public Transform placementPoint;
        public bool CanPlace { get; set; }
        public bool IsPlacedInventory { get; set; }
        public Vector2Int size;
        //public Action onPlaceInventory;
        public RectTransform onSelectParentTransform;
        public Transform ItemHolder;

        public InventorySlot InventorySlot;
        //  public Transform Parent { get; set; }
        public RectTransform rect;

        private RectTransform rectTransform;



        private RectTransform _startParent;
        private Vector3 _startPos;
        protected SkillVisualData _skillVisualData;
        private Vector3 offset;

        protected SkillBase _skillBase;
        public SkillBase Skill => _skillBase;
        protected SkillRarity _skillRarity;
        public SkillRarity SkillRarity => _skillRarity;

        private static bool _isDragging = false;
        private bool _canDragSelf = false;
        private void Awake()
        {

            _startParent = transform.parent.GetComponent<RectTransform>();
            rectTransform = GetComponent<RectTransform>();

            // _img = GetComponent<Image>();

            SetAlpha(0);
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!IsValidDragTarget(eventData)) return;
            _isDragging = true;
            _canDragSelf = true;
            SetAlpha(1f);
            _startPos = rectTransform.position;
            transform.SetParent(onSelectParentTransform);
            transform.SetAsLastSibling();
            SetRaycastTarget(false);

            offset = rectTransform.position - new Vector3(eventData.pressPosition.x, eventData.pressPosition.y, 0);
            _inventoryManager.OnBeginDrag(this);
        }

        public void OnDrag(PointerEventData eventData)
        {

            if (!_canDragSelf) return;
            transform.position = Input.mousePosition + offset;
            _inventoryManager.OnDrag();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!_canDragSelf) return;
            _isDragging = false;
            _canDragSelf = false;
            _inventoryManager.OnEndDrag();
            HandlePlacement();
        }

        private bool IsValidDragTarget(PointerEventData eventData)
        {
            return !_isDragging;
            //return !eventData.pointerDrag.gameObject.TryGetComponent<Dragable>(out var dragable) || dragable == this;
        }

        private void HandlePlacement()
        {
            var offsetPlacementPoint = transform.position - placementPoint.position;
            if (InventorySlot != null)
            {
                PlaceInInventory(offsetPlacementPoint);
            }
            else
            {
                ReturnToStartPosition();
            }
            SetRaycastTarget(true);
        }

        private void PlaceInInventory(Vector3 offsetPlacementPoint)
        {
            rectTransform.position = InventorySlot.GetComponent<RectTransform>().position + offsetPlacementPoint;
            transform.SetParent(ItemHolder);

            if (IsPlacedInventory) MovedInInventory();
            else PlacedToInventory();

            IsPlacedInventory = true;
        }

        private void ReturnToStartPosition()
        {
            SetAlpha(0);
            rectTransform.position = _startPos;
            transform.SetParent(_startParent);
            NotPlaced();
        }

        private void SetRaycastTarget(bool state)
        {
            _img.raycastTarget = state;
        }
        public void SetAlpha(float alpha)
        {
            if (_img != null)
            {
                Color color = _img.color;
                color.a = alpha;
                _img.color = color;
            }
        }
        public virtual void PlacedToInventory()
        {

        }
        public virtual void MovedInInventory()
        {

        }
        public virtual void NotPlaced()
        {

        }

        internal virtual void Kill()
        {
            if (gameObject!=null  ) Destroy(gameObject);
        }
    }


    public enum DragableStatus
    {
        OnGrid,
        OnPlacedInventory,

    }

}