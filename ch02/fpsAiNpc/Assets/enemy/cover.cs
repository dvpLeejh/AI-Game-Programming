using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cover : MonoBehaviour
{

    [SerializeField] private Animator myani;

    [SerializeField] EnemyAI m_enemy = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_enemy.cover = true;
            myani.SetBool("sfire", true);
        }
    }
}
