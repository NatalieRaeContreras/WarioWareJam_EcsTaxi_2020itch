using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : BaseMinigame
{
   public List<Image> indicator = new List<Image>();
   public Animator attack;

   private bool success = false;

   private void OnTriggerEnter2D(Collider2D collision)
   {
      if (collision.gameObject.CompareTag("Goal") && Active)
      {
         success = true;
      }
   }

   private void OnTriggerExit2D(Collider2D collision)
   {
      if (collision.gameObject.CompareTag("Goal") && Active)
      {
         success = false;
      }
   }

   public void DisplayAttackBar(int index)
   {
      indicator[index].color = Color.white;
      indicator[index].gameObject.SetActive(true);
   }

   public void HideAttackBars()
   {
      foreach (Image img in indicator)
      {
         img.color = Color.clear;
         img.gameObject.SetActive(false);
      }
   }

   // Start is called before the first frame update
   void Start()
   {
      Toolbox.Instance.BossScript.subGameScript = this;
      HideAttackBars();
   }

   // Update is called once per frame
   void Update()
   {
      if (Active)
      {
         if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X)))
         {
            if (success)
            {
               Toolbox.Instance.BossScript.attackSuccess = true;
            }
            else
            {
               Toolbox.Instance.BossScript.attackSuccess = false;
            }
            Active = false;
            Toolbox.Instance.BossScript.attackComplete = true;
            HideAttackBars();
            attack.gameObject.SetActive(false);
         }
      }
   }

   public override void InitMinigame()
   {
      success = false;
      Toolbox.Instance.BossScript.attackComplete = false;
      int i = Random.Range(0, 3);
      indicator[i].gameObject.SetActive(true);
      DisplayAttackBar(i);
      Active = true;
      Toolbox.Instance.BossScript.attackComplete = false;
   }
}
