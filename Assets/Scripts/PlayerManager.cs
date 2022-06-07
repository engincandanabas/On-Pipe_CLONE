using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Constants
    private const float size_scalel = 0.18f;
    private const float offset=.05f;
    private const float checker_radius = .18f;
    #endregion
    #region Serialize Fields
    [SerializeField] private Vector3 default_size = new Vector3(1, 1, 1);
    [SerializeField] private LayerMask cylinder_layer;
    [SerializeField] private AudioClip blipClick;
    #endregion
    [HideInInspector]
    public bool can_collect=false;
    public float health = 10.0f;
    private void Update()
    {
        GameObject cyl = Physics.OverlapSphere(transform.position, checker_radius, cylinder_layer)[0].gameObject; // belirledigimiz konumda ve capta hayali bir kure olusturur. Degen objenin ozelliklerini verir
        float cyl_radius = cyl.transform.localScale.x * size_scalel;
        if(cyl_radius>transform.localScale.y || health<0)
        {
            Death();
        }
        if(cyl.CompareTag("Enemy"))
        {
            if(cyl_radius+offset>transform.localScale.y)
            {
                Death();
            }
        }
        if(cyl_radius+offset>transform.localScale.y)
        {
            can_collect = true;
        }
        else
        {
            can_collect = false;
        }
        ChangeRingRadius(cyl_radius);
        HealthCounter();
    }
    #region Functions
    private void Death()
    {
        print("GameOver");
        Camera.main.GetComponent<CameraController>().enabled = false;
        // Player alive boolean 
        GameManager.instance.isPlayerAlive = false;
        //Open game over ui
        UIManager.Instance.OpenGameOverPanel();
        if (PlayerPrefs.GetFloat("HighScore", 0)<=GameManager.instance.distance)
        {
            PlayerPrefs.SetFloat("HighScore",GameManager.instance.distance);
        }
        
        Destroy(this.gameObject);

    }
    private void ChangeRingRadius(float cyl_radius)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Camera.main.GetComponent<AudioSource>().PlayOneShot(blipClick,0.2f);
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 targetVector = new Vector3(default_size.x, cyl_radius, cyl_radius);
            transform.localScale = Vector3.Slerp(transform.localScale, targetVector, 0.125f);
        }
        else
        {
            transform.localScale = Vector3.Slerp(transform.localScale, default_size, 0.125f);
        }
    }
    private void HealthCounter()
    {
        health = Mathf.Clamp(health,-1, 10.0f);
        if(health>=0)
        {
            health -= Time.deltaTime;
            UIManager.Instance.SetPlayerHealth(health);
        }
    }
    public void IncreaseHealth(float value)
    {
        health+=value;
    }
    #endregion
}
