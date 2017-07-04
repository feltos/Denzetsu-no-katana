using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    PlayerManager playerManager;
    
	void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
