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
      moveToPlay,
      moveToGameOver,
   }

   public float timer = 0.0f;
   public Task task;
   public Task prev;


   // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
   override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      Toolbox.Instance.CurrentState = GameState.State.Ready;
      Toolbox.Instance.MiniManager.timer.Reset();

      prev = Task.none;
      task = Task.none;

      if (Toolbox.Instance.Vars.isGameOver)
         task = Task.moveToGameOver;
      else
         task = Task.loadMinigame;
   }

   // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
   override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      if (prev != task)
      {
         PreformTasks();
         prev = task;
      }

      switch (task)
      {
         case Task.loadMinigame:
            if (Toolbox.Instance.MiniManager.minigameScene.progress >= 0.9f && Toolbox.Instance.State.timeInState >= 3.0f)
               task = Task.displayInfo;
            break;
         case Task.displayInfo:
            if (Toolbox.Instance.State.timeInState >= 4.0f)
               task = Task.getSceneFromHierarchy;
            break;
         case Task.getSceneFromHierarchy:
            if (Toolbox.Instance.State.timeInState >= 5.0f && Toolbox.Instance.MiniManager.scene.IsValid())
               task = Task.initializeScene;
            break;
         case Task.initializeScene:
            if (Toolbox.Instance.MiniManager.scene.IsValid())
               task = Task.initializeMinigame;
            break;
         case Task.initializeMinigame:
            if (Toolbox.Instance.MiniManager.minigameScript != null)
            {
               Toolbox.Instance.State.SetTrigger(GameState.Trigger.Playing);
               task = Task.moveToPlay;
            }
            break;
         case Task.moveToPlay:
            break;
         //default:
         //   Debug.LogError("ErrState: " + this.ToString() + " task: " + this.task);
         //   break;
      }

      timer += Time.deltaTime;
   }

   private void PreformTasks()
   {
      switch (task)
      {
         case Task.loadMinigame:
            Toolbox.Instance.MiniManager.LoadNextMinigame();
            break;
         case Task.displayInfo:
            Toolbox.Instance.AssetAnim.DisplayPregameInfo();
            break;
         case Task.getSceneFromHierarchy:
            Toolbox.Instance.MiniManager.minigameScene.allowSceneActivation = true;
            break;
         case Task.initializeScene:
            Toolbox.Instance.MiniManager.InitScene();
            break;
         case Task.initializeMinigame:
            Toolbox.Instance.MiniManager.InitMinigame();
            break;
         //case Task.moveToPlay:
         //   Toolbox.Instance.State.SetTrigger(GameState.Trigger.Playing);
            //break;
      }
   }

   //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
   override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      Toolbox.Instance.Canvas.canvasElements[0].SetActive(true);
   }

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