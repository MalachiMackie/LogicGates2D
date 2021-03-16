using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndGate : MonoBehaviour, IInputDevice, IOutputDevice
{
    private bool _value1;
    private bool _value2;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetInput1(bool value)
    {
        _value1 = value;
    }

    public void SetInput2(bool value)
    {
        _value2 = value;
    }

    public bool GetOutput()
    {
        return _value1 && _value2;
    }
}
