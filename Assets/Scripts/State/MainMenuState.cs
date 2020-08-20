using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuState : StateMachineBehaviour
{
   public string targetScene = "Scenes/Story/Taxi";
   public string cutsceneScene = "";


   bool cutscene = false;
   bool latch = false;
   bool proceed = false;
   bool loading = false;
   AsyncOperation async;

   // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
   override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      cutscene = !(cutsceneScene.Equals("") || cutsceneScene.Equals(null));
      Toolbox.Instance.State.current = GameState.State.Menu;
   }

   // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
   override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      if (proceed && !cutscene)
      {
         async.allowSceneActivation = true;
         Toolbox.Instance.State.SetTrigger(GameState.Trigger.Init);
      }
      else if (proceed && cutscene)
      {
         async.allowSceneActivation = true;
         Toolbox.Instance.State.SetTrigger(GameState.Trigger.Cutscene);
      }
      else
      {
         if (!loading && !cutscene)
         {
            async = SceneManager.LoadSceneAsync(targetScene);
            async.allowSceneActivation = false;
            loading = true;
         }
         else
         {
            if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X)) && latch == false)
            {
               latch = true;
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && latch == false)
            {
               Toolbox.Instance.State.SetTrigger(GameState.Trigger.Exit);
               latch = true;
            }

            if (latch)
            {
               if (async.progress >= 0.9f)
               {
                  proceed = true;
               }
            }
         }
      }
   }

   // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
   override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      latch = false;
      proceed = false;
      loading = false;
      async = null;
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
