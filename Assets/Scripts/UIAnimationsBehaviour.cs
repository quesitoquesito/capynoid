using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimationsBehaviour : MonoBehaviour
{
    public static UIAnimationsBehaviour instance;
    
    [SerializeField] RectTransform menuBackground;
    [SerializeField] public GameObject pauseCanvas;
    [SerializeField] Button[] menuButtons;
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
    bool animationRunning;

    //Next Level Canvas
    [SerializeField] GameObject nextLevelCanvas;

    //All Canvas
    [SerializeField] LeanTweenType canvasAnimationTypeUP;
    [SerializeField] LeanTweenType canvasAnimationTypeDOWN;
    [SerializeField] float canvasAnimationSpeed;
    bool continueWithStartup = false;

    //Game Over
    [SerializeField] GameObject gameOverCanvas;

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
        nextLevelCanvas.SetActive(false);
        pauseCanvas.gameObject.SetActive(false);
        animationRunning = false;
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
        if (!animationRunning)
        {
            animationRunning = true;
            CapyBallBehaviour.instance.capyBallStoredDirection = CapyBallBehaviour.instance.capyBallRB.velocity;
            PlayerBehaviour.instance.isGameActive = false;
            isPaused = true;
            pauseCanvas.gameObject.SetActive(true);
            LeanTween.moveLocalY(pauseCanvas.gameObject, 0f, canvasAnimationSpeed).setEase(canvasAnimationTypeDOWN).setOnComplete(() =>
            {
                animationRunning = false;
            });
        }
    }

    public void ResumeGame()
    {
        if (!animationRunning)
        {
            animationRunning = true;
            LeanTween.moveLocalY(pauseCanvas.gameObject, 1100f, canvasAnimationSpeed).setEase(canvasAnimationTypeUP).setOnComplete(() =>
            {
                pauseCanvas.gameObject.SetActive(false);
                PlayerBehaviour.instance.isGameActive = true;
                CapyBallBehaviour.instance.capyBallRB.velocity = CapyBallBehaviour.instance.capyBallStoredDirection.normalized;
                isPaused = false;
                animationRunning = false;
            });
        }
    }

    public void NextLevel()
    {
        if (!animationRunning)
        {
            animationRunning = true;
            nextLevelCanvas.SetActive(true);
            LeanTween.moveLocalY(nextLevelCanvas, 0f, canvasAnimationSpeed).setEase(canvasAnimationTypeDOWN).setOnComplete(() =>
            {
                LevelsBehaviour.instance.SelectLevel();
                CapyBallBehaviour.instance.Restart();
                animationRunning = false;
                LeanTween.moveLocalY(nextLevelCanvas, 1100f, canvasAnimationSpeed).setEase(canvasAnimationTypeUP);
            });
        }
    }

    public void GameOver()
    {
        PlayerBehaviour.instance.isGameActive = false;
        gameOverCanvas.SetActive(true);
        LeanTween.moveLocalY(gameOverCanvas, 0f, canvasAnimationSpeed).setEase(canvasAnimationTypeDOWN);
    }

    public void ResetGame()
    {
        PlayerBehaviour.instance.gameObject.transform.position = new Vector3(0, PlayerBehaviour.instance.gameObject.transform.position.y, PlayerBehaviour.instance.gameObject.transform.position.z);
        LivesPointsBehaviour.instance.lives = 3;
        LivesPointsBehaviour.instance.livesCount.text = "Vidas restantes: " + LivesPointsBehaviour.instance.lives.ToString();
        LivesPointsBehaviour.instance.currentScore = 0;
        LivesPointsBehaviour.instance.pointsText.text = LivesPointsBehaviour.instance.currentScore.ToString();
        LevelsBehaviour.instance.wantedLevelDifficulty = 0;
        LevelsBehaviour.instance.SelectLevel();
        CapyBallBehaviour.instance.Restart();
        UIBehaviour.instance.timer = 0;
        LeanTween.moveLocalY(gameOverCanvas, 1100f, canvasAnimationSpeed).setEase(canvasAnimationTypeUP);
        PlayerBehaviour.instance.isGameActive = true;
    }
}
