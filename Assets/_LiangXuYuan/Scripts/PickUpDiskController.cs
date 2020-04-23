using UnityEngine;

[RequireComponent(typeof(DiskEventMainController))]
public class PickUpDiskController : MonoBehaviour
{
    [Header("玩家")]
    [Tooltip("玩家GameObject")]
    public Transform player;

    [Tooltip("物体移动到的目标位置，这个位置是玩家的相对位置"),SerializeField]
    private Vector3 targetLocalPosition = new Vector3(0f, -0.3f, 0f);

    [Tooltip("物体移动速度，一秒多少米"), SerializeField]
    private float moveSpeed = 1f;

    private DiskEventMainController diskEventMainController;//总控制脚本，用于返回捡起磁盘动作完毕的消息

    private bool is_pickup = false; //标记磁盘是否正在被玩家捡起

    private void Start()
    {
        // 初始化私有变量
        diskEventMainController = GetComponent<DiskEventMainController>();
    }

    private void Update()
    {
        if(is_pickup)
        {
            Vector3 currPosition = transform.position - (player.transform.position + targetLocalPosition); // 当前磁盘相对于玩家的位置
            Vector3 nextPosition = currPosition.normalized * (currPosition.magnitude - moveSpeed * Time.deltaTime); // 这一帧磁盘相对位置
            transform.position = player.transform.position + targetLocalPosition + nextPosition; // 设置磁盘位置

            // 当距离小到一定程度，就让物体消失，表示物体已经捡到了
            if ((player.transform.position + targetLocalPosition - transform.position).magnitude < 0.1f)
            {
                // 恢复标记
                is_pickup = false;

                // 将Renderer取消掉，将碰撞体取消掉，产生磁盘消失的效果
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Collider>().enabled = false;

                // 发送完成的消息
                //Debug.Log("玩家捡起了磁盘");
                diskEventMainController.OnPickUpDiskEnd();

                // 关闭此脚本
                this.enabled = false;
            }
        }
    }

    public void PickUpDisk()
    {
        is_pickup = true;
    }
}
