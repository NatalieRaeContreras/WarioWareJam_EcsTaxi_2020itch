using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TaxiBoss : BaseMinigame
{
   public int taxiHealth = 5;
   public int playerHealth = 20;
   public bool attackSuccess = false;
   public bool attackComplete = false;

   public Animator NULLANIM;
   public SpriteRenderer NULLSPRITE;

   public AssetAnimator bossAssetAnim;
   public Text playerHP;
   public Animator bossStateMachine;
   public Animator subGameRender;
   public BaseMinigame subGameScript;
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

   private Vector2 countdownLocation = new Vector2(85f, -45f);
   private Vector2 timerLocation = new Vector2(170f, 30f);

   public bool boardIsClosed;
   public bool windowIsClosed;

   public bool isAwake = false;

   public override void InitMinigame()
   {
      playerHP.text = "" + playerHealth;

      SetupAssets();
      Toolbox.Instance.AssetAnim = bossAssetAnim;
      Toolbox.Instance.BossScript.HideSelectionIndicators();

      Active = true;

      Toolbox.Instance.AssetAnim.GameBoard.ResetTrigger("Open");
      Toolbox.Instance.AssetAnim.CloseGameBoard();
      Toolbox.Instance.AssetAnim.CloseGameWindow();
   }

   // Start is called before the first frame update
   private void Start()
   {
      Toolbox.Instance.SetMinigameScript(this);
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

      bossAssetAnim.Init();
      bossAssetAnim.ActivatePreGame();

      Toolbox.Instance.Canvas.canvasElements[0].SetActive(false);
      bossAssetAnim.GameWindow = window;
      bossAssetAnim.GameBoard = gameBoard;

      bossAssetAnim.LoserAnim = NULLANIM;
      bossAssetAnim.WinnerAnim = NULLANIM;
      bossAssetAnim.midgroundImage = NULLSPRITE;

      Destroy(Toolbox.Instance.AssetAnim);
      Toolbox.Instance.AttachAssetAnimator(bossAssetAnim);

      Toolbox.Instance.AssetAnim.Indicator = indicator;

      Toolbox.Instance.Canvas.canvasElements[1].transform.position = timerLocation;
      Toolbox.Instance.Canvas.canvasElements[2].transform.position = countdownLocation;
   }

   public void UnloadMinigame()
   {
      Toolbox.Instance.BossScript.subGameScript.Active = false;
      Toolbox.Instance.BossScript.subGameScript = null;
      Resources.UnloadUnusedAssets();
      Toolbox.Instance.BossScript.attackComplete = false;
      Toolbox.Instance.BossScript.attackSuccess = false;
      Toolbox.Instance.MiniManager.result = MinigameManager.MinigameState.None;
   }

   // Update is called once per frame
   private void Update()
   {
      playerHP.text = "" + playerHealth;
   }
}