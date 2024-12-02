using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _GameFolders.Scripts
{
    public class CharacterController : BaseCharacter
    {
        private UIManager _uiManager;
        private GameManager _gameManager;
        private EnemyManager _enemyManager;

        private void Start()
        {
            _enemyManager = EnemyManager.Instance;
            _uiManager = UIManager.Instance;
            _gameManager = GameManager.Instance;
            _gameManager.CharacterController = this;
        }

        private void Update()
        {
            if (!_gameManager.IsPlayable()) return;

            Character.Move(_uiManager.GetDirection());
            MoveState = _uiManager.GetDirection().magnitude == 0 ? MoveState.NotMoving : MoveState.Moving;

            if (IsAttack)
            {
                BaseDummy nearestDummy = _enemyManager.KdTree.FindNearest(transform.position);
                // BaseDummy nearestDummy = _enemyManager.Dummies.OrderBy(d => Vector3.Distance(transform.position, d.transform.position)).FirstOrDefault();
                // BaseDummy nearestDummy = GameHelper.FindNearestDummy(transform.position);
                Debug.Log($"En yakın dummy: {nearestDummy.name}", nearestDummy.gameObject);
            }

            SetAnimationState();
        }


        private void SetAnimationState()
        {
            if (MoveState == MoveState.NotMoving)
            {
                PlayAnimation(PlayingAnimationState.Attack).Forget();
            }
            else
            {
                if (!animationController.IsAnimationPlaying(PlayingAnimationState.Run))
                {
                    animationController.StopAnimation();
                    PlayAnimation(PlayingAnimationState.Run).Forget();
                }
            }
        }
    }
}