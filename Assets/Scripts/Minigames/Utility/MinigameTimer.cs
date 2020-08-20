using UnityEngine;
using UnityEngine.UI;

public class MinigameTimer : MonoBehaviour
{
   public bool Done => !active;
   public bool Visible => hourglassRenderer.color == Color.white && countdownRenderer.color == Color.white;

   public bool active = false;
   public bool darkMode = false;
   public float timeLimit = 6.0f;
   public float timeToDisplayTimer = 4.0f;
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
      darkMode = false;
      active = false;
      timer = 0.0f;
   }

   public void SetDarkMode()
   {
      darkMode = true;
   }

   public void Display()
   {
      countdownAnimator.ResetTrigger("Reset");
      countdownAnimator.SetTrigger("Play");
      hourglassAnimator.SetTrigger("Play");
      if (darkMode)
      {
         countdownRenderer.color = Color.black;
      }
      else
      {
         countdownRenderer.color = Color.white;
      }
      hourglassRenderer.color = Color.white;
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

   public void timeOut()
   {
      if ((Toolbox.Instance.CurrentState == GameState.State.Playing) &&
          (Toolbox.Instance.MiniManager.result != MinigameManager.MinigameState.Win))
      {
         Toolbox.Instance.MiniManager.result = MinigameManager.MinigameState.Lose;
      }

      Hide();
      countdownAnimator.SetTrigger("Reset");
      active = false;
   }

   // Update is called once per frame
   private void Update()
   {
      if (active)
      {
         if (((Toolbox.Instance.MiniManager.result == MinigameManager.MinigameState.Win) ||
              (Toolbox.Instance.MiniManager.result == MinigameManager.MinigameState.Lose)) &&
             Toolbox.Instance.MiniManager.minigameScript.Active)
         {
            Toolbox.Instance.MiniManager.minigameScript.Active = false;
            if (!Visible)
            {
               timer = (timeLimit - timeToDisplayTimer) - 0.2f;
            }
         }
         if (timer >= (timeLimit - timeToDisplayTimer) && !Visible)
         {
            Display();
         }
      }
      timer += Time.deltaTime;
   }
}