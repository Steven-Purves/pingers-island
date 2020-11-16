using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Actions/MeleeAttackAction")]
public class MeleeAttackAction : Actions
{
    private readonly string leftHandAttack = "LeftHandAttack";
    private readonly string rightHandAttack = "RightHandAttack";

    public override void Act(EnemyStateController controller)
    { 
        controller.enemy_Components.enermy_State_Methods.TurnTowardsPlayer();
    }
    public override void StartState(EnemyStateController controller)
    {
        string attack = Random.value > 0.5f ? leftHandAttack : rightHandAttack;

        controller.enemy_Components.animator.SetTrigger(attack);
    }
    public override void EndState(EnemyStateController controller)
    {
    }
}
