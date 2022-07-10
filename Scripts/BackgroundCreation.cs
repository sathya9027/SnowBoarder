using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class BackgroundCreation : MonoBehaviour
{
    [SerializeField] SpriteShapeController shape;

    int noOfPoints;

    private void Start()
    {
        noOfPoints = shape.spline.GetPointCount();
    }

    private void LateUpdate()
    {
        SpawnTree();
    }

    private void SpawnTree()
    {
        for (int i = 2; i < noOfPoints - 4; i++)
        {
            float xPos = shape.spline.GetPosition(i).x;
            float yPos = shape.spline.GetPosition(i).y + (float)2.557269;
            float zPos = shape.spline.GetPosition(i).z;

            //Instantiate(tree[i], new Vector3(xPos, yPos, zPos), Quaternion.identity);
        }
    }
}
