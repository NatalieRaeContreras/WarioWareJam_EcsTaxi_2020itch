using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : Singleton<MinigameManager>
{
   public List<string> miniSceneName;
   public List<int> miniMaxTime;
   public List<Sprite> miniVerb;

   private List<MinigameInformation> miniInfo = new List<MinigameInformation>();

   // Start is called before the first frame update
   void Start()
   {
      if ((miniSceneName.Count != miniMaxTime.Count) || (   miniSceneName.Count != miniVerb.Count))
      {
         Debug.LogError("Imbalanced parameter count for " + this.ToString() + " name: " + miniSceneName.Count +
            " time: " + miniMaxTime.Count + " verb: " + miniVerb.Count);
      }

      foreach (string str in miniSceneName)
      {
         if (str.Equals(null) || str.Equals(""))
         {
            Debug.LogError("Null/Empty value for element " + miniSceneName.IndexOf(str) + " in " + miniSceneName.ToString());
         }
      }
      foreach (Sprite spr in miniVerb)
      {
         if (spr.Equals(null))
         {
            Debug.LogError("Null/Empty value for element " + miniVerb.IndexOf(spr) + " in " + miniVerb.ToString());
         }
      }

      InitList();
   }

   // Update is called once per frame
   void Update()
   {

   }

   public void InitList()
   {
      for (int ix = 0; ix < miniSceneName.Count; ix++)
      {
         miniInfo.Add(new MinigameInformation(miniMaxTime[ix], miniSceneName[ix], miniVerb[ix]));
      }
   }
}
