using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TaxiBossStrings
{
   public List<Text> lines = new List<Text>();

   public List<string> playerDialogue = new List<string>();

   public List<string> enemyDialogueIntro = new List<string>();
   public List<string> enemyDialogueOne = new List<string>();
   public List<string> enemyDialogueTwo = new List<string>();
   public List<string> enemyDialogueThree = new List<string>();
   public List<string> enemyDialogueWin = new List<string>();
   public List<string> enemyDialogueLose = new List<string>();
   public List<string> playerChoices = new List<string>();
   public List<string> emotion = new List<string>();

   // Start is called before the first frame update
   public void Init()
   {
      enemyDialogueIntro.Add("[Taxi]");
      enemyDialogueIntro.Add("I always hated drives");
      enemyDialogueIntro.Add("through the desert...");

      enemyDialogueIntro.Add("[Taxi]");
      enemyDialogueOne.Add("Don't scratch the");
      enemyDialogueOne.Add("paint job, bub!");

      enemyDialogueTwo.Add("(the ecs-taxi");
      enemyDialogueTwo.Add("changes the mix tape");
      enemyDialogueTwo.Add("to the next track)");

      enemyDialogueIntro.Add("[Taxi]");
      enemyDialogueThree.Add("Let's not get");
      enemyDialogueThree.Add("too hasty friend-o");

      enemyDialogueIntro.Add("[Taxi]");
      enemyDialogueWin.Add("How about we call");
      enemyDialogueWin.Add("this a draw?");

      enemyDialogueLose.Add("[Taxi]");
      enemyDialogueLose.Add("Destination: ");
      enemyDialogueLose.Add("Shadow Realm");

      playerDialogue.Add("You try negotiating");
      playerDialogue.Add("...");
      playerDialogue.Add("It's not interested");

      playerChoices.Add("Attack");
      playerChoices.Add("Look");
      playerChoices.Add("Talk");

      emotion.Add("Groovy");
      emotion.Add("Thirsty");
      emotion.Add("Agitated");
      emotion.Add("Upset");
      emotion.Add("Upsetti");
      emotion.Add("Annoyed");
      emotion.Add("Peeved");
      emotion.Add("Bothered");
      emotion.Add("Irate");
      emotion.Add("Cross");
      emotion.Add("Galled");
      emotion.Add("Irked");
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
      lines[1].text = "Health: " + Toolbox.Instance.Vars.taxiHealth;
      lines[2].text = "Mood: " + emotion[Random.Range(0, emotion.Count)];
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
      if (Toolbox.Instance.Vars.TaxiDefeated)
      {
         Toolbox.Instance.Vars.enemyDialogueState = 4;
      }
      else if (Toolbox.Instance.Vars.PlayerDefeated)
      {
         Toolbox.Instance.Vars.enemyDialogueState = -1;
      }
      else if (Toolbox.Instance.Vars.enemyDialogueState < 3)
      {
         Toolbox.Instance.Vars.enemyDialogueState++;
      }
   }

   public void FetchEnemyDialogue()
   {
      switch (Toolbox.Instance.Vars.enemyDialogueState)
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
