﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Idamageable
{
    void TakeDamage(float amount);
}

public interface IHealable
{
    void FullHealth();
}

