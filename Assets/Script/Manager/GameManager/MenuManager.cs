using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

	
	void Start ()
    {
		
	}
		
	void Update ()
    {
		
	}

    public void Commencer()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Quitter()
    {
        Application.Quit();
    }
}
