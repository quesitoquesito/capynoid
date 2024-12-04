using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimationsBehaviour : MonoBehaviour
{
    public static UIAnimationsBehaviour instance;
    
    [SerializeField] RectTransform menuBackground;
    [SerializeField] public GameObject pauseCanvas;
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
    [HideInInspector] public bool isPaused;

    //Next Level Canvas
    [SerializeField] GameObject nextLevelCanvas;

    //All Canvas
    [SerializeField] LeanTweenType canvasAnimationType;
    [SerializeField] float canvasAnimationSpeed;
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

    private void Start()
    {
        nextLevelCanvas.transform.localScale = Vector2.zero;
        pauseCanvas.transform.localScale = Vector2.zero;

        nextLevelCanvas.SetActive(false);
        pauseCanvas.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        StartCoroutine(StartGameAnimation());
    }
    IEnumerator StartGameAnimation()
    {
        LevelsBehaviour.instance.SelectLevel();
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
        CapyBallBehaviour.instance.capyBallStoredDirection = CapyBallBehaviour.instance.capyBallRB.velocity;
        isPaused = true;
        pauseCanvas.gameObject.SetActive(true);
        LeanTween.scale(pauseCanvas.gameObject, Vector3.one, canvasAnimationSpeed).setEase(canvasAnimationType).setOnComplete(() =>
        {

        });
    }

    public void ResumeGame()
    {
        isPaused = false;
        CapyBallBehaviour.instance.capyBallRB.velocity = CapyBallBehaviour.instance.capyBallStoredDirection.normalized;
    }

    public void NextLevel()
    {
        nextLevelCanvas.SetActive(true);
        LeanTween.scale(nextLevelCanvas, Vector3.one, canvasAnimationSpeed).setEase(canvasAnimationType).setOnComplete(() =>
        {
            LevelsBehaviour.instance.SelectLevel();
            CapyBallBehaviour.instance.Restart();
        });
    }
}
