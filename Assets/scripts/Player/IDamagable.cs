using UnityEngine;

public interface IDamageable
{
    void TakeHit(int damage, Vector3 hitPoint, Vector3 hitDirection);
    void TakeHit(int damage, bool isEnemyAttacker);
    void Splash();
}