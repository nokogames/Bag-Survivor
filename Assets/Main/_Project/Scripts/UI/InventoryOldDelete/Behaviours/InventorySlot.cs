using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts.UI.Inventory.Behaviours
{

    public class InventorySlot : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            GameObject dropped = eventData.pointerDrag;
            Dragable dragable = dropped.GetComponent<Dragable>();
            dragable.ParentAfterDrag = transform;
        }
    }
}
