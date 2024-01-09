using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionAreaGO;
    public LayerMask whatisPlatform;
    public CircleCollider2D circleCollider2D;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        explosionAreaGO.SetActive(false);
        StartCoroutine(Boom());
    }

    IEnumerator Boom()
    {
        yield return new WaitForSeconds(1.7f);
        this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        anim.SetBool("Explosion", true);
        explosionAreaGO.SetActive(true);
        DestroyArea();
        yield return new WaitForSeconds(0.3f);
        Destroy(this.gameObject, 0.05f);
    }    

    void DestroyArea()
    {
        int radiusInt = Mathf.RoundToInt(circleCollider2D.radius);
        for (int i= -radiusInt; i<=radiusInt; i++)
        {
            for(int j= -radiusInt; j<=radiusInt; j++)
            {
                Vector3 checkCellPos = new Vector3(transform.position.x + i, transform.position.y + j, 0);
                float distance = Vector2.Distance(transform.position, checkCellPos) - 0.001f;

                if(distance<=radiusInt)
                {
                    Collider2D overCollider2d = Physics2D.OverlapCircle(checkCellPos, 0.01f, whatisPlatform);
                    if (overCollider2d != null)
                    {
                        overCollider2d.transform.GetComponent<Bricks>().MakeDot(checkCellPos);
                    }

                    //Collider2D[] overCollider2d = Physics2D.OverlapCircleAll(checkCellPos, 0.01f, whatisPlatform);
                    //if(overCollider2d.Length>0)
                    //{
                    //    foreach(Collider2D overColl in overCollider2d)
                    //    {
                    //        overColl.GetComponent<Bricks>().MakeDot(checkCellPos);
                    //    }
                    //}
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

}
