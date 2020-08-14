using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
   public List<GameObject> canvasElements;

   public void Start()
   {
      Toolbox.Instance.AttachCanvasManager(this);
      DontDestroyOnLoad(this);
   }

   public void Init()
   {
      foreach (GameObject obj in canvasElements)
      {
         if (!obj.activeSelf)
         {
            obj.SetActive(true);
         }
      }
   }
}