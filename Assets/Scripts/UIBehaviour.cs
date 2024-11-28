using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIBehaviour : MonoBehaviour
{
    [SerializeField] RectTransform pauseCanvas;
    [SerializeField] RectTransform mainMenuCanvas;
    [SerializeField] TextMeshProUGUI timerText;
    float timer;
    bool isPaused;
    bool completePause;
    void Start()
    {
        isPaused = false;
        //Change to animation
        pauseCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        if (PlayerBehaviour.instance.isGameActive)
        {
            Timer();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && PlayerBehaviour.instance.hasGameStarted)
        {
            StartCoroutine(PauseGame());
        }
    }

    public void FullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Timer()
    {
        timer += Time.deltaTime;
        int hours = (int)(timer / 3600);
        int minutes = (int)((timer % 3600) / 60);
        int seconds = (int)(timer % 60);
        int miliseconds = (int)((timer * 100) % 100);
        timerText.text = $"{hours}:{minutes:00}:{seconds:00}:{miliseconds:00}";
    }

    public void PauseActivate()
    {
        StartCoroutine(PauseGame());
    }
    IEnumerator PauseGame()
    {
        if (isPaused)
        {
            pauseCanvas.gameObject.SetActive(false);
            PlayerBehaviour.instance.isGameActive = true;
            while (Time.timeScale < 1.0f && !completePause)
            {
                Time.timeScale += 0.1f;
                yield return new WaitForSeconds(0.01f);
                Debug.Log(Time.timeScale);
                if (Time.timeScale >= 1.0f)
                {
                    Time.timeScale = 1.0f;
                    Debug.Log("1 reached");
                    completePause = true;
                }
            }
            yield return new WaitUntil(() => completePause);
            completePause = false;
            isPaused = false;
            Debug.Log("de-paused");
        }
        else
        {
            while (Time.timeScale > 0.0f && !completePause)
            {
                Time.timeScale -= 0.1f;
                yield return new WaitForSeconds(0.01f);
                Debug.Log(Time.timeScale);
                if (Time.timeScale <= 0.1f)
                {
                    Time.timeScale = 0.0f;
                    Debug.Log("0 reached");
                    completePause = true;
                }
            }
            yield return new WaitUntil(() => completePause);
            completePause = false;
            isPaused = true;
            PlayerBehaviour.instance.isGameActive = false;
            pauseCanvas.gameObject.SetActive(true);
            Debug.Log("paused");
        }
        yield return null;
    }

    public void MainMenuButton()
    {
        //Reload Scene
    }
}
