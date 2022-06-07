using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField] private Vector3 rotate_axis;
    [SerializeField] private LayerMask layerMask;
    private PlayerManager playerManager;
    [SerializeField] private Color collectable_color, nonCollectable_color;
    [SerializeField] private AudioClip pickupSound;
    private MeshRenderer meshRenderer;
    private void Start()
    {
        playerManager= GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        meshRenderer=GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        transform.Rotate(rotate_axis*Time.deltaTime);
        
        if(playerManager.can_collect)
        {
            // collect point
            meshRenderer.material.color = collectable_color;
            rotate_axis.y = 270;
            bool touchingToPlayer = Physics.CheckSphere(transform.position, .2f, layerMask);
            if (touchingToPlayer && playerManager.can_collect)
            {
                Camera.main.GetComponent<AudioSource>().PlayOneShot((pickupSound));   
                playerManager.IncreaseHealth(2.0f);
                Destroy(this.gameObject);
            }
        }
        else
        {
            rotate_axis.y = 45;
            meshRenderer.material.color = nonCollectable_color;
        }
        
    }
    
}
