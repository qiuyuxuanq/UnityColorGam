using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum LightState{Red, Green, Yellow}
public class TrafficLightController : MonoBehaviour
{
    public LightState currentState = LightState.Red;

    public bool CanGoStright()
    {
        return currentState == LightState.Green;
    }
}
