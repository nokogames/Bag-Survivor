using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.UI.Inventory
{
    public class InventoryGridFactory
    {
        [Inject] private CustomInventoryData _data;
       
        public void CreateGrid(int x, int y, InventoryManager inventoryManager)
        {
          
            _data.slots.Clear();
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    var inventorySlot = GameObject.Instantiate(_data.slotPref, _data.grid.transform).GetComponent<InventorySlot>();
                    inventorySlot.Setup(false, new Vector2Int(i, j), inventoryManager);
                    _data.slots.Add(inventorySlot);
                }
            }
        }
    }
}