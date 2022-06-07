using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float distance;
    [HideInInspector] public bool isPlayerAlive = true;
    [SerializeField] private Transform playerStartPoint;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private float difficulty;
    private GameObject player;
    #region Unity
    private void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        cameraController = Camera.main.GetComponent<CameraController>();
    }
    private void Update()
    {
        if (!isPlayerAlive)
        {
            if(Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        //Check Player Distance
        if(player.gameObject!=null)
        {
            distance = Vector3.Distance(player.transform.position, playerStartPoint.position);
            UIManager.Instance.SetDistanceValue(distance);
        }
        cameraController.speed += Time.timeSinceLevelLoad / 10000f* difficulty;
        cameraController.speed=Mathf.Clamp(cameraController.speed, 1, 50);
    }
    #endregion
}
