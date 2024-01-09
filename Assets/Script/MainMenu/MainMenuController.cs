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
        // 키보드 방향키 입력 확인
        float verticalInput = Input.GetAxis("Vertical");

        // 버튼 선택 변경
        if (verticalInput > 0.5f || verticalInput < -0.5f)
        {
            if (!isButtonPressed)
            {
                isButtonPressed = true;
                isPlayButtonSelected = !isPlayButtonSelected;

                // 선택된 버튼에 따라 화살표 위치 변경
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

        // 엔터 키로 선택된 버튼 동작 수행
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (isPlayButtonSelected)
            {
                // 플레이 버튼 동작 수행
                Debug.Log("Play button selected!");
                StartCoroutine(FadeOut());
            }
            else
            {
                // 게임종료 버튼 동작 수행
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

        // 화면 어두워짐
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            Panel.color = alpha;
            yield return null;
        }

        // 여기서 대기 시간 설정
        yield return new WaitForSeconds(1f);

        // 대기 후 씬 로드
        SceneManager.LoadScene("Lobby");
    }
}
