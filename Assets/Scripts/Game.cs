using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : Singleton<Game>
{
   public enum State
   {
      None,
      Pregame,
      Playing,
      Win,
      Lose,
      Taxi,
      Ready,
      Gameover,
      Exit,
   }

   public int health = 3;
   public Countdown timer;
   public State state;
   public AssetAnimator anim;
   public RenderTexture miniRender;
   public State miniResult;
   public List<string> sceneList;

   private float timeInState = 0.0f;
   private Scene scene;
   private bool preGameLatch = false;
   private bool preMinigameLatch = false;
   private bool moveToGameOver = false;

   private string nextMinigame;

   private void Start()
   {
      state = State.Pregame;
      anim.Init();
      timer.Reset();
   }

   private void InitGame()
   {
      if (nextMinigame == null)
      {
         return;
      }

      //find a way to remove these latches
      preGameLatch = false;
      preMinigameLatch = false;

      //clear any necessary flags/triggers and reset any objects
      timer.Reset();
      anim.WinnerAnim.SetTrigger("Reset");
      anim.LoserAnim.SetTrigger("Reset");

      //Additively load the next mini-game scene
      var sceneParameters = new LoadSceneParameters(LoadSceneMode.Additive);
      var sceneName = nextMinigame;
      scene = SceneManager.LoadScene(sceneName, sceneParameters);

      // run awake/start initializations
      foreach (var root in scene.GetRootGameObjects())
      {
         root.BroadcastMessage("Awake", SendMessageOptions.DontRequireReceiver);
      }
      foreach (var root in scene.GetRootGameObjects())
      {
         root.BroadcastMessage("Start", SendMessageOptions.DontRequireReceiver);
      }

      state = State.Playing;
      timer.Reset();
   }

   // Update is called once per frame
   private void Update()
   {
      State prev = this.state;

      switch (state)
      {
         case State.Pregame:  //===============================================
            if (timeInState >= 5.0f)
            {
               state = State.Ready;
               preMinigameLatch = false;
            }
            else if (timeInState >= 4.0f && !preMinigameLatch)
            {
               preMinigameLatch = true;
               nextMinigame = sceneList[Random.Range(0, sceneList.Count)];
               anim.DisplayGameVerb(nextMinigame);
            }
            else if (timeInState >= 2.0f && !preGameLatch)
            {
               anim.ActivatePreGame();
               preGameLatch = true;
            }
            break;
         case State.Ready:    //===============================================
            miniResult = State.None;
            InitGame();
            break;
         case State.Playing:  //===============================================
            if (!timer.active)
            {
               timer.Init();
            }
            break;
         case State.Win:      //===============================================
            if (timer.Done)
            {
               timer.Reset();
               miniResult = State.Win;
               anim.ResultAnim(true);
               state = State.Taxi;
            }
            break;
         case State.Lose:     //===============================================
            if (timer.Done )
            {
               timer.Reset();
               health--;
               miniResult = State.Lose;
               anim.ResultAnim(false);
               state = State.Taxi;
            }
            break;
         case State.Taxi:     //===============================================
            if (scene.isLoaded && scene.IsValid())
            {
               SceneManager.UnloadSceneAsync(scene);
               nextMinigame = sceneList[Random.Range(0, sceneList.Count)];
            }

            if (miniResult == State.Win)
            {
               if (timeInState >= 5.0f)
               {
                  state = State.Ready;
               }
               else if (timeInState >= 4.0f)
               {
                  anim.DisplayGameVerb(nextMinigame);
                  anim.WinnerAnim.SetTrigger("Reset");
                  anim.LoserAnim.SetTrigger("Reset");
               }
            }
            else if (miniResult == State.Lose)
            {
               if (timeInState >= 5.0f)
               {
                  state = State.Ready;
               }
               else if (timeInState >= 4.0f)
               {
                  anim.DisplayGameVerb(nextMinigame);
                  anim.LoserAnim.SetTrigger("Reset");
                  anim.WinnerAnim.SetTrigger("Reset");
               }
            }
            else if (moveToGameOver)
            {
               state = State.Gameover;
            }
            break;
         case State.Gameover:    //===============================================
            if (moveToGameOver)
            {
               if (scene.isLoaded && scene.IsValid())
               {
                  SceneManager.UnloadSceneAsync(scene);
                  nextMinigame = null;
               }
               anim.GameOver();
               moveToGameOver = false;
            }
            break;
         case State.Exit:        //===============================================
            Application.Quit();
            break;
         default:       //========================================================
            break;
      }

      if (health <= 0)
      {
         moveToGameOver = true;
      }
      else if (prev != state)
      {
         timeInState = 0.0f;
      }
      else
      {
         timeInState += Time.deltaTime;
      }
   }
}