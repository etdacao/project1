using UnityEngine;

public class DishInteraction : MonoBehaviour
{
    public LayerMask dishLayer;   // 碗所在的层
    public LayerMask rackLayer;   // 沥水架所在的层
    public float dragHeight = 2f; // 碗跟随鼠标时悬浮的高度

    private GameObject heldDish;  // 当前抓着的碗
    private bool isHolding = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isHolding)
                TryPickUp();
            else
                TryPlaceDown();
        }

        if (isHolding && heldDish != null)
        {
            FollowMouse();
        }
    }

    void TryPickUp()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, dishLayer))
        {
            heldDish = hit.collider.gameObject;
            isHolding = true;
            // 取消碗的物理特性，防止拖拽时乱撞
            if (heldDish.GetComponent<Rigidbody>()) 
                heldDish.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    void FollowMouse()
    {
        // 创建一个距离相机一定距离的平面，让碗在上面滑动
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5f; // 碗离摄像机的距离
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        
        // 平滑跟随
        heldDish.transform.position = Vector3.Lerp(heldDish.transform.position, worldPos, Time.deltaTime * 10f);
    }

    void TryPlaceDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // 检测鼠标是否在沥水架范围内
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, rackLayer))
        {
            RackManager rack = hit.collider.GetComponent<RackManager>();
            Transform targetSlot = rack.GetAvailableSlot(heldDish.transform.position);

            if (targetSlot != null)
            {
                // 成功放置：对齐到槽位
                heldDish.transform.position = targetSlot.position;
                heldDish.transform.rotation = targetSlot.rotation;
                
                // 放置成功后重置状态
                heldDish = null;
                isHolding = false;
            }
            else
            {
                Debug.Log("架子满了！");
            }
        }
        else
        {
            // 如果点到了空地上，可以逻辑处理为：放回原位或者扔回水槽
            Debug.Log("请点击沥水架来放置。");
        }
    }
}