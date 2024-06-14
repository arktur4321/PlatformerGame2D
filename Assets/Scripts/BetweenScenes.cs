using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetweenScenes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        for (int i = 0; i < Object.FindObjectsOfType<BetweenScenes>().Length; i++)
        {
            BetweenScenes betweenScenes = Object.FindObjectsOfType<BetweenScenes>()[i];
            if (betweenScenes != this)
            {
                if (betweenScenes.name == gameObject.name)
                {
                    Destroy(gameObject);
                }
            }
        }
        DontDestroyOnLoad(gameObject);
    }
}
