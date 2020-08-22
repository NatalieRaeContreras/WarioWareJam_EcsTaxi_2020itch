using UnityEngine;

public class InitializeGameState : StateMachineBehaviour
{
   public int timeToWait = 5;
   public int miniIndex = 0;

   // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
   override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      Toolbox.Instance.CurrentState = GameState.State.Init;

      if (Toolbox.Instance.Vars.isRestart || Toolbox.Instance.State.PreviousState == GameState.State.Menu)
      {
         Toolbox.Instance.State.BroadcastMessage("Awake", SendMessageOptions.RequireReceiver);
         Toolbox.Instance.State.BroadcastMessage("Start", SendMessageOptions.RequireReceiver);
         Toolbox.Instance.State.Init();
         Toolbox.Instance.Canvas.Init();
         Toolbox.Instance.Vars.Init();
         Toolbox.Instance.AssetAnim.Init();
         Toolbox.Instance.AssetAnim.ActivatePreGame();
         Toolbox.Instance.MiniManager.Init();
         Toolbox.Instance.MiniManager.result = MinigameManager.MinigameState.None;
      }

      Toolbox.Instance.State.SetTrigger(GameState.Trigger.Pregame);
   }

   // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
   //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   //{
   //}

   // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
   //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   //{
   //}

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
