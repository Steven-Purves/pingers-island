using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Actions/WinDanceAction")]
public class WinDanceAction : Actions
{
    public override void Act(EnemyStateController controller)
    {
        
    }

    public override void EndState(EnemyStateController controller)
    {

    }

    public override void StartState(EnemyStateController controller)
    {
        controller.enemy_Components.navMeshAgent.enabled = false;

        string[] whichDance = { "DanceFirst", "DanceSecond", "DanceThird" };
        int random = Random.Range(0, whichDance.Length);

        controller.enemy_Components.animator.SetTrigger(whichDance[random]);
    }

}
