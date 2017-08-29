using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class GroundCheck : MonoBehaviour
{
    int groundedValue = 0;
    [SerializeField]
    Animator playerAnim;

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
            playerAnim.SetInteger("Saut", 0);
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
