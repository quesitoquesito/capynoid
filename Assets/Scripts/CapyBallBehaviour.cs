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

    //Debug Testing
    Vector2 initialCapyBallPos;

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
            ballLaunched=false;
            capyBallRB.velocity = Vector2.zero;
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
            Debug.Log("Selected Launch Option: " + selectedLaunchDirection);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            Destroy(collision.gameObject);
        }
        /*if (!collision.gameObject.CompareTag("Player"))
        {
            ChangeCollisionDirection();
        }*/
    }






    //CHANGE
    /*private void ChangeCollisionDirection()
    {
        float velocityDelta = 0.5f;
        float minVelocity = 0.2f;

        if (Mathf.Abs(capyBallRB.velocity.x) < minVelocity)
        {
            velocityDelta = Random.value < 0.5f ? velocityDelta : -velocityDelta;
            capyBallRB.velocity += new Vector2(velocityDelta, 0f);
        }

        if (Mathf.Abs(capyBallRB.velocity.y) < minVelocity)
        {
            velocityDelta = Random.value < 0.5f ? velocityDelta : -velocityDelta;
            capyBallRB.velocity += new Vector2(0f, velocityDelta);
        }
    }*/
}
