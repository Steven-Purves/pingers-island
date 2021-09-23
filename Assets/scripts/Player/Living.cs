using UnityEngine;

public class Living : MonoBehaviour, IDamageable
{
    [HideInInspector] public bool isPlayer;
    public int startingHealth;
    [SerializeField] protected int health;
    protected bool dead;

    protected virtual void Start()
    {
        health = startingHealth;
    }
    public void TakeHit(int damage, bool isEnemyAttacker)
    {
        TakeDamage(damage, isEnemyAttacker);
    }

    public virtual void TakeHit(int damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        TakeDamage(damage);
    }

    public virtual void TakeDamage(int damage, bool isEnemyAttacker = false)
    {
        health -= damage;

        if (health <= 0 && !dead)
        {
            Die(isEnemyAttacker);
        }
    }

    public virtual void Die(bool isEnemyAttacker = false)
    {
        dead = true;
    }

    public virtual void Splash()
    {
        
    }
}
