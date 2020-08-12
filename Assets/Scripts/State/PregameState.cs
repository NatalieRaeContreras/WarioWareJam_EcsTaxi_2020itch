using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PregameState : StateMachineBehaviour
{
   public int timeToWait = 5;

   // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
   override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      if (GameVars.Instance.isRestart)
      {
         GameVars.Instance.BroadcastMessage("Awake", SendMessageOptions.RequireReceiver);
         GameVars.Instance.BroadcastMessage("Start", SendMessageOptions.RequireReceiver);
         GameState.Instance.BroadcastMessage("Awake", SendMessageOptions.RequireReceiver);
         GameState.Instance.BroadcastMessage("Start", SendMessageOptions.RequireReceiver);
      }
      GameVars.Instance.Init();
      GameState.Instance.Init();

      GameState.Instance.CurrentState = GameState.State.Pregame;

      CanvasManager.Instance.Init();
      GameVars.Instance.timer.Reset();
      GameState.Instance.miniResult = GameState.State.None;

   }

   // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
   override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      if (GameState.Instance.timeInState >= 5.0f)
      {
         GameVars.Instance.anim.DisplayGameVerb(GameVars.Instance.nextMinigame);
         GameState.Instance.state.SetTrigger("Ready");
         GameVars.Instance.preMinigameLatch = false;
      }
      else if (GameState.Instance.timeInState >= 4.0f && !GameVars.Instance.preMinigameLatch)
      {
         GameVars.Instance.preMinigameLatch = true;
         GameVars.Instance.nextMinigame = MinigameManager.Instance.miniSceneName[Random.Range(0, MinigameManager.Instance.miniSceneName.Count)];
      }
      else if (GameState.Instance.timeInState >= 2.0f && !GameVars.Instance.preGameLatch)
      {
         GameVars.Instance.anim.ActivatePreGame();
         GameVars.Instance.preGameLatch = true;
      }
   }

   // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
   override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      GameVars.Instance.timer.Reset();
   }

   // OnStateMove is called right after Animator.OnAnimatorMove()
   //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   //{
   //    // Implement code that processes and affects root motion
   //}

   // OnStateIK is called right after Animator.OnAnimatorIK()
   //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   //{
   //    // Implement code that sets up animation IK (inverse kinematics)
   //}
}
