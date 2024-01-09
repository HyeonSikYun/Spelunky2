using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    Vector3 MousePosition;
    public LayerMask whatisPlatform;
    public GameObject boomClone;
    public GameObject player;
    public GameObject ropeClone;
    public GameObject gloveImg;

    public float bomb = 4f;
    public float rope = 4f;
    private UI_Manager bombcount;
    private UI_Manager ropecount;

    private Move moveScript;

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawSphere(MousePosition, 0.2f);
    }

    private void Start()
    {
        moveScript = GameObject.FindObjectOfType<Move>();
        bombcount = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        ropecount = GameObject.Find("Canvas").GetComponent<UI_Manager>();
    }
    // Update is called once per frame
    void Update()
    {
        //else if (Input.GetMouseButtonDown(1))
        //{
        //    MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    Vector2 mousepoint = Input.mousePosition;
        //    mousepoint = Camera.main.ScreenToWorldPoint(mousepoint);
        //    Instantiate(boomClone, mousepoint, Quaternion.identity);
        //}
        

        if (Input.GetKeyDown(KeyCode.S)) // S 키를 누르면 폭탄 생성
        {

            //if(moveScript.plusBomb)
            //{
            //    bomb++;
            //    bombcount.UpdateBombText(bomb);
            //}

            if (moveScript.throwbomb)
            {
                Vector3 playerPosition = player.transform.position;
                Vector3 bombPosition;

                if (Input.GetKey(KeyCode.DownArrow)) // 아래키가 눌린 경우
                {
                    if (player.GetComponent<SpriteRenderer>().flipX)
                    {
                        Vector3 leftPlayerPosition = playerPosition + new Vector3(-1.0f, 0.0f, 0.0f); // 원하는 위치값으로 수정
                        Instantiate(boomClone, leftPlayerPosition, Quaternion.identity);
                    }
                    else
                    {
                        Vector3 rightPlayerPosition = playerPosition + new Vector3(1.0f, 0.0f, 0.0f); // 원하는 위치값으로 수정
                        Instantiate(boomClone, rightPlayerPosition, Quaternion.identity);
                    }

                }
                else
                {
                    bombPosition = playerPosition + new Vector3(0, 0.5f, 0); // 폭탄의 초기 위치 설정
                    GameObject bomb = Instantiate(boomClone, bombPosition, Quaternion.identity);
                    Rigidbody2D bombRigidbody = bomb.GetComponent<Rigidbody2D>();
                    if(moveScript.hasGlove)
                    {
                        Debug.Log("글러브 먹어서 직선");
                        float bombSpeed = 30f;
                        Vector2 bombDirection = player.GetComponent < SpriteRenderer>().flipX ? Vector2.left : Vector2.right;
                        bombRigidbody.velocity = bombDirection * bombSpeed;
                        bombRigidbody.gravityScale = 0f;
                    }
                    else
                    {
                        Debug.Log("포물선");
                        // 폭탄을 앞으로 포물선을 그리며 날리기
                        float bombSpeed = 80f; // 폭탄의 초기 속도
                        float bombAngle = 30f; // 폭탄의 발사 각도

                        // 플레이어의 flipX 값에 따라 폭탄의 방향 설정
                        Vector2 bombForce;
                        if (player.GetComponent<SpriteRenderer>().flipX)
                        {
                            // 플레이어가 왼쪽을 바라보는 경우
                            bombForce = new Vector2(-Mathf.Cos(bombAngle * Mathf.Deg2Rad) * bombSpeed, Mathf.Sin(bombAngle * Mathf.Deg2Rad) * bombSpeed);
                        }
                        else
                        {
                            // 플레이어가 오른쪽을 바라보는 경우
                            bombForce = new Vector2(Mathf.Cos(bombAngle * Mathf.Deg2Rad) * bombSpeed, Mathf.Sin(bombAngle * Mathf.Deg2Rad) * bombSpeed);
                        }

                        bombRigidbody.AddForce(bombForce, ForceMode2D.Impulse);
                    }
                    
                }
            }  
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            //rope -= 1;
            //ropecount.UpdateRopeText(rope);
            if (moveScript.hasRope)
            {
                //CreateRope();
                Invoke("CreateRope", 0.6f);
            }
        }
    }

    void CreateRope()
    {
        // 플레이어의 현재 위치 가져오기
        Vector3 playerPosition = player.transform.position;

        // 로프를 생성할 높이 설정 (로프를 머리 위로 올리려면 해당 값을 조절)
        float ropeHeight = 3.0f; // 예시로 1.0f로 설정

        // 로프를 플레이어 머리 위에 생성
        Vector3 ropeSpawnPosition = new Vector3(playerPosition.x-0.3f, playerPosition.y + ropeHeight, playerPosition.z);

        // 로프 프리팹을 인스턴스화하여 생성
        Instantiate(ropeClone, ropeSpawnPosition, Quaternion.identity);
    }
}
