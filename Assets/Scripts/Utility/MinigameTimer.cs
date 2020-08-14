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
      active = true;
      timer = 0.0f;
   }

   public void Reset()
   {
      Hide();
      countdownAnimator.ResetTrigger("Play");
      hourglassAnimator.ResetTrigger("Play");
      hourglassAnimator.ResetTrigger("Done");
      countdownAnimator.ResetTrigger("Done");
      active = false;
      timer = 0.0f;
   }

   public void Display()
   {
      countdownAnimator.SetTrigger("Play");
      hourglassAnimator.SetTrigger("Play");
      hourglassRenderer.color = Color.white;
      countdownRenderer.color = Color.white;
   }

   public void Hide()
   {
      hourglassAnimator.SetTrigger("Done");
      hourglassRenderer.color = Color.clear;
      countdownRenderer.color = Color.clear;
   }

   public void Init()
   {
      hourglassRenderer = Toolbox.Instance.Canvas.canvasElements[4].GetComponent<Image>();
      hourglassAnimator = Toolbox.Instance.Canvas.canvasElements[4].GetComponent<Animator>();

      countdownRenderer = Toolbox.Instance.Canvas.canvasElements[2].GetComponent<Image>();
      countdownAnimator = Toolbox.Instance.Canvas.canvasElements[2].GetComponent<Animator>();

      if (countdownRenderer == null || hourglassRenderer == null || hourglassAnimator == null || countdownAnimator == null)
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
         if (Toolbox.Instance.CurrentState == GameState.State.Playing && Done && Toolbox.Instance.MiniManager.result != MinigameManager.MinigameState.Win)
         {
            Toolbox.Instance.MiniManager.result = MinigameManager.MinigameState.Lose;
         }
         else if ((Toolbox.Instance.MiniManager.result == MinigameManager.MinigameState.Win ||
            Toolbox.Instance.MiniManager.result == MinigameManager.MinigameState.Lose) && Done)
         {
            Toolbox.Instance.MiniManager.minigameScript.Active = false;
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

   }
}