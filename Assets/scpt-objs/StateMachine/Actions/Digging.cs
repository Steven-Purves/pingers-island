using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Digging")]
public class Digging : Actions
{
    public override void Act(EnemyStateController controller)
    {
        
    }

    public override void StartState(EnemyStateController controller)
    {   
        controller.enemy_Components.navMeshAgent.speed = 0;
    }

    public override void EndState(EnemyStateController controller)
    {
       
    }
}
