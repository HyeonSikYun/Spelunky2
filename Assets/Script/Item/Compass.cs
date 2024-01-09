using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    private Transform player; // 플레이어의 Transform
    private Transform exit; // 출구의 Transform
    public RectTransform arrowImage; // 화살표 이미지의 RectTransform


    private void Start()
    {
        player = GameObject.Find("Player").transform; // "PlayerObjectName"은 플레이어 오브젝트의 이름입니다.
        exit = GameObject.Find("Volcano_Exit").transform;
    }

    private void Update()
    {
        if (player == null || exit == null || arrowImage == null)
            return;

        // 출구 방향 벡터 계산
        Vector3 direction = exit.position - player.position;
        direction.z = 0f; // 2D 게임에서는 Z 축을 사용하지 않습니다
        //Debug.Log(direction);
        // 방향 벡터로부터 각도 계산
        float angle = Vector3.SignedAngle(-Vector3.up, direction, Vector3.forward);

        // 화살표 이미지의 Z 회전값을 설정
        arrowImage.localRotation = Quaternion.Euler(0f, 0f, angle);
    }
}
