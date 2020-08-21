using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostState : StateMachineBehaviour
{
   public float timeToSetStateAtReady = 5.0f;
   public float timeToDisplayNextVerb = 4.0f;

   // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
   override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      if (Toolbox.Instance.Vars.isGameOver)
      {
         Toolbox.Instance.CurrentState = GameState.State.Gameover;
      }
      else
      {
         Toolbox.Instance.CurrentState = GameState.State.PostMinigame;
         Toolbox.Instance.AssetAnim.CloseGameBoard();
         Toolbox.Instance.AssetAnim.CloseGameWindow();
         Toolbox.Instance.State.ResetTriggers();
      }
   }

   // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
   //override public void  OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   //{
   //}

   // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
   override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      Toolbox.Instance.MiniManager.UnloadCurrentMinigame();
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
