using UnityEngine;

namespace _GameFolders.Scripts
{
    public static class GameHelper
    {
        public static BaseDummy FindNearestDummy(Vector3 targetPosition)
        {
            BaseDummy nearestDummy = null;
            float closestDistanceSqr = Mathf.Infinity;

            foreach (var dummy in EnemyManager.Instance.Dummies)
            {
                float distanceSqr = (dummy.transform.position - targetPosition).sqrMagnitude;

                if (distanceSqr < closestDistanceSqr)
                {
                    closestDistanceSqr = distanceSqr;
                    nearestDummy = dummy;
                }
            }

            return nearestDummy;
        }
    }
}