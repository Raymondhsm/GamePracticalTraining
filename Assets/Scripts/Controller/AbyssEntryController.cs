using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AbyssEntryController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("Abyss");
            Physics.gravity = new Vector3(0, -9.81f, 0);
        }
    }

    /*IEnumerator LoadAbyss()
    {
        while(!PlayerPrefs.HasKey("FindRheaSecretTask")){
            yield return 0;
        }

      SceneManager.LoadScene("Abyss");
    }*/
}
