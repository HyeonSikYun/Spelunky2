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

    public TextMeshProUGUI hpText; // UI Text ������Ʈ�� �����մϴ�.
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
            // ��� �ð� ���
            float elapsedTime = Time.time - startTime;

            // ��� �ð��� �а� �ʷ� ��ȯ
            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);

            // �ð��� �ؽ�Ʈ�� ǥ��
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

                // ȭ��ǥ ��ġ ������Ʈ
                UpdateArrowIndicator();
            }
        }
        
    }
    private void ChangeSelectedButton(int direction)
    {
        selectedButtonIndex = (selectedButtonIndex + direction + buttons.Length) % buttons.Length;
    }

    // �߰��� �Լ�: ���õ� ��ư�� ���� ���� ����
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

    // �߰��� �Լ�: ȭ��ǥ ��ġ ������Ʈ
    private void UpdateArrowIndicator()
    {
        arrowIndicator.transform.position = buttons[selectedButtonIndex].transform.position;
    }
    public void TogglePauseMenu()
    {
        // UI ���¸� ����մϴ�.
        if (pauseMenuUI != null)
        {
            bool isPaused = !pauseMenuUI.activeSelf;
            pauseMenuUI.SetActive(isPaused);

            // ���� �Ͻ� ���� �� �簳
            Time.timeScale = isPaused ? 0f : 1f;
        }
    }
    public void UpdateHPText(float currentHP)
    {
        if (hpText != null)
        {
            currentHP = Mathf.Clamp(currentHP, 0, 9);
            hpText.text = currentHP.ToString(); // UI Text�� HP ���� ǥ��
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

        // ȭ�� ��ο���
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            Panel.color = alpha;
            yield return null;
        }
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Lobby");
        asyncLoad.allowSceneActivation = false;

        // �ε��� 90% �̻� �Ϸ�� ������ ��ٸ�
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

    IEnumerator FadeInOut()
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
        LoadRandomScene();
        //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Stage1");
        //asyncLoad.allowSceneActivation = false;

        //// �ε��� 90% �̻� �Ϸ�� ������ ��ٸ�
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
        // �� �̸� �迭
        string[] scenes = { "Stage1", "Stage2", "Stage3" };

        // ������ �� ����
        int randomIndex = Random.Range(0, scenes.Length);
        string randomScene = scenes[randomIndex];

        // ���õ� ���� ������ �̵�
        SceneManager.LoadScene(randomScene);
    }
}
