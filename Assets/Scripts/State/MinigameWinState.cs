using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameWinState : StateMachineBehaviour
{
   // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
   override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      GameState.Instance.CurrentState = GameState.State.Win;
      GameState.Instance.miniResult = GameState.State.Win;
   }

   // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
   override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      if (GameVars.Instance.timer.Done)
      {
         GameState.Instance.state.SetTrigger("Done");
      }
   }

   // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
   override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      GameVars.Instance.timer.Reset();
      GameVars.Instance.timer.Hide();
      GameVars.Instance.anim.ResultAnim(true);
      GameState.Instance.state.SetTrigger("Win");
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
