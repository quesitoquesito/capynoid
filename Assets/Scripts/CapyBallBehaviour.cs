using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CapyBallBehaviour : MonoBehaviour
{
    Rigidbody2D capyBallRB;
    //Set min and max speed and direction values to determine the bounds in which the capyBall first launches

    [SerializeField] Vector2[] launchDirections;

    bool ballLaunched;

    [SerializeField] Transform player;

    //Debug Testing
    Vector2 initialCapyBallPos;

    [SerializeField] float capyBallSpawnAnimSpeed;
    [SerializeField] LeanTweenType capyBallSpawnAnimType;

    void Start()
    {
        capyBallRB = GetComponent<Rigidbody2D>();
        ballLaunched = false;
        //Debug Testing
        initialCapyBallPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && !ballLaunched)
        {
            LaunchBall();
        }

        //Debug Testing
        if (Input.GetKeyDown(KeyCode.G))
        {
            gameObject.transform.position = initialCapyBallPos;
            ballLaunched = false;
            capyBallRB.velocity = Vector2.zero;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Restart();
        }
    }
    void LaunchBall()
    {
        if (PlayerBehaviour.instance.isGameActive)
        {
            gameObject.transform.parent = null;
            int selectedLaunchDirection = Random.Range(0, launchDirections.Length);
            capyBallRB.velocity = launchDirections[selectedLaunchDirection];
            ballLaunched = true;
            PlayerBehaviour.instance.gameObject.GetComponent<Collider2D>().enabled = true;
            Debug.Log("Selected Launch Option: " + selectedLaunchDirection);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Waves"))
        {
            Restart();
        }
    }

    void Restart()
    {
        //Set velocity to 0
        capyBallRB.velocity = Vector2.zero;
        //Disable collider to launch
        PlayerBehaviour.instance.gameObject.GetComponent<Collider2D>().enabled = false;
        //Set CrocPaddle as parent
        gameObject.transform.parent = player;
        //Animation
        capyBallRB.gameObject.transform.localScale = Vector2.zero;
        gameObject.transform.localPosition = new Vector2(0.16f, 0.66f);
        LeanTween.scale(gameObject,Vector2.one, capyBallSpawnAnimSpeed).setEase(capyBallSpawnAnimType);
        //Set to launch ball
        ballLaunched = false;
    }
}