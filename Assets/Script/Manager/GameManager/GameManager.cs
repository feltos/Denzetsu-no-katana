using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    PlayerCharacter playerManager;

    int scene;
    
	void Start ()
    {

	}
	
	void Update ()
    {
        
	}

    public void Restart()
    {
        scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene,LoadSceneMode.Single);
    }

    public void GameOver()
    {
        SceneManager.LoadScene(0);
    }
}
