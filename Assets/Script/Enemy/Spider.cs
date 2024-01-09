using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    public GameObject[] effects;
    SpriteRenderer spriteRenderer;
    Animator animator;
    Rigidbody2D rigid;
    public int nextMove = 0;
    private bool foundPlayer = false;
    BoxCollider2D boxCollider;
    public AudioSource fallSpider;
    public AudioSource spiderDie;
    

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 checkVec = new Vector2(rigid.position.x, rigid.position.y);
        RaycastHit2D hit = Physics2D.Raycast(checkVec, Vector2.down, 6f, LayerMask.GetMask("Player"));
        if (hit.collider != null && !foundPlayer)
        {
            fallSpider.Play();
            Debug.Log("거미 떨어져요");
            rigid.gravityScale = 3f;
            animator.SetBool("Find", true);
            foundPlayer = true;

            // 처음 찾을 때만 랜덤한 시간 후에 JumpAtk 함수 호출
            float randomDelay = Random.Range(1f, 4f);
            InvokeRepeating("JumpAtk", randomDelay, 2f); // 2초 간격으로 반복 호출
        }
        //Debug.DrawRay(checkVec, Vector2.down*6f, new Color(1, 0, 0));
    }

    void JumpAtk()
    {
        nextMove = Random.Range(0, 2);

        if (nextMove == 0)
        {
            rigid.velocity = new Vector2(-transform.localScale.x * 6f, 8);
            animator.SetTrigger("Jump");
        }
        else
        {
            rigid.velocity = new Vector2(transform.localScale.x * 6f, 8);
            animator.SetTrigger("Jump");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("PlayerWeapon")||other.CompareTag("Trap")||other.CompareTag("Bullet"))
        {
            spiderDie.Play();
            foreach (GameObject effect in effects)
            {
                effect.SetActive(true);
            }
            spriteRenderer.enabled = false;
            boxCollider.enabled = false;
        }
    }
}
