using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceEnemy : AI
{
    Vector2 direction;
    Vector2 movement;
    bool detect = false;
    float minRange = 8f;
    float midRange = 15f;
    [SerializeField]
    float speed;
    [SerializeField]
    Rigidbody2D body;

    float fireCooldown = 0.0f;
    float firePeriod = 1.2f;

    [SerializeField]
    GameObject bullet;
    Vector2 bulletDirection;

	void Start ()
    {
        this.maxRange = 18f;
        player = GameObject.Find("Player");
    }
	

	void Update ()
    {
        
        direction = (player.transform.position - transform.position).normalized;
        movement = new Vector2(direction.x * speed, 0.0f);
	    if(Vector3.Distance(transform.position,player.transform.position) < maxRange)
        {
            detect = true;
        }
        if (Vector3.Distance(transform.position, player.transform.position) > maxRange)
        {
            detect = false;
        }
        if (detect && Vector3.Distance(transform.position, player.transform.position) <= midRange)
        {
            fireCooldown += Time.deltaTime;
            Debug.Log(fireCooldown);
            if (fireCooldown >= firePeriod)
            {
                Instantiate(bullet, transform.position, transform.rotation);
                fireCooldown = 0.0f;
            }
        }
    }

    void FixedUpdate()
    {
        if (detect && Vector3.Distance(transform.position, player.transform.position) > minRange)
        {
            body.velocity = movement;
        }
    }
}
