using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyOsclabyrinth : MonoBehaviour { 
    public extOSC.OSCReceiver oscReceiver;
public extOSC.OSCTransmitter oscTransmitter;
public GameObject player;
float myChronoStart;
public float ascentSpeed = 100.0f;
public float descentSpeed = 100.0f;

public static float ScaleValue(float value, float inputMin, float inputMax, float outputMin, float outputMax)
{
    return Mathf.Clamp(((value - inputMin) / (inputMax - inputMin) * (outputMax - outputMin) + outputMin), outputMin, outputMax);
}


void updownjoueur(extOSC.OSCMessage oscMessage)
{
    float valeur;
    if (oscMessage.Values[0].Type == extOSC.OSCValueType.Int)
    {
        valeur = oscMessage.Values[0].IntValue;
    }
    else if (oscMessage.Values[0].Type == extOSC.OSCValueType.Float)
    {
        valeur = oscMessage.Values[0].FloatValue;
    }
    else
    {
        return;
    }

    if (valeur == 1)
    {
        Vector3 newPosition = player.transform.position + Vector3.up * ascentSpeed * Time.deltaTime;
            player.transform.position = new Vector3(player.transform.position.x, newPosition.y, player.transform.position.z);
    }
    else if (valeur == 0)
    {

        Vector3 newPosition = player.transform.position + Vector3.down * descentSpeed * Time.deltaTime;
        player.transform.position = new Vector3(player.transform.position.x, newPosition.y, player.transform.position.z);
    }

}

void Start()
{

    oscReceiver.Bind("/angle", updownjoueur);



}

void Update()
{


}

void LateUpdate()
{
    if (Time.realtimeSinceStartup - myChronoStart >= 0.05f)
    {
        myChronoStart = Time.realtimeSinceStartup;

        var myOscMessage = new extOSC.OSCMessage("/pixel");

        float myPositionX = player.transform.position.x;
        float myScaledPositionX = ScaleValue(myPositionX, -7, 7, 0, 255);

        myOscMessage.AddValue(extOSC.OSCValue.Int((int)myScaledPositionX)); // Le (int) entre parenth?ses convertit le type.

        oscTransmitter.Send(myOscMessage);
    }

}
}

