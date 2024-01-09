using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeGround : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool playerOnPlatform = false;
    private float playerEnterTime = 0f;

    public float shakeForce = 10f;
    public float fallDelay = 2f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !playerOnPlatform)
        {
            playerOnPlatform = true;
            playerEnterTime = Time.time;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Lava"))
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (playerOnPlatform && Time.time - playerEnterTime >= fallDelay)
        {
            ShakeAndFall();
        }
    }

    private void ShakeAndFall()
    {
        // ��鸮�� �ϱ�
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddForce(Vector2.right * shakeForce, ForceMode2D.Impulse);

        // ������ �Ŀ��� �߷� ����
        rb.gravityScale = 2f;

        // �� �̻� �÷��̾�� ��ȣ�ۿ����� �ʵ��� �÷��� ����
        playerOnPlatform = false;
    }
}