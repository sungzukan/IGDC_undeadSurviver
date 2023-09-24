using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    public Vector2 inputVec;
    public float speed;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator ani;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
    }


    private void FixedUpdate()
    {
        Vector2 nextVec = inputVec * speed * Time.deltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }


    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    void LateUpdate()
    {
        ani.SetFloat("Speed", inputVec.magnitude);


        if(inputVec.x != 0)
        {
            spriter.flipX = (inputVec.x < 0 ? true : false);
        }


    }
}
