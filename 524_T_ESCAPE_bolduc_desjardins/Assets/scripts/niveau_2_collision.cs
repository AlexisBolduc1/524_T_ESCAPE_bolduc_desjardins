using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class niveau_2_collision : MonoBehaviour
{
    public int count;

void OnCollisionEnter2D(Collision2D collision)
{

    if (collision.gameObject.CompareTag("enemy"))
    {
        TargetManager targetManager = GameObject.FindObjectOfType<TargetManager>();

        if (targetManager != null)
        {
            targetManager.IncrementCount();
            count++;
        }
    }
}
}
