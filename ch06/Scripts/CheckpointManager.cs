using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public float MaxTimeToReachNextCheckpoint = 30f; //시간제한 30초
    public float TimeLeft = 30f;
    
    public CarAgent carAgent;

    public Checkpoint nextCheckPointToReach;
    
    private int CurrentCheckpointIndex;
    private List<Checkpoint> Checkpoints;
    private Checkpoint lastCheckpoint;

    public event Action<Checkpoint> reachedCheckpoint; 


    void Start()
    {
        Checkpoints = FindObjectOfType<Checkpoints>().checkPoints;
        ResetCheckpoints();
    }

    // 체크포인트의 인덱스를 0으로 초기화시키고, 남은시간도 초기화시킨다.

    public void ResetCheckpoints()
    {
        CurrentCheckpointIndex = 0;
        TimeLeft = MaxTimeToReachNextCheckpoint;
        
        SetNextCheckpoint();
    }

    private void Update()
    {
        //매초 시간이 깍이도록 한다.
        TimeLeft -= Time.deltaTime;

        //지정한 시간이 지나면 벌점을 주고 에피소드를 종료한다.
        if (TimeLeft < 0f)
        {
            carAgent.AddReward(-1f);
            carAgent.EndEpisode();
        }
    }

    public void CheckPointReached(Checkpoint checkpoint)
    {
        //체크포인트
        if (nextCheckPointToReach != checkpoint) 
        {
            return;
        }
        lastCheckpoint = Checkpoints[CurrentCheckpointIndex];
        reachedCheckpoint?.Invoke(checkpoint);
        CurrentCheckpointIndex++;

        if (CurrentCheckpointIndex >= Checkpoints.Count)
        {
            //carAgent.AddReward(TimeLeft / 30);         //시간에 대한 보상 추가
            carAgent.AddReward(1f);
            carAgent.EndEpisode();
        }
        else
        {
            carAgent.checkDirection();
            //carAgent.AddReward(TimeLeft / 30);     //시간에 대한 보상 추가
            carAgent.AddReward(1f);
            SetNextCheckpoint();
        }
    }


    public void dontGoBack()
    {
        if(CurrentCheckpointIndex == 0)
        {
            carAgent.AddReward(-1f);
        }
    }

    private void SetNextCheckpoint()
    {
        if (Checkpoints.Count > 0)
        {
            TimeLeft = MaxTimeToReachNextCheckpoint;
            nextCheckPointToReach = Checkpoints[CurrentCheckpointIndex];
            
        }
    }
}
