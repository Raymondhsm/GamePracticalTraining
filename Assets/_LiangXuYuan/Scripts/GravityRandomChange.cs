using System.Collections;
using UnityEngine;

//abyss混沌深渊可用
public class GravityRandomChange : MonoBehaviour
{
    [Tooltip("把玩家GameObject拖到这里")]
    public GameObject PlayerGameObject;
    private Rigidbody PlayerRigidbody;
    private Transform PlayerTransform;

    // 用两个空物体作为“锚点”，锚点仅用于取它的位置值
    // 用“锚点”把混乱重力范围框住，此范围不关心y轴，只要xy轴框住一个范围就可以
    [Tooltip("锚点，用于框住混乱重力地带，左下角")]
    public Transform AnchorBottomLeft;
    [Tooltip("锚点，用于框住混乱重力地带，右上角")]
    public Transform AnchorTopRight;

    [Tooltip("重力变化的区间的单位值，即每个单位大小为g/level"), SerializeField]
    public int Level = 2;
    
    [Tooltip("重力最大变化倍数，重力变化区间是(0,max*g]"), SerializeField] //实际上是[g/level,max*g]
    public float Max = 2;

    [Tooltip("额外限定最大重力，随机重力不会超过额外限定最大重力"), SerializeField]
    public float MaxGravity = 1.5f;

    [Header("Space")]
    [Tooltip("将重力混乱地带分成DivideFactor*DivideFactor个区域")]
    public int DivideFactor = 3;

    [Header("Time")]
    [Tooltip("每隔TimeFactor秒刷新一次重力权值")]
    public int TimeFactor = 30;

    private float g = 9.81f;//unity的刚体默认重力是9.81

    private int[][] Area; // 分区重力表，存储重力权值

    void Start()
    {
        // 初始化玩家私有变量
        PlayerRigidbody = PlayerGameObject.GetComponent<Rigidbody>();
        PlayerTransform = PlayerGameObject.GetComponent<Transform>();

        // 创建Area二维数组，用于存储重力权值：Area[DivideFactor][DivideFactor]
        Area = new int[DivideFactor][];
        for(int i = 0; i < DivideFactor; i++)
        {
            Area[i] = new int[DivideFactor];
        }

        // 给Area输入随机数（每TimeFactor秒更新）
        StartCoroutine(SetRandomNumber());
    }

    void FixedUpdate()
    {
        float gravity = 9.81f;

        // 0.判断玩家是否在混乱重力区域
        bool isInRandomGravityField = true;
        if (PlayerTransform.position.x < AnchorBottomLeft.position.x) isInRandomGravityField = false;
        if (PlayerTransform.position.x > AnchorTopRight.position.x) isInRandomGravityField = false;
        if (PlayerTransform.position.z < AnchorBottomLeft.position.z) isInRandomGravityField = false;
        if (PlayerTransform.position.z > AnchorTopRight.position.z) isInRandomGravityField = false;
        if (isInRandomGravityField == false)
        {
            gravity = g;
            return;
        }

        // 1.玩家相对于混乱重力区左下角（俯视）的位置
        Vector3 playerPositionFromAnchor = PlayerTransform.position - AnchorBottomLeft.position;
        float playerX = playerPositionFromAnchor.x;
        float playerZ = playerPositionFromAnchor.z;

        // 2.计算区域范围
        float AreaXLength = AnchorTopRight.position.x - AnchorBottomLeft.position.x;    // 重力混乱区域X宽度
        float AreaZLength = AnchorTopRight.position.z - AnchorBottomLeft.position.z;    // 重力混乱区域Y宽度
        float DivideXLength = AreaXLength / (float)DivideFactor;    // 每分区X宽度
        float DivideZLength = AreaZLength / (float)DivideFactor;    // 每分区Y宽度

        // 3.计算玩家所在区域的index
        int AreaIIndex = (int)(playerZ / DivideZLength);    // Area[i][j]的i
        int AreaJIndex = (int)(playerX / DivideXLength);    // Area[i][j]的j
        AreaIIndex = DivideFactor - AreaIIndex - 1;         // 由于表是从上到下的，而地图Z轴是从下到上的，所以这里上下翻转一下

        // 4.获取所在区域的重力
        int numberUnitOfGravity = Area[AreaIIndex][AreaJIndex]; // 表示多少单位的重力
        gravity = (g / Level) * numberUnitOfGravity;    // 计算重力值

        // 4.将重力应用到玩家身上
        PlayerRigidbody.AddForce(-Vector3.up * (-g + gravity), ForceMode.Acceleration); // 先向下增加-g的加速度，抵消unity系统本身的重力，再增加计算的重力加速度

    }

    private IEnumerator SetRandomNumber()
    {
        while (true)
        {
            // 给Area输入随机数
            for (int i = 0; i < DivideFactor; ++i)
            {
                for (int j = 0; j < DivideFactor; ++j)
                {
                    // 产生一个[1, Level*Max]范围内的整数
                    int randomNumber = 0;
                    while (randomNumber == 0 || randomNumber >= 4)
                    {
                        randomNumber = (int)(Random.value * Level * Max + 0.99999f);
                    }

                    Area[i][j] = randomNumber;
                }
            }
            yield return new WaitForSeconds(TimeFactor);
        }
    }
}
