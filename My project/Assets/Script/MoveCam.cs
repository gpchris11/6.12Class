using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour
{

    [SerializeField] Transform chaseTrs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 fixedPos = chaseTrs.position;

        fixedPos.z = transform.position.z;
        transform.position = fixedPos;//Z축 까지 따라감
    }
}
