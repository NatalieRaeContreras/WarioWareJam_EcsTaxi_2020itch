using UnityEngine;

public class Toolbox : Singleton<Toolbox>
{
   public GameState.State CurrentState
   {
      get
      {
         if (State.current == GameState.State.Playing && MiniManager.result != MinigameManager.MinigameState.None)
         {
            return GameState.State.Transitioning;
         }
         else if (State.current == GameState.State.Playing && MiniManager.result != MinigameManager.MinigameState.None)
         {
            return GameState.State.Transitioning;
         }
         else return State.current;
      }
      set
      {
         State.prev = State.current;
         State.current = value;
      }
   }

   public GameState State { get => _state; set => _state = value; }
   public GameVars Vars { get => _vars; set => _vars = value; }
   public MinigameManager MiniManager { get => _miniMan; set => _miniMan = value; }
   public AssetAnimator AssetAnim { get => _anim; set => _anim = value; }
   public CanvasManager Canvas { get => _canvas; set => _canvas = value; }

   [SerializeField]
   private GameState _state;
   [SerializeField]
   private GameVars _vars;
   [SerializeField]
   private MinigameManager _miniMan;
   [SerializeField]
   private AssetAnimator _anim;
   [SerializeField]
   private CanvasManager _canvas;

   private float timeInState;



   public void SetMinigameScript(BaseMinigame script)
   {
      MiniManager.minigameScript = script;
   }

   //==========================================================================
   public bool AttachGameState(GameState gs)
   {
      State = gs;
      return State == null;
   }

   //==========================================================================
   public bool AttachGameState()
   {
      State = GameObject.FindObjectOfType<GameState>();
      return State == null;
   }

   //==========================================================================
   public bool AttachMinigameManager(MinigameManager mm)
   {
      MiniManager = mm;
      return MiniManager == null;
   }

   //==========================================================================
   public bool AttachMinigameManager()
   {
      MiniManager = GameObject.FindObjectOfType<MinigameManager>();
      return MiniManager == null;
   }

   //==========================================================================
   public bool AttachAssetAnimator(AssetAnimator aa)
   {
      AssetAnim = aa;
      return AssetAnim == null;
   }

   //==========================================================================
   public bool AttachAssetAnimator()
   {
      AssetAnim = GameObject.FindObjectOfType<AssetAnimator>();
      return AssetAnim == null;
   }

   //==========================================================================
   public bool AttachCanvasManager(CanvasManager cm)
   {
      Canvas = cm;
      return Canvas == null;
   }

   //==========================================================================
   public bool AttachCanvasManager()
   {
      Canvas = GameObject.FindObjectOfType<CanvasManager>();
      return Canvas == null;
   }

   //==========================================================================
   private void Awake()
   {
      Vars = new GameVars();
      DontDestroyOnLoad(this);
   }

   //==========================================================================
   private void Update()
   {
      if (State.PreviousState != CurrentState)
      {
         timeInState = 0.0f;
         State.PreviousState = State.current;
      }
      if (Vars.health <= 0 && !Vars.isGameOver)
      {
         Vars.isGameOver = true;
      }

      timeInState += Time.deltaTime;
   }
}