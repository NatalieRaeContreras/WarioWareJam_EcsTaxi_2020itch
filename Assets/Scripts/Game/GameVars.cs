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
   public bool TaxiDefeated { get => taxiHealth <= 0; }
   public bool PlayerDefeated { get => playerHealth <= 0; }

   public bool GameOver { get => health <= 0; }

   public bool introductory = true;

   public int playerChoiceBoss = 0;
   public int enemyDialogueState = 0;
   public int taxiHealth = 5;
   public int playerHealth = 20;

   public int health = 3;
   public int loadedScenes = 0;
   public int minigamesRemaining = 5;

   public bool isGameOver = false;
   public bool isBossDefeated = false;
   public bool isRestart = false;

   public bool endScreenSelection = false;

   public bool boardIsClosed;
   public bool windowIsClosed;
   public bool attackSuccess = false;
   public bool attackComplete = false;

   public string lastMiningame = "null";

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
