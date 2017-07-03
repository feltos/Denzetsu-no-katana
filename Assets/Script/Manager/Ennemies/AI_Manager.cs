using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Manager : MonoBehaviour
{

    [SerializeField]protected GameObject player;
    [SerializeField]protected float minRange = 0.1f;
    [SerializeField]protected float maxRange = 10.0f;
    
	void Start ()
    {
		
	}
	
	void Update ()
    {
        
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Destroy(gameObject);
        }
    }
}
