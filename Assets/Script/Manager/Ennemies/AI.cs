using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{

    protected GameObject player;
    [SerializeField]protected float maxRange = 10.0f;
    [SerializeField]
    GameManager gameManager;
    
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
            Destroy(gameObject);
        }
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
      if(bullet != null && bullet.GetHit())
        {
            Destroy(gameObject);
        }
    }
}
