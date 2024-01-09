using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bones : MonoBehaviour
{
    Rigidbody2D rigid;
    public int randomDir = 0;

    public Vector3 startScale = new Vector3(0.1f, 0.1f, 0.1f);
    public Vector3 targetScale = new Vector3(1.0f, 1.0f, 1.0f);
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        randomDir = Random.Range(0, 4);
        if (randomDir == 1)
            rigid.velocity = new Vector2(-transform.localScale.x * 5f, 9);
        if (randomDir == 2)
            rigid.velocity = new Vector2(transform.localScale.x * 5f, 9);
        if (randomDir == 3)
            rigid.velocity = new Vector2(transform.localScale.y * 5f, 9);
        if (randomDir == 4)
            rigid.velocity = new Vector2(transform.localScale.y * 5f, 9);
    }
    private void Update()
    {
        Invoke("Delete", 0.5f);
    }

    void Delete()
    {
        gameObject.SetActive(false);
    }
}
