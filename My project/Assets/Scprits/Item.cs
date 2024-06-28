using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    InventoryManager inventoryManager;
    [SerializeField] string itemIdx;

    private void Start()
    {
        inventoryManager = InventoryManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))//���� ����� �÷��̾���
        {
            if (inventoryManager.GetItem(itemIdx) == true)
            {
                Destroy(gameObject);
            }
            
            //�κ��丮 �Ŵ������� ���� ���� �Ǵ��� Ȯ��
        }
    }
}
