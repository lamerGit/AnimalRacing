﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondCameraOut : MonoBehaviour
{
    // Start is called before the first frame update
    RectTransform rect;
    private void Awake()
    {
        rect=GetComponent<RectTransform>();
    }


    public IEnumerator CameraOut()
    {
        while(true)
        {

            rect.anchoredPosition += Vector2.up*2.0f;
            yield return null;
        }
    }
}
