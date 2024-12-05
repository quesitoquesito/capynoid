using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class PowerUpsBehaviour : MonoBehaviour
{
    public static PowerUpsBehaviour instance;
    //All PowerUps
    [SerializeField] GameObject[] powerUpsPrefabs;
    [SerializeField] LeanTweenType powerUpAnimType;
    [SerializeField] float powerUpAnimSpeed;
    //Slow Ball PowerUp
    [SerializeField] float slowBallSpeedToDiscount;
    [SerializeField] float slowBallDuration;
    //Inverted Croc
    [SerializeField] float invertedCrocDuration;
    //Probabilities
    [SerializeField] int[] slowBallProb;
    [SerializeField] int[] invertedCrocProb;

    bool slowApplied;
    bool invertedApplied;
    int spawnProbability;
    private void Awake()
    {
        if (PowerUpsBehaviour.instance == null)
        {
            PowerUpsBehaviour.instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        slowApplied = false;
        invertedApplied = false;
    }

    public void spawnRandomPowerUp(Vector2 powerUpSpawnPosition)
    {
        int selectedPUToSpawn = Random.Range(0, powerUpsPrefabs.Length);
        if (selectedPUToSpawn == 0)
        {
            spawnProbability = Random.Range(slowBallProb[0], slowBallProb[1]);
            spawnProbability = Random.Range(1, spawnProbability);
        }
        else if (selectedPUToSpawn == 1)
        {
            spawnProbability = Random.Range(invertedCrocProb[0], invertedCrocProb[1]);
            spawnProbability = Random.Range(1, spawnProbability);
        }

        if (spawnProbability == 1)
        {
            GameObject selectedPowerUp = Instantiate(powerUpsPrefabs[selectedPUToSpawn], new Vector3(powerUpSpawnPosition.x, powerUpSpawnPosition.y, -1f), Quaternion.identity);
            LeanTween.scale(selectedPowerUp, new Vector3(3, 3, 1), powerUpAnimSpeed).setEase(powerUpAnimType);
        }
    }

    public void CallSlowBall()
    {
        StartCoroutine(SlowBall());
    }

    IEnumerator SlowBall()
    {
        if (!slowApplied)
        {
            slowApplied = true;
            float tempCapyBallSpeed = CapyBallBehaviour.instance.capyBallSpeed;
            CapyBallBehaviour.instance.capyBallSpeed = CapyBallBehaviour.instance.capyBallSpeed - slowBallSpeedToDiscount;
            yield return new WaitForSeconds(slowBallDuration);
            CapyBallBehaviour.instance.capyBallSpeed = tempCapyBallSpeed;
            slowApplied = false;
            yield return null;
        }
        else if (slowApplied)
        {
            LivesPointsBehaviour.instance.currentScore += 100;
            LivesPointsBehaviour.instance.pointsText.text = LivesPointsBehaviour.instance.currentScore.ToString();
        }
    }

    public void CallInvertedCroc()
    {
        StartCoroutine (InvertedCroc());
    }

    IEnumerator InvertedCroc()
    {
        if (!invertedApplied)
        {
            invertedApplied = true;
            PlayerBehaviour.instance.isInverted = true;
            yield return new WaitForSeconds(invertedCrocDuration);
            PlayerBehaviour.instance.isInverted = false;
            invertedApplied = false;
            yield return null;
        }
        else if (invertedApplied)
        {
            LivesPointsBehaviour.instance.AddLives();
        }
    }
}
