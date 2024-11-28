using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LivesPointsBehaviour : MonoBehaviour
{
    public static LivesPointsBehaviour instance;

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
        
    }

    void Update()
    {
        
    }

    public void DisplayLives()
    {
        lives -= 1;
        livesCount.text = lives.ToString();
        if (lives == 0)
        {
            Debug.Log("Game Over");
            CapyBallBehaviour.instance.capyBallRB.velocity = Vector2.zero;
        }
        else if (lives > 0)
        {
            CapyBallBehaviour.instance.Restart();
            Debug.Log("Restart");
        }
    }
}
