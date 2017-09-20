using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField]
    AudioClip playerAttack;
    [SerializeField]
    AudioClip fleche;
    [SerializeField]
    AudioClip course;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("Multiple instances of SoundEffects");
        }
        Instance = this;
    }
    void Start ()
    {
        DontDestroyOnLoad(this);
	}

    private void MakeSound(AudioClip OriginalClip)
    {
        AudioSource.PlayClipAtPoint(OriginalClip, transform.position);
    }
    public void PlayerAttack()
    {
        MakeSound(playerAttack);
    }
    public void Fleche()
    {
        MakeSound(fleche);
    }
    public void Course()
    {
        MakeSound(course);
    }

}
