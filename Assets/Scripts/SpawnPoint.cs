using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject point;
    private void Start()
    {
        if(!this.gameObject.CompareTag("Enemy"))
            CreatePoints();
    }
    private void CreatePoints()
    {
        float radius = transform.localScale.x / 2;
        float radius_cube = point.transform.localScale.x / 2;
        float minRange = transform.position.z - transform.localScale.y;
        float maxRange = transform.position.z + transform.localScale.y;
        float height = radius + radius_cube;
        Instantiate(point, new Vector3(transform.position.x,transform.position.y+height,Random.Range(minRange, maxRange)), Quaternion.identity);
    }
    
}
