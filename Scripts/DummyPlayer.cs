using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyPlayer : MonoBehaviour
{
    [SerializeField] float baseSpeed = 20f;
    [SerializeField] Transform reSpawnPoint;

    SurfaceEffector2D sF2D;

    private void Start()
    {
        sF2D = FindObjectOfType<SurfaceEffector2D>();
    }

    private void Update()
    {
        sF2D.speed = baseSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.position = reSpawnPoint.position;
    }
}
