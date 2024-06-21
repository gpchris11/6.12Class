using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("커서 이미지")]
    [SerializeField, Tooltip("0은 디폴트, 1은 클릭")]  List<Texture2D> cursors;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKey(KeyCode.Mouse0))//클릭했을때
       {

            Cursor.SetCursor(cursors[1], new Vector2(cursors[1].width * 0.5f, cursors[1].height * 0.5f), CursorMode.Auto);
       } 
       else
       {
            Cursor.SetCursor(cursors[0], new Vector2(cursors[1].width * 0.5f, cursors[1].height * 0.5f), CursorMode.Auto);
        }
    }
}
