using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class Metronome : BaseMinigame
{
   public List<SpriteRenderer> indicators = new List<SpriteRenderer>();
   public Transform pointer;
   public Animator pointerAnimator;

   private float easyRange = 13f;
   private float medHardRange = 7.4f;
   private float goal;

   public override void InitMinigame()
   {
      foreach (SpriteRenderer spr in indicators)
      {
         spr.color = Color.clear;
      }

      if (Toolbox.Instance.Vars.difficulty == GameVars.Difficulty.Easy)
      {
         indicators[0].color = Color.white;
         goal = easyRange;
      }
      else
      {
         indicators[1].color = Color.white;
         goal = medHardRange;
         if (Toolbox.Instance.Vars.difficulty == GameVars.Difficulty.Hard)
         {
            pointerAnimator.speed = 1.5f;
         }
      }
      SetMinigameTimer = 4.0f;
      pointerAnimator.SetTrigger("Play");
   }

   private void Start()
   {
      Toolbox.Instance.SetMinigameScript(this);
   }

   // Update is called once per frame
   private void Update()
   {
      if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X)))
      {
         pointerAnimator.speed = 0f;
         Vector3 rot = pointer.rotation.eulerAngles;
         if (Math.Abs(rot.z) <= goal)
         {
            Toolbox.Instance.MiniManager.result = MinigameManager.MinigameState.Win;
         }
         else
         {
            Toolbox.Instance.MiniManager.result = MinigameManager.MinigameState.Lose;
         }
      }
   }
}
