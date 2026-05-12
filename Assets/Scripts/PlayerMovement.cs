using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("移动参数")]
    public float moveSpeed = 5f;        // 移动速度
    public float turnSpeed = 10f;       // 转向速度
    public float gravity = 9.81f;       // 重力

    float horizontal = 0f;
    float vertical = 0f; // 如果需要前后移动，可以使用这个输入

    public bool isPlayerAlive = true; // 玩家是否存活
    public int playerID; // 玩家ID，1或2
    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero; //(1,1,1)
    private Quaternion targetRotation; //(1,1,1,0.5pai)
    //private Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        //animator = GetComponent<Animator>();
        // 初始化朝向为当前朝向
        //targetRotation = transform.rotation;
    }

    void Update()
    {
        // 1. 获取输入 (A/D 或 左右方向键)
        
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical"); // 如果需要前后移动，可以使用这个输入

        // 计算目标朝向
        if(horizontal != 0 || vertical != 0)
        {
            targetRotation = Quaternion.LookRotation(new Vector3(horizontal, 0, vertical));
        }


        Vector3 move = new Vector3(horizontal, 0, vertical);
        // 平滑转向过渡 
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);

        // 4. 应用重力 (确保角色贴地)
        if (controller.isGrounded)
        {
            moveDirection.y = -0.5f; 
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
    
        // 5. 执行移动
        controller.Move(move * moveSpeed * Time.deltaTime + moveDirection * Time.deltaTime);
    }
}
