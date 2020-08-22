using System.Collections.Generic;
using UnityEngine;

public class SkiFree : BaseMinigame
{
   public Rigidbody player;
   private const float force = 20f;

   public List<GameObject> list = new List<GameObject>();
   public List<GameObject> list2 = new List<GameObject>();

   private int success = 0;
   private bool done = false;

   private int rand1 = 0;
   private int rand2 = 0;

   public override void InitMinigame()
   {
      success = 0;
      rand1 = Random.Range(0, 3);
      do
      {
         rand2 = Random.Range(0, 3);
      }
      while (rand2 == rand1);

      list[rand1].SetActive(true);
      list2[rand2].SetActive(true);

      MinigameTimer = 7.0f;
      Toolbox.Instance.MiniManager.timer.darkMode = true;
   }

   // Start is called before the first frame update
   private void Start()
   {
      Toolbox.Instance.SetMinigameScript(this);
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Goal"))
      {
         success++;
      }

      if (other.CompareTag("Failure"))
      {
         done = true;
      }
   }

   // Update is called once per frame
   private void Update()
   {
      if (Active)
      {
         player.AddForce(Vector3.forward * Mathf.Sqrt(force));

         if (Input.GetKey(KeyCode.LeftArrow))
         {
            player.AddForce(Vector3.left * force);
         }
         else if (Input.GetKey(KeyCode.RightArrow))
         {
            player.AddForce(Vector3.right * force);
         }

         if (success >= 2)
         {
            Toolbox.Instance.MiniManager.result = MinigameManager.MinigameState.Win;
         }
         else if (done && Toolbox.Instance.MiniManager.result != MinigameManager.MinigameState.Win)
         {
            Toolbox.Instance.MiniManager.result = MinigameManager.MinigameState.Lose;
         }
      }
   }
}
