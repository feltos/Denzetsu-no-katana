using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedEnnemies : MonoBehaviour
{

	
	void Start ()
    {
		
	}

   
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D collision)
    { 
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Destroy(gameObject);
        }
    }
}
