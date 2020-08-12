using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : Singleton<GameState>
{
   public enum State
   {
      None,
      Menu,
      Pregame,
      Ready,
      Playing,
      Win,
      Lose,
      PostMinigame,
      Gameover,
      Exit,
      Transitioning,
   }

   public bool Initialized { get => initialized; }
   public State CurrentState
   {
      get
      {
         if (current != State.Win && state.GetBool("Win"))
         {
            return State.Transitioning;
         }
         else if (current != State.Lose && state.GetBool("Lose"))
         {
            return State.Transitioning;
         }
         else return current;
      }
      set
      {
         prev = current;
         current = value;
      }
   }

   public State PreviousState
   {
      get => prev;
      set => prev = value;
   }
   public float timeInState;
   public string triggers;
   public State miniResult;
   public Animator state;

   private bool initialized = false;
   private State current;
   private State prev;
   private List<string> stateTriggers = new List<string>();

   // Start is called before the first frame update
   public void Init()
   {
      TrackTriggers();
      initialized = true;
   }

   // Update is called once per frame
   void Update()
   {
      timeInState += Time.deltaTime;
      if (PreviousState != CurrentState)
      {
         timeInState = 0.0f;
         PreviousState = CurrentState;
      }
   }

   void TrackTriggers()
   {
      stateTriggers.Add("Menu");
      stateTriggers.Add("Win");
      stateTriggers.Add("Lose");
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
   }

   public void ResetTriggers()
   {
      foreach (String trigger in stateTriggers)
      {
         state.ResetTrigger(trigger);
      }
   }
}
