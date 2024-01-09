using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftPunch : MonoBehaviour
{
    Rigidbody2D rigid;
    public GameObject punch;
    float punchSpeed = 3f;

    //Vector2 originalPosition;
    private BoxCollider2D punchCollider;
    public float colliderActivationDuration = 0.4f;
    bool isPunching = false;
    public AudioSource punchSound;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        punchCollider = gameObject.GetComponent<BoxCollider2D>();
        //originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPunching)
        {
            RaycastHit2D leftPunch = Physics2D.Raycast(punch.transform.position, Vector2.left, 2f, LayerMask.GetMask("Player"));

            if (leftPunch.collider != null)
            {
                punchSound.Play();
                Debug.DrawRay(punch.transform.position, Vector2.left * 2f, new Color(1, 0, 0));
                punchCollider.enabled = true;
                rigid.velocity = new Vector2(-1f * punchSpeed, rigid.velocity.y);

                StartCoroutine(DeactivateCollider());

                isPunching = true;
                StartCoroutine(ResetPunch());
            }
        }
        
    }

    IEnumerator ResetPunch()
    {
        yield return new WaitForSeconds(0.4f); // 1�� ���
        //rigid.velocity = Vector2.zero; // ��ġ �ʱ�ȭ
        rigid.velocity = new Vector2(1f * punchSpeed, rigid.velocity.y);
        yield return new WaitForSeconds(0.4f);
        rigid.velocity = Vector2.zero;
        isPunching = false; // ��ġ �� �÷��� ����

        //transform.position = originalPosition;
    }

    IEnumerator DeactivateCollider()
    {
        // ������ �ð� ���� ����� �� �ݶ��̴��� ��Ȱ��ȭ
        yield return new WaitForSeconds(colliderActivationDuration);

        if (punchCollider != null)
        {
            punchCollider.enabled = false;
        }
    }
}
