using UnityEngine;
using UnityEngine.SceneManagement;

public class TransportToScene : MonoBehaviour
{
    [Tooltip("输入要传送到哪个场景，输入场景名字")]
    public string transportToSceneName = "Gaea";
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "m_FPP" && PlayerPrefs.HasKey("LocateGaeaTask"))
        {
            //SceneManager.LoadScene(transportToSceneName);
            NextSceneData.getInstance().NextSceneName = transportToSceneName;
            SceneManager.LoadSceneAsync("LoadScene");
        }
    }
}
