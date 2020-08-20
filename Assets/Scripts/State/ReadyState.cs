using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class ReadyState : StateMachineBehaviour
{
   public enum Task
   {
      none,
      loadMinigame,
      displayInfo,
      getSceneFromHierarchy,
      initializeMinigame,
      initializeScene,
      moveToGameOver,
      moveToFinalBoss,
   }

   public float timer = 0.0f;
   public Task task;
   public Task prev;

   private bool once = true;

   // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
   override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      Toolbox.Instance.CurrentState = GameState.State.Ready;
      Toolbox.Instance.MiniManager.timer.Reset();

      prev = Task.none;
      task = Task.none;

      if (Toolbox.Instance.Vars.isGameOver)
      {
         task = Task.moveToGameOver;
      }
      else
      {
         Toolbox.Instance.MiniManager.LoadNextMinigame();
         task = Task.loadMinigame;
      }
   }

   // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
   override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      if (prev != task)
      {
         prev = task;
      }

      if (Toolbox.Instance.Vars.isGameOver)
      {
         task = Task.moveToGameOver;
      }
      //else if (Toolbox.Instance.Vars.minigamesRemaining <= 0)
      if (once)
      {
         Debug.LogWarning("Holy shit dont forget to fix this");
         Toolbox.Instance.Vars.minigamesRemaining = 0;
         task = Task.moveToFinalBoss;
      }

      switch (task)
      {
         case Task.loadMinigame:
            if (Toolbox.Instance.State.timeInState >= 2.0f)
            {
               task = Task.displayInfo;
            }
            break;
         case Task.displayInfo:
            if (Toolbox.Instance.State.timeInState >= 3.0f && Toolbox.Instance.MiniManager.minigameScene.progress >= 0.9f)
            {
               Toolbox.Instance.AssetAnim.DisplayPregameInfo();
               Toolbox.Instance.MiniManager.minigameScene.allowSceneActivation = true;
               task = Task.getSceneFromHierarchy;
            }
            break;
         case Task.getSceneFromHierarchy:
            if (Toolbox.Instance.State.timeInState >= 5.0f && Toolbox.Instance.MiniManager.scene.IsValid())
            {
               Toolbox.Instance.AssetAnim.MaximizeGameBoard();
               task = Task.initializeScene;
            }
            break;
         case Task.initializeScene:
            if (Toolbox.Instance.MiniManager.scene.IsValid())
            {
               Toolbox.Instance.MiniManager.InitScene();
               //Toolbox.Instance.Canvas.canvasElements[0].SetActive(true);
               Toolbox.Instance.AssetAnim.ShowGameWindow();
               Toolbox.Instance.AssetAnim.DisplayMinigameWindow();
               task = Task.initializeMinigame;
            }
            break;
         case Task.initializeMinigame:
            if (Toolbox.Instance.MiniManager.minigameScript != null)
            {
               Toolbox.Instance.MiniManager.InitMinigame();
               Toolbox.Instance.State.SetTrigger(GameState.Trigger.Playing);
            }
            break;
         case Task.moveToGameOver:
            //TODO: Gameover
            Debug.LogWarning("GameOver; todo");
            SceneManager.LoadScene(0);
            break;
         case Task.moveToFinalBoss:
            Toolbox.Instance.State.stateAnim.SetBool("BossFight", true);
            break;

      }

      timer += Time.deltaTime;
   }

   //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
   //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   //{
   //}

   // OnStateMove is called right after Animator.OnAnimatorMove()
   //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   //{
   //    // Implement code that processes and affects root motion
   //}

   // OnStateIK is called right after Animator.OnAnimatorIK()
   //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   //{
   //    // Implement code that sets up animation IK (inverse kinematics)
   //}
}