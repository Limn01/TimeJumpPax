using System.Collections;
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

public interface IEnemyState
{
    void Execute();
    void Enter(PatrolEnemy enemy);
    void Exit();
}
