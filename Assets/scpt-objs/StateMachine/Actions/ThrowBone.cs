using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Actions/ThrowBone")]
public class ThrowBone : Actions
{
    public override void Act(EnemyStateController controller)
    {
        controller.enemy_Components.enermy_State_Methods.TurnTowardsPlayer();
    }
    public override void StartState(EnemyStateController controller)
    {
        controller.enemy_Components.animator.SetTrigger("ThrowAttack");
    }

    public override void EndState(EnemyStateController controller)
    {
     
    }
}
