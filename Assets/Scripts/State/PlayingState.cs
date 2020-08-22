using UnityEngine;

public class PlayingState : StateMachineBehaviour
{
   // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
   override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      Toolbox.Instance.CurrentState = GameState.State.Playing;
      Toolbox.Instance.MiniManager.minigameScript.Active = true;
   }

   // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
   override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      if (Toolbox.Instance.MiniManager.timer.Done && Toolbox.Instance.MiniManager.result != MinigameManager.MinigameState.None)
      {
         Toolbox.Instance.State.SetTrigger(GameState.Trigger.Post);
      }
   }

   //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
   override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      if (Toolbox.Instance.MiniManager.result == MinigameManager.MinigameState.Win)
      {
         Toolbox.Instance.AssetAnim.ResultAnim(true);
      }
      else
      {
         Toolbox.Instance.Vars.health--;
         Toolbox.Instance.AssetAnim.ResultAnim(false);
      }
      Toolbox.Instance.State.SetTrigger(GameState.Trigger.Post);
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
