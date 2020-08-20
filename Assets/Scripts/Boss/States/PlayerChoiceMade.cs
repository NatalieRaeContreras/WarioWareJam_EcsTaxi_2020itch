using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChoiceMade : StateMachineBehaviour
{

   public int choice = -1;
   public bool choiceLocked;

   // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
   override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      choiceLocked = false;
      Toolbox.Instance.BossScript.bossStateMachine.ResetTrigger("p_ActionComplete");
   }

   // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
   override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      if (!choiceLocked)
      {
         choiceLocked = true;
         switch (choice)
         {
            case 0:
               Toolbox.Instance.BossScript.bossStateMachine.SetTrigger("p_Attack");
               break;
            case 1:
               Toolbox.Instance.BossScript.bossStateMachine.SetTrigger("p_Look");
               break;
            case 2:
               Toolbox.Instance.BossScript.bossStateMachine.SetTrigger("p_Talk");
               break;
            default:
               choiceLocked = false;
               break;
         }
      }
   }

   // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
   override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
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
