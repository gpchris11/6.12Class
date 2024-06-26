using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//아이템을 드래그 시작하거나 놓으면 이 스크립트가 드래그 아이템을 파악하여 자식으로 분류해줌

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
    /// 이벤트 시스템으로 인해 드래그 되느 대상이 이 스크립트 위에서 드롭되게 되면 
    /// 해당 드롭 오브젝트를 나의 자식으로 변경합니다.
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
