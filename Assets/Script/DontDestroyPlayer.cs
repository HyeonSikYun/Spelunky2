using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyPlayer : MonoBehaviour
{
    private static DontDestroyPlayer s_Instance = null;

    private void Awake()
    {
        if (s_Instance == null) //존재하고 있지 않을때
        {
            s_Instance = this; //다시 최신화
            DontDestroyOnLoad(gameObject); //씬변경시 파괴 x
        }
        else
        {
            if (s_Instance != this) //중복으로 존재할시에는 파괴! 
                Destroy(this.gameObject);
        }
    }
}
