using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public static PlayerBehaviour instance;
    
    public float easyMovementSpeed;
    public float mediumMovementSpeed;
    public float hardMovementSpeed;

    [HideInInspector] public float movementSpeed;

    [SerializeField] float colliderInArea;
    [HideInInspector] public bool isGameActive;
    [HideInInspector] public bool hasGameStarted;
    public GameObject crocPaddle;

    bool isFacingRight = true;

    public bool isInverted;

    float movementDirection;

    void Awake()
    {
        if (PlayerBehaviour.instance == null)
        {
            PlayerBehaviour.instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        isGameActive = false;
        hasGameStarted = false;
        GetComponent<Collider2D>().enabled = false;
    }

    void Update()
    {
        PlayerMovement();
        if ((Input.GetAxis("Horizontal") > 0 && !isFacingRight) && isGameActive || (Input.GetAxis("Horizontal") < 0 && isFacingRight) && isGameActive)
        {
            FlipPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Slow"))
        {
            Destroy (collision.gameObject);
            PowerUpsBehaviour.instance.CallSlowBall();
        }

        if (collision.gameObject.name.Contains("Inverted"))
        {
            Destroy(collision.gameObject);
            PowerUpsBehaviour.instance.CallInvertedCroc();
        }
    }

    void PlayerMovement()
    {
        if (isGameActive && hasGameStarted)
        {
            Vector3 gameObjectPosition = gameObject.transform.localPosition;
            movementDirection = Input.GetAxis("Horizontal");
            if (isInverted)
            {
                movementDirection = -movementDirection;
            }
            gameObjectPosition.x = Mathf.Clamp(gameObjectPosition.x + (movementDirection) * movementSpeed * Time.deltaTime, -colliderInArea, colliderInArea);
            transform.localPosition = gameObjectPosition;
        }
    }

    void FlipPlayer()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = crocPaddle.transform.localScale;
        scale.x *= -1;
        crocPaddle.transform.localScale = scale;
    }
}
