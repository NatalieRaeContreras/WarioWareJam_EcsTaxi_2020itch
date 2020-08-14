using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class AssetAnimator : MonoBehaviour
{
   public Animator GameBoard;
   public Animator GameWindow;
   public Animator Indicator;
   public Animator LoserAnim;
   public Animator WinnerAnim;
   public Animator verbAnim;
   public Image GameVerb;
   public SpriteRenderer BG;
   public SpriteRenderer City;

   public List<Animator> Lives;
   public List<Animator> LoseLifeIndicator;
   public List<Animator> PreGame = new List<Animator>();
   public List<Sprite> BGList;
   public List<Sprite> CityList;

   private int total = 10;
   private int progress = 0;
   private bool pregameLatch = false;

   //==========================================================================
   public void Start()
   {
      Toolbox.Instance.AttachAssetAnimator(this);
   }

   //==========================================================================
   public void Init()
   {
      foreach (GameObject obj in Toolbox.Instance.Canvas.canvasElements)
      {
         obj.SetActive(true);
      }

      Toolbox.Instance.Canvas.canvasElements[0].SetActive(true);
      Toolbox.Instance.Canvas.canvasElements[1].SetActive(true);

      GameWindow = Toolbox.Instance.Canvas.canvasElements[0].GetComponent<Animator>();
      GameVerb = Toolbox.Instance.Canvas.canvasElements[1].GetComponent<Image>();
      verbAnim = Toolbox.Instance.Canvas.canvasElements[1].GetComponent<Animator>();

   }

   //==========================================================================
   public void ActivatePreGame()
   {
      if (pregameLatch)
      {
         return;
      }
      Indicator = GameObject.FindGameObjectWithTag("MinigameIndicator").GetComponent<Animator>();
      Indicator.SetInteger("Remaining", total);
      foreach (Animator obj in PreGame)
      {
         obj.SetTrigger("Activate");
      }
   }

   //==========================================================================
   public void ResultAnim(bool success)
   {
      verbAnim.SetTrigger("Reset");
      verbAnim.ResetTrigger("Activate");

      if (success)
      {
         OnSuccess();
      }
      else
      {
         OnFailure();
      }
   }

   //==========================================================================
   public void DisplayPregameInfo()
   {
      DisplayGameVerb();
      OpenGameBoard();
      GameWindow.SetTrigger("Open");
      GameWindow.ResetTrigger("Close");
   }

   //==========================================================================
   public void OpenGameBoard()
   {
      GameBoard.SetTrigger("Open");
      GameBoard.ResetTrigger("Close");
   }

   //==========================================================================
   public void CloseGameBoard()
   {
      GameWindow.SetTrigger("Close");
      GameBoard.SetTrigger("Close");
      GameWindow.ResetTrigger("Open");
      GameBoard.ResetTrigger("Open");
   }

   //==========================================================================
   public void DisplayGameVerb()
   {
      WinnerAnim.SetTrigger("Reset");
      LoserAnim.SetTrigger("Reset");

      GameVerb.sprite = Toolbox.Instance.MiniManager.minigameInfo.verbSprite;

      verbAnim.ResetTrigger("Reset");
      verbAnim.SetTrigger("Activate");
   }

   //==========================================================================
   public void GameOver()
   {
      foreach (Animator obj in PreGame)
      {
         obj.ResetTrigger("Activate");
         obj.SetTrigger("GameOver");
      }
   }

   //==========================================================================
   public void ProgressWorld()
   {
      progress++;
      BG.sprite = BGList[progress];
      City.sprite = CityList[progress];
      Indicator.SetInteger("Remaining", total - progress);
   }

   //==========================================================================
   private void OnSuccess()
   {
      ProgressWorld();
      WinnerAnim.SetTrigger("Activate");
      WinnerAnim.ResetTrigger("Reset");
   }

   //==========================================================================
   private void OnFailure()
   {
      LoserAnim.SetTrigger("Activate");
      LoserAnim.ResetTrigger("Reset");
      Lives[Toolbox.Instance.Vars.health].SetTrigger("Death");
      LoseLifeIndicator[Toolbox.Instance.Vars.health].SetTrigger("Activate");
   }
}