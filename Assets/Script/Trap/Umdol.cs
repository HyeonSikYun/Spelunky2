using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Umdol : MonoBehaviour
{
    public float detectionRange = 7f; // ���� ����
    public float rollSpeed = 5f; // ���� �̵� �ӵ�

    private Rigidbody2D rb;
    private Vector2 moveDirection; // ���� �̵� ����

    private bool isMoving = false; // ���� �̵� ������ ���θ� ��Ÿ���� �÷���

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveDirection = Vector2.zero;
    }

    private void Update()
    {
        if (!isMoving)
        {
            // �÷��̾ ������ ����ĳ��Ʈ�� ���ϴ�.
            Vector2[] directions = { Vector2.right, Vector2.up, Vector2.left, Vector2.down };

            foreach (Vector2 direction in directions)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionRange, LayerMask.GetMask("Player"));
                //Debug.DrawRay(transform.position, direction * detectionRange, Color.red);

                if (hit.collider != null)
                {
                    // �÷��̾ ����� ���, �ش� �������� ���� �̵��մϴ�.
                    moveDirection = (hit.transform.position - transform.position).normalized;
                    StartCoroutine(MovingUmdol());
                    break; // �� �� ����Ǹ� ������ ������ �˻����� ����
                }
            }
        }
    }

    private IEnumerator MovingUmdol()
    {
        isMoving = true;
        rb.velocity = moveDirection * rollSpeed;

        // ���� �̵� ���� ���� ���
        yield return new WaitForSeconds(1.3f); // �̵� �ð� (���ϴ� �ð����� ����)

        // �̵��� �Ϸ�Ǹ� �ӵ��� 0���� ����
        rb.velocity = Vector2.zero;

        // ���� �̵� �Ϸ������� ��Ÿ���� �÷��׸� false�� ����
        isMoving = false;
    }
}
