using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowingCamera : MonoBehaviour
{

    private Vector3 offset;
    [SerializeField]
    GameObject player;
    public float minPosY;
    public float maxPosY;
    public float minPosX;
    public float maxPosX;
    [SerializeField]
    GameManager gameManager;

	void Start ()
    {
        player = GameObject.Find("Player");
        offset = transform.position - player.transform.position;
    }
	
	
	void Update ()
    {
		
	}

    void LateUpdate()
    {
        Vector3 CameraPosition = player.transform.position + offset;
        if (CameraPosition.y < minPosY)
        {
            CameraPosition.y = minPosY;
        }
        if (CameraPosition.y > maxPosY)
        {
            CameraPosition.y = maxPosY;
        }
        if (CameraPosition.x < minPosX)
        {
            CameraPosition.x = minPosX;
        }
        if (CameraPosition.x > maxPosX)
        {
            CameraPosition.x = maxPosX;
        }
        transform.position = CameraPosition;
    }
}

