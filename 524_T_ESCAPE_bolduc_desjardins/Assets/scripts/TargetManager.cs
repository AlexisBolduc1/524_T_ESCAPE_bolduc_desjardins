using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TargetManager : MonoBehaviour
{
   public int totalCount = 0;
    public GameObject winText;

    public void IncrementCount()
    {
        totalCount++;
        Debug.Log("Total Hit Count: " + totalCount);

        if (totalCount >= 3)
        {
            Debug.Log("You win!");
            winText.SetActive(true);
            SceneManager.LoadScene(2);

        }
    }
}
