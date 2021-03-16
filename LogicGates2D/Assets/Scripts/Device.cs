using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IInputDevice
{
    public void SetInput1(bool value);
    public void SetInput2(bool value);
}

public interface IOutputDevice
{
    public bool GetOutput();
}
