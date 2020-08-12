using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameVars : Singleton<GameVars>
{
   public enum Difficulty
   {
      Easy,
      Medium,
      Hard,
   }
   public bool Initialized { get => initialized; }

   public int health = 3;
   public MinigameTimer timer;
   public AssetAnimator anim;
   public RenderTexture miniRender;
   public Difficulty difficulty = Difficulty.Hard;

   private bool initialized = false;
   public bool preGameLatch = false;
   public bool preMinigameLatch = false;
   public bool isGameOver = false;
   public bool isRestart = false;

   public Scene scene;
   public string nextMinigame;

   private void Awake()
   {
      DontDestroyOnLoad(this);
   }

   public void Init()
   {
      initialized = true;
      GameState.Instance.state.SetTrigger("Pregame");
      if (anim == null)
      {
         anim = GameObject.FindGameObjectWithTag("AnimManager").GetComponent<AssetAnimator>();
      }
      anim.Init();
   }

   //==========================================================================
   public void InitGame()
   {
      if (nextMinigame == null)
      {
         return;     // no mini-game to initialize
      }
      ClearLatches();
      ClearFlags();
      GameState.Instance.ResetTriggers();
      LoadNextMinigame();
   }

   //==========================================================================
   public void LoadNextMinigame()
   {
      //Additively load the next mini-game scene
      var sceneParameters = new LoadSceneParameters(LoadSceneMode.Additive);

      var sceneName = nextMinigame;
      scene = SceneManager.LoadScene(sceneName, sceneParameters);
      anim.OpenGameBoard();
      InitScene();
      GameState.Instance.state.SetTrigger("Playing");
      timer.Reset();
   }

   //==========================================================================
   public void ClearLatches()
   {
      //find a way to remove these latches
      preGameLatch = false;
      preMinigameLatch = false;
   }

   //==========================================================================
   public void ClearFlags()
   {
      //clear any necessary flags/triggers and reset any objects
      timer.Reset();
      anim.WinnerAnim.SetTrigger("Reset");
      anim.LoserAnim.SetTrigger("Reset");
   }

   //==========================================================================
   public void InitScene()
   {
      // run awake/start initializations
      foreach (var root in scene.GetRootGameObjects())
      {
         root.BroadcastMessage("Awake", SendMessageOptions.DontRequireReceiver);
      }
      foreach (var root in scene.GetRootGameObjects())
      {
         root.BroadcastMessage("Start", SendMessageOptions.DontRequireReceiver);
      }
   }

   //==========================================================================
   private void EndOfGameLoop()
   {
      if (health <= 0)
      {
         isGameOver = true;
      }
   }

   //==========================================================================
   // Update is called once per frame
   private void Update()
   {
      if (!initialized)
      {
         return;
      }
      if (health <= 0 && !isGameOver)
      {
         isGameOver = true;
      }
   }
}