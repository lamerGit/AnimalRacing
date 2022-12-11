using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultAnimalInfo : MonoBehaviour
{
    //레이스결과에서 보여줄 동물정보 스크립트

    TextMeshProUGUI number;
    TextMeshProUGUI animalName;

    private void Awake()
    {
        number = transform.Find("Number").GetComponent<TextMeshProUGUI>();
        animalName = transform.Find("Name").GetComponent<TextMeshProUGUI>();
    }

    public void InfoChange(int n, string an)
    {
        number.text = n.ToString();
        animalName.text = an;

    }
}
