using UnityEngine;

public class Victory : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D collider)
   {
      if (collider.gameObject.CompareTag("Pawn"))
      {
         Toolbox.Instance.MiniManager.result = MinigameManager.MinigameState.Win;
      }
   }
}
