using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Umdol : MonoBehaviour
{
    public float detectionRange = 7f; // 검출 범위
    public float rollSpeed = 5f; // 돌의 이동 속도

    private Rigidbody2D rb;
    private Vector2 moveDirection; // 돌의 이동 방향

    private bool isMoving = false; // 돌이 이동 중인지 여부를 나타내는 플래그

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveDirection = Vector2.zero;
    }

    private void Update()
    {
        if (!isMoving)
        {
            // 플레이어를 검출할 레이캐스트를 쏩니다.
            Vector2[] directions = { Vector2.right, Vector2.up, Vector2.left, Vector2.down };

            foreach (Vector2 direction in directions)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionRange, LayerMask.GetMask("Player"));
                //Debug.DrawRay(transform.position, direction * detectionRange, Color.red);

                if (hit.collider != null)
                {
                    // 플레이어가 검출된 경우, 해당 방향으로 돌을 이동합니다.
                    moveDirection = (hit.transform.position - transform.position).normalized;
                    StartCoroutine(MovingUmdol());
                    break; // 한 번 검출되면 나머지 방향은 검사하지 않음
                }
            }
        }
    }

    private IEnumerator MovingUmdol()
    {
        isMoving = true;
        rb.velocity = moveDirection * rollSpeed;

        // 돌이 이동 중인 동안 대기
        yield return new WaitForSeconds(1.3f); // 이동 시간 (원하는 시간으로 변경)

        // 이동이 완료되면 속도를 0으로 설정
        rb.velocity = Vector2.zero;

        // 돌이 이동 완료했음을 나타내는 플래그를 false로 설정
        isMoving = false;
    }
}
