using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSceneData
{
    private static NextSceneData m_NextSceneData = null;
    public string NextSceneName;

    // Start is called before the first frame update
    public static NextSceneData getInstance()
    {
        if (m_NextSceneData == null)
        {
            m_NextSceneData = new NextSceneData();
        }
        return m_NextSceneData;
    }
    
}
