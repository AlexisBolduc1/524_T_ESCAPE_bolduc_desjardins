using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyOsclabyrinth : MonoBehaviour
{
    public extOSC.OSCReceiver oscReceiver;
    public extOSC.OSCTransmitter oscTransmitter;
    public GameObject player;
    float myChronoStart;
    public float moveSpeed = 5f; // Add this line

    public static float ScaleValue(float value, float inputMin, float inputMax, float outputMin, float outputMax)
    {
        return Mathf.Clamp(((value - inputMin) / (inputMax - inputMin) * (outputMax - outputMin) + outputMin), outputMin, outputMax);
    }

    void joueurCallback(extOSC.OSCMessage oscMessage)
    {
        if (oscMessage.Values.Count > 0 && oscMessage.Values[0].Type == extOSC.OSCValueType.Float)
        {
            float valeur = oscMessage.Values[0].FloatValue;

            // Rotation and aiming
            float hautbas = ScaleValue(valeur, 0, 4095, -2, 5);
            player.transform.eulerAngles = new Vector3(0, hautbas, 0);

            float dgjoueur = ScaleValue(valeur, 0, 4095, -12, 3);
            player.transform.position = new Vector3(dgjoueur, 0, 0);
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
            Debug.Log("Button Pressed. Moving forward!");

            // Move the player forward along its own local forward direction (2D)
            player.transform.Translate(Vector2.up * Time.deltaTime * moveSpeed);
        }
    }
}


    void Start()
    {
        oscReceiver.Bind("/enc", joueurCallback);
        oscReceiver.Bind("/button", buttonCallback);
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
}
