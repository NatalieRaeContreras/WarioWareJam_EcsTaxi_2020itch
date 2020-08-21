using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TaxiBoss : MonoBehaviour
{
   public Animator fade;
   public Animator NULLANIM;
   public SpriteRenderer NULLSPRITE;

   private bool once = true;

   public AssetAnimator bossAssetAnim;
   public Text playerHP;
   public Animator bossStateMachine;
   public Animator subGameRender;

   public Sprite actionVerb;

   public Image advanceText;
   public Animator playerIndicator;
   public Animator textBox;

   public PlayerSelectChoice _playerSelectChoice;
   public PlayerChoiceMade _playerChoiceMade;
   public TaxiBossStrings _taxiStrings;
   public PlayerAttackState _playerAttackState;

   public AsyncOperation attackSceneAsyncOp;
   public AsyncOperation taxiAttackAsyncOp;
   public Scene attackScene;
   public Scene taxiAttackScene;

   public List<Image> fightBars = new List<Image>();
   public List<Image> choices = new List<Image>();
   public List<Text> lines = new List<Text>();

   private Vector2 countdownLocation = new Vector2(80f, -50f);
   private Vector2 timerLocation = new Vector2(-400f, -400f);
   private Vector2 winConUILoc = new Vector2(-60f, -40f);

   //public bool boardIsClosed;
   //public bool windowIsClosed;

   public bool isAwake = false;

   public bool Active { get; set; }

   public void Init()
   {
      once = true;
      Active = true;
      playerHP.text = "" + Toolbox.Instance.Vars.playerHealth;

      SetupAssets();
      Toolbox.Instance.AssetAnim = bossAssetAnim;
      Toolbox.Instance.BossScript.HideSelectionIndicators();

      Toolbox.Instance.AssetAnim.GameBoard.ResetTrigger("Open");
      Toolbox.Instance.AssetAnim.CloseGameBoard();
      Toolbox.Instance.AssetAnim.CloseGameWindow();

      Toolbox.Instance.Vars.taxiHealth = 1;
      Toolbox.Instance.Vars.playerHealth = 1;
   }

   // Start is called before the first frame update
   private void Start()
   {
      Toolbox.Instance.BossScript = this;
      _playerSelectChoice.choices = this.choices;
      _taxiStrings = new TaxiBossStrings
      {
         lines = this.lines
      };

      isAwake = true;
   }

   public void LoadPlayerAttack()
   {
      var sceneParameters = new LoadSceneParameters(LoadSceneMode.Additive);
      attackSceneAsyncOp = SceneManager.LoadSceneAsync("Scenes/Boss/AttackIndicator", sceneParameters);
      attackSceneAsyncOp.completed += (operation) =>
      {
         attackScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
      };
      attackSceneAsyncOp.allowSceneActivation = false;
   }

   public void LoadTaxiAttack()
   {
      var sceneParameters = new LoadSceneParameters(LoadSceneMode.Additive);
      taxiAttackAsyncOp = SceneManager.LoadSceneAsync("Scenes/Boss/BossAttackOne", sceneParameters);
      taxiAttackAsyncOp.completed += (operation) =>
      {
         taxiAttackScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
      };
      taxiAttackAsyncOp.allowSceneActivation = false;
   }

   public void DisplayTextBox()
   {
      Toolbox.Instance.BossScript.textBox.ResetTrigger("Close");
      Toolbox.Instance.BossScript.textBox.SetTrigger("Open");

      Toolbox.Instance.BossScript.playerIndicator.SetTrigger("Selecting");
      Toolbox.Instance.BossScript.playerIndicator.ResetTrigger("Battling");
   }

   public void HideSelectionIndicators()
   {
      foreach (Image img in choices)
      {
         img.color = Color.clear;
      }
   }

   public void InitScene(Scene scene)
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

   public void HideTextAdvanceIndication()
   {
      advanceText.color = Color.clear;
   }

   public void DisplaySelectionIndicators(int index)
   {
      choices[index].color = Color.white;
   }

   public void DisplayTextAdvanceIndication()
   {
      advanceText.color = Color.white;
   }

   public void DisplayTextToRead()
   {
      foreach (Text tx in lines)
      {
         tx.color = Color.white;
      }
   }

   public void HideReadableText()
   {
      foreach (Text tx in lines)
      {
         tx.color = Color.clear;
         tx.text = "";
      }
   }

   public void CloseTextBox()
   {
      Toolbox.Instance.BossScript.textBox.ResetTrigger("Open");
      Toolbox.Instance.BossScript.textBox.SetTrigger("Close");

      Toolbox.Instance.BossScript.playerIndicator.ResetTrigger("Selecting");
      Toolbox.Instance.BossScript.playerIndicator.SetTrigger("Battling");
   }

   private void SetupAssets()
   {
      _taxiStrings.Init();

      var indicator = bossAssetAnim.Indicator;
      var window = bossAssetAnim.GameWindow;
      var gameBoard = bossAssetAnim.GameBoard;
      var Winner = bossAssetAnim.WinnerAnim;

      bossAssetAnim.Init();
      bossAssetAnim.ActivatePreGame();

      Toolbox.Instance.Canvas.canvasElements[0].SetActive(false);
      bossAssetAnim.GameWindow = window;
      bossAssetAnim.GameBoard = gameBoard;

      bossAssetAnim.LoserAnim = NULLANIM;
      bossAssetAnim.WinnerAnim = Winner;
      bossAssetAnim.midgroundImage = NULLSPRITE;
      bossAssetAnim.Indicator = indicator;

      Destroy(Toolbox.Instance.AssetAnim);
      Toolbox.Instance.AttachAssetAnimator(bossAssetAnim);
      var mainCanv = Toolbox.Instance.Canvas.GetComponent<Canvas>();
      mainCanv.renderMode = RenderMode.ScreenSpaceCamera;
      mainCanv.worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

      Toolbox.Instance.Canvas.canvasElements[2].GetComponent<RectTransform>().anchoredPosition = countdownLocation;
      Toolbox.Instance.Canvas.canvasElements[4].GetComponent<RectTransform>().anchoredPosition = timerLocation;
      Toolbox.Instance.Canvas.canvasElements[3].GetComponent<RectTransform>().anchoredPosition = winConUILoc;
      mainCanv.planeDistance = 0.5f;

      Toolbox.Instance.MiniManager.timer.Init();
      Toolbox.Instance.MiniManager.timer.killSwitch = false;
      Toolbox.Instance.MiniManager.timer.darkMode = false;
      Toolbox.Instance.MiniManager.timer.Reset();
   }

   public void UnloadMinigame()
   {
      Toolbox.Instance.MiniManager.minigameScript.Active = false;
      Toolbox.Instance.MiniManager.minigameScript = null;
      Resources.UnloadUnusedAssets();
      Toolbox.Instance.Vars.attackComplete = false;
      Toolbox.Instance.Vars.attackSuccess = false;
      Toolbox.Instance.MiniManager.result = MinigameManager.MinigameState.None;
   }

   // Update is called once per frame
   private void Update()
   {
      playerHP.text = "" + Toolbox.Instance.Vars.playerHealth;
      if (Toolbox.Instance.Vars.PlayerDefeated && once)
      {
         fade.SetTrigger("Go");
         once = false;
      }
   }
}
