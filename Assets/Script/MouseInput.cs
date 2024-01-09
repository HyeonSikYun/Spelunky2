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
        

        if (Input.GetKeyDown(KeyCode.S)) // S Ű�� ������ ��ź ����
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

                if (Input.GetKey(KeyCode.DownArrow)) // �Ʒ�Ű�� ���� ���
                {
                    if (player.GetComponent<SpriteRenderer>().flipX)
                    {
                        Vector3 leftPlayerPosition = playerPosition + new Vector3(-1.0f, 0.0f, 0.0f); // ���ϴ� ��ġ������ ����
                        Instantiate(boomClone, leftPlayerPosition, Quaternion.identity);
                    }
                    else
                    {
                        Vector3 rightPlayerPosition = playerPosition + new Vector3(1.0f, 0.0f, 0.0f); // ���ϴ� ��ġ������ ����
                        Instantiate(boomClone, rightPlayerPosition, Quaternion.identity);
                    }

                }
                else
                {
                    bombPosition = playerPosition + new Vector3(0, 0.5f, 0); // ��ź�� �ʱ� ��ġ ����
                    GameObject bomb = Instantiate(boomClone, bombPosition, Quaternion.identity);
                    Rigidbody2D bombRigidbody = bomb.GetComponent<Rigidbody2D>();
                    if(moveScript.hasGlove)
                    {
                        Debug.Log("�۷��� �Ծ ����");
                        float bombSpeed = 30f;
                        Vector2 bombDirection = player.GetComponent < SpriteRenderer>().flipX ? Vector2.left : Vector2.right;
                        bombRigidbody.velocity = bombDirection * bombSpeed;
                        bombRigidbody.gravityScale = 0f;
                    }
                    else
                    {
                        Debug.Log("������");
                        // ��ź�� ������ �������� �׸��� ������
                        float bombSpeed = 80f; // ��ź�� �ʱ� �ӵ�
                        float bombAngle = 30f; // ��ź�� �߻� ����

                        // �÷��̾��� flipX ���� ���� ��ź�� ���� ����
                        Vector2 bombForce;
                        if (player.GetComponent<SpriteRenderer>().flipX)
                        {
                            // �÷��̾ ������ �ٶ󺸴� ���
                            bombForce = new Vector2(-Mathf.Cos(bombAngle * Mathf.Deg2Rad) * bombSpeed, Mathf.Sin(bombAngle * Mathf.Deg2Rad) * bombSpeed);
                        }
                        else
                        {
                            // �÷��̾ �������� �ٶ󺸴� ���
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
        // �÷��̾��� ���� ��ġ ��������
        Vector3 playerPosition = player.transform.position;

        // ������ ������ ���� ���� (������ �Ӹ� ���� �ø����� �ش� ���� ����)
        float ropeHeight = 3.0f; // ���÷� 1.0f�� ����

        // ������ �÷��̾� �Ӹ� ���� ����
        Vector3 ropeSpawnPosition = new Vector3(playerPosition.x-0.3f, playerPosition.y + ropeHeight, playerPosition.z);

        // ���� �������� �ν��Ͻ�ȭ�Ͽ� ����
        Instantiate(ropeClone, ropeSpawnPosition, Quaternion.identity);
    }
}
