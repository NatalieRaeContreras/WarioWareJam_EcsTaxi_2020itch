using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameInformation
{
   public int timeLimit;
   public string sceneName;
   public Sprite verbName;

   public MinigameInformation(int time, string scene, Sprite verb)
   {
      timeLimit = time;
      sceneName = scene;
      verbName = verb;
   }
}