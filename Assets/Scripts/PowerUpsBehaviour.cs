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

    bool slowApplied;

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
    }

    public void spawnRandomPowerUp(Vector2 powerUpSpawnPosition)
    {
        int spawnPosibility = Random.Range(1, 3);
        if (spawnPosibility == 1)
        {
            GameObject selectedPowerUp = Instantiate(powerUpsPrefabs[Random.Range(0, powerUpsPrefabs.Length)], new Vector3(powerUpSpawnPosition.x, powerUpSpawnPosition.y, -1f), Quaternion.identity);
            LeanTween.scale(selectedPowerUp, new Vector3(2, 2, 1), powerUpAnimSpeed).setEase(powerUpAnimType);
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
    }
}
