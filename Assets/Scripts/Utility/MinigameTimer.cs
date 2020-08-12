using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameTimer : MonoBehaviour
{
   public bool Done => timer >= timeLimit;
   public bool Visible => hourglassRenderer.color == Color.white && countdownRenderer.color == Color.white;

   public bool active = false;
   public float timeLimit = 6.0f;
   public float timeToDisplayTimer = 3.2f;
   public UnityEngine.UI.Image hourglassRenderer;
   public Animator hourglassAnimator;
   public UnityEngine.UI.Image countdownRenderer;
   public Animator countdownAnimator;

   private float timer = 0.0f;

   public void Activate()
   {
      CheckNull();
      active = true;
      timer = 0.0f;
   }

   public void Reset()
   {
      CheckNull();
      Hide();
      countdownAnimator.ResetTrigger("Play");
      hourglassAnimator.ResetTrigger("Play");
      hourglassAnimator.ResetTrigger("Done");
      active = false;
      timer = 0.0f;
   }

   public void Display()
   {
      CheckNull();
      countdownAnimator.SetTrigger("Play");
      hourglassAnimator.SetTrigger("Play");
      hourglassRenderer.color = Color.white;
      countdownRenderer.color = Color.white;
   }

   public void Hide()
   {
      CheckNull();
      hourglassAnimator.SetTrigger("Done");
      hourglassRenderer.color = Color.clear;
      countdownRenderer.color = Color.clear;
   }

   private void CheckNull()
   {
      if (hourglassRenderer.Equals(null))
      {
         hourglassRenderer = GameObject.FindGameObjectWithTag("CanvasTimer").GetComponent<Image>();
      }
      if (countdownRenderer.Equals(null))
      {
         countdownRenderer = GameObject.FindGameObjectWithTag("CanvasCountdown").GetComponent<Image>();
      }
      if (hourglassAnimator.Equals(null))
      {
         hourglassAnimator = GameObject.FindGameObjectWithTag("CanvasTimer").GetComponent<Animator>();
      }
      if (countdownAnimator.Equals(null))
      {
         countdownAnimator = GameObject.FindGameObjectWithTag("CanvasCountdown").GetComponent<Animator>();
      }

      if (countdownRenderer.Equals(null) || hourglassRenderer.Equals(null) || hourglassAnimator.Equals(null) || countdownAnimator.Equals(null))
      {
         Debug.LogError("What the hell, mane");
      }
   }

   // Update is called once per frame
   private void Update()
   {
      if (active)
      {
         //advance games state if out of time
         if (GameState.Instance.CurrentState == GameState.State.Playing && Done)
         {
            GameState.Instance.state.SetTrigger("Lose");
         }

         if (timer >= (timeLimit - timeToDisplayTimer) && !Visible)
         {
            Display();
         }
         if (timer <= 0.0f && Visible)
         {
            Hide();
         }

         timer += Time.deltaTime;
      }

      if (GameState.Instance.CurrentState == GameState.State.Win || GameState.Instance.CurrentState == GameState.State.Lose)
      {
         Hide();
      }
   }
}