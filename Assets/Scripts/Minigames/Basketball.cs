using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basketball : BaseMinigame
{
   public Rigidbody2D ball_large;
   public Rigidbody2D ball_small;
   public GameObject hoop;

   private bool ballThrown = false;
   private bool ballOffScreen = false;
   private bool lowerBall = false;
   private float timer = 0.0f;

   public override void InitMinigame()
   {
      MinigameTimer = 5.0f;
      Toolbox.Instance.MiniManager.timer.SetDarkMode();
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
         if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X)) && !ballThrown)
         {
            ballThrown = true;
            ball_large.gameObject.GetComponentInParent<SideToSide>().Stop();
            hoop.GetComponent<SideToSide>().Stop();
            ball_large.AddForce(Vector2.up * 5000);
         }
         else if (ballOffScreen && !lowerBall)
         {
            lowerBall = true;
            ball_small.gravityScale = 1.0f;
            ball_small.AddForce(Vector2.down * 0.1f);
         }
         else if (ballThrown && !lowerBall)
         {
            if (timer >= 1.0f)
               ballOffScreen = true;
            timer += Time.deltaTime;
         }
      }
   }
}
