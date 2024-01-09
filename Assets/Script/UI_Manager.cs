using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private Button[] buttons;
    public GameObject arrowIndicator;
    private int selectedButtonIndex = 0;
    public Image Panel;
    float time = 0f;
    float F_time = 1f;

    public TextMeshProUGUI hpText; // UI Text 엘리먼트를 연결합니다.
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI timerText_DeathIcon;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI moneyText_DeathIcon;
    public TextMeshProUGUI bombText;
    public TextMeshProUGUI RopeText;
    public float money = 0;
    private float startTime;
    private Move diecheck;

    public TextMeshProUGUI deathTimer;

    private void Start()
    {
        diecheck = GameObject.Find("Player").GetComponent<Move>();
        StartCoroutine(FadeOut());
        startTime = Time.time;

        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
            buttons = pauseMenuUI.GetComponentsInChildren<Button>();
        }
        //float savedTime = PlayerPrefs.GetFloat("ElapsedTime", 0f);
        //float savedHP = PlayerPrefs.GetFloat("PlayerHP", 4f);
        //UpdateHPText(savedHP);
        //startTime = Time.time - savedTime;
    }

    private void Update()
    {
        if(!diecheck.isdie)
        {
            // 경과 시간 계산
            float elapsedTime = Time.time - startTime;

            // 경과 시간을 분과 초로 변환
            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);

            // 시간을 텍스트로 표시
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            timerText_DeathIcon.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePauseMenu();
            }

            if (pauseMenuUI != null && pauseMenuUI.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    ChangeSelectedButton(-1);
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    ChangeSelectedButton(1);
                }
                else if (Input.GetKeyDown(KeyCode.Return))
                {
                    ExecuteSelectedButtonAction();
                }

                // 화살표 위치 업데이트
                UpdateArrowIndicator();
            }
        }
        
    }
    private void ChangeSelectedButton(int direction)
    {
        selectedButtonIndex = (selectedButtonIndex + direction + buttons.Length) % buttons.Length;
    }

    // 추가된 함수: 선택된 버튼에 따라 동작 실행
    private void ExecuteSelectedButtonAction()
    {
        switch (selectedButtonIndex)
        {
            case 0:
                TogglePauseMenu();
                break;
            case 1:
                TogglePauseMenu();
                StartCoroutine(GoLobby());
                break;
            case 2:
                TogglePauseMenu();
                StartCoroutine(FadeInOut());
                break;
            case 3:
                TogglePauseMenu();
                StartCoroutine(GoMenu());
                break;
            case 4:
                Application.Quit();
                break;
        }
    }

    // 추가된 함수: 화살표 위치 업데이트
    private void UpdateArrowIndicator()
    {
        arrowIndicator.transform.position = buttons[selectedButtonIndex].transform.position;
    }
    public void TogglePauseMenu()
    {
        // UI 상태를 토글합니다.
        if (pauseMenuUI != null)
        {
            bool isPaused = !pauseMenuUI.activeSelf;
            pauseMenuUI.SetActive(isPaused);

            // 게임 일시 정지 및 재개
            Time.timeScale = isPaused ? 0f : 1f;
        }
    }
    public void UpdateHPText(float currentHP)
    {
        if (hpText != null)
        {
            currentHP = Mathf.Clamp(currentHP, 0, 9);
            hpText.text = currentHP.ToString(); // UI Text에 HP 값을 표시
        }
    }

    public void UpdateBombText(float currentBomb)
    {
        if(bombText!=null)
        {
            currentBomb = Mathf.Clamp(currentBomb, 0, 20);
            bombText.text = currentBomb.ToString();
        }
    }

    public void UpdateRopeText(float currentRope)
    {
        if(RopeText!=null)
        {
            currentRope = Mathf.Clamp(currentRope, 0, 20);
            RopeText.text = currentRope.ToString();
        }
    }

    public void IncreaseMoney(float increaseMoney)
    {
        //money += 1600;
        moneyText.text = increaseMoney.ToString();
        moneyText_DeathIcon.text = increaseMoney.ToString();
    }

    public void DecreaseMoney(float decreasMoney)
    {
        //money -= 3000;
        //decreasMoney = Mathf.Max(money, 0);
        moneyText.text = decreasMoney.ToString();
        moneyText_DeathIcon.text = decreasMoney.ToString();
    }

    public void FadeAndRestart()
    {
        //PlayerPrefs.DeleteKey("PlayerHP");
        //PlayerPrefs.DeleteKey("ElapsedTime");
        StartCoroutine(FadeInOut());
        //RestartGame();
    }

    public void FadeAndLobby()
    {
        StartCoroutine(GoLobby());
    }

    public void FandAndExit()
    {
        Application.Quit();
    }

    IEnumerator GoLobby()
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
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Lobby");
        asyncLoad.allowSceneActivation = false;

        // 로딩이 90% 이상 완료될 때까지 기다림
        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }
        asyncLoad.allowSceneActivation = true;
    }

    IEnumerator GoMenu()
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
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Menu");
        asyncLoad.allowSceneActivation = false;

        // 로딩이 90% 이상 완료될 때까지 기다림
        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }
        asyncLoad.allowSceneActivation = true;
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
        LoadRandomScene();
        //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Stage1");
        //asyncLoad.allowSceneActivation = false;

        //// 로딩이 90% 이상 완료될 때까지 기다림
        //while (asyncLoad.progress < 0.9f)
        //{
        //    yield return null;
        //}
        //asyncLoad.allowSceneActivation = true;


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

    void LoadRandomScene()
    {
        // 씬 이름 배열
        string[] scenes = { "Stage1", "Stage2", "Stage3" };

        // 랜덤한 씬 선택
        int randomIndex = Random.Range(0, scenes.Length);
        string randomScene = scenes[randomIndex];

        // 선택된 랜덤 씬으로 이동
        SceneManager.LoadScene(randomScene);
    }
}
