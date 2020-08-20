using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
   void OnTriggerEnter2D(Collider2D collider)
   {
      if (collider.gameObject.CompareTag("Pawn"))
      {
         Toolbox.Instance.MiniManager.result = MinigameManager.MinigameState.Win;
      }
   }
}
