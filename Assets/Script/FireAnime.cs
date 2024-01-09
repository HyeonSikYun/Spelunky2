using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAnime : MonoBehaviour
{
    private float curTime;
    public float coolTime = 0.08f;
    Animator anim;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (curTime <= 0)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                curTime = coolTime;
                anim.SetTrigger("Shot");
                Debug.Log("รั ฝ๔");
            }
        }
        else
        {
            curTime -= Time.deltaTime;
        }
    }
}
