using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyOscLight : MonoBehaviour
{
    
    public extOSC.OSCReceiver oscReceiver;
public extOSC.OSCTransmitter oscTransmitter;
public GameObject textun;
public GameObject textdeux;
public GameObject boutton;
float myChronoStart;

public static float ScaleValue(float value, float inputMin, float inputMax, float outputMin, float outputMax)
{
    return Mathf.Clamp(((value - inputMin) / (inputMax - inputMin) * (outputMax - outputMin) + outputMin), outputMin, outputMax);
}


void apparition(extOSC.OSCMessage oscMessage)
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

    if (valeur <= 300)
        {
        textun.SetActive(true);
        }else if(valeur > 300){
            textun.SetActive(false);
        };

        if (valeur <= 200)
        {
        textdeux.SetActive(true);
        } else if(valeur > 200){
            textdeux.SetActive(false);
        };

        if (valeur <= 80)
        {
        boutton.SetActive(true);
        }else if(valeur > 80){
            boutton.SetActive(false);
        };

}

void Start()
{

    oscReceiver.Bind("/photo", apparition);




}

void Update()
{


}

void LateUpdate()
{


}
}


