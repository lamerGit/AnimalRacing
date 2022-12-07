using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaringMassege : MonoBehaviour
{
    private void Awake()
    {
        Button cancel = transform.Find("Cancel_Button").GetComponent<Button>();
        cancel.onClick.AddListener(Close);
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    void Close()
    {
        gameObject.SetActive(false);
    }
}
