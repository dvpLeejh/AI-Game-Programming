using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class CarAgent : Agent
{
   public CheckpointManager _checkpointManager;
   private CarController _carCtrl;
   private bool isFront;
   private Rigidbody rb;


   //called once at the start
   public override void Initialize()
   {
      _carCtrl = GetComponent<CarController>();
      rb = GetComponent<Rigidbody>();
   }
   
   //Called each time it has timed-out or has reached the goal
   public override void OnEpisodeBegin()
   {
      rb.velocity = Vector3.zero;
      rb.angularVelocity = Vector3.zero;
      _checkpointManager.ResetCheckpoints();
      _carCtrl.Respawn();
   }


      //Collecting extra Information that isn't picked up by the RaycastSensors
      public override void CollectObservations(VectorSensor sensor)
      {
         Vector3 diff = _checkpointManager.nextCheckPointToReach.transform.position - transform.position;
         
         sensor.AddObservation(diff); // 자동차의 다음체크포인트의 백터좌표 차는 레이케스트가 계산해주는게 아니므로 추가해준다. 
         //sensor.AddObservation(_checkpointManager.TimeLeft / 30); // 시간에 대한 보상
         sensor.AddObservation(rb.velocity.magnitude); // 자동차의 속도를 고려하도록 한다.(커브를 위해)

         AddReward(-0.001f); // 자동차가 활발하게 움직이게끔 보상을 조금 줄인다.
      }

      //Processing the actions received
      public override void OnActionReceived(ActionBuffers actions)
      {
         var  input = actions.ContinuousActions;
         
         _carCtrl.GetInput(input[0], input[1]); // 이동에 필요한 입력받은 값을 전달한다.
         
         if(input[0] < 0 )
         {
            isFront = false;
         }
         else
         {
            isFront = true;
         }
         AddReward(-.001f);
   
      }
      
      //For manual testing with human input, the actionsOut defined here will be sent to OnActionRecieved
      //간단히 말에 테스트를 위한 입력을 받는다.
      public override void Heuristic(in ActionBuffers actionsOut)
      {
         var action = actionsOut.ContinuousActions;

         action[1] = Input.GetAxis("Horizontal");
         action[0] = Input.GetAxis("Vertical");
      }

      // 차가 뒤쪽으로 체크포인트들을 통과했을때는 벌점을 부과하여 그러지 못하도록 한다.
      public void checkDirection()
      {
         if(isFront == false)
         {
            AddReward(-1f);
         }
      }
}
