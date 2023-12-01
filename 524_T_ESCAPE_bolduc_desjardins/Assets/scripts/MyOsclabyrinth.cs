using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyOsclabyrinth : MonoBehaviour { 
    public extOSC.OSCReceiver oscReceiver;
public extOSC.OSCTransmitter oscTransmitter;
public GameObject player;
float myChronoStart;

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

    float hautbas = ScaleValue(valeur, 0, 4095, -2, 5);
    // Appliquer la rotation au GameObject ciblé :
    player.transform.eulerAngles = new Vector3(0,hautbas,0);

}

void Start()
{

    oscReceiver.Bind("/tof", updownjoueur);



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

