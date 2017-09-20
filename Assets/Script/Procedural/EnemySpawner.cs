using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    List<GameObject> enemies = new List<GameObject>();
    int idRandom;
    int enemiesRandom;
    List<Transform> childs = new List<Transform>();

    private void Awake()
    {
       foreach(Transform child in transform)
        {
            childs.Add(child);
        }
    }

    void Start ()
    {
		for(int i = 0; i < childs.Count; i++)
        {
            idRandom = Random.Range(0, 2);
            enemiesRandom = Random.Range(0, enemies.Count);
       
            if (idRandom == 1)
            {
                Instantiate(enemies[enemiesRandom], childs[i].transform.position, childs[i].transform.rotation);
            }
           
        }
	}
		
	void Update ()
    {
		
	}
}
