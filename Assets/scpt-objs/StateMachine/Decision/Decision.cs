using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Decision : ScriptableObject
{
    public abstract void StartDecision(EnemyStateController controller);
    public abstract void EndDecision(EnemyStateController controller);
    public abstract bool Decide(EnemyStateController controller);
}
