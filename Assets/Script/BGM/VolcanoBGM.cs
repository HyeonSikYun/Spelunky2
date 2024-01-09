using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoBGM : MonoBehaviour
{
    public AudioSource volcanoBgm;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            volcanoBgm.Play();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            volcanoBgm.Stop();
        }
    }

    public void Mute()
    {
        volcanoBgm.volume = 0.0f;
    }

    public void noMute()
    {
        volcanoBgm.volume = 1f;
    }
}
