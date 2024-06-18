using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class MoveController : MonoBehaviour
{
    [Header("�÷��̾� �̵� �� ����")]
    Rigidbody2D rigid;
    CapsuleCollider2D coll;
    BoxCollider2D Box2d;
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

    Camera camMain;

    [Header("������")]
    [SerializeField] bool touchWall;
    bool isWallJump;
    [SerializeField] float WallJumpTime = 0.3f;
    float WallJumpTimer = 0.0f;//Ÿ�̸�

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
    public void TriggerEnter(HitBox.ehitboxType _type,Collider2D _collision)
    {
        if(_type == HitBox.ehitboxType.WallCheck)
        {
            if (_collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                touchWall = true;
            }
        }
    }

    public void TriggerExit(HitBox.ehitboxType _type, Collider2D _collision)
    {
        if(_type == HitBox.ehitboxType.WallCheck)
        {
            if (_collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                touchWall = false;
            }
        }
    }


    //private void OnTriggerEnter2D(Collider2D collision)//������ �ݶ��̴��� ������, ���� �����Ų���� ��
    //{
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
    //    {
    //        touchWall = true;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
    //    {
    //        touchWall = false;
    //    }
    //}

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        Box2d = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        camMain = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        checkGrounded();

        Moving();
        checkAim();
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
        //Physics2D.Raycast(transform.position, Vector2.down, groundCheckLength, LayerMask.GetMask("Ground"));

        Physics2D.BoxCast(Box2d.bounds.center, Box2d.bounds.size, 0f, Vector2.down, 0.05f, LayerMask.GetMask("Ground"));
        if (hit)
        {
            isGround = true;
        }

    }


    private void checkAim()
    {
        //transform.localPosition; �� �������� �θ��� ��ǥ����
        //transform.position; �� ���������� ��ǥ

        ///�¿� �Ĵٺ���

        //    Vector3 scale = transform.localScale;
        //if (moveDir.x < 0 && scale.x != 1.0f )//����

        //{
        //    scale.x = 1.0f;
        //    transform.localScale = scale;


        //}
        //else if(moveDir.x > 0 && scale.x != -1.0f)//������
        //{
        //    scale.x = -1.0f;
        //    transform.localScale = scale;
        //}
        ///

        
        Vector2 mouseWorldPos = camMain.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos =  transform.position;
        Vector2 fixedPos = mouseWorldPos - playerPos;
       

        Vector3 playerScale = transform.localScale;
        if (fixedPos.x > 0 && playerScale.x != -1.0f)
        {
            playerScale.x = -1.0f;
        }
        else if (fixedPos.x < 0 && playerScale.x != 1.0f)
        {
            playerScale.x = 1.0f;
        }
        transform.localScale = playerScale;



    }



    private void Moving()
    {
        //�¿�Ű�� ������ �¿�� �����δ�
        moveDir.x = Input.GetAxisRaw("Horizontal") * moveSpeed;//a, L A ke -1, d R A key 1, �ƹ��͵� �Է����� ������ 0
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





           if(isGround == false)//���߿� ���ִ� ���¶��
           {
            //���� �پ��ְ�, �׸��� ���� ���� �÷��̾ ����Ű�� ������ �ִµ� ����Ű�� �����ٸ�
            if (touchWall == true && moveDir.x != 0f && Input.GetKeyDown(KeyCode.Space))
            {
                isWallJump = true;
            }


               return;
           }
           
           if (Input.GetKeyDown(KeyCode.Space) == true) 
           {
               isJump = true;
           }
    }

    private void checkGrav()
    {
        if(isWallJump == true)
        {
            isWallJump = false;

            Vector2 dir = rigid.velocity;
            dir.x *= -1f;//�ݴ����
            rigid.velocity = dir;

            verticalVelocity = jumpForce * 0.5f;
            //�����ð� ������ �Է��Ҽ� ����� ���� �߷�â x���� ���� ����
            //�ԷºҰ� Ÿ�̸Ӱ� �۵����Ѿ���
        }
        else if (isGround == false)//���߿� ���ִ� ����
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



