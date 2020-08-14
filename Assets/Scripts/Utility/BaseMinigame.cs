using UnityEngine;

public abstract class BaseMinigame : MonoBehaviour
{
   public virtual bool Active
   {
      get => _active;
      set => _active = value;
   }

   public virtual void SetActive(bool value)
   {
      Active = value;
   }

   private bool _active = false;

   public abstract void InitMinigame();
}