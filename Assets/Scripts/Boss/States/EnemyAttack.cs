using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAttack : StateMachineBehaviour
{
   public bool sceneLoaded;
   public bool once = true;
   public bool done = false;
   public float timer = 0.0f;

   // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
   override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      Toolbox.Instance.AssetAnim.OpenGameBoard();
      Toolbox.Instance.AssetAnim.DisplayMinigameWindow();
      Toolbox.Instance.AssetAnim.ShowGameWindow();
      Toolbox.Instance.Vars.attackComplete = false;
      Toolbox.Instance.BossScript.bossStateMachine.ResetTrigger("Base_toCombat");

      sceneLoaded = false;
      once = true;
      done = false;
   }

   // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
   override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      if (Toolbox.Instance.BossScript.taxiAttackScene.IsValid() && !sceneLoaded)
      {
         sceneLoaded = true;
         Toolbox.Instance.BossScript.InitScene(Toolbox.Instance.BossScript.taxiAttackScene);
         Toolbox.Instance.MiniManager.minigameScript.InitMinigame();
         Toolbox.Instance.AssetAnim.DisplayMinigameWindow();
         Toolbox.Instance.AssetAnim.ShowGameWindow();
      }
      else if (sceneLoaded && !done)
      {
         if (Toolbox.Instance.Vars.attackComplete)
         {
            if (once)
            {
               Toolbox.Instance.AssetAnim.GameBoard.ResetTrigger("Open");
               Toolbox.Instance.AssetAnim.CloseGameBoard();
               Toolbox.Instance.AssetAnim.CloseGameWindow();
               Toolbox.Instance.MiniManager.minigameScript.Active = false;
               once = false;
               done = true;
            }
         }
      }

      if (Toolbox.Instance.BossScript.taxiAttackAsyncOp != null)
      {
         if (Toolbox.Instance.BossScript.taxiAttackAsyncOp.progress >= 0.9f &&
             Toolbox.Instance.BossScript.taxiAttackAsyncOp.allowSceneActivation == false)
         {
            if (!Toolbox.Instance.Vars.boardIsClosed)
            {
               Toolbox.Instance.BossScript.taxiAttackAsyncOp.allowSceneActivation = true;
            }
         }
      }

      if (done && Toolbox.Instance.Vars.boardIsClosed)
      {
         Toolbox.Instance.BossScript.bossStateMachine.SetTrigger("Base_toTaxi");
      }
   }

   // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
   override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      SceneManager.UnloadSceneAsync(Toolbox.Instance.BossScript.taxiAttackScene);
      Toolbox.Instance.BossScript.taxiAttackAsyncOp = null;

      Toolbox.Instance.BossScript.UnloadMinigame();
   }
}
