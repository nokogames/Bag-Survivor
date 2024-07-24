using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace _Project.Scripts.UI.Inventory.Behaviours
{

    public class Dragable : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {

        //  public Transform Parent { get; set; }
        public Transform ParentAfterDrag { get; set; }
        private RectTransform rectTransform;
    
        private Image _img;
        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
       
            _img = GetComponent<Image>();

        }
        public void OnBeginDrag(PointerEventData eventData)
        {


            transform.SetAsLastSibling();
            _img.raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            // if (ParentAfterDrag == null)
            // {
            //   //  transform.parent = Parent;
            // }
            // else
            // {
            //   //  transform.parent = ParentAfterDrag;
            // }
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);

            // Set the pivot to the center
            // rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.position = ParentAfterDrag.position;
            _img.raycastTarget = true;
        }
    }

}