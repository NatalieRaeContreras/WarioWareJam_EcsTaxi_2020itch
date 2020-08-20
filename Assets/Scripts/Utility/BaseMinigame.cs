using UnityEngine;

public abstract class BaseMinigame : MonoBehaviour
{
   public virtual bool Active
   {
      get => _active;
      set => _active = value;
   }

   private bool _active = false;

   public virtual void SetActive(bool value)
   {
      Active = value;
   }

   public abstract void InitMinigame();
}