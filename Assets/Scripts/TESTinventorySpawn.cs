using System.Collections;
using System.Collections.Generic;
using CsvHelper.Configuration;
using InventorySystem;
using UnityEngine;

public class TESTinventorySpawn : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!InventoryController.instance.InventoryFull("Inventory", "Purple coral"))
            {
                InventoryController.instance.AddItem("Inventory", "Purple coral");
                //Destroy(gameObject);

            }
            else
            {
                Debug.Log("Inventory Cannot Fit Item");
            }
        }
    }
}
