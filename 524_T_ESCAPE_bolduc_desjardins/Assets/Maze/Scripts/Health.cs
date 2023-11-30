using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public GameObject[] healthUI = new GameObject[3];
 
    // Update is called once per frame
    void Update()
    {
        switch (Accident.health)
        {
            case 2:
                healthUI[2].SetActive(false);
                break;
            case 1:
                healthUI[1].SetActive(false);
                break;
            case 0:
                healthUI[0].SetActive(false);
                break;
        }
    }
}
