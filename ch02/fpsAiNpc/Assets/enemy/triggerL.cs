using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerL : MonoBehaviour
{

    [SerializeField] private Animator myani;

    [SerializeField] EnemyAI m_enemy = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myani.SetBool("crouch", true);
            m_enemy.triggerL = true;
            m_enemy.playerPosition = "triggerL";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myani.SetBool("crouch", false);
        }
    }
}
