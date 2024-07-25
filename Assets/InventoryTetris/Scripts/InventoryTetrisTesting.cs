using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.UI.Inventory;
using UnityEngine;
using VContainer;

public class InventoryTetrisTesting : MonoBehaviour
{

    [SerializeField] private Transform outerInventoryTetrisBackground;
    [SerializeField] private InventoryTetris inventoryTetris;
    [SerializeField] private InventoryTetris outerInventoryTetris;
    [SerializeField] private List<string> addItemTetrisSaveList;
    [Inject] private InventoryTestingData data;
    private int addItemTetrisSaveListIndex;

    private void Start()
    {
        // if (data != null) data.outerInventoryTetrisBackground.gameObject.SetActive(false);
        // else { outerInventoryTetrisBackground.gameObject.SetActive(false); }
        if (data != null)
        {

            data.outerInventoryTetrisBackground.gameObject.SetActive(true);
            data.outerInventoryTetris.Load(data.addItemTetrisSaveList[addItemTetrisSaveListIndex]);
            addItemTetrisSaveListIndex = (addItemTetrisSaveListIndex + 1) % data.addItemTetrisSaveList.Count;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (data != null)
            {

                data.outerInventoryTetrisBackground.gameObject.SetActive(true);
                data.outerInventoryTetris.Load(data.addItemTetrisSaveList[addItemTetrisSaveListIndex]);
                addItemTetrisSaveListIndex = (addItemTetrisSaveListIndex + 1) % data.addItemTetrisSaveList.Count;
            }
            else
            {
                outerInventoryTetrisBackground.gameObject.SetActive(true);
                outerInventoryTetris.Load(addItemTetrisSaveList[addItemTetrisSaveListIndex]);

                addItemTetrisSaveListIndex = (addItemTetrisSaveListIndex + 1) % addItemTetrisSaveList.Count;
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            //  Debug.Log(data.inventoryTetris.Save());
        }
    }

}
