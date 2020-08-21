using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

public class BossAttack : BaseMinigame
{
   public bool playerHit;
   private bool hasJumped;
   private bool sent;
   public bool end;
   private bool begin = false;
   private float timer2 = 0.0f;

   public Animator player;
   public Animator taxi;
   public Animator hazard;
   public float timeToGo;
   public float timer;

   public override void InitMinigame()
   {
      Toolbox.Instance.MiniManager.result = MinigameManager.MinigameState.None;
      playerHit = false;
      hasJumped = false;
      sent = false;
      begin = false;
      timer = 0.0f;
      timeToGo = Random.Range(1.0f, 3.0f);
      Active = true;
      Toolbox.Instance.Vars.attackComplete = false;
   }

   private void OnTriggerEnter2D(Collider2D collision)
   {
      if (collision.gameObject.CompareTag("Failure"))
      {
         playerHit = true;
         collision.gameObject.GetComponent<Animator>().enabled = false;
         collision.gameObject.GetComponent<Rigidbody2D>().AddForce((Vector2.up + Vector2.right)*100);
      }
      else if (collision.gameObject.CompareTag("Goal"))
      {
         end = true;
      }
   }

   // Start is called before the first frame update
   void Start()
   {
      Toolbox.Instance.BossScript.subGameScript = this;
   }

   // Update is called once per frame
   void Update()
   {
      if (Active)
      {
         if (((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X)) || Input.GetKeyDown(KeyCode.UpArrow)) && !hasJumped && !playerHit)
         {
            hasJumped = true;
            player.SetTrigger("Go");
         }

         if (timer >= timeToGo && !sent)
         {
            sent = true;
            hazard.SetTrigger("Go");
            taxi.SetTrigger("Go");
         }

         if (end && !begin)
         {
            if (playerHit)
            {
               Toolbox.Instance.Vars.attackSuccess = true;
               Toolbox.Instance.Vars.playerHealth -= 5;
               Toolbox.Instance.MiniManager.result = MinigameManager.MinigameState.Lose;
            }
            else
            {
               Toolbox.Instance.Vars.attackSuccess = false;
               Toolbox.Instance.MiniManager.result = MinigameManager.MinigameState.Win;
            }
            begin = true;
         }
         timer += Time.deltaTime;

         if (begin)
         {
            if (timer2 >= 1.0f)
            {
               Toolbox.Instance.Vars.attackComplete = true;
               Active = false;
               begin = false;
            }
            timer2 += Time.deltaTime;
         }
      }
   }
}
