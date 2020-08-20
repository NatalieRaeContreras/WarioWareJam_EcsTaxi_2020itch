using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookState : StateMachineBehaviour
{
   public bool textAdvanced;
   public bool done;

   // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
   override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      Toolbox.Instance.BossScript.DisplayTextBox();
      Toolbox.Instance.BossScript._taxiStrings.GenerateTaxiLookAt();
      Toolbox.Instance.BossScript._taxiStrings.CopyTextFromStringTable();
      Toolbox.Instance.BossScript.DisplayTextToRead();
      Toolbox.Instance.BossScript.DisplayTextAdvanceIndication();

      textAdvanced = false;
      done = false;
   }

   // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
   override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      if (textAdvanced && !done)
      {
         Toolbox.Instance.BossScript.bossStateMachine.SetTrigger("p_ActionComplete");
      }
      else if (done)
      {
         ; //do nothing
      }
      else if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X)))
      {
         textAdvanced = true;
      }
   }

   // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
   override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      Toolbox.Instance.BossScript.CloseTextBox();
      Toolbox.Instance.BossScript.HideTextAdvanceIndication();
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
