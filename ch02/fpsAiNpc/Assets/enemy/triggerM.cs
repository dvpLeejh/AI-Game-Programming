using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerM : MonoBehaviour
{

    [SerializeField] private Animator myani;

    [SerializeField] EnemyAI m_enemy = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myani.SetBool("backshot", true);
            m_enemy.triggerM = true;
            m_enemy.playerPosition = "triggerM";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myani.SetBool("backshot", false);
        }
    }
}
