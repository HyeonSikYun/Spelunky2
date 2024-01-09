using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Animator animator;
    Transform target = null;
    float enemySpeed = 1.5f;
    Rigidbody2D rigid;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector2 dir2 = target.position - transform.position;
            transform.Translate(dir2.normalized * enemySpeed * Time.deltaTime);

            if (target.position.x < transform.position.x)
            {
                Debug.Log("¿ÞÂÊ");
                spriteRenderer.flipX = true;
            }
            if (target.position.x > transform.position.x)
            {
                Debug.Log("¿À¸¥ÂÊ");
                spriteRenderer.flipX = false;
            }
        }
    }
}
