using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    int groundedValue = 0;

    void Start ()
    {
        
	}
	
	
	void Update ()
    {
      
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            groundedValue ++;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            groundedValue--;
        }
    }

    public int GetGroundedValue()
    {
        return groundedValue;
    }
}
