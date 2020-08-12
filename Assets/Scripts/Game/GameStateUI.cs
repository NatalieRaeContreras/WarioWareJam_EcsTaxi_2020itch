using UnityEngine;
using UnityEngine.UI;

public class GameStateUI : MonoBehaviour
{
   public Image target;
   public Sprite win;
   public Sprite lose;

   // Update is called once per frame
   private void Update()
   {
      switch (GameState.Instance.CurrentState)
      {
         case GameState.State.Lose:
            target.enabled = true;
            target.sprite = lose;
            break;

         case GameState.State.Win:
            target.enabled = true;
            target.sprite = win;
            break;

         default:
            target.enabled = false;
            break;
      }
   }
}