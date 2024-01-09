using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleBGM : MonoBehaviour
{
    public AudioSource templeBgm;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            templeBgm.Play();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            templeBgm.Stop();
        }
    }
}
