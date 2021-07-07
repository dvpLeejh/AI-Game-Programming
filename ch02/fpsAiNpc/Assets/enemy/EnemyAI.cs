using UnityEngine;
using System;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour {

    private int Health = 100;
    private bool statePassive;
    private bool stateAggressive;
    private bool stateDefensive;

    private bool morningTime;
    private bool afternoonTime;
    private bool eveningTime;
    private bool nightTime;
    private int now;

    public bool triggerL;
    public bool triggerR;
    public bool triggerM;

    public Transform wallPosition;
    public Transform buildingPosition;
    public Transform triggerLPosition;

    public bool cover;
    private float speed = 0.5f;
    private float speedBack = 0.5f;
    private float walk;
    private float walkBack;

    public Transform playerSoldier;
    public string playerPosition;

    private int rndNumber;

    public NavMeshAgent agent;

    [SerializeField] private Animator myani;

    // Use this for initialization
    void Start() {

        now = int.Parse(DateTime.Now.ToString(("HH")));
        if ((now >= 7) && (now < 12))
        {
            morningTime = true;
        }
        else if ((now >=12) && (now < 18))
        {
            afternoonTime = true;
        }
        else if ((now >=18) && (now < 24))
        {
            eveningTime = true;
        }
        else
        {
            nightTime = true;
        }



        statePassive = true;

        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update() {

        rndNumber = UnityEngine.Random.Range(0, 100); //**시간 간격 두기**


        // The AI will remain passive until an interaction with the player occurs
        if (Health == 100 && triggerL == false && triggerR == false && triggerM == false)
            {
                statePassive = true;
                stateAggressive = false;
                stateDefensive = false;

            Passive();
            }

            // 방어모드
            if (triggerL == true || triggerM == true)
            {
                statePassive = false;
                stateAggressive = false;
                stateDefensive = true;

                Defensive();
            }

            // 공격모드
            if (triggerR == true)
            {
                statePassive = false;
                stateAggressive = true;
                stateDefensive = false;

            Aggressive();
        }

        walk = speed * Time.deltaTime;
        walkBack = speedBack * Time.deltaTime;
        
    }

    void Passive() {

        if (eveningTime == true && rndNumber > 13) { // We have 87% of chance
            myani.SetBool("stretch", true);
        }

        if (eveningTime == true && rndNumber <= 13 && rndNumber < 3) { // We have 10% of chance
            
        }

        if (eveningTime == true && rndNumber <= 3) { // We have 3% of chance
            myani.SetBool("drink", true);
        }
}
 /*
       if(afternoonTime == true && rndNumber > 52){ // We have 48% of chance
            
       }

       if(afternoonTime == true && rndNumber =< 34 && rndNumber < 2){ // We have 32% of chance
            
       }

       if(afternoonTime == true && rndNumber <= 2){ // We have 2% of chance
            
       }

       if(nightTime == true && rndNumber > 65){ // We have 35% of chance
            
       }

       if(nightTime == true && rndNumber =< 65 && rndNumber < 25){ // We have 40% of chance
            
       }

       if(nightTime == true && rndNumber <= 25){ // We have 25% of chance
            
       }
    }
    */

    void Defensive () {

        if (playerPosition == "triggerL")
        { // Check if player is currently located on the triggerR position
            transform.LookAt(playerSoldier); // Face the direction of the player

            if (cover == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, wallPosition.position, walk);

                if(transform.position == wallPosition.position)
                {
                    myani.SetBool("idlecrouch", true);
                }
            }

            if (cover == true)
            {
                coverFire();
            }

            if (transform.position == buildingPosition.position)
            {
                myani.SetBool("die", true);
            }
        }

        if (playerPosition == "triggerM")
        {
            transform.LookAt(playerSoldier); // Face the direction of the player
            transform.position = Vector3.MoveTowards(transform.position, buildingPosition.position, walkBack);
            backwardsFire();
        }
    }

    void Aggressive () {

        if (playerPosition == "triggerR")
        {
            transform.LookAt(playerSoldier); // Face the direction of the player
            frontFire();
        }

    }
        

    void coverFire () {
        // Here we can write the necessary code that makes the enemy firing while in cover position.
    }

    void backwardsFire () {
        // Here we can write the necessary code that makes the enemy firing while going back..
    }

    void frontFire() {
        agent.SetDestination(playerSoldier.position);
   
    }
}
