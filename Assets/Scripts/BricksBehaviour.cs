using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BricksBehaviour : MonoBehaviour
{
    SpriteRenderer brickSpriteRenderer;
    int brickID;
    int initialBlockLives;

    [SerializeField] int pointsWhenBroken;
    [SerializeField] int blockLives;
    [SerializeField] float destroyTime;
    [SerializeField] float changeSpriteTime;
    [SerializeField] LeanTweenType blockBreakAnim;
    [SerializeField] LeanTweenType blockChangeAnimIN;
    [SerializeField] LeanTweenType blockChangeAnimOUT;

    [SerializeField] Sprite[] normalClouds;
    [SerializeField] Sprite[] pelicanLeftClouds;
    [SerializeField] Sprite[] pelicanRightClouds;

    void Start()
    {
        brickSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        initialBlockLives = blockLives;

        if (brickSpriteRenderer.name.Contains("Pelican"))
        {
            for (int i = 0; i < normalClouds.Length; i++)
            {
                if (brickSpriteRenderer.sprite.name == pelicanLeftClouds[i].name || brickSpriteRenderer.sprite.name == pelicanRightClouds[i].name)
                {
                    brickID = i;
                    break;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("CapyBall"))
        {
            blockLives--;
            if (blockLives <= 0)
            {
                LeanTween.scale(gameObject, Vector2.zero, destroyTime).setEase(blockBreakAnim).setOnComplete(() =>
                {
                    Destroy(gameObject);
                    LivesPointsBehaviour.instance.PointsCounter(pointsWhenBroken);
                    CapyBallBehaviour.instance.startDestroyBricks();
                    PowerUpsBehaviour.instance.spawnRandomPowerUp(gameObject.transform.position);
                });
            }
            if (blockLives == initialBlockLives - 1 && blockLives != 0)
            {
                LeanTween.scale(gameObject, Vector2.zero, changeSpriteTime).setEase(blockChangeAnimIN).setOnComplete(() =>
                {
                    brickSpriteRenderer.sprite = normalClouds[brickID];
                    LeanTween.scale(gameObject, Vector2.one, changeSpriteTime).setEase(blockChangeAnimOUT);
                });
            }
        }
    }
}
