using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LivesPointsBehaviour : MonoBehaviour
{
    public static LivesPointsBehaviour instance;

    int highScore;
    [HideInInspector] public int currentScore;
    public TextMeshProUGUI pointsText;

    public TextMeshProUGUI livesCount;
    public int lives;
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
        }
    }

    public void AddLives()
    {
        lives += 1;
        livesCount.text = "Vidas restantes: " + lives.ToString();
    }

    public void PointsCounter(int scoreAdd)
    {
        currentScore += scoreAdd;
        pointsText.text = currentScore.ToString();
    }
}
