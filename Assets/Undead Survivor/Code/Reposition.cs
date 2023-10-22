using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D coll;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    //is trigger가 체크된 콜라이더에서 벗어났을 때 호출
    private void OnTriggerExit2D(Collider2D collision)
    {
        //벗어난 콜라이더의 오브젝트 Tag가 "Area"가 아니면 아래 코드를 실행하지 않음
        if (!collision.CompareTag("Area"))
            return;

        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = transform.position;

        //(플레이어 위치 - 타일맵 위치)의 절댓값
        float diffx = Mathf.Abs(playerPos.x - myPos.x);
        float diffy = Mathf.Abs(playerPos.y - myPos.y);

        Vector3 playerDir = GameManager.instance.player.inputVec;
        //대각선 normalized 값이 1보다 작기 때문에 
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground":
                if (diffx > diffy) // 왼쪽 -> 오른쪽 이동
                {
                    //Translate: 지정된 값 만큼 현재 위치에서 이동
                    transform.Translate(Vector3.right * dirX * 40);
                }
                else if (diffx < diffy) //
                {
                    transform.Translate(Vector3.up * dirY * 40);
                }
                break;
            case "Enemy":
                if (coll.enabled)
                {
                    transform.Translate(playerDir * 20 + new Vector3(Random.Range(-3f, 3f), 0f));
                }
                break;
        }
    }
}