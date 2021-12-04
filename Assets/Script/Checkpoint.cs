using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public bool checkpointCheck = true;
    
       

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Agent")
        {
            //checkpointCheck = false;
            //Debug.Log("touch");
        }
    }

    public void ResetCheckpoint()
    {
        checkpointCheck = true;
    }




}
