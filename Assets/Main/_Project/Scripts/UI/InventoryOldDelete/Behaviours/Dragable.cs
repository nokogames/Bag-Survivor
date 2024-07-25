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
        public bool CanPlace { get; set; }
        public Vector2Int size;
        public Action onPlaceInventory;
        public RectTransform onSelectParentTransform;
        public Transform ItemHolder;

        public InventorySlot startPlaceInventorySlot;
        //  public Transform Parent { get; set; }
        public RectTransform rect;

        private RectTransform rectTransform;

        private Image _img;


        private RectTransform _startParent;
        private Vector3 _startPos;
        protected SkillVisualData _skillVisualData;
        private void Awake()
        {
           
            _startParent = transform.parent.GetComponent<RectTransform>();
            rectTransform = GetComponent<RectTransform>();

            _img = GetComponent<Image>();

        }
        public void OnBeginDrag(PointerEventData eventData)
        {

            _startPos = rectTransform.position;
            transform.SetParent(onSelectParentTransform);
            transform.SetAsLastSibling();
            _img.raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData)
        {

            // RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, null, out Vector2 localPoint);
            // rectTransform.anchoredPosition = localPoint;
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            // RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, null, out Vector2 localPoint);
            if (startPlaceInventorySlot != null && CanPlace)
            {

                rectTransform.position = startPlaceInventorySlot.GetComponent<RectTransform>().position;
                transform.SetParent(ItemHolder);
                onPlaceInventory?.Invoke();
            }

            else
            {
                rectTransform.position = _startPos;
                transform.SetParent(_startParent);
            }

            _img.raycastTarget = true;
        }

        public void PlacedToInventory()
        {

        }

    }


    public enum SizeDir
    {
        Up,
        Down,
        Left,
        Right,
    }

}