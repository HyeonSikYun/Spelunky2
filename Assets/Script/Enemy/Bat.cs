using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    public GameObject[] effects;

    SpriteRenderer spriteRenderer;
    Animator animator;
    Transform target = null;
    float enemySpeed = 2.5f;
    Rigidbody2D rigid;
    BoxCollider2D boxCollider;
    public AudioSource batDie;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (target != null)
        {
            Vector2 dir2 = target.position - transform.position;
            transform.Translate(dir2.normalized * enemySpeed * Time.deltaTime);

            if (target.position.x < transform.position.x)
            {
                Debug.Log("왼쪽");
                spriteRenderer.flipX = true;
            }
            if (target.position.x > transform.position.x)
            {
                Debug.Log("오른쪽");
                spriteRenderer.flipX = false;
            }
        }
        //박쥐가 플레이어 발견
        Vector2 checkVec = new Vector2(rigid.position.x, rigid.position.y);
        RaycastHit2D hit = Physics2D.Raycast(checkVec, Vector2.down , 6f, LayerMask.GetMask("Player"));
        if(hit.collider!=null)
        {
            Debug.Log("박쥐가 플레이어 발견");
            target = hit.collider.transform;
            animator.SetBool("Open", false);
            animator.SetBool("Close", false);
            animator.SetBool("Flying", true);
        }
        //Debug.DrawRay(checkVec, Vector2.down*6f, new Color(1, 0, 0));

        //가까이왔을때 박쥐가 위협
        int numRays = 8;
        for(int i=0;i<numRays;i++)
        {
            float xOffset = i * 0.3f - ((numRays - 1) * 0.3f) / 2f;
            Vector2 leftCheckVec = new Vector2(rigid.position.x - 2f+xOffset, rigid.position.y);
            Vector2 rightCheckVec = new Vector2(rigid.position.x + 2f-xOffset, rigid.position.y);

            RaycastHit2D WarningLeft = Physics2D.Raycast(leftCheckVec, Vector2.down, 6f, LayerMask.GetMask("Player"));
            RaycastHit2D WarningRight = Physics2D.Raycast(rightCheckVec, Vector2.down, 6f, LayerMask.GetMask("Player"));

            if (WarningLeft.collider != null || WarningRight.collider != null)
            {
                animator.SetBool("Close", false);
                animator.SetBool("Open", true);
            }
            else
            {
                animator.SetBool("Open", false);
                animator.SetBool("Close", true);
            }

            //Debug.DrawRay(leftCheckVec, Vector2.down * 6f, new Color(0, 1, 0));
            //Debug.DrawRay(rightCheckVec, Vector2.down * 6f, new Color(0, 1, 0));
        }
        
        
        
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("PlayerWeapon")|| other.gameObject.tag=="Trap"||other.gameObject.tag=="Bullet")
        {
            batDie.Play();
            foreach (GameObject effect in effects)
            {
                effect.SetActive(true);
            }
            spriteRenderer.enabled = false;
            boxCollider.enabled = false;
            //gameObject.SetActive(false);
        }
    }
}

