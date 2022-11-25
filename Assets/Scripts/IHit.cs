using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHit 
{
    void TakeHit(float stateDamage, HitType hitType=HitType.None);
}
