using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : Singleton<CanvasManager>
{
   public List<GameObject> canvasElements;

   public void Init()
   {
      foreach (GameObject obj in canvasElements)
      {
         obj.SetActive(true);
      }

   }
}
