using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    [SerializeField] GameObject viewInventory;//인벤토리 뷰
    [SerializeField] GameObject fabItem;//인벤토리에 생성될 프리팹

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

        Transform[] childs = viewInventory.GetComponentsInChildren<Transform>();//문제 있음

        ListTrsInventory.AddRange(childs);
        ListTrsInventory.RemoveAt(0);

    }
    /// <summary>
    /// 인벤토리가 열려있다면 닫힘. 닫혀있다면 열림
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
    /// 비어있는 인벤토리 넘버를 리턴합니다. -1이 리턴된다면 비어있는 슬롯이 없다는 의미입니다.
    /// </summary>
    /// <returns>비어있는 아이템 슬롯 번호</returns>
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
