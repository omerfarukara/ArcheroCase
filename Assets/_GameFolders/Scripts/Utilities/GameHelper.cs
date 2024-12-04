using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _GameFolders.Scripts
{
    public static class GameHelper
    {
        public static async UniTask LaunchProjectileAsync(
            Transform arrowTransform,
            Transform target,
            float gravity,
            float launchAngle,
            GameManager gameManager,
            CancellationToken cancellationToken)
        {
            if (arrowTransform == null || target == null || gameManager == null)
            {
                return;
            }

            Vector3 startPosition = arrowTransform.position;
            Vector3 targetPosition = target.position;

            if (float.IsNaN(startPosition.x) || float.IsNaN(startPosition.y) || float.IsNaN(startPosition.z) ||
                float.IsNaN(targetPosition.x) || float.IsNaN(targetPosition.y) || float.IsNaN(targetPosition.z))
            {
                return;
            }

            Vector3 direction = new Vector3(
                targetPosition.x - startPosition.x,
                0,
                targetPosition.z - startPosition.z
            ).normalized;

            float distance = Vector3.Distance(
                new Vector3(startPosition.x, 0, startPosition.z),
                new Vector3(targetPosition.x, 0, targetPosition.z)
            );

            if (distance <= 0)
            {
                return;
            }

            float launchAngleRad = Mathf.Deg2Rad * launchAngle;

            if (Mathf.Sin(2 * launchAngleRad) == 0)
            {
                return;
            }

            float initialVelocity = Mathf.Sqrt((distance * gravity) / Mathf.Sin(2 * launchAngleRad));

            Vector3 velocity = direction * (initialVelocity * Mathf.Cos(launchAngleRad));
            velocity.y = initialVelocity * Mathf.Sin(launchAngleRad);

            await SimulateProjectileAsync(arrowTransform, velocity, gravity, gameManager, cancellationToken);
        }

        private static async UniTask SimulateProjectileAsync(
            Transform arrowTransform,
            Vector3 velocity,
            float gravity,
            GameManager gameManager,
            CancellationToken cancellationToken)
        {
            Vector3 position = arrowTransform.position;
            Vector3 gravityVector = Vector3.down * gravity;

            while (position.y >= -1)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                while (!gameManager.IsPlayable())
                {
                    await UniTask.Yield();
                }

                velocity += gravityVector * Time.deltaTime;
                position += velocity * Time.deltaTime;

                if (arrowTransform != null)
                {
                    arrowTransform.position = position;

                    if (velocity != Vector3.zero)
                    {
                        arrowTransform.rotation = Quaternion.LookRotation(velocity, Vector3.up);
                    }
                }

                await UniTask.Yield(PlayerLoopTiming.FixedUpdate);
            }
        }

        public static BaseDummy FindNearestDummy(List<BaseDummy> dummies,Vector3 myPosition)
        {
            BaseDummy nearestDummy = null;
            float closestDistanceSqr = Mathf.Infinity;

            foreach (BaseDummy dummy in dummies)
            {
                float distanceSqr = (dummy.transform.position - myPosition).sqrMagnitude;

                if (distanceSqr < closestDistanceSqr)
                {
                    closestDistanceSqr = distanceSqr;
                    nearestDummy = dummy;
                }
            }

            return nearestDummy;
        }


        public static BaseDummy FindSelectedSystemDummy(List<BaseDummy> dummies,Vector3 myPosition)
        {
            switch (GameManager.Instance.FindNearestSystem)
            {
                case FindNearestSystem.MySystem:
                    return FindNearestDummy(dummies,myPosition);
                case FindNearestSystem.Linq:
                    return dummies.OrderBy(d => Vector3.Distance(myPosition, d.transform.position)).FirstOrDefault();
                default:
                    return FindNearestDummy(dummies,myPosition);
            }
        }


        public static float GetMultiplierAbilityCount(float currentValue, float rageMultiplier)
        {
            float value = 0;

            if (GameManager.Instance.AbilityManager.RageModeActive)
            {
                value = currentValue * rageMultiplier;
            }
            else
            {
                value = currentValue;
            }

            return value;
        }

        public static float GetDivisionCount(float currentValue, float rageMultiplier)
        {
            float value = 0;

            if (GameManager.Instance.AbilityManager.RageModeActive)
            {
                value = currentValue / rageMultiplier;
            }
            else
            {
                value = currentValue;
            }

            return value;
        }
    }
}