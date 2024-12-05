using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CapyBallBehaviour : MonoBehaviour
{
    public static CapyBallBehaviour instance;
    [HideInInspector] public Rigidbody2D capyBallRB;

    public float easyCapyBallSpeed;
    public float mediumCapyBallSpeed;
    public float hardCapyBallSpeed;

    [HideInInspector] public float capyBallSpeed;

    bool ballLaunched;

    [SerializeField] Transform player;

    [SerializeField] float capyBallSpawnAnimSpeed;
    [SerializeField] LeanTweenType capyBallSpawnAnimType;

    //Launch Indicator
    Vector2 capyBallVector;

    [SerializeField] GameObject launchIndicator;
    float oscillationTime;
    [SerializeField] float oscillationSpeed;
    [SerializeField] float oscillationRadius;
    float minAngle = Mathf.PI / 4;
    float maxAngle = 3 * (Mathf.PI / 4);

    [HideInInspector] public Vector2 capyBallStoredDirection;

    public GameObject nextLevelCanvas;

    [SerializeField] AudioSource sfxAudioSource;
    [SerializeField] AudioClip brickSFX;
    [SerializeField] AudioClip powerUpSFX;
    [SerializeField] AudioClip restartSFX;

    void Awake()
    {
        if (CapyBallBehaviour.instance == null)
        {
            CapyBallBehaviour.instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        capyBallRB = GetComponent<Rigidbody2D>();
        ballLaunched = false;
        launchIndicator.SetActive(true);
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && !ballLaunched)
        {
            LaunchBall();
        }


        //Debug Restart
        if (Input.GetKeyDown(KeyCode.F))
        {
            Restart();
        }

        //Launch Indicator
        if (PlayerBehaviour.instance.isGameActive && !ballLaunched)
        {
            oscillationTime += Time.deltaTime * oscillationSpeed;

            float normalizedAngle = (Mathf.Sin(oscillationTime) + 1) / 2;
            float angle = Mathf.Lerp(minAngle, maxAngle, normalizedAngle);

            Vector2 position = new Vector2(
                oscillationRadius * Mathf.Cos(angle),
                oscillationRadius * Mathf.Sin(angle)
            );
            capyBallVector = new Vector2(-position.x, -position.y);

            float zRotation = Mathf.Atan2(position.y, position.x) * Mathf.Rad2Deg - 90f;

            if (launchIndicator != null)
            {
                launchIndicator.transform.rotation = Quaternion.Euler(0, 0, -zRotation);
            }
        }
    }
    void FixedUpdate()
    {
        if (ballLaunched && !UIAnimationsBehaviour.instance.isPaused)
        {
            capyBallRB.velocity = capyBallRB.velocity.normalized * capyBallSpeed;
        }
        else if (UIAnimationsBehaviour.instance.isPaused)
        {
            capyBallRB.velocity = capyBallRB.velocity.normalized * 0;
        }
    }

    void LaunchBall()
    {
        if (PlayerBehaviour.instance.isGameActive)
        {
            launchIndicator.SetActive(false);
            gameObject.transform.parent = null;
            capyBallRB.velocity = capyBallVector.normalized * capyBallSpeed;
            ballLaunched = true;
            PlayerBehaviour.instance.GetComponent<Collider2D>().enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            sfxAudioSource.clip = brickSFX;
            sfxAudioSource.Play();
        }

        if (collision.gameObject.CompareTag("Waves"))
        {
            sfxAudioSource.clip = restartSFX;
            sfxAudioSource.Play();
            LivesPointsBehaviour.instance.DisplayLives();
        }

        if (!collision.gameObject.CompareTag("Waves"))
        {
            ModifyDirection();
        }

        if (collision.gameObject.CompareTag("PowerUp"))
        {
            sfxAudioSource.clip = powerUpSFX;
            sfxAudioSource.Play();
        }
    }

    public void startDestroyBricks()
    {
        StartCoroutine(DestroyBricks());
    }

    IEnumerator DestroyBricks()
    {
        yield return 0.1f;
        LevelsBehaviour.instance.bricksActive = LevelsBehaviour.instance.gameObject.transform.childCount;
        if (LevelsBehaviour.instance.bricksActive <= 0)
        {
            LevelsBehaviour.instance.wantedLevelDifficulty += 1;
            LeanTween.scale(gameObject, Vector2.zero, capyBallSpawnAnimSpeed).setEase(LeanTweenType.easeOutBack).setOnComplete(() =>
            {
                UIAnimationsBehaviour.instance.NextLevel();
            });
        }
        yield return null;
    }

    public void Restart()
    {
        oscillationTime = 0f;
        capyBallRB.velocity = Vector2.zero;
        PlayerBehaviour.instance.GetComponent<Collider2D>().enabled = false;
        gameObject.transform.parent = player;
        capyBallRB.gameObject.transform.localScale = Vector2.zero;
        gameObject.transform.localPosition = new Vector2(0.16f, 0.66f);
        LeanTween.scale(gameObject,Vector2.one, capyBallSpawnAnimSpeed).setEase(capyBallSpawnAnimType);
        ballLaunched = false;
        launchIndicator.SetActive(true);
    }

    void ModifyDirection()
    {
        float modifiedAddedV = 1f;
        float minimumV = 0.5f;

        if(Mathf.Abs(capyBallRB.velocity.x) < minimumV)
        {
            modifiedAddedV = Random.value < 0.5f ? modifiedAddedV : -modifiedAddedV;
            capyBallRB.velocity += new Vector2(modifiedAddedV, 0f);
        }
        if (Mathf.Abs(capyBallRB.velocity.y) < minimumV)
        {
            modifiedAddedV = Random.value < 0.5f ? modifiedAddedV : -modifiedAddedV;
            capyBallRB.velocity += new Vector2(0f, modifiedAddedV);
        }
    }
}