using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    float offset;
    [SerializeField]
    float speed;
    Vector3 previousCameraPos;

    void Update ()
    {
        Vector3 deltaPos = Camera.main.transform.position - previousCameraPos;
        offset += deltaPos.x * speed;
        GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        previousCameraPos = Camera.main.transform.position;
    }
}
