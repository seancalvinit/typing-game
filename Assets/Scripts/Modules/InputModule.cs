using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputModule : MonoBehaviour
{
    public static InputModule Instance
    {
        get; private set;
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public event Action<string> OnType = delegate { };

    private void Update()
    {
        OnTypeAction(Input.inputString);
    }

    void OnTypeAction(string inputString)
    {
        if(inputString != "")
        {
            OnType(inputString);
        }
    }

}
