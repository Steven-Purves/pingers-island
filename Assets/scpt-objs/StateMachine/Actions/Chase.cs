using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Chase")]
public class Chase : Actions
{
    public override void StartState(EnemyStateController controller)
    {
        controller.enemy_Components.navMeshAgent.speed = controller.enemy_Components.currentEnemyData.speed;

        controller.enemy_Components.enermy_State_Methods.StartCoroutine(controller.enemy_Components.enermy_State_Methods.UpdateNavMeshRefreshRate());

        controller.enemy_Components.animator.SetTrigger("Chase");
    }

    public override void EndState(EnemyStateController controller)
    {
        controller.enemy_Components.navMeshAgent.speed = 0;
    }

    public override void Act(EnemyStateController controller)
    {
        controller.enemy_Components.animator.SetFloat(controller.speed, controller.enemy_Components.navMeshAgent.velocity.magnitude / controller.enemy_Components.navMeshAgent.speed);
    }
}
