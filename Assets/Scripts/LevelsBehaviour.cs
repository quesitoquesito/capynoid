using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsBehaviour : MonoBehaviour
{
    public static LevelsBehaviour instance;

    [HideInInspector] public int wantedLevelDifficulty; //1 - Easy / 2 - Medium

    [SerializeField] int easyLevelCount;
    [SerializeField] int mediumLevelCount;


    //Easy Level Designs
    [SerializeField] Vector3[] easyLevelDesign1;
    [SerializeField] Vector3[] easyLevelDesign2;
    [SerializeField] Vector3[] easyLevelDesign3;
    [SerializeField] Vector3[] easyLevelDesign4;

    //Medium Level Designs
    [SerializeField] Vector3[] mediumLevelDesign1;
    [SerializeField] Vector3[] mediumLevelDesign2;
    [SerializeField] Vector3[] mediumLevelDesign3;
    [SerializeField] Vector3[] mediumLevelDesign4;

    Vector3[][] easyLevel;

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
    private void Start()
    {

    }

    public void SelectLevel()
    {
        int easyLevelSelected = Random.Range(0, easyLevelCount);
        easyLevel = new Vector3[easyLevelSelected][];
    }
}
