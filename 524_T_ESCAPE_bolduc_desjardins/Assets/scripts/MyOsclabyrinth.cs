using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyOsclabyrinth : MonoBehaviour
{
    public extOSC.OSCReceiver oscReceiver;
    public extOSC.OSCTransmitter oscTransmitter;
    public GameObject player;
    float myChronoStart;
    public float rotationSpeed = 1f;
    public float moveSpeed = 5f;
     private Rigidbody2D playerRigidbody2D;
     private float accumulatedRotation = 0f;

    void Start()
    {
        oscReceiver.Bind("/enc", joueurCallback);
        oscReceiver.Bind("/button", buttonCallback);
        playerRigidbody2D = player.GetComponent<Rigidbody2D>();
    }


void joueurCallback(extOSC.OSCMessage oscMessage)
    {
        if (oscMessage.Values.Count > 0 && oscMessage.Values[0].Type == extOSC.OSCValueType.Int)
        {
            int rotationInput = oscMessage.Values[0].IntValue;

            // Normalize the rotation input to be between -1 and 1
            float normalizedRotationInput = rotationInput / 4095f;

            // Update the accumulated rotation
            accumulatedRotation += normalizedRotationInput * rotationSpeed;

            // Rotation and aiming using the accumulated rotation
            player.transform.rotation = Quaternion.Euler(0, 0, accumulatedRotation);
        }
    }



    void buttonCallback(extOSC.OSCMessage oscMessage)
    {
        if (oscMessage.Values.Count > 0 && oscMessage.Values[0].Type == extOSC.OSCValueType.Int)
        {
            int buttonState = oscMessage.Values[0].IntValue;

            // Forward movement when the button is pressed
            if (buttonState == 0)
            {
                player.transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
            }
        }
    }

    void LateUpdate()
    {
        if (Time.realtimeSinceStartup - myChronoStart >= 0.05f)
        {
            myChronoStart = Time.realtimeSinceStartup;

            var myOscMessage = new extOSC.OSCMessage("/pixel");

            float myPositionX = player.transform.position.x;
            float myScaledPositionX = ScaleValue(myPositionX, -7, 7, 0, 255);

            myOscMessage.AddValue(extOSC.OSCValue.Int((int)myScaledPositionX));
            oscTransmitter.Send(myOscMessage);
        }
    }

    float ScaleValue(float value, float inputMin, float inputMax, float outputMin, float outputMax)
    {
        return Mathf.Clamp(((value - inputMin) / (inputMax - inputMin) * (outputMax - outputMin) + outputMin), outputMin, outputMax);
    }
}
