using UnityEngine;

public class SideToSide : MonoBehaviour
{
   public float leftmost_x;
   public float rightmost_x;
   public float speed;

   private bool movingLeft = true;
   private bool active = true;

   // Start is called before the first frame update
   private void Start()
   {
   }

   public void Stop()
   {
      active = false;
   }

   // Update is called once per frame
   private void Update()
   {
      if (active)
      {
         if (this.transform.position.x <= leftmost_x)
         {
            movingLeft = false;
         }
         else if (this.transform.position.x >= rightmost_x)
         {
            movingLeft = true;
         }

         if (movingLeft)
         {
            this.transform.position += new Vector3(-0.1f * speed, 0, 0);
         }
         else
         {
            this.transform.position += new Vector3(0.1f * speed, 0, 0);
         }
      }
   }
}
