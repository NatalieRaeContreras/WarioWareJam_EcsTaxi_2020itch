using UnityEngine;

public abstract class BaseMinigame : MonoBehaviour
{
   public virtual bool Active
   {
      get => _active;
      set => SetActive(value);
   }

   private bool _active = false;

   public virtual void SetActive(bool value)
   {
      Active = value;
      if (value)
      {
         Toolbox.Instance.MiniManager.timer.Activate();
      }
   }

   public abstract void InitMinigame();
}
