using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TireGame : BaseMinigame
{
   public Transform tire;
   public Animator tireAnim;
   public SpriteRenderer spriteRenderer;
   public Animator plunger;
   public Slider gauge;
   private int high = 90;
   private int increment = 15;
   private int min = 54;
   private int val = 5;

   private const int baseVal = 10;

   public override void InitMinigame()
   {
      gauge.value = 0;
      val = baseVal;
      MinigameTimer = 5.0f;
      tireAnim = tire.gameObject.GetComponent<Animator>();
      spriteRenderer = tire.gameObject.GetComponent<SpriteRenderer>();
      tireAnim.enabled = false;
      tire.localScale = new Vector2(0.0f, 0.0f);
   }

   // Start is called before the first frame update
   private void Start()
   {
      Toolbox.Instance.SetMinigameScript(this);
   }

   // Update is called once per frame
   private void Update()
   {
      if (Active)
      {
         plunger.ResetTrigger("Go");
         if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Z))
         {
            plunger.SetTrigger("Go");
            gauge.value += increment;
            tire.localScale = new Vector2(0.1f + (0.01f * gauge.value), 0.1f + (0.01f * gauge.value));
         }
         if (gauge.value >= 85)
         {
            float clr = 1.0f - (100.0f - (gauge.value * 0.03f));
            spriteRenderer.color = new Color(1.0f, clr, clr);
         }

         if (5.0f - MinigameTimer <= 0.0f || gauge.value >= high)
         {
            Active = false;
            if (gauge.value >= high)
            {
               tire.gameObject.GetComponent<Animator>().enabled = true;
               Toolbox.Instance.MiniManager.result = MinigameManager.MinigameState.Lose;
            }
            else if (gauge.value <= min)
            {
               Toolbox.Instance.MiniManager.result = MinigameManager.MinigameState.Lose;
            }
            else
            {
               Toolbox.Instance.MiniManager.result = MinigameManager.MinigameState.Win;
            }
         }
         else if (val <= 0)
         {
            val = baseVal;
            gauge.value -= 2;
         }
         else
         {
            val--;
         }
      }
   }
}
