using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerR : MonoBehaviour
{

    [SerializeField] private Animator myani;

    [SerializeField] EnemyAI m_enemy = null;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            myani.SetBool("fire", true);
            m_enemy.triggerR = true;
            m_enemy.playerPosition = "triggerR";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            myani.SetBool("fire", false);
        }
    }
}
