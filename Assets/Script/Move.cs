using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Rigidbody2D rigid;
    private float jump = 15f;
    private float jumpPlus = 18f;
    public float max_speed;
    SpriteRenderer spriteRenderer;
    Animator anim;
    public Animator ATK1;
    public Animator ATK2;
    public Animator ATK3;
    public bool isLadder; // ��ٸ� üũ
    bool isUp;
    bool isDown;
    public Transform pos;
    public Vector2 boxSize;
    private Transform childObjectTransform; // WeaponParent ����
    private Transform childObjectTransform2; // WeaponArea ����
    private Transform WeaponChildTransform; // Gun ������Ʈ ����
    private float curTime;
    public float coolTime = 0.08f;
    public GameObject[] effects;
    public GameObject[] bones;

    private Transform playerTransform; // �÷��̾� ������Ʈ�� Transform ����
    private GameObject childObject; // �÷��̾��� �ڽ��� �ڽ� ��ü
    private BoxCollider2D childCollider; // �ڽ��� �ڽ� ��ü�� �ݶ��̴�
    public float colliderActivationDuration = 0.2f;

    public float player_HP = 4f;
    public bool isdie = false;
    public float bomb = 4f;
    public float rope = 4f;
    public float money = 0f;

    public GameObject bulletPrefab; // �Ѿ� ������
    public GameObject bulletPrefab2; // �Ѿ� ������
    public GameObject bulletPrefab3; // �Ѿ� ������
    public Transform firePoint; // �Ѿ��� �߻�� ��ġ
    public Transform firePoint2; // �Ѿ��� �߻�� ��ġ
    public Transform firePoint3; // �Ѿ��� �߻�� ��ġ
    private float recoilForce = 6f;

    public float knockbackForce = 15f;
    public float knockbackUpwardForce = 5f;

    public float deathDistance = 0.1f;
    private bool isBeingPushed = false;

    public bool hasKey = false;
    public bool hasRope = false;
    public bool throwbomb = true;
    public bool plusBomb = false;
    public bool hasGlove = false;
    private bool hasSpring = false;
    private bool hasGun = false;
    private bool onoffGun = false;
    bool purchaseGun = false;
    bool wearingGun = false;

    bool purchaseBigbomb = false;
    bool purchaseRope = false;
    bool purchaseBomb = false;
    bool purchaseJelly = false;
    bool purchaseChicken = false;
    bool purchaseSpring = false;
    bool purchaseCompass = false;
    bool purchaseGlove = false;

    public GameObject KeyImage;
    public GameObject arrow;
    public GameObject gloveImage;
    public GameObject compassImage;
    public GameObject springImage;
    public GameObject GunSet;
    public GameObject gunimage;
    public GameObject gameOverUI;
    public GameObject dieIcon;
    private UI_Manager playerhp;
    private UI_Manager playerMoney;
    public GameObject Gun;

    public GameObject ropeObject; // Ȱ��ȭ/��Ȱ��ȭ�� ��������Ʈ �̹��� ������Ʈ
    public float moveDistance = 10f; // ���� �̵��� �Ÿ�
    public float moveDuration = 0.4f; // �̵��� �ɸ� �ð�
    private Vector3 initialPosition; // �ʱ� ��ġ
    private bool isMoving = false; // �̵� ������ ����

    public AudioSource gunSound;
    public AudioSource attackSound;
    public AudioSource bombSound;
    public AudioSource gasiSound;
    public AudioSource moneyEat;
    public AudioSource ropeSound;
    public AudioSource buySound;
    public AudioSource dieSound;
    public AudioSource damageSound;
    public AudioSource ghostkillSound;

    //public Camera mainCamera; // ī�޶� ������Ʈ�� �ν����Ϳ��� �Ҵ�
    //private Vector3 initialCameraPosition;
    //private bool isCameraMoving = false;
    //private float cameraMoveSpeed = 1.0f;
    //private float cameraMoveDuration = 2.0f;

    void Awake()
    {
        Screen.SetResolution(960, 540, false);
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        childObjectTransform = transform.Find("WeaponParent");
        childObjectTransform2 = transform.Find("WeaponArea");

        playerTransform = transform;
        Transform child1 = playerTransform.Find("WeaponParent");
        childObject = child1.Find("Weapon3").gameObject;
        childCollider = childObject.GetComponent<BoxCollider2D>();
        WeaponChildTransform = transform.Find("GunParent");
        gameOverUI.SetActive(false);
        playerhp = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        playerMoney = GameObject.Find("Canvas").GetComponent<UI_Manager>();

        initialPosition = ropeObject.transform.localPosition;
        ropeObject.SetActive(false);

    }

    void Update()
    {
       
        if (isdie)
        {
            Gun.SetActive(false);
            dieIcon.SetActive(true);
            Invoke("GameOverUI", 2);
            return;
        }

        if (isBeingPushed)
        {
            CheckDistanceToWall(Vector2.up); // ���� ����
            CheckDistanceToWall(Vector2.down); // �Ʒ��� ����
            CheckDistanceToWall(Vector2.left); // ���� ����
            CheckDistanceToWall(Vector2.right); // ������ ����
        }

        //����
        bool jumpButtonPressed = Input.GetButtonDown("Jump");
        if (jumpButtonPressed && !anim.GetBool("Jumping")&&!isUp&&!isDown)
        {
            if(hasSpring)
            {
                anim.ResetTrigger("ViewAbove");
                anim.ResetTrigger("LieDown");
                rigid.AddForce(Vector2.up * jumpPlus, ForceMode2D.Impulse);
                anim.SetBool("Jumping", true);
            }
            else
            {
                anim.ResetTrigger("ViewAbove");
                anim.ResetTrigger("LieDown");
                rigid.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
                anim.SetBool("Jumping", true);
            }
            
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!isLadder)
            {
                isDown = true;
                anim.SetTrigger("LieDown");
                //if(Input.GetKey(KeyCode.RightArrow))
                //{
                //    anim.SetTrigger("Crawl");
                //    float moveSpeed = 5f; // ������ �ӵ��� �����ϼ���.
                //    rigid.velocity = new Vector2(moveSpeed, rigid.velocity.y);
                //}
            }
        }

        if (Input.GetKeyDown(KeyCode.C) && !isMoving)
        {
            
            if (rope > 0)
            {
                rope -= 1;
                ropeSound.Play();
                hasRope = true;
                playerhp.UpdateRopeText(rope);
                StartCoroutine(MoveAndActivateSprite());
            }
            if (rope <= 0)
                hasRope = false;
                
        }

        if (Input.GetKeyDown(KeyCode.S) && !isDown)
        {
                
                if (bomb > 0)
                {
                    bomb--;
                    throwbomb = true;
                    playerhp.UpdateBombText(bomb);
                    bombSound.Play();
                    //Debug.Log(bomb);
                    anim.SetTrigger("Throw");
                }
                if (bomb <= 0)
                    throwbomb = false;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            if (!isLadder)
            {
                isDown = false;
                anim.SetTrigger("StandUp");
                if (Input.GetKeyDown(KeyCode.S))
                {
                    bomb--;
                    if (bomb >= 0)
                    {
                        throwbomb = true;
                        playerhp.UpdateBombText(bomb);

                        //Debug.Log("�Ʒ� ������ ��");
                    }
                    if (bomb <= 0)
                        throwbomb = false;
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!isLadder)
            {
                isUp = true;
                anim.SetTrigger("ViewAbove");
                //StartCoroutine(MoveCameraUp());
            }
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (!isLadder)
            {
                isUp = false;
                anim.SetTrigger("AboveCancle");
            }
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            if(onoffGun)
            {
                hasGun = !hasGun;
                if (hasGun)
                {
                    Gun.SetActive(false);
                    wearingGun = false;
                }
               
                else
                {
                    Gun.SetActive(true);
                    wearingGun = true;
                }

            }
        }
        if (spriteRenderer.flipX == true)
        {
            Vector3 childPosition = WeaponChildTransform.localPosition;
            childPosition.x = -Mathf.Abs(childPosition.x);
            WeaponChildTransform.localPosition = childPosition;

            Vector3 childScale = WeaponChildTransform.localScale;
            childScale.x = -Mathf.Abs(childScale.x);
            WeaponChildTransform.localScale = childScale;
        }
        else if (spriteRenderer.flipX == false)
        {
            Vector3 childPosition = WeaponChildTransform.localPosition;
            childPosition.x = Mathf.Abs(childPosition.x);
            WeaponChildTransform.localPosition = childPosition;

            Vector3 childScale = WeaponChildTransform.localScale;
            childScale.x = Mathf.Abs(childScale.x);
            WeaponChildTransform.localScale = childScale;
        }

        //����
        if (curTime<=0)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                if(wearingGun)
                {
                    curTime = coolTime;
                    gunSound.Play();
                    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                    GameObject bullet2 = Instantiate(bulletPrefab2, firePoint2.position, firePoint2.rotation);
                    GameObject bullet3 = Instantiate(bulletPrefab3, firePoint3.position, firePoint3.rotation);
                    if (spriteRenderer.flipX == false)
                    {
                        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                        rb.AddForce(firePoint.right * 20f, ForceMode2D.Impulse); // �Ѿ˿� ���� ���� �߻�
                        Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
                        rb2.AddForce(firePoint2.right * 18f, ForceMode2D.Impulse); // �Ѿ˿� ���� ���� �߻�
                        Rigidbody2D rb3 = bullet3.GetComponent<Rigidbody2D>();
                        rb3.AddForce(firePoint3.right * 17f, ForceMode2D.Impulse); // �Ѿ˿� ���� ���� �߻�
                        rigid.AddForce(Vector2.left * recoilForce, ForceMode2D.Impulse);
                    }

                    if (spriteRenderer.flipX == true)
                    {
                        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                        rb.AddForce(firePoint.right * -20f, ForceMode2D.Impulse); // �Ѿ˿� ���� ���� �߻�
                        Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
                        rb2.AddForce(firePoint2.right * -17f, ForceMode2D.Impulse); // �Ѿ˿� ���� ���� �߻�
                        Rigidbody2D rb3 = bullet3.GetComponent<Rigidbody2D>();
                        rb3.AddForce(firePoint3.right * -15f, ForceMode2D.Impulse); // �Ѿ˿� ���� ���� �߻�
                        rigid.AddForce(Vector2.right * recoilForce, ForceMode2D.Impulse);
                    }

                }
                else
                {
                    childCollider.enabled = true;
                    StartCoroutine(DeactivateCollider());

                    curTime = coolTime;
                    Vector3 childScale = childObjectTransform.localScale;
                    Vector3 childPosition = childObjectTransform2.localPosition;
                    float RightPositionX = 1.59f;
                    float LeftPositionX = -1.59f;
                    float RightScaleX = 1f;
                    float LeftScaleX = -1f;
                    float horizontalInput = Input.GetAxis("Horizontal");
                    attackSound.Play();
                    if (spriteRenderer.flipX == false)
                    {
                        childScale.x = RightScaleX;
                        childPosition.x = RightPositionX;
                        childObjectTransform.localScale = childScale;
                        childObjectTransform2.localPosition = childPosition;
                        //Debug.Log("������");
                        anim.SetTrigger("Attack");
                        StartCoroutine(DelayedAttack());
                    }
                    //else if (horizontalInput < 0)
                    else if (spriteRenderer.flipX == true)
                    {
                        //Debug.Log("����");
                        childScale.x = LeftScaleX;
                        childPosition.x = LeftPositionX;
                        childObjectTransform.localScale = childScale;
                        childObjectTransform2.localPosition = childPosition;
                        anim.SetTrigger("Attack");
                        StartCoroutine(DelayedAttack());
                    }
                }
                

            }
        }
        else
        {
            curTime -= Time.deltaTime;
        }
        

        //���߱�,����
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.3f, rigid.velocity.y);
        }

        //�¿� ����
        if (Input.GetButton("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

        //�ִϸ��̼�
        if (Mathf.Abs(rigid.velocity.x) < 0.2)
        {
            anim.SetBool("Walking", false);
        }
        else
        {
            if(!isLadder)
                anim.SetBool("Walking", true);
        }

        if(Input.GetKeyDown(KeyCode.G))
        {
            if (purchaseGun && money >= 10000) 
            {
                buySound.Play();
                money -= 10000;
                wearingGun = true;
                gunimage.SetActive(true);
                playerMoney.DecreaseMoney(money);
                Gun.SetActive(true);
                GunSet.SetActive(false);
            }
            if (purchaseJelly && money >= 3000)
            {
                buySound.Play();
                money -= 3000;
                //Debug.Log("������ ���!");
                playerMoney.DecreaseMoney(money);
                player_HP += 3;
                playerhp.UpdateHPText(player_HP);
            }
            if (purchaseChicken && money >= 1500) 
            {
                buySound.Play();
                money -= 1500;
                playerMoney.DecreaseMoney(money);
                player_HP += 1;
                playerhp.UpdateHPText(player_HP);

            }
            if (purchaseSpring && money >= 5000) 
            {
                buySound.Play();
                money -= 5000;
                playerMoney.DecreaseMoney(money);
                springImage.SetActive(true);
                hasSpring = true;
            }
            if (purchaseCompass && money > 3000) 
            {
                buySound.Play();
                money -= 3000;
                playerMoney.DecreaseMoney(money);
                compassImage.SetActive(true);
                arrow.SetActive(true);
            }
            if (purchaseGlove && money >= 5000) 
            {
                buySound.Play();
                money -= 5000;
                playerMoney.DecreaseMoney(money);
                gloveImage.SetActive(true);
                hasGlove = true;
            }
            if (purchaseBomb && money >= 2000) 
            {
                buySound.Play();
                money -= 2000;
                playerMoney.DecreaseMoney(money);
                plusBomb = true;
                bomb += 1;
                playerMoney.UpdateBombText(bomb);
                //Debug.Log("��ź��" + bomb);
            }
            plusBomb = false;
            if (purchaseRope && money >= 2000) 
            {
                buySound.Play();
                money -= 2000;
                playerMoney.DecreaseMoney(money);
                rope += 1;
                playerMoney.UpdateRopeText(rope);
            }
            if(purchaseBigbomb&&money>=5000)
            {
                buySound.Play();
                money -= 5000;
                playerMoney.DecreaseMoney(money);
                bomb += 3;
                playerMoney.UpdateBombText(bomb);
            }
        }


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isdie)
            return;
        //�̵��ӵ�
        float h = Input.GetAxisRaw("Horizontal");
        if (!isUp && !isDown)
        {
            rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
        }

        //�ְ� �ӵ�
        if (rigid.velocity.x > max_speed)
            rigid.velocity = new Vector2(max_speed, rigid.velocity.y);

        else if(rigid.velocity.x < max_speed*(-1))
            rigid.velocity = new Vector2(max_speed*(-1), rigid.velocity.y);

        //���� Ȯ��
        if(rigid.velocity.y<0)
        {
            Vector2 LeftPosition = rigid.position;
            Vector2 RightPosition = rigid.position;
            //Debug.DrawRay(LeftPosition, Vector3.down, new Color(1, 0, 0));
            //Debug.DrawRay(RightPosition, Vector3.down, new Color(1, 0, 0));
            LeftPosition.x = LeftPosition.x - 1;
            RightPosition.x = RightPosition.x + 1;
            RaycastHit2D LeftrayHit = Physics2D.Raycast(LeftPosition, Vector3.down, 1, LayerMask.GetMask("TileMap"));
            RaycastHit2D RightrayHit = Physics2D.Raycast(RightPosition, Vector3.down, 1, LayerMask.GetMask("TileMap"));
            if (LeftrayHit.collider != null||RightrayHit.collider!=null)
            {
                if (LeftrayHit.distance < 1.1f||RightrayHit.distance<1.1f)
                {
                    anim.SetBool("Jumping", false);
                } 

            }
        }

        if(isLadder)
        {
            float ver = Input.GetAxis("Vertical");
            rigid.gravityScale = 0;
            rigid.velocity = new Vector2(rigid.velocity.x, ver * max_speed);

            if (ver > 0)
            {
                anim.ResetTrigger("ViewAbove");
                anim.ResetTrigger("LieDown");
                anim.SetFloat("ladder", 1f); // �ִϸ��̼� ��� �ӵ� ����
            }
            else if (ver < 0)
            {
                anim.SetFloat("ladder", -1f);// �ִϸ��̼� �Ͻ� ����
            }
            else
            {
                anim.SetFloat("ladder", 0f);
            }
        }
        else
        {
            rigid.gravityScale = 4;
        }



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ladder"))
        {
            isLadder = true;
            //Debug.Log("��ٸ� ����");
            
            anim.SetBool("Walking", false);  
            anim.SetBool("Jumping", false);
            anim.SetBool("Laddering", true);

        }
        if(collision.CompareTag("Money"))
        {
            money += 1600;
            Destroy(collision.gameObject);
            playerMoney.IncreaseMoney(money);
            moneyEat.Play();
        }
        if(collision.CompareTag("Ghost"))
        {
            isdie = true;
            ghostkillSound.Play();
            player_HP -= 4;
            playerhp.UpdateHPText(player_HP);
            Vector3 newScale = new Vector3(1.5f, 1.5f, 1.0f); // X �� Y ������ �� ����
            spriteRenderer.transform.localScale = newScale;
            anim.SetTrigger("Evaporation");
        }
        if(collision.CompareTag("Lava"))
        {
            isdie = true;
            foreach (GameObject effect in effects)
            {
                effect.SetActive(true);
            }
            player_HP -= 4;
            playerhp.UpdateHPText(player_HP);
            spriteRenderer.enabled = false;
        }
        if (collision.CompareTag("Shop_Gun"))
        {
            purchaseGun = true;
            onoffGun = true;
        }
        if(collision.CompareTag("Shop_Jelly"))
        {
            purchaseJelly = true;
        }
        if (collision.CompareTag("Shop_Chicken"))
        {
            purchaseChicken = true;
        }
        if(collision.CompareTag("Shop_Spring"))
        {
            purchaseSpring = true;
        }
        if (collision.CompareTag("Shop_Compass"))
        {
            purchaseCompass = true;
        }
        if(collision.CompareTag("Shop_Glove"))
        {
            purchaseGlove = true;
        }
        if (collision.CompareTag("Shop_Bomb"))
        {
            purchaseBomb = true;
        }
        if (collision.CompareTag("Shop_Rope"))
        {
            purchaseRope = true;
        }
        if (collision.CompareTag("Shop_BigBomb"))
        {
            purchaseBigbomb = true;
        }
        if(collision.CompareTag("EndingKey"))
        {
            hasKey = true;
            KeyImage.SetActive(true);
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            //Debug.Log("��ٸ� ������");
            anim.SetBool("Laddering", false);

        }
        if (collision.CompareTag("Shop_Gun"))
        {
            purchaseGun = false;
        }
        if (collision.CompareTag("Shop_Jelly"))
        {
            purchaseJelly = false;
        }
        if (collision.CompareTag("Shop_Chicken"))
        {
            purchaseChicken = false;
        }
        if (collision.CompareTag("Shop_Spring"))
        {
            purchaseSpring = false;
        }
        if (collision.CompareTag("Shop_Compass"))
        {
            purchaseCompass = false;
        }
        if (collision.CompareTag("Shop_Glove"))
        {
            purchaseGlove = false;
        }
        if (collision.CompareTag("Shop_Bomb"))
        {
            purchaseBomb = false;
        }
        if (collision.CompareTag("Shop_Rope"))
        {
            purchaseRope = false;
        }
        if (collision.CompareTag("Shop_BigBomb"))
        {
            purchaseBigbomb = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Enemy"||collision.gameObject.tag=="Trap")
        {
            //���� �Ӹ� ���
            if (rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y) 
            {
                OnAttack(collision.transform);
            }
            else
            {
                OnDamaged(collision.transform.position);
                if (player_HP<=0)
                {
                    anim.SetTrigger("Die");
                    PlayerDied(collision.transform.position);
                }
            }
        }

        if(collision.gameObject.tag=="DeathTrap")
        {
            anim.SetTrigger("Thorn");
            player_HP -= 4;
            playerhp.UpdateHPText(player_HP);
            gasiSound.Play();
            isdie = true;
        }

        if(collision.gameObject.tag=="ShopKeeper")
        {
           // Debug.Log("����");
            OnDamaged(collision.transform.position);
            if (player_HP <= 0)
            {
                anim.SetTrigger("Die");
                PlayerDied(collision.transform.position);
            }

            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
            rigid.AddForce(new Vector2(knockbackDirection.x * knockbackForce, knockbackUpwardForce), ForceMode2D.Impulse);
        }

        if(collision.gameObject.CompareTag("Umdol"))
        {

            isBeingPushed = true;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Umdol"))
        {
            isBeingPushed = false;
        }
    }

    void CheckDistanceToWall(Vector2 direction)
    {
        Vector2 playerPosition = transform.position;

        RaycastHit2D hit = Physics2D.Raycast(playerPosition, direction, deathDistance, LayerMask.GetMask("Wall"));
        RaycastHit2D hit2 = Physics2D.Raycast(playerPosition, direction, deathDistance, LayerMask.GetMask("TileMap"));

        if (hit.collider != null||hit2.collider!=null)
        {
            float distanceToWall = Vector2.Distance(playerPosition, hit.point);
            float distanceToWall2 = Vector2.Distance(playerPosition, hit2.point);
            if (distanceToWall <= deathDistance|| distanceToWall2 <= deathDistance)
            {
                isdie = true;
                foreach (GameObject bones in bones)
                {
                    bones.SetActive(true);
                    dieSound.Play();
                }
                player_HP -= 4;
                playerhp.UpdateHPText(player_HP);
                spriteRenderer.enabled = false;
            }
        }
    }

    public void Player_AlphaValue()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1f);
    }
    public void Entrance()
    {
        anim.SetTrigger("Entrance");
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float duration = 0.7f; // �ִϸ��̼� ���� �ð� (���� ����)
        Color startColor = spriteRenderer.color;

        float startTime = Time.time;
        while (Time.time < startTime + duration)
        {
            float elapsed = (Time.time - startTime) / duration;
            Color newColor = startColor;
            newColor.a = Mathf.Lerp(1.0f, 0.0f, elapsed); // ���� ���� ������ ���ҽ�Ŵ
            spriteRenderer.color = newColor;
            yield return null;
        }

        // �ִϸ��̼� �Ϸ� �Ŀ� �÷��̾� ������Ʈ�� ��Ȱ��ȭ�� ���� �ֽ��ϴ�.
        // gameObject.SetActive(false);
    }

    void PlayerDied(Vector2 targetPos)
    {
        Gun.SetActive(false);
        isdie = true;
        int dir = transform.position.x - targetPos.x > 0 ? 1 : -1;
        if (dir == 1)
        {
            spriteRenderer.flipX = true;
            rigid.AddForce(new Vector2(dir, 1) * 3, ForceMode2D.Impulse);
            Invoke("OffDamaged", 1);
        }
        else
        {
            
            rigid.AddForce(new Vector2(dir, 1) * 3, ForceMode2D.Impulse);
            Invoke("OffDamaged", 1);
        }    
    }

    void OnAttack(Transform enemy)
    {
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

        Cave_Move caveMove = enemy.GetComponent<Cave_Move>();
        //caveMove.OnDamaged();
    }
    void OnDamaged(Vector2 targetPos) //�ǰ� �����ð�
    {
        damageSound.Play();
        gameObject.layer = 8;
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        int dir = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dir, 1) * 6, ForceMode2D.Impulse);

        player_HP--;
        playerhp.UpdateHPText(player_HP);

        anim.SetTrigger("Damaged");
        Invoke("OffDamaged", 1);
        
    }

    void OffDamaged()
    {
        gameObject.layer = 6;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    IEnumerator DelayedAttack()
    {
        ATK1.SetTrigger("ATK1");

        yield return new WaitForSeconds(0.1f); // ATK1 �� 1�� ������
        ATK2.SetTrigger("ATK2");

        yield return new WaitForSeconds(0.1f); // ATK2 �� 1�� ������
        ATK3.SetTrigger("ATK3");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }

    IEnumerator DeactivateCollider()
    {
        // ������ �ð� ���� ����� �� �ݶ��̴��� ��Ȱ��ȭ
        yield return new WaitForSeconds(colliderActivationDuration);

        if (childCollider != null)
        {
            childCollider.enabled = false;
        }
    }

    void GameOverUI()
    {
        gameOverUI.SetActive(true);
    }

    //IEnumerator MoveCameraUp()
    //{
    //    if (!isCameraMoving)
    //    {
    //        Debug.Log("ī�޶� ������ �ڷ�ƾ ����1");
    //        isCameraMoving = true;
    //        initialCameraPosition = mainCamera.transform.position;
    //        float elapsedTime = 0;

    //        while (elapsedTime < cameraMoveDuration)
    //        {
    //            Debug.Log("ī�޶� ������ �ڷ�ƾ ����2");
    //            float newYPosition = initialCameraPosition.y + (cameraMoveSpeed * Time.deltaTime);
    //            mainCamera.transform.position = new Vector3(initialCameraPosition.x, newYPosition, initialCameraPosition.z);

    //            elapsedTime += Time.deltaTime;
    //            yield return null;
    //        }

    //        isCameraMoving = false;
    //    }
    //}

    IEnumerator MoveAndActivateSprite()
    {
        isMoving = true;
        ropeObject.SetActive(true);

        float elapsedTime = 0f;
        Vector3 targetPosition = initialPosition + new Vector3(0, moveDistance, 0);

        while (elapsedTime < moveDuration)
        {
            ropeObject.transform.localPosition = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        ropeObject.transform.localPosition = targetPosition;

        ropeObject.SetActive(false);
        ropeObject.transform.localPosition = initialPosition;
        isMoving = false;
    }
}
