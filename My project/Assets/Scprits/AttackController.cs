using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    Camera mainCam;
    [SerializeField] Transform trsHand;
    [SerializeField] GameObject objThrowWeapon;
    [SerializeField] Transform trsWeapon;
    [SerializeField] Transform trsDynamic;
    [SerializeField] Vector2 throwForce = new Vector2(10f, 0f);

    private void Start()
    {
        mainCam = Camera.main;//메인카메라
        //카메라가 2개이상일 경우도 존재함
        //Camera.current
    }

    void Update()
    {
        checkAim();
        checkCreate();
    }

    private void checkAim()
    {
        Vector2 mouseWorldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos = transform.position;
        Vector2 fixedPos = mouseWorldPos - playerPos;

        //fixedPos.x > 0 또는 transform.localScale.x -1 => 오른쪽, 1 => 왼쪽

        float angle = Quaternion.FromToRotation(
            transform.localScale.x < 0 ? Vector3.right : Vector3.left
            , fixedPos).eulerAngles.z;
        trsHand.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void checkCreate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            createWeapon();
        }
    }

    private void createWeapon()
    {
        GameObject go = Instantiate(objThrowWeapon, trsWeapon.position, trsWeapon.rotation, trsDynamic);
        ThrowWeapon goSc = go.GetComponent<ThrowWeapon>();
        bool isRight = transform.localScale.x < 0 ? true : false;
        Vector2 fixedThrowForce = throwForce;
        if (isRight == false)
        {
            fixedThrowForce = -throwForce;//x 10 y 0
        }
        goSc.SetForce(trsWeapon.rotation * fixedThrowForce, isRight);
    }
}
