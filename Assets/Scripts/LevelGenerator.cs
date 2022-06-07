using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private GameObject previous_cylinder;
    #region SerializeField
    [SerializeField] private GameObject cylinder;
    [SerializeField] private Color enemy_cylinder;
    [SerializeField] private float minRadius,maxRadius;
    #endregion
   
    #region Functions
    private float FindRadius(float minRadius,float maxRadius)
    {
        float radius = Random.Range(minRadius, maxRadius);
        if (previous_cylinder != null)
        {
            while (Mathf.Abs(radius - previous_cylinder.transform.localScale.x) < .4f)
            {
                radius = Random.Range(1f, 4f);
            }
        }
        return radius;
    }

    public void SpawnCylinder()
    {
        float radius = FindRadius(minRadius, maxRadius);
        float height = Random.Range(2f, 6f);
        cylinder.transform.localScale = new Vector3(radius, height, radius);
        if (previous_cylinder is null)
        {
            // ilk olusacak silindirde onceki bir silindir yok.
            previous_cylinder = Instantiate(cylinder, Vector3.zero, Quaternion.identity);
        }
        else
        {
            float spawnPoint = previous_cylinder.transform.position.z + previous_cylinder.transform.localScale.y + cylinder.transform.localScale.y;
            previous_cylinder = Instantiate(cylinder, new Vector3(0, 0, spawnPoint), Quaternion.identity);
            if (Random.value < 0.1f)
            {
                // Random.value 0-1 arasi sayi tutmakta. Yukaridaki if blogu %10'luk sans tanimakta bana.
                previous_cylinder.GetComponent<Renderer>().material.color = enemy_cylinder;
                previous_cylinder.tag = "Enemy";
            }
        }
        previous_cylinder.transform.Rotate(90, 0, 0);
    }
    #endregion
}
