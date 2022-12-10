﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 상태이상 타입 Enum
/// </summary>
public enum HitType
{
    None=0,
    Spin,
    airborne,
    silence
}

/// <summary>
/// 스테이지 Enum
/// </summary>
public enum StageEnum
{
    Lobby=0,
    Race
}

public enum TicketType
{
    None=0,
    Danseung,
    Yeonseung,
    Bogseung,
    Ssangseung,
    Bogyeonseung,
    Sambogseung,
    Samssangseung
}
