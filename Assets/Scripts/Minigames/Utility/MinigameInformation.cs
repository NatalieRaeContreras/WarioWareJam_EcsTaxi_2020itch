using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameInformation
{
   public string sceneName;
   public Sprite verbSprite;

   public MinigameInformation(string scene, Sprite verb)
   {
      sceneName = scene;
      verbSprite = verb;
   }
}
