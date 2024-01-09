using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BaseCamp : MonoBehaviour
{
    bool OutBaseCamp = false;
    public Image Panel;
    private Move entrance;

    float time = 0f;
    float F_time = 1f;

    public AudioSource doorSound;

    // Start is called before the first frame update
    void Start()
    {
        entrance = GameObject.Find("Player").GetComponent<Move>();
    }

    // Update is called once per frame
    void Update()
    {
        if (OutBaseCamp && Input.GetKeyDown(KeyCode.F))
        {
            doorSound.Play();
            entrance.Entrance();
            StartCoroutine(FadeOut());
            StartCoroutine(Wait());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OutBaseCamp = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OutBaseCamp = false;
        }
    }

    IEnumerator FadeOut()
    {

        Panel.gameObject.SetActive(true);
        time = 0f;
        Color alpha = Panel.color;

        // È­¸é ¾îµÎ¿öÁü
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

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        LoadRandomScene();
    }

    void LoadRandomScene()
    {
        // ¾À ÀÌ¸§ ¹è¿­
        string[] scenes = { "Stage1", "Stage2", "Stage3" };

        // ·£´ýÇÑ ¾À ¼±ÅÃ
        int randomIndex = Random.Range(0, scenes.Length);
        string randomScene = scenes[randomIndex];

        // ¼±ÅÃµÈ ·£´ý ¾ÀÀ¸·Î ÀÌµ¿
        SceneManager.LoadScene(randomScene);
    }
}
