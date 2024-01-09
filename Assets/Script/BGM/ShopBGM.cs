using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBGM : MonoBehaviour
{
    public AudioSource shopBgm;
    private VolcanoBGM volcanoBgmScript;

    private void Start()
    {
        GameObject volcanoObject = GameObject.Find("Volcano_Start");
        if (volcanoObject != null)
        {
            // ã�� GameObject���� VolcanoBGM ��ũ��Ʈ�� ������
            volcanoBgmScript = volcanoObject.GetComponent<VolcanoBGM>();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            volcanoBgmScript.Mute();
            shopBgm.Play();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            volcanoBgmScript.noMute();
            shopBgm.Stop();
        }
    }
    public void Mute()
    {
        shopBgm.volume = 0.0f;
    }

    public void noMute()
    {
        shopBgm.volume = 1f;
    }
}
