using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField]List<GameObject> chunks =new List<GameObject>();
    float chunkLength = 60.0f;
    int idRandom;
    int idLastChunk;
    GameObject newChunk;
    [SerializeField]
    GameObject startZone;
    [SerializeField]
    GameObject endZone;
    [SerializeField]
    int nmbOfChunks;

    void Start ()
    { 
       Instantiate(startZone);
       for (float i = chunkLength; i <= chunkLength * nmbOfChunks; i = i + chunkLength)
       {
           idRandom = Random.Range(0, chunks.Count);
            if(idRandom != idLastChunk)
            {
                newChunk = Instantiate(chunks[idRandom]);
                idLastChunk = idRandom;
            }
           else
            {
                newChunk = Instantiate(chunks[idRandom + 1]);
                idLastChunk = idRandom + 1;
            }
           newChunk.transform.position = new Vector2(i, 0.0f);
       }
        Instantiate(endZone,newChunk.transform.position + new Vector3 (chunkLength,0.0f,0.0f),transform.rotation);
    }
}
