using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.UI.Inventory
{

    [Serializable]
    public class InventoryData
    {
        public Assets Assets;
        public InventoryDragDropData InventoryDragDropData;
        public InventoryTestingData testingData;
    }
    [Serializable]
    public class InventoryTestingData
    {
        public Transform outerInventoryTetrisBackground;
        public InventoryTetris inventoryTetris;
        public InventoryTetris outerInventoryTetris;
        public List<string> addItemTetrisSaveList;
    }
    [Serializable]
    public class InventoryDragDropData
    {
        public List<InventoryTetris> inventoryTetrisList;

    }

    [Serializable]
    public class Assets
    {
        public ItemTetrisSO[] itemTetrisSOArray;

        public ItemTetrisSO ammo;
        public ItemTetrisSO grenade;
        public ItemTetrisSO katana;
        public ItemTetrisSO medkit;
        public ItemTetrisSO pistol;
        public ItemTetrisSO rifle;
        public ItemTetrisSO shotgun;
        public ItemTetrisSO money;

        public ItemTetrisSO GetItemTetrisSOFromName(string itemTetrisSOName)
        {
            foreach (ItemTetrisSO itemTetrisSO in itemTetrisSOArray)
            {
                if (itemTetrisSO.name == itemTetrisSOName)
                {
                    return itemTetrisSO;
                }
            }
            return null;
        }


        public Sprite gridBackground;
        public Sprite gridBackground_2;
        public Sprite gridBackground_3;

        public Transform gridVisual;
    }
}