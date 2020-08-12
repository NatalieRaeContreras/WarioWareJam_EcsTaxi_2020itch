using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssetAnimator : MonoBehaviour
{
   public Animator GameBoard;
   public Image GameVerb;
   public Animator LoserAnim;
   public Animator WinnerAnim;
   public SpriteRenderer BG;
   public SpriteRenderer City;
   public Animator Indicator;

   public List<Animator> PreGame;
   public List<Animator> Lives;
   public List<Animator> LoseLifeIndicator;
   public List<Sprite> BGList;
   public List<Sprite> CityList;

   private int total = 10;
   private int progress = 0;
   private Animator verbAnim;

   public void Init()
   {
      verbAnim = GameVerb.GetComponent<Animator>();
   }

   public void ActivatePreGame()
   {
      Indicator.SetInteger("Remaining", total);
      foreach (Animator obj in PreGame)
      {
         obj.SetTrigger("Activate");
      }
   }

   public void doNothing()
   {
   }

   public void ResultAnim(bool success)
   {
      verbAnim.SetTrigger("Reset");
      verbAnim.ResetTrigger("Activate");

      GameVars.Instance.miniRender.DiscardContents();

      if (success)
      {
         OnSuccess();
      }
      else
      {
         OnFailure();
      }
   }

   public void OpenGameBoard()
   {
      GameBoard.SetTrigger("Open");
      GameBoard.ResetTrigger("Close");
   }

   public void CloseGameBoard()
   {
      GameBoard.SetTrigger("Close");
      GameBoard.ResetTrigger("Open");
   }

   public void DisplayGameVerb(string name)
   {
      WinnerAnim.SetTrigger("Reset");
      LoserAnim.SetTrigger("Reset");

      int index = MinigameManager.Instance.miniSceneName.IndexOf(name);
      if (index >= 0 && index < MinigameManager.Instance.miniSceneName.Count)
      {
         GameVerb.sprite = MinigameManager.Instance.miniVerb[index];
      }
      else
      {
         Debug.LogError("Holy shit" + name + ":" + index);
      }

      verbAnim.ResetTrigger("Reset");
      verbAnim.SetTrigger("Activate");
   }

   public void GameOver()
   {
      foreach (Animator obj in PreGame)
      {
         obj.ResetTrigger("Activate");
         obj.SetTrigger("GameOver");
      }
   }

   public void ProgressWorld()
   {
      progress++;
      BG.sprite = BGList[progress];
      City.sprite = CityList[progress];
      Indicator.SetInteger("Remaining", total - progress);
   }

   private void OnSuccess()
   {
      ProgressWorld();
      WinnerAnim.SetTrigger("Activate");
      WinnerAnim.ResetTrigger("Reset");
   }

   private void OnFailure()
   {
      LoserAnim.SetTrigger("Activate");
      LoserAnim.ResetTrigger("Reset");
      Lives[GameVars.Instance.health].SetTrigger("Death");
      LoseLifeIndicator[GameVars.Instance.health].SetTrigger("Activate");
   }
}