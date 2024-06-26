using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//�������� �巡�� �����ϰų� ������ �� ��ũ��Ʈ�� �巡�� �������� �ľ��Ͽ� �ڽ����� �з�����

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{

    Image imgSlot;
    RectTransform rect;

    private void Awake()
    {
        imgSlot = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
        {
            imgSlot.color = Color.red;
        }
    public void OnPointerExit(PointerEventData eventData)
    {
        imgSlot.color = Color.white;
    }

    /// <summary>
    /// �̺�Ʈ �ý������� ���� �巡�� �Ǵ� ����� �� ��ũ��Ʈ ������ ��ӵǰ� �Ǹ� 
    /// �ش� ��� ������Ʈ�� ���� �ڽ����� �����մϴ�.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            eventData.pointerDrag.transform.SetParent(transform);
            eventData.pointerDrag.transform.position = rect.position;
        }
        
    }
}
