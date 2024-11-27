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

    private void Awake()
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
    }

    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        if (isGameActive && hasGameStarted)
        {
            Vector2 gameObjectPosition = gameObject.transform.position;
            gameObjectPosition.x = Mathf.Clamp(gameObjectPosition.x + Input.GetAxisRaw("Horizontal") * movementSpeed * Time.deltaTime, -colliderInArea, colliderInArea);
            transform.position = gameObjectPosition;
        }
    }
}
