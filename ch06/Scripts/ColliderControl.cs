using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderControl : MonoBehaviour
{
    public CarAgent carAgent;

    private void OnCollisionEnter(Collision other) {
        if(other.collider.CompareTag("Fence"))
        {
            carAgent.AddReward(-1f);
        }
    } 

    private void OnCollisionStay(Collision other) {
        if(other.collider.CompareTag("Fence"))
        {   
            carAgent.AddReward(-0.5f);
        }
    }
    
}
