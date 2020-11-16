using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Decisions/AnimationFinishedDecision")]
public class AnimationFinishedDecision : Decision
{
    public override bool Decide(EnemyStateController controller)
    {
        return controller.enemy_Components.EnemyAnimationEventHandler.animationHasFinished;
    }

    public override void StartDecision(EnemyStateController controller)
    {
        
    }

    public override void EndDecision(EnemyStateController controller)
    {

    }
}