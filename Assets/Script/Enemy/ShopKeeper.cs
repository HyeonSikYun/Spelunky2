using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShopKeeper : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform
    public float moveSpeed = 2f; // 이동 속도
    public bool angry = false;
    public float jumpForce = 2f; // 점프 힘
    Animator anim;
    SpriteRenderer spriteRenderer;
    public AudioSource angrySound;
    private VolcanoBGM volcanoBgmScript;
    private ShopBGM shopBgmScript;
    [SerializeField] Transform target;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        GameObject volcanoObject = GameObject.Find("Volcano_Start");
        if (volcanoObject != null)
        {
            // 찾은 GameObject에서 VolcanoBGM 스크립트를 가져옴
            volcanoBgmScript = volcanoObject.GetComponent<VolcanoBGM>();
        }
        GameObject shopObject = GameObject.Find("InShop");
        if (shopObject != null)
        {
            // 찾은 GameObject에서 VolcanoBGM 스크립트를 가져옴
            shopBgmScript = shopObject.GetComponent<ShopBGM>();
        }
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (angry)
        {
            agent.SetDestination(target.position);
            volcanoBgmScript.Mute();
            shopBgmScript.Mute();
            anim.SetBool("Chase", true);
            if (player.position.x > transform.position.x)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
            //Vector3 moveDirection = (player.position - transform.position).normalized;

            //// 벽 감지 레이캐스트
            //RaycastHit2D hitleft = Physics2D.Raycast(transform.position, Vector2.left, 1.0f, LayerMask.GetMask("TileMap"));
            //RaycastHit2D hitright = Physics2D.Raycast(transform.position, Vector2.right, 1.0f, LayerMask.GetMask("TileMap"));
            //RaycastHit2D hitleft2 = Physics2D.Raycast(transform.position, Vector2.left, 1.0f, LayerMask.GetMask("Bomb"));
            //RaycastHit2D hitright2 = Physics2D.Raycast(transform.position, Vector2.right, 1.0f, LayerMask.GetMask("Bomb"));
            //if (player.position.x > transform.position.x)
            //{
            //    spriteRenderer.flipX = false;
            //}
            //else
            //{
            //    spriteRenderer.flipX = true;
            //}
            //if (hitleft.collider != null||hitright.collider!=null|| hitleft2.collider != null || hitright2.collider != null)
            //{
            //    // 벽을 감지하면 점프
            //    Jump();
            //}

            //transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!angry&&(collision.transform.CompareTag("PlayerWeapon") || collision.CompareTag("Bullet")|| collision.transform.CompareTag("Bomb")))
        {
            angry = true;
            angrySound.Play();
            
        }
    }

    private void Jump()
    {
        // 점프 힘을 적용하여 벽을 넘어갑니다.
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public void Mute()
    {
        angrySound.volume = 0.0f;
    }
}
