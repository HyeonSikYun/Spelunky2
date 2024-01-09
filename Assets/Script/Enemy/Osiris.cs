using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Osiris : MonoBehaviour
{
    public GameObject rightHandPrefab; // ������ �� �������� �����մϴ�.
    public GameObject leftHandPrefab; // ���� �� �������� �����մϴ�.
    public float creationInterval = 2.0f; // ���� ������ �����մϴ�.
    private float nextCreationTime = 0f;
    public float handYOffset = -2.0f;

    public float moveSpeed = 1f;
    private bool isMovingRight = true;
    private float leftBound = 286f;
    private float rightBound = 322f;

    private osirisHand osirisScript;

    private GameObject rightHandInstance; // ������ �� �ν��Ͻ�
    private GameObject leftHandInstance; // ���� �� �ν��Ͻ�

    SpriteRenderer spriteRenderer;
    public GameObject[] effects;
    BoxCollider2D boxCollider;
    float hp = 10f;
    public bool isdie = false;

    public AudioSource osirisDieSound;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        // ó���� �� ������Ʈ�� �����մϴ�.
        rightHandInstance = Instantiate(rightHandPrefab, transform.position + Vector3.right * 2f + new Vector3(0f, handYOffset, 0f), Quaternion.identity);
        leftHandInstance = Instantiate(leftHandPrefab, transform.position + Vector3.left * 2f + new Vector3(0f, handYOffset, 0f), Quaternion.identity);
        rightHandInstance.transform.parent = transform;
        leftHandInstance.transform.parent = transform;
    }

    void Update()
    {
        float distanceToTarget = isMovingRight ? rightBound - transform.position.x : transform.position.x - leftBound;

        if (distanceToTarget < 0.1f)
        {
            isMovingRight = !isMovingRight;
        }

        Vector3 moveDirection = isMovingRight ? Vector3.right : Vector3.left;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        if (Time.time >= nextCreationTime)
        {
            // �̹� ������ ���� ���� ��쿡�� ���� �����մϴ�.
            if (rightHandInstance == null)
            {
                rightHandInstance = Instantiate(rightHandPrefab, transform.position + Vector3.right * 2f + new Vector3(0f, handYOffset, 0f), Quaternion.identity);
                rightHandInstance.transform.parent = transform;
            }

            if (leftHandInstance == null)
            {
                leftHandInstance = Instantiate(leftHandPrefab, transform.position + Vector3.left * 2f + new Vector3(0f, handYOffset, 0f), Quaternion.identity);
                leftHandInstance.transform.parent = transform;
            }

            // ���� ���� �ð��� �����մϴ�.
            nextCreationTime = Time.time + creationInterval;
        }
        if (isdie)
        {
            Destroy(rightHandInstance);
            Destroy(leftHandInstance);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("PlayerWeapon") || other.gameObject.tag == "Trap" || other.gameObject.tag == "Bullet")
        {
            OnDamaged(other.transform.position);

            if (hp == 0)
            {
                isdie = true;
                osirisDieSound.Play();
                foreach (GameObject effect in effects)
                {
                    effect.SetActive(true);
                }
                spriteRenderer.enabled = false;
                boxCollider.enabled = false;
            }

        }
    }

    void OnDamaged(Vector2 targetPos) //�ǰ� �����ð�
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
