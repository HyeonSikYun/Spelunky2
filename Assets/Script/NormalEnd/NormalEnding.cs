using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NormalEnding : MonoBehaviour
{
    public float scaleChangeSpeed = 0.5f; // ������ ���� �ӵ�
    public float minScale = 0.5f; // �ּ� ������
    public float maxScale = 2.0f; // �ִ� ������

    private Vector3 currentScale; // ���� ������
    private bool isScalingUp = true; // �������� ���� ������ ����

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

        // ������ ������ ����
        currentScale = new Vector3(
            Mathf.Clamp(currentScale.x, minScale, maxScale),
            Mathf.Clamp(currentScale.y, minScale, maxScale),
            Mathf.Clamp(currentScale.z, minScale, maxScale)
        );

        transform.localScale = currentScale;

        // �ִ� ������ �Ǵ� �ּ� �����Ͽ� �����ϸ� ����
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
        yield return new WaitForSeconds(15.0f); // 30�ʸ� ��ٸ�
        Panel.gameObject.SetActive(true);
        time = 0f;
        Color alpha = Panel.color;

        // ȭ�� ��ο���
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            Panel.color = alpha;
            yield return null;
        }
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Menu");
        asyncLoad.allowSceneActivation = false;

        // �ε��� 90% �̻� �Ϸ�� ������ ��ٸ�
        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }
        asyncLoad.allowSceneActivation = true;
    }
}
