using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NormalEnding : MonoBehaviour
{
    public float scaleChangeSpeed = 0.5f; // 스케일 변경 속도
    public float minScale = 0.5f; // 최소 스케일
    public float maxScale = 2.0f; // 최대 스케일

    private Vector3 currentScale; // 현재 스케일
    private bool isScalingUp = true; // 스케일이 증가 중인지 여부

    float time = 0f;
    float F_time = 1f;
    public Image Panel;

    void Start()
    {
        currentScale = transform.localScale;
        StartCoroutine(FadeIn());
        StartCoroutine(StartSceneTransition());
    }

    void Update()
    {
        if (isScalingUp)
        {
            currentScale += Vector3.one * scaleChangeSpeed * Time.deltaTime;
        }
        else
        {
            currentScale -= Vector3.one * scaleChangeSpeed * Time.deltaTime;
        }

        // 스케일 범위를 제한
        currentScale = new Vector3(
            Mathf.Clamp(currentScale.x, minScale, maxScale),
            Mathf.Clamp(currentScale.y, minScale, maxScale),
            Mathf.Clamp(currentScale.z, minScale, maxScale)
        );

        transform.localScale = currentScale;

        // 최대 스케일 또는 최소 스케일에 도달하면 반전
        if (currentScale.x >= maxScale || currentScale.x <= minScale)
        {
            isScalingUp = !isScalingUp;
        }
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

    IEnumerator StartSceneTransition()
    {
        yield return new WaitForSeconds(15.0f); // 30초를 기다림
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
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Menu");
        asyncLoad.allowSceneActivation = false;

        // 로딩이 90% 이상 완료될 때까지 기다림
        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }
        asyncLoad.allowSceneActivation = true;
    }
}
