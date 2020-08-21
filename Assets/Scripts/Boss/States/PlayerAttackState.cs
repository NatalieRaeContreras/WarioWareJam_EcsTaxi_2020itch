using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttackState : StateMachineBehaviour
{
   public bool sceneLoaded = false;
   public bool once = true;
   public bool winOnce = true;
   public bool done = false;
   public bool timeLatch = true;

   // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
   override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      Toolbox.Instance.MiniManager.result = MinigameManager.MinigameState.None;
      Toolbox.Instance.BossScript.CloseTextBox();
      Toolbox.Instance.BossScript.HideReadableText();
      Toolbox.Instance.AssetAnim.OpenGameBoard();
      Toolbox.Instance.AssetAnim.DisplayMinigameWindow();
      Toolbox.Instance.AssetAnim.ShowGameWindow();
      Toolbox.Instance.BossScript.bossStateMachine.ResetTrigger("p_ActionComplete");
      Toolbox.Instance.Vars.attackComplete = false;
      sceneLoaded = false;
      once = true;
      winOnce = true;
      done = false;
      timeLatch = false;
   }

   // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
   override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      if (!Toolbox.Instance.Vars.boardIsClosed && !done)
      {
         if (winOnce)
         {
            Toolbox.Instance.BossScript.attackSceneAsyncOp.allowSceneActivation = true;
            winOnce = false;
         }

         if (!Toolbox.Instance.Vars.windowIsClosed && timeLatch)
         {
            Toolbox.Instance.MiniManager.timer.Reset();
            Toolbox.Instance.MiniManager.timer.Activate();
            timeLatch = true;
         }

         if (Toolbox.Instance.BossScript.attackScene.IsValid() && !sceneLoaded)
         {
            sceneLoaded = true;
            Toolbox.Instance.BossScript.InitScene(Toolbox.Instance.BossScript.attackScene);
            Toolbox.Instance.MiniManager.minigameScript.InitMinigame();
         }
         else if ((sceneLoaded && once) &&
                  ((Toolbox.Instance.Vars.attackComplete) ||
                   (Toolbox.Instance.MiniManager.timer.TimeOut)))
         {
            Toolbox.Instance.AssetAnim.GameBoard.ResetTrigger("Open");
            Toolbox.Instance.AssetAnim.CloseGameBoard();
            Toolbox.Instance.AssetAnim.CloseGameWindow();
            Toolbox.Instance.MiniManager.minigameScript.Active = false;
            once = false;
            done = true;

            if (Toolbox.Instance.MiniManager.timer.TimeOut && !Toolbox.Instance.Vars.attackComplete)
            {
               Toolbox.Instance.MiniManager.result = MinigameManager.MinigameState.Lose;
               Toolbox.Instance.Vars.attackSuccess = false;
            }
         }
      }
      else if (done && Toolbox.Instance.Vars.boardIsClosed)
      {
         if (Toolbox.Instance.Vars.attackSuccess)
         {
            Toolbox.Instance.AssetAnim.Indicator.SetTrigger("Hit");
            Toolbox.Instance.AssetAnim.WinnerAnim.SetTrigger("Go");
            Toolbox.Instance.Vars.taxiHealth -= 2;
         }

         Toolbox.Instance.MiniManager.timer.Reset();
         Toolbox.Instance.BossScript.bossStateMachine.SetTrigger("p_ActionComplete");
      }
   }

   // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
   override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      SceneManager.UnloadSceneAsync(Toolbox.Instance.BossScript.attackScene);
      Toolbox.Instance.BossScript.UnloadMinigame();
      Toolbox.Instance.BossScript.attackSceneAsyncOp = null;
   }
}
