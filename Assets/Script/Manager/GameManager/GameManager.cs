using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    PlayerCharacter playerManager;
    [SerializeField]
    SpriteRenderer fadeTexture;
    [SerializeField]
    FollowingCamera followingCamera;
    float fadeOutPeriod = 2f;
    bool fadingOut = false;

    int sceneIndex;

    public enum SwitchLevel
    {
        LEVEL1,
        BOSS_BATTLE1
    }

    public SwitchLevel SwitchCameraLimit = SwitchLevel.LEVEL1;
    
	void Start ()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
	
	void Update ()
    {
        if(fadingOut)
        {
            UpdateFadeOut();
        }
    }

    public void Restart()
    {    
        SceneManager.LoadScene(sceneIndex);
    }

    public void GameOver()
    {
        SceneManager.LoadScene(0);
    }

    public void StartFadeOut()
    {
        fadingOut = true;
    }

    void UpdateFadeOut()
    {
       fadeTexture.color = new Color(fadeTexture.color.r, fadeTexture.color.g,
       fadeTexture.color.b, fadeTexture.color.a + Time.deltaTime / fadeOutPeriod);
        if (fadeTexture.color.a >= 1.0f)
        {
            SceneManager.LoadScene(sceneIndex + 1);
        }
    }

   public void SwitchCamera()
    {
        switch(SwitchCameraLimit)
        {
            case SwitchLevel.LEVEL1:
                {
                    followingCamera.minPosX = -14.08f;
                    followingCamera.maxPosX = 54;
                    followingCamera.minPosY = 0f;
                    followingCamera.maxPosY = 7f;
                }
                break;
            case SwitchLevel.BOSS_BATTLE1:
                {
                    followingCamera.minPosX = -13.17f;
                    followingCamera.maxPosX = 20f;
                    followingCamera.minPosY = 0f;
                    followingCamera.maxPosY = 5.18f;
                }
                break;
        }
    }
}
