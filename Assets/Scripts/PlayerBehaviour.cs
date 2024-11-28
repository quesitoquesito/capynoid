using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public static PlayerBehaviour instance;
    
    [SerializeField] float movementSpeed;
    [SerializeField] float colliderInArea;
    [HideInInspector] public bool isGameActive;
    [HideInInspector] public bool hasGameStarted;

    bool isFacingRight = true;

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
        gameObject.GetComponent<Collider2D>().enabled = false;
    }

    void Update()
    {
        PlayerMovement();
        if ((Input.GetAxis("Horizontal") > 0 && !isFacingRight) || (Input.GetAxis("Horizontal") < 0 && isFacingRight))
        {
            FlipPlayer();
        }
    }

    void PlayerMovement()
    {
        if (isGameActive && hasGameStarted)
        {
            Vector3 gameObjectPosition = gameObject.transform.localPosition;
            gameObjectPosition.x = Mathf.Clamp(gameObjectPosition.x + Input.GetAxisRaw("Horizontal") * movementSpeed * Time.deltaTime, -colliderInArea, colliderInArea);
            transform.localPosition = gameObjectPosition;
        }
    }

    void FlipPlayer()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = gameObject.transform.localScale;
        scale.x *= -1;
        gameObject.transform.localScale = scale;
    }
}
