using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimationsBehaviour : MonoBehaviour
{
    [SerializeField] RectTransform menuBackground;
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

    bool continueWithStartup = false;

    private void Start()
    {
        
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
        
        yield return null;
    }


}
