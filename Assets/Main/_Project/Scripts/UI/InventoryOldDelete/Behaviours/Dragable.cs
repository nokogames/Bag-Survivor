using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.SkillManagement;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace _Project.Scripts.UI.Inventory.Behaviours
{

    public class Dragable : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private Image _img;
        protected InventoryManager _inventoryManager;
        public Transform checkPoint;
        public Transform placementPoint;
        public bool CanPlace { get; set; }
        public Vector2Int size;
        public Action onPlaceInventory;
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
        private void Awake()
        {

            _startParent = transform.parent.GetComponent<RectTransform>();
            rectTransform = GetComponent<RectTransform>();

            _img = GetComponent<Image>();

            SetAlpha(0);
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            SetAlpha(1f);
            _startPos = rectTransform.position;
            transform.SetParent(onSelectParentTransform);
            transform.SetAsLastSibling();
            _img.raycastTarget = false;

            //   RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out offset);
            offset = rectTransform.position - new Vector3(eventData.pressPosition.x, eventData.pressPosition.y, 0);

            _inventoryManager.OnBeginDrag(this);
        }

        public void OnDrag(PointerEventData eventData)
        {

            // RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, null, out Vector2 localPoint);
            // rectTransform.anchoredPosition = localPoint;

            transform.position = Input.mousePosition + offset;
            _inventoryManager.OnDrag();
        }

        public void OnEndDrag(PointerEventData eventData)
        {    // 1-Select from skill bar

            // RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, null, out Vector2 localPoint);
            _inventoryManager.OnEndDrag();

            var offsetPlacementPoint = transform.position - placementPoint.position;
            if (InventorySlot != null && CanPlace)
            {
                rectTransform.position = InventorySlot.GetComponent<RectTransform>().position + offsetPlacementPoint;
                transform.SetParent(ItemHolder);
                onPlaceInventory?.Invoke();
            }
            else if (InventorySlot != null)
            {
                rectTransform.position = InventorySlot.GetComponent<RectTransform>().position + offsetPlacementPoint;
                transform.SetParent(ItemHolder);
            }
            else
            {
                SetAlpha(0);
                rectTransform.position = _startPos;
                transform.SetParent(_startParent);
            }

            _img.raycastTarget = true;
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
        public void PlacedToInventory()
        {

        }

    }


    public enum DragableStatus
    {
        OnGrid,
        OnPlacedInventory,

    }

}