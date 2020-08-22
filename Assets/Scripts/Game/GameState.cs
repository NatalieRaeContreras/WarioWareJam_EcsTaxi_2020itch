using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
   public enum State
   {
      None,
      Menu,
      Pregame,
      Ready,
      Playing,
      PostMinigame,
      Gameover,
      Exit,
      Transitioning,
      Init,
   }

   public enum Trigger
   {
      Done,
      Pregame,
      Ready,
      Playing,
      Post,
      GameOver,
      Exit,
      ToMain,
      PlayAgain,
      LevelSelect,
      Menu,
      Cutscene,
      Init,
   }

   public bool Initialized { get => _initialized; }

   public State PreviousState
   {
      get => prev;
      set => prev = value;
   }

   public float timeInState
   {
      get => _timeInState;
   }

   public State current;
   public State prev;

   public Animator stateAnim;

   private float _timeInState = 0.0f;
   private bool _initialized = false;
   private List<string> stateTriggers = new List<string>();

   public void Start()
   {
      Toolbox.Instance.AttachGameState(this);
   }

   //==========================================================================
   public void Init()
   {
      TrackTriggers();
      _initialized = true;
   }

   //==========================================================================
   public void SetTrigger(Trigger trigger)
   {
      stateAnim.SetTrigger(trigger.ToString());
   }

   //==========================================================================
   public void ResetTriggers()
   {
      foreach (string trig in stateTriggers)
      {
         stateAnim.ResetTrigger(trig);
      }
   }

   //==========================================================================
   private void TrackTriggers()
   {
      stateTriggers.Add("Done");
      stateTriggers.Add("Pregame");
      stateTriggers.Add("Ready");
      stateTriggers.Add("Playing");
      stateTriggers.Add("Post");
      stateTriggers.Add("GameOver");
      stateTriggers.Add("Exit");
      stateTriggers.Add("ToMain");
      stateTriggers.Add("PlayAgain");
      stateTriggers.Add("LevelSelect");
      stateTriggers.Add("Menu");
      stateTriggers.Add("Cutscene");
      stateTriggers.Add("Init");
   }

   //==========================================================================
   private void Update()
   {
      if (prev != current)
      {
         _timeInState = 0.0f;
         prev = current;
      }
      _timeInState += Time.deltaTime;
   }

   private void LateUpdate()
   {
      var stateinfo = stateAnim.GetCurrentAnimatorStateInfo(0);
   }
}
