using UnityEngine;
using System.Collections.Generic;

public class RackManager : MonoBehaviour
{
    [Header("预设的放置位置")]
    public List<Transform> slots; // 在编辑器里把子物体的Transform拖进来
    private bool[] isFull;

    void Awake()
    {
        isFull = new bool[slots.Count];
    }

    // 寻找最近的可用位置
    public Transform GetAvailableSlot(Vector3 currentPos)
    {
        float minDistance = float.MaxValue;
        int bestIndex = -1;

        for (int i = 0; i < slots.Count; i++)
        {
            if (!isFull[i])
            {
                float dist = Vector3.Distance(currentPos, slots[i].position);
                if (dist < minDistance)
                {
                    minDistance = dist;
                    bestIndex = i;
                }
            }
        }

        if (bestIndex != -1)
        {
            isFull[bestIndex] = true; // 标记该位置已被占用
            return slots[bestIndex];
        }
        return null; // 架子满了
    }
}