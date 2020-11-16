using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Decisions/ThrowBoneDecision")]
public class ThrowBoneDecision : Decision
{
    public override bool Decide(EnemyStateController controller)
    {
        if (controller.enemy_Components.enermy_State_Methods.throwAttack)
        {
            return true;
        }
        return false;
    }

    public override void StartDecision(EnemyStateController controller)
    {  
        controller.enemy_Components.enermy_State_Methods.StartCoroutine(controller.enemy_Components.enermy_State_Methods.ThrowWeaponTimer());     
    }

    public override void EndDecision(EnemyStateController controller)
    {
        controller.enemy_Components.enermy_State_Methods.throwAttack = false;
    }
}
