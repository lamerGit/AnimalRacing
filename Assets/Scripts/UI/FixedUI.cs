using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedUI : MonoBehaviour
{
    private void Awake()
    {
        SetFixed();
    }
    void SetFixed()
    {
        int setWidth = 1920;
        int setHeight = 1080;
        Screen.SetResolution(setWidth, setHeight, true);
    }
}
