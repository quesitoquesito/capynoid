using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LivesPointsBehaviour : MonoBehaviour
{
    public static LivesPointsBehaviour instance;

    int highScore;
    int currentScore;
    [SerializeField] TextMeshProUGUI pointsText;

    [SerializeField] TextMeshProUGUI livesCount;
    [SerializeField] int lives;
     void Awake()
    {
        if (LivesPointsBehaviour.instance == null)
        {
            LivesPointsBehaviour.instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        currentScore = 0;
    }

    public void DisplayLives()
    {
        lives -= 1;
        livesCount.text = "Vidas restantes: " + lives.ToString();
        if (lives == 0)
        {
            UIAnimationsBehaviour.instance.GameOver();
            CapyBallBehaviour.instance.capyBallRB.velocity = Vector2.zero;
        }
        else if (lives > 0)
        {
            CapyBallBehaviour.instance.Restart();
            Debug.Log("Restart");
        }
    }

    public void PointsCounter(int scoreAdd)
    {
        currentScore += scoreAdd;
        pointsText.text = currentScore.ToString();
    }
}
