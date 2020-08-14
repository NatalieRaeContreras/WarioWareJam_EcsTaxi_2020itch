using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameInformation
{
   public string sceneName;
   public int timeLimit;
   public Sprite verbSprite;

   public MinigameInformation(string scene, int time, Sprite verb)
   {
      sceneName = scene;
      timeLimit = time;
      verbSprite = verb;
   }
}