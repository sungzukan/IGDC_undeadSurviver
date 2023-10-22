//GameManager.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //다른 클래스에서 접근할 수 있도록 public static으로 선언
    public static GameManager instance;
    public Player player;

    private void Awake()
    {
        //현재 생성된 인스턴스 할당
        instance = this;
    }
}