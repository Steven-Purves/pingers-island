using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/GameOverDecision")]
public class GameOverDecision : Decision
{
    public override bool Decide(EnemyStateController controller)
    {
        return GamePeriod.isGameOver;
    }

    public override void StartDecision(EnemyStateController controller)
    {
     
    }

    public override void EndDecision(EnemyStateController controller)
    {
        
    }
}
