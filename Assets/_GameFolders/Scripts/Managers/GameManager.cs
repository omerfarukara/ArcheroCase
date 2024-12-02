namespace _GameFolders.Scripts
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public GameState GameState { get; set; } = GameState.Playing;
        
        private AbilityManager _abilityManager;
        public AbilityManager AbilityManager => _abilityManager; 
        
        public CharacterController CharacterController { get; set; }

        
        protected override void Awake()
        {
            base.Awake();
            _abilityManager = new AbilityManager();
        }

        private void OnEnable()
        {
            GameEventManager.OnSetGameState += SetState;
        }

        private void OnDisable()
        {
            GameEventManager.OnSetGameState -= SetState;
        }

        private void SetState(GameState newState)
        {
            GameState = newState;
        }

        public bool IsPlayable()
        {
            return GameState == GameState.Playing;
        }
    }
}