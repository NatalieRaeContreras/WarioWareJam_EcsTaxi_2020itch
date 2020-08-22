using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverState : StateMachineBehaviour
{
   public string mainMenuString = "Scenes/Story/Title";
   public string gameString = "Scenes/Story/Taxi";
   public int choice = 0;
   public bool selecting = true;
   public bool toMenu = false;

   private bool choiceMade = false;
   private bool choiceOperated = false;

   // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
   override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      SceneManager.LoadScene("Scenes/Story/GameOver");

      Toolbox.Instance.State.ResetTriggers();
   }

   //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
   override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      if (Input.anyKeyDown)
      {
         Application.Quit();
      }
   }

   //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
   override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      //if (GameState.Instance.state.GetBool("PlayAgain") == true)
      //{
      //   SceneManager.LoadScene(gameString);
      //}
      //else if (GameState.Instance.state.GetBool("ToMain") == true)
      //{
      //   SceneManager.LoadScene(mainMenuString);
      //}
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
