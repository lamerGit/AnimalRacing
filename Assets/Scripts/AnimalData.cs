using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Animal Data", menuName ="Scriptable Object/Animal Data",order = 1)]
public class AnimalData : ScriptableObject
{
    //동물데이터 스크립트오브젝트

    [Header("기본 데이터")]
    public string animalName; //동물의 이름
    public string special; // 동물의 기술
    public float allocation; // 동물이 1등했을때의 배당률
}
