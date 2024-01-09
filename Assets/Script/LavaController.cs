using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaController : MonoBehaviour
{
    private bool isVisible = false; // 용암이 시야에 보이는지 여부를 나타내는 변수
    private SpriteRenderer renderer; // 용암의 렌더러 컴포넌트

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // 시야에 용암이 보이는지 여부를 체크
        if (IsVisibleInCamera())
        {
            isVisible = true;
            Debug.Log("보인다");
        }
        else
        {
            isVisible = false;
            Debug.Log("안보인다");
        }

        // isVisible 값에 따라 용암 활성화 또는 비활성화
        gameObject.SetActive(isVisible);
    }

    private bool IsVisibleInCamera()
    {
        // 용암의 중심 위치를 기준으로 시야에 보이는지 체크
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        return !GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}
