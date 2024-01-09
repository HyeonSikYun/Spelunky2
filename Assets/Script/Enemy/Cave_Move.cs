using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cave_Move : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove = 0;
    Animator anim;
    SpriteRenderer render;
    BoxCollider2D boxcollider;
    public float normalSpeed = 5f; // 원래 속도
    public float detectPlayerSpeed = 10f; // 플레이어를 발견한 후의 속도
    private bool detectedPlayer = false;
    public GameObject localPlayer = null;
    private bool isHit = false;
    public GameObject[] effects;
    public AudioSource Chase;
    public AudioSource Die;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        boxcollider = GetComponent<BoxCollider2D>();
        Invoke("Think", 5);
    }

    void FixedUpdate()
    {
        if (isHit)
            return;

        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.3f, rigid.position.y); //지형 체크
        Vector2 frontVec2 = new Vector2(rigid.position.x + nextMove * 5.2f, rigid.position.y);
        //Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("TileMap"));
        RaycastHit2D rayHitPlayer = Physics2D.Raycast(frontVec2, Vector2.right, 6f, LayerMask.GetMask("Player"));
        //Debug.DrawRay(frontVec2, Vector2.right * 6f, new Color(1, 0, 0));

        float currentSpeed = (localPlayer != null) ? detectPlayerSpeed : normalSpeed;

        if (rayHitPlayer.collider != null)
        {
            detectedPlayer = true;
            localPlayer = rayHitPlayer.collider.gameObject;
            detectedPlayer = true;
            anim.SetBool("Running", true);
            anim.SetBool("Walking", false);
            anim.SetBool("Rest", false);

            Debug.Log("발견");
        }
        else
        {
            detectedPlayer = false;
        }

        if (localPlayer&&nextMove==0)
        {
            nextMove = (int)Mathf.Sign(localPlayer.transform.position.x - transform.position.x);
            CancelInvoke("Think");
        }

        rigid.velocity = new Vector2(nextMove*currentSpeed, rigid.velocity.y); //움직임
        
        if (rayHit.collider == null)
        {
            Turn();
        }

        if (detectedPlayer)
            Chase.Play();
    }

    void Think()
    {
        nextMove = Random.Range(-1, 2);
        float nextThink = Random.Range(2f, 5f);
        Invoke("Think", nextThink);
        if(nextMove==0)
        {
            anim.SetBool("Rest", true);
            anim.SetBool("Walking", false);
        }
        if(nextMove!=0)
        {
            anim.SetBool("Walking", true);
            anim.SetBool("Rest", false);
            render.flipX = nextMove == -1;
        }

    }

    void Turn()
    {
        nextMove *= -1;
        render.flipX = nextMove == -1;
        CancelInvoke();
        Invoke("Think", 2);
    }

    public void OnDamaged()
    {
        boxcollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("PlayerWeapon")|| collision.CompareTag("Trap")||collision.CompareTag("Bullet"))
        {
            foreach (GameObject effect in effects)
            {
                effect.SetActive(true);
            }
            Debug.Log("무기 맞음");
            Chase.Stop();
            Die.Play();
            TakeDamage(collision.transform.position);
            anim.SetTrigger("Die");
            isHit = true;
            gameObject.layer = 10;
            Invoke("Dead", 2f);
        }
    }

    void TakeDamage(Vector2 targetPos)
    {
        render.color = new Color(1, 1, 1, 0.4f);
        int dir = transform.position.x - targetPos.x > 0 ? 1 : -1;
        if(dir==1)
        {
            render.flipX = true;
            rigid.AddForce(new Vector2(dir, 1) * 8, ForceMode2D.Impulse);
            Invoke("OffDamaged", 1);
        }
        else
        {
            rigid.AddForce(new Vector2(dir, 1) * 8, ForceMode2D.Impulse);
            Invoke("OffDamaged", 1);
        }
        
    }
    void OffDamaged()
    {
        render.color = new Color(1, 1, 1, 1);
    }
    void Dead()
    {
        gameObject.SetActive(false);
    }
}
