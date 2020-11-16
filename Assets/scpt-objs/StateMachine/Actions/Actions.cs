using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actions: ScriptableObject
{
    public abstract void Act(EnemyStateController controller);

    public abstract void StartState(EnemyStateController controller);

    public abstract void EndState(EnemyStateController controller);
}
