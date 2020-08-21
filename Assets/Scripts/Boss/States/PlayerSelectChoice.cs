using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectChoice : StateMachineBehaviour
{
   public int selectedOption;
   public bool selecting;
   public List<Image> choices = new List<Image>();

   // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
   override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      selecting = true;
      selectedOption = 0;

      Toolbox.Instance.BossScript.DisplaySelectionIndicators(selectedOption);

      Toolbox.Instance.BossScript.DisplayTextBox();
      Toolbox.Instance.BossScript._taxiStrings.GetPlayerChoices();
      //Toolbox.Instance.BossScript._taxiStrings.CopyTextFromStringTable();
      Toolbox.Instance.BossScript.DisplayTextToRead();
      Toolbox.Instance.BossScript.bossStateMachine.ResetTrigger("Base_toPlayer");

   }

   // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
   override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      if (selectedOption < 0)
      {
         selectedOption = 2;
      }
      if (selectedOption > 2)
      {
         selectedOption = 0;
      }

      Toolbox.Instance.BossScript.HideSelectionIndicators();
      Toolbox.Instance.BossScript.DisplaySelectionIndicators(selectedOption);

      if (selecting)
      {
         if (Input.GetKeyDown(KeyCode.DownArrow))
         {
            selectedOption++;
         }
         else if (Input.GetKeyDown(KeyCode.UpArrow))
         {
            selectedOption--;
         }
         else if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X)))
         {
            selecting = false;
            Toolbox.Instance.BossScript.bossStateMachine.SetTrigger("p_ChoiceMade");
            Toolbox.Instance.Vars.playerChoiceBoss = selectedOption;
         }
      }
   }

   // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
   override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      Toolbox.Instance.BossScript.HideSelectionIndicators();
   }
}