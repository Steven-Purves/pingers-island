using UnityEngine;

public class EnemyStateController : MonoBehaviour
{
    [HideInInspector] public Enemy_Components enemy_Components;
    [Space]
    public State currentState;
    public State remainState;

    public bool isGameOver;

    [HideInInspector] public int speed = Animator.StringToHash("Speed");

    void Start()
    {
        enemy_Components = GetComponent<Enemy_Components>();
        Player.OnPlayerDied += () => { isGameOver = true; };
    }

    void Update() => currentState.UpdateState(this);

    public void TransitionToState(State nextState)
    {
        if (nextState != remainState)
        {
            currentState.EndState(this);

            currentState = nextState;
            enemy_Components.EnemyAnimationEventHandler.animationHasFinished = false;
            nextState.StartState(this);
        }
    }
}
