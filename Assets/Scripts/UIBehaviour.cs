using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIBehaviour : MonoBehaviour
{
    [SerializeField] RectTransform mainMenuCanvas;
    [SerializeField] TextMeshProUGUI timerText;
    float timer;
    bool completePause;

    [HideInInspector] Vector2 capyDirection;

    void Start()
    {
        UIAnimationsBehaviour.instance.isPaused = false;
        //Change to animation
        UIAnimationsBehaviour.instance.pauseCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        if (PlayerBehaviour.instance.isGameActive)
        {
            Timer();
        }
        if (Input.GetButtonDown("Cancel") && PlayerBehaviour.instance.hasGameStarted)
        {
            if (UIAnimationsBehaviour.instance.isPaused)
            {
                UIAnimationsBehaviour.instance.ResumeGame();
            }
            else
            {
                UIAnimationsBehaviour.instance.PauseGame();
            }
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
        timerText.text = $"{hours:00}:{minutes:00}:{seconds:00}";
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {

    }
}
