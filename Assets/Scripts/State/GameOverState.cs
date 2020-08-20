using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverState : StateMachineBehaviour
{
   public string mainMenuString = "Scenes/Story/Title";
   public string gameString = "Scenes/Story/Taxi";
   public int choice = 0;

   private AsyncOperation a_zero;
   private Scene s_zero;
   private AsyncOperation a_one;
   private Scene s_one;

   private bool choiceMade = false;
   private bool choiceOperated = false;

   // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
   override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      Toolbox.Instance.State.ResetTriggers();
      a_zero = SceneManager.LoadSceneAsync(mainMenuString);
      a_zero.completed += (operation) =>
      {
         s_zero = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
      };
      a_zero.allowSceneActivation = false;

      a_one = SceneManager.LoadSceneAsync(gameString);
      a_one.completed += (operation) =>
      {
         s_one = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
      };
      a_one.allowSceneActivation = false;

   }

   //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
   override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      if (choice < 0)
      {
         choice = 2;
      }
      if (choice > 2)
      {
         choice = 0;
      }

      if (!choiceOperated)
      {
         switch (choice)
         {
            case 0:
               Toolbox.Instance.SetupFromGameOverToMainMenu();
               a_zero.allowSceneActivation = true;
               break;
            case 1:
               a_one.allowSceneActivation = true;
               Toolbox.Instance.SetupFromGameOverToPlayAgain();
               break;
            default:
               Toolbox.Instance.State.SetTrigger(GameState.Trigger.Exit);
               break;
         }
         choiceOperated = true;
      }
      else if (!choiceMade)
      {
         if (Input.GetKeyDown(KeyCode.DownArrow))
         {
            choice++;
            choiceMade = true;
         }
         else if (Input.GetKeyDown(KeyCode.UpArrow))
         {
            choice--;
            choiceMade = true;
         }
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
