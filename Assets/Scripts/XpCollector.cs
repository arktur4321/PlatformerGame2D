using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class XpCollector : MonoBehaviour
{
    [SerializeField] TMP_Text pointText;
    int XpPoints = 0;

    void Start()
    {
        XpPoints = 0;
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.name == "xpPoint1")
        {
            XpPoints += 1;
            pointText.text = "Points: " + XpPoints;
            Destroy(coll.gameObject);
        }

        if (coll.gameObject.name == "xpPoint5")
        {
            XpPoints += 5;
            pointText.text = "Points: " + XpPoints;
            Destroy(coll.gameObject);
        }
    }

}
