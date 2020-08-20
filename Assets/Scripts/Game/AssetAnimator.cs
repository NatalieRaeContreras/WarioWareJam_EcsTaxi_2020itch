using System;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
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
   public SpriteRenderer midgroundImage;

   public List<Animator> Lives;
   public List<Animator> LoseLifeIndicator;
   public List<Animator> PreGame = new List<Animator>();
   public List<Sprite> BGList;
   public List<Sprite> midgroundList;

   private int total;
   private int progress = 0;
   private bool pregameLatch = false;
   public bool attachAtStart = true;
   private AssetAnimator bossAssetAnim;

   public AssetAnimator(AssetAnimator otherAssetAnim)
   {
      this.bossAssetAnim = otherAssetAnim;
   }

   //==========================================================================
   public void Start()
   {
      if (attachAtStart)
      {
         Toolbox.Instance.AttachAssetAnimator(this);
      }
      total = Toolbox.Instance.Vars.minigamesRemaining;
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
      pregameLatch = true;
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
   }

   //==========================================================================
   public void DisplayMinigameWindow()
   {
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
   public void MaximizeGameBoard()
   {
      GameBoard.SetTrigger("Maximize");
      GameBoard.ResetTrigger("Open");
   }

   //==========================================================================
   public void CloseGameBoard()
   {
      GameBoard.ResetTrigger("Maximize");
      GameBoard.SetTrigger("Close");
   }
   //==========================================================================
   public void CloseGameWindow()
   {
      GameWindow.SetTrigger("Close");
      GameWindow.ResetTrigger("Open");
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
      midgroundImage.sprite = midgroundList[progress];
      Indicator.SetInteger("Remaining", total - progress);
      Toolbox.Instance.Vars.minigamesRemaining = total - progress;
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

   //==========================================================================
   public void ShowGameWindow()
   {
      GameWindow.gameObject.GetComponent<RawImage>().color = Color.white;
   }

   //==========================================================================
   public void HideGameWindow()
   {
      GameWindow.gameObject.GetComponent<RawImage>().color = Color.clear;
   }
}