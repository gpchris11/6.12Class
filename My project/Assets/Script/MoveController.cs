using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class MoveController : MonoBehaviour
{
    [Header("�÷��̾� �̵� �� ����")]
    Rigidbody2D rigid;
    CapsuleCollider2D coll;
    Animator anim;
    Vector3 moveDir;
    float verticalVelocity = 0f; //�������� �������� ��

    [SerializeField] float jumpForce;
    [SerializeField] float moveSpeed;

    [SerializeField] bool showGroundCheck;
    [SerializeField] float groundCheckLength;//�� ���̰� ���ӿ��� �󸶸�ŭ�� ���̷� �������� �������� ������������ �˼��� ����
    [SerializeField] Color colorGroundCheck;
    
    
    [SerializeField] bool isGround;//�ν����Ϳ��� �÷��̾ �÷���Ÿ�Ͽ� ���� �ߴ���
    bool isJump;

    private void OnDrawGizmos()
    {
        if (showGroundCheck == true)
        {
            Debug.DrawLine(transform.position, transform.position - new Vector3(0, groundCheckLength), colorGroundCheck);//������ġ���� ���� y�� �Ʒ���
            
        }

        //Debug.DrawLine(); ����׵� üũ�뵵�� �� ī�޶� ���� �׷��ټ� ����
        //Gizmos.DrawSphere(); ����� ���� �� ���� �ð�ȿ���� ����
        //Handles.DrawWireArc

    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkGrounded();

        Moving();
        jump();

        checkGrav();

        doAnim();
    }

    private void checkGrounded()
    {
        isGround = false;

        if(verticalVelocity > 0f)
        {
            return;
        }

        //Layer int�� ����� ���̾ ����
        //Layer�� int�� ���������� Ȱ���ϴ� int�� �ٸ�

        RaycastHit2D hit = 
        Physics2D.Raycast(transform.position, Vector2.down, groundCheckLength, LayerMask.GetMask("Ground"));
        
        if(hit)
        {
            isGround = true;
        }
    }
    private void Moving()
    {
        //�¿�Ű�� ������ �¿�� �����δ�
        moveDir.x = Input.GetAxisRaw("Horizontal");//a, L A ke -1, d R A key 1, �ƹ��͵� �Է����� ������ 0
        moveDir.y = rigid.velocity.y;
        rigid.velocity = moveDir;

        //���ð��� ���鶧�� ������Ʈ�� �ڵ忡 ���ؼ� �����̵��ϰ� ����
        //������ ���ؼ� �̵�


    }

    private void jump()
    {
            //if (isGround == true&& Input.GetKeyDown(KeyCode.Space))
            //{
            //    rigid.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);//������ �̴� ��
            
            //Vector2 force = rigid.velocity;
            //force.y = 0;
            //rigid.velocity = force;
           if(isGround == false)
           {
               return;
           }
           
           if (Input.GetKeyDown(KeyCode.Space) == true) 
           {
               isJump = true;
           }
    }

    private void checkGrav()
    {
        if(isGround == false)//���߿� ���ִ� ����
        {

            verticalVelocity += Physics.gravity.y * Time.deltaTime;//-9.81

            if(verticalVelocity < -10f)
            {
                verticalVelocity = -10f;
            }
        }
        else if(isJump == true)
        {
            isJump = false;
            verticalVelocity = jumpForce;
         }
        else if (isGround == true)
        {
            verticalVelocity = 0; 
        }

        rigid.velocity = new Vector2(rigid.velocity.x, verticalVelocity);
    }

    private void doAnim()
    {
        anim.SetInteger("Horizontal", (int)moveDir.x);
        anim.SetBool("IsGround", isGround);
    }
}



