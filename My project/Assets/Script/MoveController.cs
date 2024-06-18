using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class MoveController : MonoBehaviour
{
    [Header("플레이어 이동 및 점프")]
    Rigidbody2D rigid;
    CapsuleCollider2D coll;
    BoxCollider2D Box2d;
    Animator anim;
    Vector3 moveDir;
    float verticalVelocity = 0f; //수직으로 떨어지는 힘

    [SerializeField] float jumpForce;
    [SerializeField] float moveSpeed;

    [SerializeField] bool showGroundCheck;
    [SerializeField] float groundCheckLength;//이 길이가 게임에서 얼마만큼의 길이로 나오는지 육안으로 보기전까지는 알수가 없음
    [SerializeField] Color colorGroundCheck;
    
    
    [SerializeField] bool isGround;//인스펙터에서 플레이어가 플랫폼타일에 착지 했는지
    bool isJump;

    Camera camMain;

    [Header("벽점프")]
    [SerializeField] bool touchWall;
    bool isWallJump;
    [SerializeField] float WallJumpTime = 0.3f;
    float WallJumpTimer = 0.0f;//타이머

    private void OnDrawGizmos()
    {
        if (showGroundCheck == true)
        {
            Debug.DrawLine(transform.position, transform.position - new Vector3(0, groundCheckLength), colorGroundCheck);//현재위치에서 일정 y값 아래로
            
        }

        //Debug.DrawLine(); 디버그도 체크용도로 씬 카메라에 선을 그려줄수 있음
        //Gizmos.DrawSphere(); 디버그 보다 더 많은 시각효과를 제공
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


    //private void OnTriggerEnter2D(Collider2D collision)//상대방의 콜라이더를 가져옴, 누가 실행시킨지는 모름
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

        //Layer int로 대상의 레이어를 구분
        //Layer의 int와 공통적으로 활용하는 int와 다름

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
        //transform.localPosition; 씬 공간에서 부모의 좌표기준
        //transform.position; 씬 공간에서의 좌표

        ///좌우 쳐다보기

        //    Vector3 scale = transform.localScale;
        //if (moveDir.x < 0 && scale.x != 1.0f )//왼쪽

        //{
        //    scale.x = 1.0f;
        //    transform.localScale = scale;


        //}
        //else if(moveDir.x > 0 && scale.x != -1.0f)//오른쪽
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
        //좌우키를 누르면 좌우로 움직인다
        moveDir.x = Input.GetAxisRaw("Horizontal") * moveSpeed;//a, L A ke -1, d R A key 1, 아무것도 입력하지 않으면 0
        moveDir.y = rigid.velocity.y;
        rigid.velocity = moveDir;

        //슈팅게임 만들때는 오브젝트를 코드에 의해서 순간이동하게 만듬
        //물리에 의해서 이동

        


    }

    private void jump()
    {
            //if (isGround == true&& Input.GetKeyDown(KeyCode.Space))
            //{
            //    rigid.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);//지긋이 미는 힘
            
            //Vector2 force = rigid.velocity;
            //force.y = 0;
            //rigid.velocity = force;





           if(isGround == false)//공중에 떠있는 상태라면
           {
            //벽에 붙어있고, 그리고 벽을 향해 플레이어가 방향키를 누르고 있는데 점프키를 눌렀다면
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
            dir.x *= -1f;//반대방향
            rigid.velocity = dir;

            verticalVelocity = jumpForce * 0.5f;
            //일정시간 유저가 입력할수 없어야 벽을 발로창 x값을 볼수 있음
            //입력불가 타이머가 작동시켜야함
        }
        else if (isGround == false)//공중에 떠있는 상태
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



