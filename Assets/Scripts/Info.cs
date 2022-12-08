using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Info : MonoBehaviour
{
    //정보창을 눌렀을때 동물의 정보를 보여주는 스크립트

    TextMeshProUGUI number;
    TextMeshProUGUI animalName;
    TextMeshProUGUI special;
    TextMeshProUGUI allocation;
    private void Awake()
    {
        number=transform.Find("number").GetComponent<TextMeshProUGUI>();
        animalName=transform.Find("name").GetComponent<TextMeshProUGUI>();
        special=transform.Find("special").GetComponent<TextMeshProUGUI>();
        allocation=transform.Find("allocation").GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// 보여줘야하는 정보변경
    /// </summary>
    /// <param name="n">번호</param>
    /// <param name="an">이름</param>
    /// <param name="s">스킬</param>
    /// <param name="al">배당률</param>
    public void InfoChange(int n,string an,string s, float al)
    {
        number.text=n.ToString();
        animalName.text = an;
        special.text = s;
        allocation.text = al.ToString();
    }
}
