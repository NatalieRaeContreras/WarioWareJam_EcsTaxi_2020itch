using UnityEngine;

public class Failure : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D collider)
   {
      if (collider.gameObject.CompareTag("Pawn"))
      {
         Toolbox.Instance.MiniManager.result = MinigameManager.MinigameState.Lose;
      }
   }
}
