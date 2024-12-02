using System.Collections.Generic;
using _GameFolders.Scripts;
using UnityEngine;

public class KdTree
{
    private class Node
    {
        public BaseDummy Dummy;
        public Vector3 Position;
        public Node Left;
        public Node Right;
    }

    private Node _root;

    public void Build(List<BaseDummy> dummies)
    {
        _root = BuildRecursive(dummies, 0);
    }

    private Node BuildRecursive(List<BaseDummy> dummies, int depth)
    {
        if (dummies.Count == 0) return null;

        int axis = depth % 3; // 0 = X, 1 = Y, 2 = Z
        dummies.Sort((a, b) => a.transform.position[axis].CompareTo(b.transform.position[axis]));

        int medianIndex = dummies.Count / 2;
        BaseDummy medianDummy = dummies[medianIndex];

        Node node = new Node
        {
            Dummy = medianDummy,
            Position = medianDummy.transform.position,
            Left = BuildRecursive(dummies.GetRange(0, medianIndex), depth + 1),
            Right = BuildRecursive(dummies.GetRange(medianIndex + 1, dummies.Count - medianIndex - 1), depth + 1)
        };

        return node;
    }

    public BaseDummy FindNearest(Vector3 targetPosition)
    {
        return FindNearestRecursive(_root, targetPosition, 0)?.Dummy;
    }

    private Node FindNearestRecursive(Node node, Vector3 targetPosition, int depth)
    {
        if (node == null) return null;

        int axis = depth % 3;
        Node nextNode = targetPosition[axis] < node.Position[axis] ? node.Left : node.Right;
        Node otherNode = nextNode == node.Left ? node.Right : node.Left;

        Node best = ClosestNode(node, FindNearestRecursive(nextNode, targetPosition, depth + 1), targetPosition);
        if (otherNode != null && Mathf.Abs(targetPosition[axis] - node.Position[axis]) < Vector3.Distance(targetPosition, best.Position))
        {
            best = ClosestNode(best, FindNearestRecursive(otherNode, targetPosition, depth + 1), targetPosition);
        }

        return best;
    }

    private Node ClosestNode(Node a, Node b, Vector3 targetPosition)
    {
        if (a == null) return b;
        if (b == null) return a;

        float distA = (a.Position - targetPosition).sqrMagnitude;
        float distB = (b.Position - targetPosition).sqrMagnitude;

        return distA < distB ? a : b;
    }
}
