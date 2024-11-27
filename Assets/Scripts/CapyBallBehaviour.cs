using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CapyBallBehaviour : MonoBehaviour
{
    Rigidbody2D capyBallRB;
    //Set min and max speed and direction values to determine the bounds in which the capyBall first launches
    [SerializeField] float minStartSpeed;
    [SerializeField] float maxStartSpeed;
    [SerializeField] float minStartDirection;
    [SerializeField] float maxStartDirection;

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
            capyBallRB.velocity = new Vector2(Random.Range(minStartDirection, maxStartDirection), Random.Range(minStartSpeed, maxStartSpeed));
            ballLaunched = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            Destroy(collision.gameObject);
        }
        if (!collision.gameObject.CompareTag("Player"))
        {
            ChangeCollisionDirection();
        }
    }

    //CHANGE
    private void ChangeCollisionDirection()
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
    }
}
