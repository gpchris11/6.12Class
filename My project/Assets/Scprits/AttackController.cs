using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;//메인카메라
        //카메라가 2개이상일 경우도 존재함
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
