using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anubis : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Animator anim;
    Rigidbody2D rigid;
    Transform target = null;
    float enemySpeed = 2.5f;
    float hp = 3f;
    public GameObject[] effects;
    BoxCollider2D boxCollider;
    public GameObject Key;
    public AudioSource findSound;
    public AudioSource dieSound;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            
            Invoke("Find", 1f);
            
        }

        Vector2[] directions = { Vector2.right, Vector2.up, Vector2.left, Vector2.down };

        foreach (Vector2 direction in directions)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 5f, LayerMask.GetMask("Player"));
            //Debug.DrawRay(transform.position, direction * detectionRange, Color.red);

            if (hit.collider != null)
            {
                findSound.Play();
                anim.SetBool("WakeUp",true);
                target = hit.collider.transform;
                anim.SetBool("Move",true);
            }
        }
    }

    void Find()
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("PlayerWeapon") || other.gameObject.tag == "Trap" || other.gameObject.tag == "Bullet")
        {
            OnDamaged(other.transform.position);

            if (hp==0)
            {
                dieSound.Play();
                foreach (GameObject effect in effects)
                {
                    effect.SetActive(true);
                }
                Key.SetActive(true);
                spriteRenderer.enabled = false;
                boxCollider.enabled = false;
            }
            
        }
    }

    void OnDamaged(Vector2 targetPos) //피격 무적시간
    {
        hp--;
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        Invoke("OffDamaged", 1);

    }

    void OffDamaged()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
}
