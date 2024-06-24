using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    Camera mainCam;
    [SerializeField] Transform trsHand;
    [SerializeField] GameObject objThrowWeapon;
    [SerializeField] Transform TrsWeapon;
    [SerializeField] Transform trsobjDyanamic;
    [SerializeField] Vector2 ThrowForce = new Vector2(10f, 0f);

    private void Start()
    {
        mainCam = Camera.main;//����ī�޶�
        

        //ī�޶� 2���̻��� ��쵵 ������
        //Camera.current
    }

    void Update()
    {
        checkAim();
        checKCreate();
    }

    private void checkAim()
    {
        Vector2 mouseWorldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos = transform.position;
        Vector2 fixedPos = mouseWorldPos - playerPos;

        //fixedPos.x > 0 �Ǵ� transform.localScale.x -1 => ������, 1 => ����

        float angle = Quaternion.FromToRotation(transform.localScale.x < 0 ? Vector3.right : Vector3.left, fixedPos).eulerAngles.z;
        trsHand.rotation = Quaternion.Euler(0, 0, angle);


    }

    private void checKCreate()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            creatWeapon();
        }
    }

   private void creatWeapon()
    {
        GameObject go = Instantiate(objThrowWeapon, TrsWeapon.position, TrsWeapon.rotation, trsobjDyanamic);
        ThrowWeapon goSc = go.GetComponent<ThrowWeapon>();
        bool isRight = transform.localScale.x < 0 ? true : false;
        Vector2 fixedThrowForce = ThrowForce;
        if (isRight == false)
        {
            fixedThrowForce = -ThrowForce;
        }
        goSc.SetForce(TrsWeapon.rotation * fixedThrowForce, isRight);


    }
}
