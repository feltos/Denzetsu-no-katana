using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    private Vector3 offset;
    [SerializeField]
    GameObject player;
    [SerializeField]float minPosY;

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
        
        transform.position = CameraPosition;
    }
}

