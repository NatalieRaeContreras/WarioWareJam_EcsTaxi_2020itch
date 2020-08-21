using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : BaseMinigame
{
   public List<Image> indicator = new List<Image>();
   public Animator attack;

   private bool success = false;
   private bool begin = false;

   private void OnTriggerEnter2D(Collider2D collision)
   {
      if (collision.gameObject.CompareTag("Goal") && Active)
      {
         success = true;
         Debug.Log(success + " enter");
      }
   }

   private void OnTriggerStay2D(Collider2D collision)
   {
      if (collision.gameObject.CompareTag("Goal") && Active)
      {
         success = true;
         Debug.Log(success + " stay");
      }
   }

   private void OnTriggerExit2D(Collider2D collision)
   {
      if (collision.gameObject.CompareTag("Goal") && Active)
      {
         success = false;
         Debug.Log(success + " exit");
      }
   }

   public void DisplayAttackBar(int index)
   {
      indicator[index].color = Color.white;
      indicator[index].gameObject.SetActive(true);
   }

   // Start is called before the first frame update
   private void Start()
   {
      Toolbox.Instance.SetMinigameScript(this);

      //Toolbox.Instance.BossScript.subGameScript = this;
   }

   // Update is called once per frame
   private void Update()
   {
      if (Active)
      {
         if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X)))
         {
            Toolbox.Instance.Vars.attackSuccess = success;
            begin = true;
            Toolbox.Instance.MiniManager.result = (success) ? MinigameManager.MinigameState.Win : MinigameManager.MinigameState.Lose;
            Active = false;
         }
      }
      else if (begin)
      {
         Toolbox.Instance.Vars.attackComplete = true;
         begin = false;
      }
   }

   public override void InitMinigame()
   {
      success = false;
      begin = false;
      Toolbox.Instance.Vars.attackComplete = false;
      int i = Random.Range(0, 3);
      indicator[i].gameObject.SetActive(true);
      DisplayAttackBar(i);
      SetMinigameTimer = 6.0f;
      Active = true;
   }
}
