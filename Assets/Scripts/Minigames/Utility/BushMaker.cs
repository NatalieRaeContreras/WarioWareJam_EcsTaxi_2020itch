using System;
using System.Collections.Generic;
using UnityEngine;

public class BushMaker : MonoBehaviour
{
   public List<Transform> bushTiles;
   private float increment = 1.25f;
   private float start = -2.5f;

   // Start is called before the first frame update
   private void Start()
   {
      for (int iy = 0; iy < Math.Sqrt(bushTiles.Count); iy++)
      {
         for (int ix = 0; ix < Math.Sqrt(bushTiles.Count); ix++)
         {
            bushTiles[(iy * (int)Math.Sqrt(bushTiles.Count)) + ix].position = new Vector3(start + (ix * increment), (-1 * start) + (-iy * increment));
         }
      }
   }
}
