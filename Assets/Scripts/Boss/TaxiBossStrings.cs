using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaxiBossStrings
{
   public List<Text> lines = new List<Text>();

   public int enemyDialogueState = 0;

   public List<string> playerDialogue = new List<string>();

   public List<string> enemyDialogueIntro = new List<string>();
   public List<string> enemyDialogueOne = new List<string>();
   public List<string> enemyDialogueTwo = new List<string>();
   public List<string> enemyDialogueThree = new List<string>();
   public List<string> enemyDialogueWin = new List<string>();
   public List<string> enemyDialogueLose = new List<string>();
   public List<string> playerChoices = new List<string>();

   // Start is called before the first frame update
   public void Init()
   {
      enemyDialogueIntro.Add("I always hated drives");
      enemyDialogueIntro.Add("through the desert...");
      enemyDialogueIntro.Add("");

      enemyDialogueOne.Add("Don't scratch the");
      enemyDialogueOne.Add("paint job, bub!");
      enemyDialogueOne.Add("");

      enemyDialogueTwo.Add("(the ecs-taxi");
      enemyDialogueTwo.Add("changes the mix tape");
      enemyDialogueTwo.Add("to the next track)");

      enemyDialogueThree.Add("Let's not get");
      enemyDialogueThree.Add("too hasty friend-o");
      enemyDialogueThree.Add("");

      enemyDialogueWin.Add("How about we call");
      enemyDialogueWin.Add("this a draw?");
      enemyDialogueWin.Add("");

      playerDialogue.Add("You try negotiating");
      playerDialogue.Add("...");
      playerDialogue.Add("It's not interested");

      playerChoices.Add("Attack");
      playerChoices.Add("Look");
      playerChoices.Add("Talk");
   }

   public void PlayerTalk()
   {
      lines[0].text = playerDialogue[0];
      lines[1].text = playerDialogue[1];
      lines[2].text = playerDialogue[2];
   }

   public void GetPlayerChoices()
   {
      lines[0].text = playerChoices[0];
      lines[1].text = playerChoices[1];
      lines[2].text = playerChoices[2];
   }

   public void GenerateTaxiLookAt()
   {
      lines[0].text = "The Ecs-Taxi";
      lines[1].text = "Health: " + Toolbox.Instance.BossScript.taxiHealth;
      lines[2].text = "Mood: Displeased";
   }

   public void CopyTextFromStringTable()
   {
      int index = 0;
      foreach (Text txtObj in Toolbox.Instance.BossScript.lines)
      {
         txtObj.text = Toolbox.Instance.BossScript._taxiStrings.lines[index].text;
         index++;
      }
   }

   public void AdvanceEnemyDialogueState()
   {
      if (Toolbox.Instance.BossScript.taxiHealth <= 0)
      {
         enemyDialogueState = -1;
      }
      else if (Toolbox.Instance.BossScript.playerHealth <= 0)
      {
         enemyDialogueState = 4;
      }
      else if (enemyDialogueState < 3)
      {
         enemyDialogueState++;
      }
   }

   public void FetchEnemyDialogue()
   {
      switch (enemyDialogueState)
      {
         case 0:
            lines[0].text = enemyDialogueIntro[0];
            lines[1].text = enemyDialogueIntro[1];
            lines[2].text = enemyDialogueIntro[2];
            break;
         case 1:
            lines[0].text = enemyDialogueOne[0];
            lines[1].text = enemyDialogueOne[1];
            lines[2].text = enemyDialogueOne[2];
            break;
         case 2:
            lines[0].text = enemyDialogueTwo[0];
            lines[1].text = enemyDialogueTwo[1];
            lines[2].text = enemyDialogueTwo[2];
            break;
         case 3:
            lines[0].text = enemyDialogueThree[0];
            lines[1].text = enemyDialogueThree[1];
            lines[2].text = enemyDialogueThree[2];
            break;
         case 4:
            lines[0].text = enemyDialogueWin[0];
            lines[1].text = enemyDialogueWin[1];
            lines[2].text = enemyDialogueWin[2];
            break;
         case -1:
            lines[0].text = enemyDialogueLose[0];
            lines[1].text = enemyDialogueLose[1];
            lines[2].text = enemyDialogueLose[2];
            break;
         default:
            lines[0].text = "yeah this shouldnt be here";
            lines[1].text = "yeah this shouldnt be here";
            lines[2].text = "yeah this shouldnt be here";
            break;
      }
   }
}