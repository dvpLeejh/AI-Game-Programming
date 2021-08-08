using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontGoBack : MonoBehaviour
{
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CheckpointManager>() != null)
        {
            other.GetComponent<CheckpointManager>().dontGoBack();
        }
    }
    
    
}
