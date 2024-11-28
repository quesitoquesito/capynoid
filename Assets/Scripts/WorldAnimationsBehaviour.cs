using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldAnimationsBehaviour : MonoBehaviour
{
    [SerializeField] GameObject wavesSprite;
    [SerializeField] LeanTweenType waterAnimType;
    [SerializeField] float waterPositionTo;
    [SerializeField] float waterAnimSpeed;
    void Start()
    {
        WaterAnimation();
    }

    void Update()
    {
        
    }

    void WaterAnimation()
    {
        LeanTween.moveLocalX(wavesSprite, waterPositionTo, waterAnimSpeed).setEase(waterAnimType).setLoopPingPong();
    }
}
