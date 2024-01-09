using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyPlayer : MonoBehaviour
{
    private static DontDestroyPlayer s_Instance = null;

    private void Awake()
    {
        if (s_Instance == null) //�����ϰ� ���� ������
        {
            s_Instance = this; //�ٽ� �ֽ�ȭ
            DontDestroyOnLoad(gameObject); //������� �ı� x
        }
        else
        {
            if (s_Instance != this) //�ߺ����� �����ҽÿ��� �ı�! 
                Destroy(this.gameObject);
        }
    }
}
