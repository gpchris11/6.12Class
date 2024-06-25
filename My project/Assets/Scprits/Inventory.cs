using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    [SerializeField] GameObject viewInventory;//�κ��丮 ��
    [SerializeField] GameObject fabItem;//�κ��丮�� ������ ������

    List<Transform> ListTrsInventory = new List<Transform>();
    private void Awake()
    {
        if (Instance == null)
        {
            
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        initInventory();
    }
    private void initInventory()
    {
        ListTrsInventory.Clear();

        Transform[] childs = viewInventory.GetComponentsInChildren<Transform>();//���� ����

        ListTrsInventory.AddRange(childs);
        ListTrsInventory.RemoveAt(0);

    }
    /// <summary>
    /// �κ��丮�� �����ִٸ� ����. �����ִٸ� ����
    /// </summary>
   public void InActiveInventory()
    {
        if (viewInventory.activeSelf == true)
        {
            viewInventory.SetActive(false);
        }
        else
        {
            viewInventory.SetActive(true);
        }

        //viewInventory.SetActive(!viewInventory.activeSelf);
    }
    /// <summary>
    /// ����ִ� �κ��丮 �ѹ��� �����մϴ�. -1�� ���ϵȴٸ� ����ִ� ������ ���ٴ� �ǹ��Դϴ�.
    /// </summary>
    /// <returns>����ִ� ������ ���� ��ȣ</returns>
    private int getEmptyItemSlot()
    {
        int count = ListTrsInventory.Count;
        for (int inum = 0; inum < count; ++inum) 
        { 
            Transform TrsSlot = ListTrsInventory[inum];
            if (TrsSlot.childCount == 0)
            {
                return inum;
            }
        }
        return -1;
    }
    
}
