using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllable
{
    float ForwardDirection { get; }
    float SideDirection { get; }
    void SetDirection(float forwardDir, float sideDir);
}
public interface IData
{
    float Data { get; }
    void SetData(float data);
}