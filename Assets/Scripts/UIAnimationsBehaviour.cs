using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimationsBehaviour : MonoBehaviour
{
    public static UIAnimationsBehaviour instance;
    
    [SerializeField] RectTransform menuBackground;
    [SerializeField] public RectTransform pauseCanvas;
    [SerializeField] Button[] menuButtons; //0 Start - 1 Options - 2 Quit
    //Button locations and intervals SAVE START BUTTON POS AT START and calculate interval with first and second button
    [SerializeField] int startButtonLocation;
    [SerializeField] float buttonPosOutOfSight;
    [SerializeField] float menuBackgroundPosOutOfSight;
    [SerializeField] int buttonInterval;
    //Menu Animations
    [SerializeField] float buttonAnimSpeed;
    [SerializeField] float buttonAnimInterval;
    [SerializeField] float menuAnimSpeed;
    [SerializeField] float menuAnimDelay;
    [SerializeField] LeanTweenType buttonAnimType;
    [SerializeField] LeanTweenType menuAnimType;

    //Pause Animations
    [SerializeField] float slowDownDuration;
    [HideInInspector] public bool isPausing;
    [HideInInspector] public bool isPaused;

    bool continueWithStartup = false;

    void Awake()
    {
        if (UIAnimationsBehaviour.instance == null)
        {
            UIAnimationsBehaviour.instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void StartGame()
    {
        StartCoroutine(StartGameAnimation());
    }
    IEnumerator StartGameAnimation()
    {
        LeanTween.moveLocalY(menuButtons[2].gameObject, buttonPosOutOfSight, buttonAnimSpeed).setEase(buttonAnimType);
        yield return new WaitForSeconds(buttonAnimInterval);
        LeanTween.moveLocalY(menuButtons[1].gameObject, buttonPosOutOfSight, buttonAnimSpeed + 0.1f).setEase(buttonAnimType);
        yield return new WaitForSeconds(buttonAnimInterval);
        LeanTween.moveLocalY(menuButtons[0].gameObject, buttonPosOutOfSight, buttonAnimSpeed + 0.2f).setEase(buttonAnimType).setOnComplete(() =>
        {
            for (int i = 0; i < menuButtons.Length; i++)
            {
                menuButtons[i].gameObject.SetActive(false);
            }
            continueWithStartup = true;
        });
        yield return new WaitUntil(() => continueWithStartup);
        yield return new WaitForSeconds(menuAnimDelay);
        LeanTween.moveLocalY(menuBackground.gameObject, menuBackgroundPosOutOfSight, menuAnimSpeed).setEase(menuAnimType).setOnComplete(() =>
        {
            PlayerBehaviour.instance.hasGameStarted = true;
            PlayerBehaviour.instance.isGameActive = true;
        });
        yield return null;
    }

    public void PauseGame()
    {
        StartCoroutine(PauseGameAnimation());
    }

    IEnumerator PauseGameAnimation()
    {
        isPausing = true;

        float elapsedTime = 0f;

        while (elapsedTime < slowDownDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Lerp(1f, 0f, elapsedTime / slowDownDuration);
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            yield return null;
        }

        pauseCanvas.gameObject.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        isPausing = false;
    }

    public void ResumeGame()
    {
        StartCoroutine(ResumeGameAnimation());
    }

    IEnumerator ResumeGameAnimation()
    {
        isPausing = true;

        pauseCanvas.gameObject.SetActive(false);

        float elapsedTime = 0f;

        while (elapsedTime < slowDownDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Lerp(0f, 1f, elapsedTime / slowDownDuration);
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            yield return null;
        }

        Time.timeScale = 1f;
        isPaused = false;
        isPausing = false;
    }
}
