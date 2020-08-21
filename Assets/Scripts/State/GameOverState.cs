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
   private GameObject menu;
   private GameObject replay;

   // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
   override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      SceneManager.LoadScene("Scenes/Story/GameOver");

      Toolbox.Instance.State.ResetTriggers();

      bool victory = Toolbox.Instance.Vars.TaxiDefeated;
      bool defeat = Toolbox.Instance.Vars.PlayerDefeated;

      foreach (var root in SceneManager.GetActiveScene().GetRootGameObjects())
      {
         if (root.CompareTag("Victory") && victory)
         {
            root.gameObject.SetActive(true);
         }
         if (root.CompareTag("Defeat") && defeat)
         {
            root.gameObject.SetActive(false);
         }
      }

      menu = GameObject.FindGameObjectWithTag("Menu");
      replay = GameObject.FindGameObjectWithTag("Replay");

      selecting = true;
   }

   //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
   override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      if (selecting)
      {
         if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow))
         {
            toMenu = !toMenu;
            if (toMenu)
               choice = 0;
            else
               choice = 1;
         }
         else if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Z))
         {
            selecting = false;
         }
      }
      else if (!choiceOperated)
      {
         switch (choice)
         {
            case 0:
               Toolbox.Instance.SetupFromGameOverToMainMenu();
               SceneManager.LoadScene(mainMenuString);
               break;
            case 1:
               Toolbox.Instance.SetupFromGameOverToPlayAgain();
               SceneManager.LoadScene(gameString);
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

      menu.GetComponent<Animator>().SetBool("Selected", toMenu);
      replay.GetComponent<Animator>().SetBool("Selected", !toMenu);

      if (menu.GetComponent<Animator>().GetBool("Selected"))
      {
         menu.GetComponentInChildren<Animator>().GetComponentInChildren<Image>().color = Color.white;
         replay.GetComponentInChildren<Animator>().GetComponentInChildren<Image>().color = Color.clear;
      }
      else
      {
         menu.GetComponentInChildren<Animator>().GetComponentInChildren<Image>().color = Color.clear;
         replay.GetComponentInChildren<Animator>().GetComponentInChildren<Image>().color = Color.white;
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
