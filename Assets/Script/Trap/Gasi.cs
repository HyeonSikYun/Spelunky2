using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gasi : MonoBehaviour
{
    public GameObject gasi;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gasi.SetActive(true);
        }


    }
}
