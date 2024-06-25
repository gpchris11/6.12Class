using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    Inventory inventortManager;
    // Start is called before the first frame update
    private void Start()
    {
        inventortManager = Inventory.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I) && inventortManager != null) 
        {
            inventortManager.InActiveInventory();
        }
    }
}
