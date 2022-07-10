using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class TerrainCreation : MonoBehaviour
{
    [SerializeField] int scale = 100;
    [SerializeField] int noiseHeight = 10;
    [SerializeField] int noOfPoints = 150;
    [SerializeField] GameObject player;
    [SerializeField] GameObject[] tree;

    [HideInInspector] public float playerTravelDistance;

    int totalNoOfPoints;
    SpriteShapeController terrainSpriteShape;
    PlayerController playerController;
    float startPos;
    float curPos;

    private void Start()
    {
        StartGameLoop();
    }

    private void LateUpdate()
    {
        UpdateGameLoop();
    }

    private void StartGameLoop()
    {
        #region TutorialCode
        CreateSpriteShape();
        SpawnPlayer();
        #endregion
    }

    private void UpdateGameLoop()
    {

        #region MyCode
        UpdateSpriteShape();
        UpdateTree();
        UpdatePlayerTravelDistance();
        #endregion
    }

    private void UpdatePlayerTravelDistance()
    {
        curPos = playerController.transform.position.x;
        playerTravelDistance = curPos - startPos;
        //Debug.Log(playerTravelDistance);
    }

    private void CreateSpriteShape()
    {
        terrainSpriteShape = GetComponent<SpriteShapeController>();

        float distanceBetweenPoints = (float)scale / noOfPoints;

        terrainSpriteShape.spline.SetPosition(
            2, 
            terrainSpriteShape.spline.GetPosition(2) + Vector3.right * scale
            );
        terrainSpriteShape.spline.SetPosition(
            3, 
            terrainSpriteShape.spline.GetPosition(3) + Vector3.right * scale
            );

        for (int i = 0; i < noOfPoints; i++)
        {
            float xPos = terrainSpriteShape.spline.GetPosition(i + 1).x + distanceBetweenPoints;
            terrainSpriteShape.spline.InsertPointAt(
                i + 2, 
                new Vector3(xPos, noiseHeight * Mathf.PerlinNoise(i * Random.Range(15f, 35f), 0))
                );
        }

        for (int i = 0; i < noOfPoints + 4; i++)
        {
            terrainSpriteShape.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            if (i != 0)
            {
                terrainSpriteShape.spline.SetLeftTangent(i, new Vector3(-2, 0, 0));
                terrainSpriteShape.spline.SetRightTangent(i, new Vector3(2, 0, 0));
            }
        }

        startPos = terrainSpriteShape.spline.GetPosition(5).x;
    }

    private void UpdateTree()
    {
        for (int i = 2; i < noOfPoints; i++)
        {
            float xPos = terrainSpriteShape.spline.GetPosition(i).x;
            float yPos = terrainSpriteShape.spline.GetPosition(i).y + (float)2.557269;
            float zPos = terrainSpriteShape.spline.GetPosition(i).z;
            tree[i - 2].transform.position = new Vector3(xPos, yPos, zPos);
            //Instantiate(tree, new Vector3(xPos, yPos, zPos), Quaternion.identity);
        }
    }

    public void SpawnPlayer()
    {
        float xPos = terrainSpriteShape.spline.GetPosition(5).x;
        float yPos = terrainSpriteShape.spline.GetPosition(5).y + 10;
        float zPos = terrainSpriteShape.spline.GetPosition(5).z;
        //if (!isGameStarted)
        //{
        //    startPos.position = new Vector3(xPos, yPos, zPos);
        //    isGameStarted = true;
        //}
        Instantiate(player, new Vector3(xPos, yPos, zPos), Quaternion.identity);

        playerController = FindObjectOfType<PlayerController>();
    }

    private void UpdateSpriteShape()
    {
        int playerXPos = (int)playerController.transform.position.x;
        totalNoOfPoints = terrainSpriteShape.spline.GetPointCount();

        for (int i = 2; i < noOfPoints; i++)
        {
            if (playerXPos > terrainSpriteShape.spline.GetPosition(i + 4).x)
            {
                float distanceBetweenPoints = (float)scale / noOfPoints;
                float xPos = terrainSpriteShape.spline.GetPosition(noOfPoints + 1).x + distanceBetweenPoints;

                terrainSpriteShape.spline.InsertPointAt(
                    noOfPoints + 2, 
                    new Vector3(xPos, noiseHeight * Mathf.PerlinNoise(i * Random.Range(15f, 35f), 0))
                    );
                terrainSpriteShape.spline.RemovePointAt(i);

                Vector3 firstIndex = terrainSpriteShape.spline.GetPosition(0);
                Vector3 secondIndex = terrainSpriteShape.spline.GetPosition(1);
                Vector3 thirdIndex = terrainSpriteShape.spline.GetPosition(2);

                terrainSpriteShape.spline.SetPosition(
                    0, 
                    new Vector3(thirdIndex.x - 5, firstIndex.y, firstIndex.z)
                    );
                terrainSpriteShape.spline.SetPosition(
                    1, 
                    new Vector3(thirdIndex.x - 5, secondIndex.y, secondIndex.z)
                    );
                terrainSpriteShape.spline.SetPosition(
                    totalNoOfPoints - 2,
                    terrainSpriteShape.spline.GetPosition(totalNoOfPoints - 3) + Vector3.right * 5
                    );
                Vector3 lastIndex = terrainSpriteShape.spline.GetPosition(totalNoOfPoints - 2);
                terrainSpriteShape.spline.SetPosition(
                    totalNoOfPoints - 1,
                    new Vector3(lastIndex.x, terrainSpriteShape.spline.GetPosition(totalNoOfPoints - 1).y, lastIndex.z)
                    );
            }
        }

        for (int i = 0; i < noOfPoints + 4; i++)
        {
            terrainSpriteShape.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            if (i != 0)
            {
                terrainSpriteShape.spline.SetLeftTangent(i, new Vector3(-2, 0, 0));
                terrainSpriteShape.spline.SetRightTangent(i, new Vector3(2, 0, 0));
            }
        }
    }
}
