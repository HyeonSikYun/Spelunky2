using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeMusic : MonoBehaviour
{
    public AudioSource volcanoBgm;
    private ShopKeeper shopKeeperScript;

    private void Start()
    {
        GameObject shopObject = GameObject.Find("ShopKeeper");
        if (shopObject != null)
        {
            // ã�� GameObject���� VolcanoBGM ��ũ��Ʈ�� ������
            shopKeeperScript = shopObject.GetComponent<ShopKeeper>();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            volcanoBgm.Play();
            shopKeeperScript.Mute();
        }
    }

    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            volcanoBgm.Stop();
        }
    }
}
