using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
   float offset;
    [SerializeField]
    float speed;

	void Update ()
    {
        offset += Input.GetAxis("Horizontal") * speed;
        GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
	}
}
