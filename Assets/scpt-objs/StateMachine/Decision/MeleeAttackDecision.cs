using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Decisions/MeleeAttackDecision")]
public class MeleeAttackDecision : Decision
{
    public override bool Decide(EnemyStateController controller)
    {
        return CheckAttackConditions(controller);
    }

    private bool CheckAttackConditions(EnemyStateController controller)
    {
        if (IsFacingPlayer(controller) && IsCloseToPlayer(controller))
        { 
            return true;
        }
        else
            return false;
    }
    private bool IsFacingPlayer(EnemyStateController controller)
    {
        if (controller.enemy_Components.currentEnemyData.enemyType == EnemyType.Green || controller.enemy_Components.currentEnemyData.enemyType == EnemyType.Red)
        {
            return true;
        }

        Vector3 forward = controller.transform.forward;
        Vector3 toOther = (controller.playerTransform.position - controller.transform.position).normalized;

        if (Vector3.Dot(forward, toOther) < 0.7f)
        {
            return false;
        }

        return true;
    }

    private bool IsCloseToPlayer(EnemyStateController controller)
    {
        float sqrDistanceToPlayer = (controller.playerTransform.position - controller.transform.position).sqrMagnitude;

        if (sqrDistanceToPlayer < Mathf.Pow(3f, 2))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void StartDecision(EnemyStateController controller)
    {
      
    }

    public override void EndDecision(EnemyStateController controller)
    {
        
    }
}
