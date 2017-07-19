using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    float switchToMenuTimer;
    const float switchToMenuPeriod = 2f;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        Debug.Log(switchToMenuTimer);
        switchToMenuTimer += Time.deltaTime;
        if(switchToMenuTimer >= switchToMenuPeriod && (Input.anyKeyDown)||(Input.GetKeyDown("joystick button 0")))
        {
            SceneManager.LoadScene(0);
        }
	}
}
