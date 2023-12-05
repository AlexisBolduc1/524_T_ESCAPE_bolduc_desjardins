using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class niveau_2_collision : MonoBehaviour
{
    public int count;

void OnCollisionEnter2D(Collision2D collision)
{
    Debug.Log("Collision detected");

    if (collision.gameObject.CompareTag("enemy"))
    {
        // Find the TargetManager script in the scene
        TargetManager targetManager = GameObject.FindObjectOfType<TargetManager>();

        if (targetManager != null)
        {
            // Increment the count using the TargetManager
            targetManager.IncrementCount();

            // You can still keep track of individual target count if needed
            count++;
            Debug.Log("Hit! Count: " + count);
        }
        else
        {
            Debug.LogError("TargetManager not found in the scene!");
        }
    }
    else
    {
        Debug.Log("Collision with non-'enemy' object: " + collision.gameObject.name);
    }
}

}
