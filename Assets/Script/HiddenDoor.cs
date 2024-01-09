using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenDoor : MonoBehaviour
{
    private Move moveScript;
    public GameObject closedoor;
    public GameObject opendoor;
    bool Ready = false;
    // Start is called before the first frame update
    void Start()
    {
        moveScript = GameObject.FindObjectOfType<Move>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Ready)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (moveScript.hasKey)
                {
                    closedoor.SetActive(false);
                    opendoor.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Ready = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Ready = false;
        }
    }
}
