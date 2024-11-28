using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsBehaviour : MonoBehaviour
{
    public static LevelsBehaviour instance;

    [SerializeField] float easy

    //Easy Level Designs
    [SerializeField] Vector2[] easyLevelDesign1;
    [SerializeField] Vector2[] easyLevelDesign2;
    [SerializeField] Vector2[] easyLevelDesign3;
    [SerializeField] Vector2[] easyLevelDesign4;

    //Medium Level Designs
    [SerializeField] Vector2[] mediumLevelDesign1;
    [SerializeField] Vector2[] mediumLevelDesign2;
    [SerializeField] Vector2[] mediumLevelDesign3;
    [SerializeField] Vector2[] mediumLevelDesign4;

    void Awake()
    {
        if (LevelsBehaviour.instance == null)
        {
            LevelsBehaviour.instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
