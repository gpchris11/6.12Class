using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;//����ī�޶�
        //ī�޶� 2���̻��� ��쵵 ������
        //Camera.current
    }

    void Update()
    {
        checkAim();
    }

    private void checkAim()
    {
        Vector2 mouseWorldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(mouseWorldPos);
    }
}
