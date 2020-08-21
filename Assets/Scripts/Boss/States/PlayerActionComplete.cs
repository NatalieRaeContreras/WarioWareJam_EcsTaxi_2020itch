using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerActionComplete : StateMachineBehaviour
{
   public bool once;

   // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
   override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      Toolbox.Instance.AssetAnim.GameBoard.ResetTrigger("Open");
      Toolbox.Instance.AssetAnim.CloseGameBoard();
      Toolbox.Instance.AssetAnim.CloseGameWindow();

      Toolbox.Instance.BossScript.bossStateMachine.ResetTrigger("p_Attack");
      Toolbox.Instance.BossScript.bossStateMachine.ResetTrigger("p_Look");
      Toolbox.Instance.BossScript.bossStateMachine.ResetTrigger("p_Talk");

      once = true;
   }

   // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
   override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      if (Toolbox.Instance.Vars.TaxiDefeated)
      {
         Toolbox.Instance.BossScript.bossStateMachine.SetBool("Base_toCombat", true);
      }
      else
      {
         if (once)
         {
            Toolbox.Instance.BossScript.LoadTaxiAttack();
            once = false;
         }

         if (Toolbox.Instance.BossScript.taxiAttackScene.IsValid())
         {
            Toolbox.Instance.BossScript.bossStateMachine.SetTrigger("Base_toCombat");
         }
         else if (Toolbox.Instance.BossScript.taxiAttackAsyncOp != null)
         {
            if (Toolbox.Instance.BossScript.taxiAttackAsyncOp.progress >= 0.9f &&
                Toolbox.Instance.BossScript.taxiAttackAsyncOp.allowSceneActivation == false)
            {
               Toolbox.Instance.BossScript.taxiAttackAsyncOp.allowSceneActivation = true;
            }
         }
      }
   }

   // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
   override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      Toolbox.Instance.BossScript.bossStateMachine.ResetTrigger("p_ActionComplete");
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
