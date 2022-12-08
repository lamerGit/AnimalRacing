using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaringMassege : MonoBehaviour
{
    //레이스가 생성되지 않은 상태에서 시작이나 정보창을 눌렀을때 생기는 오브젝트 스크립트
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
