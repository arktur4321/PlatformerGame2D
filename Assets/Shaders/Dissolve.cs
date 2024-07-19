using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer;
    Material[] materials;
    float dissolveTime = 0.0125f;
    void Start()
    {
        if (meshRenderer != null)
        {
            materials = meshRenderer.materials;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(CoDissolve());
        }
    }

    IEnumerator CoDissolve()
    {
        if (materials.Length > 0)
        {
            float counter = 0f;
            while (materials[0].GetFloat("_AlfaClip")< 1)
            {
                counter += dissolveTime;
                for(int i = 0; i < materials.Length; i++)
                {
                    materials[i].SetFloat("_AlfaClip", counter) ;
                }

                yield return new WaitForSeconds(0.025f);

            }
        }
    }
}
