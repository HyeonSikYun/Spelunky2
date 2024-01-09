using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Osiris : MonoBehaviour
{
    public GameObject rightHandPrefab; // 오른쪽 손 프리팹을 설정합니다.
    public GameObject leftHandPrefab; // 왼쪽 손 프리팹을 설정합니다.
    public float creationInterval = 2.0f; // 생성 간격을 설정합니다.
    private float nextCreationTime = 0f;
    public float handYOffset = -2.0f;

    public float moveSpeed = 1f;
    private bool isMovingRight = true;
    private float leftBound = 286f;
    private float rightBound = 322f;

    private osirisHand osirisScript;

    private GameObject rightHandInstance; // 오른쪽 손 인스턴스
    private GameObject leftHandInstance; // 왼쪽 손 인스턴스

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
        // 처음에 손 오브젝트를 생성합니다.
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
            // 이미 생성된 손이 없는 경우에만 새로 생성합니다.
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

            // 다음 생성 시간을 갱신합니다.
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
