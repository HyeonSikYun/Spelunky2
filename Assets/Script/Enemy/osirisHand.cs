using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class osirisHand : MonoBehaviour
{
    Rigidbody2D rigid;
    public bool hitPlayer = false;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 5f, LayerMask.GetMask("Player"));
        if (hit.collider != null)
        {
            hitPlayer = true;
            rigid.gravityScale = 10f;
            StartCoroutine(DeleteAfterDelay(0.4f));
        }
        else
            hitPlayer = false;

    }

    private IEnumerator DeleteAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // 지연 후 오브젝트를 삭제합니다.
        Destroy(gameObject);
    }

    
}