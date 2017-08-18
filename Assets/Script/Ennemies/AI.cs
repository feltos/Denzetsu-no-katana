﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{

    protected GameObject player;
    [SerializeField]protected float maxRange = 10.0f;
    [SerializeField]
    GameManager gameManager;
    protected bool fall = false;
    
	void Start ()
    {

	}
	
	void Update ()
    {
       
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
      if(collision.gameObject.layer == LayerMask.NameToLayer("Player") && collision.isTrigger)
        {
            Destroy(GetComponent<Collider2D>());
            Destroy(gameObject,0.3f);
        }

        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
      if(bullet != null && bullet.GetHit())
        {
            Destroy(gameObject);
        }
 
        if (collision.gameObject.layer == LayerMask.NameToLayer("Fall"))
        {
            fall = true;
        }  
}
}
