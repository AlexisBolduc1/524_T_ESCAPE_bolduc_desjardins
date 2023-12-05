using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
   public int totalCount = 0;

    public void IncrementCount()
    {
        totalCount++;
        Debug.Log("Total Hit Count: " + totalCount);

        if (totalCount == 3)
        {
            // You can add your win condition here
            Debug.Log("You win!");
        }
    }
}
