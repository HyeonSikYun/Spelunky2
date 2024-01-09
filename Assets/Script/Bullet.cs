using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletLifetime = 2.0f; // 총알이 존재할 시간 (예: 2초)
    private float currentLifetime = 0.0f;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        currentLifetime += Time.deltaTime;

        // 총알의 현재 lifetime이 총알이 존재해야 하는 시간보다 크다면 총알을 파괴
        if (currentLifetime >= bulletLifetime)
        {
            Destroy(gameObject);
        }
    }
}
