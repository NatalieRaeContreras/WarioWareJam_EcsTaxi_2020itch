using UnityEngine;

public class Displayer : MonoBehaviour
{
   private int notesEntered = 0;
   private int[] notes = new int[4];
   private int Size => (int)Mathf.Sqrt(transform.childCount);

   // Start is called before the first frame update
   private void Start()
   {
      for (int ix = 0; ix < Size; ix++)
      {
         int val = Random.Range(0, Size) + (ix * Size);
         transform.GetChild(val).gameObject.SetActive(true);
         notes[ix] = val;
      }
   }

   // Update is called once per frame
   private void Update()
   {
      int key = -1;

      if (Game.Instance.state == Game.State.Playing)
      {
         if (Input.GetKeyDown(KeyCode.UpArrow))
         {
            key = 0;
            //0
         }
         else if (Input.GetKeyDown(KeyCode.DownArrow))
         {
            //1
            key = 1;
         }
         else if (Input.GetKeyDown(KeyCode.LeftArrow))
         {
            //2
            key = 2;
         }
         else if (Input.GetKeyDown(KeyCode.RightArrow))
         {
            //3
            key = 3;
         }
      }

      if (key != -1 && (Game.Instance.state == Game.State.Playing))
      {
         if (key != (notes[notesEntered] - notesEntered * Size))
         {
            Game.Instance.state = Game.State.Lose;
            Game.Instance.miniResult = Game.State.Lose;
         }
         else
         {

            transform.GetChild(key + (notesEntered * Size)).GetComponent<SpriteRenderer>().color = Color.green;
            notesEntered++;

            if (notesEntered == Size)
            {
               Game.Instance.state = Game.State.Win;
               Game.Instance.miniResult = Game.State.Win;
            }
         }
      }
   }
}