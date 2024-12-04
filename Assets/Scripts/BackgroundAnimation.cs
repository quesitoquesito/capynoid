using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BackgroundAnimation : MonoBehaviour
{
    public GameObject player;
    float size;
    float initialPosition;
    public float effect;

    void Start()
    {
        initialPosition = transform.position.x;
        size = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float temp = (player.transform.position.x * (1 - effect));
        float dist = (player.transform.position.x * effect);

        transform.position = new Vector3(initialPosition + dist, transform.position.y, transform.position.z);

        if (temp > initialPosition + size) initialPosition += size;
        else if (temp < initialPosition - size) initialPosition -= size;
    }
}
