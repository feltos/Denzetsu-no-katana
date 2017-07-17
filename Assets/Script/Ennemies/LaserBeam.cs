using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    Rigidbody2D body;
    Vector2 direction;
    Vector2 movement;
    GameObject player;
    [SerializeField]
    float speed;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        direction = (player.transform.position - transform.position).normalized;
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        movement = new Vector2(speed * direction.x, 0.0f);
    }
    void FixedUpdate()
    {
        body.velocity = movement;
    }

}
