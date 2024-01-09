using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeInFadeOut : MonoBehaviour
{
    public Image Panel;
    float time = 0f;
    float F_time = 1f;

    private void Start()
    {
        StartCoroutine(FadeOut());
    }
    public void FadeAndRestart()
    {
        StartCoroutine(FadeInOut());
    }

    IEnumerator FadeInOut()
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

        // 씬 로드
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Stage1");
        asyncLoad.allowSceneActivation = false;

        // 로딩이 90% 이상 완료될 때까지 기다림
        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }
        asyncLoad.allowSceneActivation = true;
        
    }

    IEnumerator FadeOut()
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
