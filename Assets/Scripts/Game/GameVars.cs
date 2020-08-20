using UnityEngine;
using UnityEngine.SceneManagement;

public class GameVars
{
   public enum Difficulty
   {
      Easy,
      Medium,
      Hard,
   }

   public bool Initialized { get => _initialized; }

   public int health = 3;
   public int loadedScenes = 0;
   public int minigamesRemaining = 10;

   public bool isGameOver = false;
   public bool isBossDefeated = false;
   public bool isRestart = false;

   public Difficulty difficulty = Difficulty.Hard;

   private bool _initialized = false;

   //==========================================================================
   public void Init()
   {
      loadedScenes = SceneManager.sceneCount;
      _initialized = true;
   }

   //==========================================================================
   public void ClearLatches()
   {
      isGameOver = false;
      isRestart = false;
   }
}