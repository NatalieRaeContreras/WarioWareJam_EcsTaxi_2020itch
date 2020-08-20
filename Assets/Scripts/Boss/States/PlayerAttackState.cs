using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : StateMachineBehaviour
{

   public bool sceneLoaded = false;
   public bool once = true;
   public bool winOnce = true;
   public bool done = false;

   // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
   override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      Toolbox.Instance.AssetAnim.OpenGameBoard();
      Toolbox.Instance.AssetAnim.DisplayMinigameWindow();
      Toolbox.Instance.AssetAnim.ShowGameWindow();
      Toolbox.Instance.BossScript.bossStateMachine.ResetTrigger("p_ActionComplete");
      Toolbox.Instance.BossScript.attackComplete = false;
      sceneLoaded = false;
      once = true;
      winOnce = true;
      done = false;
   }

   // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
   override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      if (!Toolbox.Instance.BossScript.boardIsClosed && !done)
      {
         if (winOnce)
         {
            Toolbox.Instance.BossScript.attackSceneAsyncOp.allowSceneActivation = true;
            winOnce = false;
         }

         if (Toolbox.Instance.BossScript.attackScene.IsValid() && !sceneLoaded)
         {
            sceneLoaded = true;
            Toolbox.Instance.BossScript.InitScene(Toolbox.Instance.BossScript.attackScene);
            Toolbox.Instance.BossScript.subGameScript.InitMinigame();
         }
         else if (sceneLoaded && Toolbox.Instance.BossScript.attackComplete && once)
         {
            Toolbox.Instance.AssetAnim.GameBoard.ResetTrigger("Open");
            Toolbox.Instance.AssetAnim.CloseGameBoard();
            Toolbox.Instance.AssetAnim.CloseGameWindow();
            Toolbox.Instance.BossScript.subGameScript.Active = false;
            once = false;
            done = true;
         }
      }
      else if (done && Toolbox.Instance.BossScript.boardIsClosed)
      {
         if (Toolbox.Instance.BossScript.attackSuccess)
         {
            Toolbox.Instance.AssetAnim.Indicator.SetTrigger("Hit");
         }
         Toolbox.Instance.BossScript.bossStateMachine.SetTrigger("p_ActionComplete");
      }
   }

   // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
   override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {

   }
}
