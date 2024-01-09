using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Blood : MonoBehaviour
{
    public GameObject bat;
    Rigidbody2D rigid;
    public int randomDir = 0;
    public float scaleSpeed = 15f;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        randomDir = Random.Range(0, 2);
        if (randomDir == 1)
            rigid.velocity = new Vector2(-transform.localScale.x * 9f, 9);
        else
            rigid.velocity = new Vector2(transform.localScale.x * 9f, 10);
    }
    private void Update()
    {
        Vector3 newScale = transform.localScale - new Vector3(scaleSpeed, scaleSpeed, 0f) * Time.deltaTime;
        transform.localScale = newScale;
        Invoke("Delete", 2f);
    }

    void Delete()
    {
        //gameObject.SetActive(false);
        bat.SetActive(false);
    }
}
