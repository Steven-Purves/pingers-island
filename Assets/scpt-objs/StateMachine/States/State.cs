using UnityEngine;

[CreateAssetMenu (menuName ="PluggableAI/State")]
public class State : ScriptableObject
{
    public Actions[] actions;
    public Transition[] transitions;
    public void UpdateState(EnemyStateController controller)
    {
        DoActions(controller);
        CheckTransitions(controller);
    }    

    public void StartState(EnemyStateController controller)
    {
        for (int i = 0; i < transitions.Length; i++)
        {
            transitions[i].decision.StartDecision(controller);
        }

        for (int i = 0; i < actions.Length; i++)
        {
           actions[i].StartState(controller);     
        }
    }

    public void EndState(EnemyStateController controller)
    {
        for (int i = 0; i < transitions.Length; i++)
        {
            transitions[i].decision.EndDecision(controller);
        }

        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].EndState(controller);
        }
    }

    private void DoActions(EnemyStateController controller)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Act(controller);
        }
    }

    private void CheckTransitions (EnemyStateController controller)
    {
        for (int i = 0; i < transitions.Length; i++)
        {
            bool decisionsSucceeded = transitions[i].decision.Decide(controller);

            if (decisionsSucceeded)
            {
                controller.TransitionToState(transitions[i].trueState);
            }
            else
            {
                controller.TransitionToState(transitions[i].falseState);
            }
        }
    }
}
