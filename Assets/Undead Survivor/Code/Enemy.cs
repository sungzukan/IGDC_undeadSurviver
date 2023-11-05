using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    WaitForFixedUpdate wait;
    bool isLive;

    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;
    SpriteRenderer spriter;

    private void Awake() // 변수 초기화
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        wait = new WaitForFixedUpdate();
        coll = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;
        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (!isLive)
            return;
        spriter.flipX = target.position.x < rigid.position.x;
    }

    private void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true; 
        spriter.sortingOrder = 2; 
        anim.SetBool("Dead", false); // 죽는 에니메이터 
        isLive = true;
        health = maxHealth;
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isLive)
            return;
        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine("KnockBack"); // 문자열도 가능.

        if(health > 0)
        {
            //still alive but hit effect

            anim.SetTrigger("Hit");
        }
        else
        {
            //die
            isLive = false;
            coll.enabled = false; // 죽음.
            rigid.simulated = false; // 물리 시뮬을 막음
            spriter.sortingOrder = 1; // 순서를 내린다.
            anim.SetBool("Dead", true); // 죽는 에니메이터
            GameManager.instance.kill++;
            GameManager.instance.getExp();
        }

    }


    IEnumerator KnockBack()
    {
        // yield return null; 1프레임 쉼
        // yield return new waitForSeconds(2f) // 2초 쉬기

        yield return wait; // 최적화, 다음 하나의 물리 프레임 딜레이
        Vector3 PlayerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - PlayerPos;
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse); // 방향만 가진 벡터 * 넉백 상수, 순간적인 힘.
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }
}