using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveBGM : MonoBehaviour
{
    public AudioSource caveBgm;
    private VolcanoBGM volcano;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            caveBgm.Play();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            caveBgm.Stop();
        }
    }
}
