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
      GameState.Instance.CurrentState = GameState.State.PostMinigame;
      GameVars.Instance.anim.CloseGameBoard();

      if (GameVars.Instance.scene.isLoaded && GameVars.Instance.scene.IsValid())
      {
         SceneManager.UnloadSceneAsync(GameVars.Instance.scene);
         Resources.UnloadUnusedAssets();
         GameVars.Instance.nextMinigame = MinigameManager.Instance.miniSceneName[Random.Range(0, MinigameManager.Instance.miniSceneName.Count)];
         //GameVars.Instance.nextMinigame = GameVars.Instance.sceneList[Random.Range(0, GameVars.Instance.sceneList.Count)];
      }
   }

   // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
   override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      if (GameVars.Instance.isGameOver)
      {
         GameState.Instance.state.SetTrigger("GameOver");
      }
      else if (GameState.Instance.timeInState >= timeToSetStateAtReady)
      {
         GameState.Instance.state.SetTrigger("Ready");
      }
      else if (GameState.Instance.timeInState >= timeToDisplayNextVerb)
      {
         GameVars.Instance.anim.DisplayGameVerb(GameVars.Instance.nextMinigame);
         GameVars.Instance.anim.OpenGameBoard();
      }

   }

   // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
   //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   //{
   //
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
