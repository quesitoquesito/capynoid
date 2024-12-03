using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsBehaviour : MonoBehaviour
{
    public static LevelsBehaviour instance;

    public int wantedLevelDifficulty;

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

    [HideInInspector] public int selectedCloudsAmount;

    public int bricksActive;

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
            selectedCloudsAmount = Random.Range(easyCloudsAmount[0], easyCloudsAmount[1]);
            PlayerBehaviour.instance.movementSpeed = PlayerBehaviour.instance.easyMovementSpeed;
            CapyBallBehaviour.instance.capyBallSpeed = CapyBallBehaviour.instance.easyCapyBallSpeed;
        }

        else if (wantedLevelDifficulty > easyLevelsAmount && wantedLevelDifficulty <= mediumLevelsAmount)
        {
            selectedCloudsAmount = Random.Range(mediumCloudsAmount[0], mediumCloudsAmount[1]);
            PlayerBehaviour.instance.movementSpeed = PlayerBehaviour.instance.mediumMovementSpeed;
            CapyBallBehaviour.instance.capyBallSpeed = CapyBallBehaviour.instance.mediumCapyBallSpeed;
        }

        else if (wantedLevelDifficulty > mediumLevelsAmount)
        {
            selectedCloudsAmount = Random.Range(hardCloudsAmount[0], hardCloudsAmount[1]);
            PlayerBehaviour.instance.movementSpeed = PlayerBehaviour.instance.hardMovementSpeed;
            CapyBallBehaviour.instance.capyBallSpeed = CapyBallBehaviour.instance.hardCapyBallSpeed;
        }

        for (int i = 0; i < selectedCloudsAmount; i++)
        {
            float selectedXPosition = Random.Range(-minMaxXClouds, minMaxXClouds);
            float selectedYPosition = Random.Range(minimumYClouds, maximumYClouds);
            GameObject selectedPrefab = cloudsPrefabs[Random.Range(0, cloudsPrefabs.Length)];
            Collider2D overlap = Physics2D.OverlapBox(new Vector2(selectedXPosition, selectedYPosition), prefabColliderSize, 0f, collisionLayer);
            if (overlap == null)
            {
                selectedPrefab = Instantiate(selectedPrefab, new Vector2(selectedXPosition, selectedYPosition), Quaternion.identity);
                selectedPrefab.transform.parent = gameObject.transform;
            }
            else selectedCloudsAmount += 1;
            if (selectedCloudsAmount >= 200)
            {
                Debug.Log("No free position located in time, breaking loop to prevent crash");
                break;
            }
        }

        bricksActive = gameObject.transform.childCount;
    }
}
