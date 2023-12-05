using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;

public class oscscript : MonoBehaviour
{
    public extOSC.OSCReceiver oscReceiver;
    public extOSC.OSCTransmitter oscTransmitter;

    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public Rigidbody2D rb;

    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;

    Vector2 movement;
    Vector2 mousePosition;

    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        oscReceiver.Bind("/enc", mouvementPlayer);
        oscReceiver.Bind("/button", ShootCallback);
    }

    void mouvementPlayer(OSCMessage oscMessage)
    {
       if (oscMessage.Values.Count > 0 && oscMessage.Values[0].Type == OSCValueType.Int)
        {
            int rotationDirection = oscMessage.Values[0].IntValue;

            // Invert the rotation direction
            rotationDirection *= -1;

            float rotationValue = rotationDirection * rotationSpeed * Time.fixedDeltaTime;
            rb.rotation += rotationValue;
        }
    }

    void ShootCallback(OSCMessage oscMessage)
    {
        Debug.Log("Received /button message");

        if (oscMessage.Values.Count > 0 && oscMessage.Values[0].Type == OSCValueType.Int)
        {
            int buttonState = oscMessage.Values[0].IntValue;

            // Assuming 0 for button press and 1 for button release
            Debug.Log("Button State: " + buttonState);

            if (buttonState == 0)
            {
                Debug.Log("Button Pressed. Shooting!");
                Shoot();
            }
            else if (buttonState == 1)
            {
                Debug.Log("Button Released.");
                // Optionally, you can add logic for button release if needed
            }
        }
    }

void Shoot()
{
    Debug.Log("Shoot method called");
    
    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
    bullet.tag = "enemy";
    rbBullet.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
}

}
