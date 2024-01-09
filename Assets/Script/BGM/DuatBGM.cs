using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuatBGM : MonoBehaviour
{
    public AudioSource duatBgm;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            duatBgm.Play();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            duatBgm.Stop();
        }
    }
}
