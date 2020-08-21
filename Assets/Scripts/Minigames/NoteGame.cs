using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteGame : BaseMinigame
{
   public Animator backgroundAnim;

   private int notesEntered = 0;
   private int[] notes = new int[4];
   private int Size => (int)Mathf.Sqrt(transform.childCount);

   // Start is called before the first frame update
   private void Start()
   {
      Toolbox.Instance.SetMinigameScript(this);
   }

   public override void InitMinigame()
   {
      for (int ix = 0; ix < Size; ix++)
      {
         int val = Random.Range(0, Size) + (ix * Size);
         transform.GetChild(val).gameObject.SetActive(true);
         notes[ix] = val;
      }
      SetMinigameTimer = 3.0f;
   }

   // Update is called once per frame
   private void Update()
   {
      if (Active)
      {
         int key = -1;

         if (Toolbox.Instance.CurrentState == GameState.State.Playing)
         {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
               key = 0;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
               key = 1;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
               key = 2;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
               key = 3;
            }
         }

         if (key != -1 && (Toolbox.Instance.CurrentState == GameState.State.Playing))
         {
            if (key != (notes[notesEntered] - notesEntered * Size))
            {
               Toolbox.Instance.MiniManager.result = MinigameManager.MinigameState.Lose;
            }
            else
            {
               transform.GetChild(key + (notesEntered * Size)).GetComponent<SpriteRenderer>().color = Color.green;
               notesEntered++;

               if (notesEntered == Size)
               {
                  Toolbox.Instance.MiniManager.result = MinigameManager.MinigameState.Win;
                  backgroundAnim.SetTrigger("Display");
               }
            }
         }
      }
   }
}
