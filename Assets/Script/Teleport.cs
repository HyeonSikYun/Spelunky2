using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{
    public  Ghostbox    ghostScript;
    public  Image       Panel;
    public  GameObject  targetObj;
    public  GameObject  ToObj;

    private Move        entrance;
    private Move        Player_Color;

    bool    nextgo = false;

    float   time = 0f;
    float   F_time = 1f;
    public AudioSource entranceSound;

    public int currentLevel;
    private SoundManager soundManager;
    private void Awake()
    {
        currentLevel = 1;
        entrance = GameObject.Find("Player").GetComponent<Move>();
        Player_Color = GameObject.Find("Player").GetComponent<Move>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    private void Update()
    {
        
        if (nextgo&&Input.GetKeyDown(KeyCode.F))
        {
            currentLevel += 1;
            entranceSound.Play();
            entrance.Entrance();
            StartCoroutine(FadeOut());
            StartCoroutine(NextStage());
            ghostScript.DisableGhost();
            
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            nextgo = true;
            targetObj = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            nextgo = false;
        }
    }
    
    IEnumerator NextStage()
    {
        //yield return null;
        
        yield return new WaitForSeconds(2.5f);
        soundManager.SetCurrentLevel(currentLevel);
        targetObj.transform.position = ToObj.transform.position;
        Player_Color.Player_AlphaValue();
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeOut()
    {
        
        Panel.gameObject.SetActive(true);
        time = 0f;
        Color alpha = Panel.color;

        // 화면 어두워짐
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            Panel.color = alpha;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
    }

    IEnumerator FadeIn()
    {
        Panel.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        time = 0f;
        Color alpha = Panel.color;

        while (alpha.a > 0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            Panel.color = alpha;
            yield return null;
        }

        Panel.gameObject.SetActive(false);
    }
}
