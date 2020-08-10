using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour
{
   public bool Done => timer >= frames.Count;

   public bool active = false;
   public UnityEngine.UI.Image imageRenderer;
   public List<Sprite> frames;

   private float timer = 0.0f;
   private int Index => (int)timer;

   public void Init()
   {
      imageRenderer.color = Color.white;
      active = true;
      timer = 0.0f;
   }

   public void Reset()
   {
      imageRenderer.color = Color.clear;
      active = false;
      timer = 0.0f;
   }

   // Update is called once per frame
   private void Update()
   {
      if (active)
      {
         //update frame data
         if (!Done)
         {
            imageRenderer.sprite = frames[(int)Mathf.Clamp(Index, 0, frames.Count - 1)];
            if (imageRenderer.sprite == null)
            {
               imageRenderer.color = Color.clear;
            }
            else if (imageRenderer.sprite != null && imageRenderer.color == Color.clear)
            {
               imageRenderer.color = Color.white;
            }
         }

         //advance games state if out of time
         if (Game.Instance.state == Game.State.Playing && Done)
         {
            Game.Instance.state = Game.State.Lose;
            Game.Instance.miniResult = Game.State.Lose;
         }

         timer += Time.deltaTime;
      }
   }
}