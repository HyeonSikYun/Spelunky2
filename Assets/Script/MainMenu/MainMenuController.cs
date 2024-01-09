using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject playButton;
    public GameObject exitButton;
    public GameObject arrowIndicator;
    public GameObject arrowIndicator2;

    private bool isPlayButtonSelected = true;
    private bool isButtonPressed = false;

    public Image Panel;

    float time = 0f;
    float F_time = 1f;

    void Update()
    {
        // Ű���� ����Ű �Է� Ȯ��
        float verticalInput = Input.GetAxis("Vertical");

        // ��ư ���� ����
        if (verticalInput > 0.5f || verticalInput < -0.5f)
        {
            if (!isButtonPressed)
            {
                isButtonPressed = true;
                isPlayButtonSelected = !isPlayButtonSelected;

                // ���õ� ��ư�� ���� ȭ��ǥ ��ġ ����
                if (isPlayButtonSelected)
                {
                    arrowIndicator.transform.position = playButton.transform.position + new Vector3(-183.1f, 0, 0);
                    arrowIndicator2.transform.position = playButton.transform.position + new Vector3(183.1f, 0, 0);
                }
                else
                {
                    arrowIndicator.transform.position = exitButton.transform.position + new Vector3(-183.1f, 1.1f, 0);
                    arrowIndicator2.transform.position = exitButton.transform.position + new Vector3(183.1f, 1.1f, 0);
                }
            }

        }
        else
        {
            isButtonPressed = false;
        }

        // ���� Ű�� ���õ� ��ư ���� ����
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (isPlayButtonSelected)
            {
                // �÷��� ��ư ���� ����
                Debug.Log("Play button selected!");
                StartCoroutine(FadeOut());
            }
            else
            {
                // �������� ��ư ���� ����
                Debug.Log("Exit button selected!");
                Application.Quit();
            }
        }
    }
    IEnumerator FadeOut()
    {
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

        // ���⼭ ��� �ð� ����
        yield return new WaitForSeconds(1f);

        // ��� �� �� �ε�
        SceneManager.LoadScene("Lobby");
    }
}
