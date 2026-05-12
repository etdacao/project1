using UnityEngine;

public class CleanPlate : MonoBehaviour
{
    // 预制体（Prefab）引用，拖拽指派你的“干净碗”预制体到脚本里
    public GameObject cleanPlatePrefab;

    void OnTriggerStay(Collider other)
    {
        // 检查进入触发器的物体是否有 "Dirty" 标签，并且玩家按下了空格键
        if (other.CompareTag("Dirty") && Input.GetKeyDown(KeyCode.Space))
        {
            // 获取这个脏碗的位置
            Vector3 dirtyPlatePosition = other.transform.position;
            
            // 1. 销毁脏碗
            Destroy(other.gameObject);
            
            // 2. 在相同位置生成干净碗
            Instantiate(cleanPlatePrefab, dirtyPlatePosition, Quaternion.identity);
        }
    }
}