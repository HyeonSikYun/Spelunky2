using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect2 : MonoBehaviour
{
    public float scaleSpeed = 0.05f; // 스케일을 조절하는 속도
    public Vector3 startScale = new Vector3(0.1f, 0.1f, 0.1f);
    public Vector3 targetScale = new Vector3(1.0f, 1.0f, 1.0f);
    private bool scalingUp = true; // 시작할 때 크기를 키우는 플래그
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = startScale; // 시작 스케일 설정
    }
    private void Update()
    {
        if (scalingUp)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, scaleSpeed * Time.deltaTime);
            //transform.localScale += new Vector3(scaleSpeed, scaleSpeed, scaleSpeed) * Time.deltaTime;
            if (transform.localScale.x >= targetScale.x)
                scalingUp = false; // 목표 스케일에 도달하면 크기 키우기 중지
        }
    }

    void Delete()
    {
        gameObject.SetActive(false);

    }
}
