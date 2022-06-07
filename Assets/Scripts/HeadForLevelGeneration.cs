using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadForLevelGeneration : MonoBehaviour
{
    #region SerializeField
    [SerializeField] private LevelGenerator levelGenerator;
    [SerializeField] private LayerMask layerMask;
    #endregion
    #region Unity

    private void Update()
    {
        Collider[] cyl = Physics.OverlapSphere(transform.position, 1f, layerMask);
        if (cyl.Length == 0)
        {
            levelGenerator.SpawnCylinder();
        }
    }

    #endregion
    #region Functions

    #endregion
}
