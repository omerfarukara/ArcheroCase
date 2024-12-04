using UnityEngine;

namespace _GameFolders.Scripts
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField] private FindNearestSystem findNearestSystem;
        public FindNearestSystem FindNearestSystem => findNearestSystem;
        
        [Space]

        #region Ability Scriptables
        [SerializeField] private ArrowBounce arrowBounce;
        [SerializeField] private ArrowPerAttack arrowPerAttack;
        [SerializeField] private BurningArrow burningArrow;
        [SerializeField] private AttackSpeed attackSpeed;
        [SerializeField] private RageMode rageMode;

        public ArrowBounce ArrowBounce => arrowBounce;
        public ArrowPerAttack ArrowPerAttack => arrowPerAttack;
        public BurningArrow BurningArrow => burningArrow;
        public AttackSpeed AttackSpeed => attackSpeed;
        public RageMode RageMode => rageMode;
        #endregion
        
        private AbilityManager _abilityManager;
        public AbilityManager AbilityManager => _abilityManager; 
        
        public CharacterController CharacterController { get; set; }

        public GameState GameState { get; set; }
       
        protected override void Awake()
        {
            base.Awake();
            _abilityManager = new AbilityManager();
        }

        private void OnEnable()
        {
            GameEventManager.OnSetGameState += SetState;
        }

        private void Start()
        {
            CameraOrthographicSizeSet();
        }

        private static void CameraOrthographicSizeSet()
        {
            float targetAspect = 9f / 16f;
            float currentAspect = (float)Screen.width / Screen.height;
            Camera.main.orthographicSize = currentAspect > targetAspect ? 5.8f : 5.8f * (targetAspect / currentAspect);
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