using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    void Start()
    {
        Invoke("CooldownLoadScene", 3.0f);
    }


    void Update()
    {
        
    }

    private void CooldownLoadScene()
    {
        SceneManager.LoadScene(1);
    }
}
