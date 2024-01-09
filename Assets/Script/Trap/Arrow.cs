using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D rigid;
    Transform arrowTransform;
    public GameObject arrow;
    float arrowspeed = 30f;
    public AudioSource arrowSound;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        arrowTransform = arrow.transform;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitTarget = Physics2D.Raycast(arrow.transform.position, Vector2.right, 6f, LayerMask.GetMask("Player"));
        if(hitTarget.collider!=null)
        {
            arrowSound.Play();
            //Debug.Log("플레이어한테 쏠게");
            //Debug.DrawRay(arrow.transform.position, Vector2.right * 6f, new Color(1, 0, 0));
            arrowTransform.localScale = new Vector3(1f, 1f, 1f);
            rigid.velocity = new Vector2(1f * arrowspeed, rigid.velocity.y);
            Invoke("End_arrow", 2f);
            
        }
    }

    void End_arrow()
    {
        gameObject.SetActive(false);
    }
}
