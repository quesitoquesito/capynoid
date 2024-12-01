using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsBehaviour : MonoBehaviour
{
    public static LevelsBehaviour instance;

    int wantedLevelDifficulty; //1 - Easy / 2 - Medium / 3 - Hard

    [SerializeField] GameObject[] cloudsPrefabs;
    [SerializeField] Vector2 prefabColliderSize;
    public LayerMask collisionLayer;

    //Number of levels before calling medium and hard levels
    [SerializeField] int easyLevelsAmount;
    [SerializeField] int mediumLevelsAmount;

    //Number of clouds to spawn for each level type (select between min and max)
    [SerializeField] int[] easyCloudsAmount;
    [SerializeField] int[] mediumCloudsAmount;
    [SerializeField] int[] hardCloudsAmount;

    //Minimum and maximum Y and X values to generate clouds in (generation area)
    [SerializeField] float minimumYClouds;
    [SerializeField] float maximumYClouds;
    [SerializeField] float minMaxXClouds;

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
        mediumLevelsAmount += easyLevelsAmount;
        wantedLevelDifficulty = 0;
    }

    public void SelectLevel()
    {
        if (wantedLevelDifficulty <= easyLevelsAmount)
        {
            int selectedCloudsAmount = Random.Range(easyCloudsAmount[0], easyCloudsAmount[1]);
            for (int i = 0; i < selectedCloudsAmount; i++)
            {
                float selectedXPosition = Random.Range(-minMaxXClouds, minMaxXClouds);
                float selectedYPosition = Random.Range(minimumYClouds, maximumYClouds);
                GameObject selectedPrefab = cloudsPrefabs[Random.Range(0,cloudsPrefabs.Length)];
                Collider2D overlap = Physics2D.OverlapBox(new Vector2 (selectedXPosition, selectedYPosition), prefabColliderSize, 0f, collisionLayer);
                if (overlap == null)
                {
                    selectedPrefab = Instantiate(selectedPrefab, new Vector2(selectedXPosition, selectedYPosition), Quaternion.identity);
                    selectedPrefab.transform.parent = gameObject.transform;
                }
                else selectedCloudsAmount += 1;
                if (selectedCloudsAmount == 200)
                {
                    Debug.Log("No free position located in time, breaking loop to prevent crash");
                    break;
                }
            }
        }
    }
}
