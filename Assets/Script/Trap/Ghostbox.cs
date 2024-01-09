using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ghostbox : MonoBehaviour
{
    public GameObject GhostBox;
    public GameObject rb1;
    public GameObject rb2;
    public GameObject rb3;
    public GameObject rb4;
    public GameObject ghostimage;
    public GameObject ghost;
    public AudioSource bottle_Crash;

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerWeapon")||collision.CompareTag("Trap")|| collision.CompareTag("Bomb")|| collision.CompareTag("Bullet"))
        {
            GhostBox.SetActive(false);
            rb1.SetActive(true);
            rb2.SetActive(true);
            rb3.SetActive(true);
            rb4.SetActive(true);
            bottle_Crash.Play();
            Invoke("EnableGhostImage", 0.5f);
            Invoke("EnableGhost", 1f);
            Invoke("DisableGhostImage", 3f);
        }
    }

    private void EnableGhostImage()
    {
        ghostimage.SetActive(true);
    }

    private void DisableGhostImage()
    {
        ghostimage.SetActive(false);
    }

    private void EnableGhost()
    {
        ghost.SetActive(true);
    }

    public void DisableGhost()
    {
        ghost.SetActive(false);
    }
}
