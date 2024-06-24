using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector2 movDir = new Vector2(1f, 0f);
    [SerializeField] float moveSpeed;
    BoxCollider2D checkGroundColl;
    
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        checkGroundColl = GetComponentInChildren<BoxCollider2D>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(checkGroundColl.IsTouchingLayers(LayerMask.GetMask("Ground")) == false)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;

            movDir.x *= -1;
        }

        rigid.velocity = new Vector2(movDir.x * moveSpeed, rigid.velocity.y);
    }
}
