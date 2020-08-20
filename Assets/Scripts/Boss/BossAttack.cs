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

   public Animator player;
   public Animator taxi;
   public Animator hazard;
   public float timeToGo;
   public float timer;

   public override void InitMinigame()
   {
      playerHit = false;
      hasJumped = false;
      sent = false;
      timeToGo = Random.Range(3.0f, 5.0f);
      Active = true;
      Toolbox.Instance.BossScript.attackComplete = false;
   }

   private void OnTriggerEnter2D(Collider2D collision)
   {
      if (collision.gameObject.CompareTag("Failure"))
      {
         playerHit = true;
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
         if (((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X)) || Input.GetKeyDown(KeyCode.UpArrow)) && !hasJumped)
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
         timer += Time.deltaTime;

         if (end)
         {
            Toolbox.Instance.BossScript.attackComplete = true;
            if (playerHit)
            {
               Toolbox.Instance.BossScript.attackSuccess = true;
               Toolbox.Instance.BossScript.playerHealth -= 5;
            }
            else
            {
               Toolbox.Instance.BossScript.attackSuccess = false;
            }
            Active = false;
         }
      }

   }
}
