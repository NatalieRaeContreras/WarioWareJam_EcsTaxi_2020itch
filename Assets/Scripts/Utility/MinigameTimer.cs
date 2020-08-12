using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameTimer : MonoBehaviour
{
   public bool Done => timer >= timeLimit;

   public bool active = false;
   public float timeLimit = 5.0f;
   public float timeToDisplayTimer = 3.0f;
   public UnityEngine.UI.Image hourglassRenderer;
   public UnityEngine.UI.Image countdownRenderer;

   private float timer = 0.0f;

   public void Activate()
   {
      checkNull();
      hourglassRenderer.color = Color.white;
      countdownRenderer.color = Color.white;
      active = true;
      timer = 0.0f;
   }

   public void Reset()
   {
      checkNull();
      hourglassRenderer.color = Color.clear;
      countdownRenderer.color = Color.clear;
      active = false;
      timer = 0.0f;
   }

   public void Hide()
   {
      checkNull();
      hourglassRenderer.color = Color.clear;
      countdownRenderer.color = Color.clear;
   }

   public void checkNull()
   {
      if (hourglassRenderer.Equals(null))
      {
         hourglassRenderer = GameObject.FindGameObjectWithTag("CanvasTimer").GetComponent<Image>();
      }
      if (countdownRenderer.Equals(null))
      {
         countdownRenderer = GameObject.FindGameObjectWithTag("CanvasCountdown").GetComponent<Image>();
      }

      if (countdownRenderer.Equals(null) || hourglassRenderer.Equals(null))
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

         timer += Time.deltaTime;
      }

      if (GameState.Instance.CurrentState == GameState.State.Win || GameState.Instance.CurrentState == GameState.State.Lose)
      {
         Hide();
      }
   }
}