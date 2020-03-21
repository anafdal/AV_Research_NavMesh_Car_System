using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleRoad : MonoBehaviour
{
    public GameObject[] newRoad;//list of startpoints and endpoints
    public GameObject[] newEnd;
   
    public static Vector3 newStart;//get start position and stop position together
    public static Vector3 newStop;
    private int random;
   
    
    void Update()//do update for multiples
    {
        

        random = returnRandomPosition(newRoad);//call function to receive a random position
        newStart = new Vector3(newRoad[random].transform.position.x, 0.0f, newRoad[random].transform.position.z);
        newStop = new Vector3(newEnd[random].transform.position.x, 0.0f, newEnd[random].transform.position.z);

        // Debug.Log(newRoad[random].name);
        // Debug.Log(newStop);
        // Debug.Log(newEnd[random].transform.position);
    }


    private int returnRandomPosition(GameObject [] newRoad)
    {
        int range = Random.Range(0, newRoad.Length);//randomly generated value
        //Debug.Log(range);
        return range;
    }

}
