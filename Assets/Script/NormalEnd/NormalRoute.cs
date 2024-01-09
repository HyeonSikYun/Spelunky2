using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NormalRoute : MonoBehaviour
{
    bool normalendgo = false;
    public Image Panel;
    private Move entrance;

    float time = 0f;
    float F_time = 1f;

    public AudioSource entranceSound;

    // Start is called before the first frame update
    void Start()
    {
        entrance = GameObject.Find("Player").GetComponent<Move>();
    }

    // Update is called once per frame
    void Update()
    {
        if(normalendgo&&Input.GetKeyDown(KeyCode.F))
        {
            entranceSound.Play();
            entrance.Entrance();
            StartCoroutine(FadeOut());
            StartCoroutine(Wait());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            normalendgo = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            normalendgo = false;
        }
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

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("NormalEnd");
    }
}
