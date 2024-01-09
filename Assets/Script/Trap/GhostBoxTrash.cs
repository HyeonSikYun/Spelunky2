using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBoxTrash : MonoBehaviour
{
    Rigidbody2D rigid;
    public int randomDir = 0;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        randomDir = Random.Range(0, 2);
        if(randomDir==1)
            rigid.velocity = new Vector2(-transform.localScale.x * 5f, 7);
        else
            rigid.velocity = new Vector2(transform.localScale.x * 5f, 7);
    }
    private void Update()
    {
        Invoke("Delete", 2f);
    }

    void Delete()
    {
        gameObject.SetActive(false);
    }
}
