using System.Buffers.Text;
using UnityEngine;

public enum AntType { Worker, Soldier, Drone }
public enum AntState { Idle, Moving, Attacking }

public class AntUnit : MonoBehaviour
{
    public AntType type;
    public int maxHP;
    public int currentHP;
    public int attackDamage;
    public int sugarCost;
    public float moveSpeed = 3f;
    public float attackRange = 1.5f;
    public float attackCooldown = 1f;

    private AntState currentState = AntState.Idle;
    private EnemyUnit targetEnemy;
    private float lastAttackTime;

    public AntType GetUnitType() => type;
    public int GetCurrentHP() => currentHP;
    public int GetCurrentATK() => attackDamage;

    private void Start()
    {
        currentHP = maxHP;
    }

    private void Update()
    {
        switch (currentState)
        {
            case AntState.Idle:
                FindNearestEnemy();
                break;
            case AntState.Moving:
                if (targetEnemy != null)
                {
                    MoveTowardsEnemy();
                    CheckAttackRange();
                }
                else
                {
                    currentState = AntState.Idle;
                }
                break;
            case AntState.Attacking:
                if (targetEnemy != null)
                {
                    if (Time.time - lastAttackTime >= attackCooldown)
                    {
                        Attack();
                        lastAttackTime = Time.time;
                    }
                }
                else
                {
                    currentState = AntState.Idle;
                }
                break;
        }
    }

    private void FindNearestEnemy()
    {
        EnemyUnit[] enemies = FindObjectsOfType<EnemyUnit>();
        float closestDistance = Mathf.Infinity;
        EnemyUnit closestEnemy = null;

        foreach (EnemyUnit enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
        {
            targetEnemy = closestEnemy;
            currentState = AntState.Moving;
        }
    }

    private void MoveTowardsEnemy()
    {
        if (targetEnemy == null) return;

        Vector2 direction = (targetEnemy.transform.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    private void CheckAttackRange()
    {
        if (targetEnemy == null) return;

        float distance = Vector2.Distance(transform.position, targetEnemy.transform.position);
        if (distance <= attackRange)
        {
            currentState = AntState.Attacking;
        }
    }

    private void Attack()
    {
        if (targetEnemy != null)
        {
            targetEnemy.TakeDamage(attackDamage);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void UpgradeHP(int amount)
    {
        maxHP += amount;
    }

    public void UpgradeATK(int amount)
    {
        attackDamage += amount;
    }

    public int GetMaxHP()
    {
        return maxHP;
    }

    /* private void OnDestroy()
     {
         if (PlayerBase.instance != null)
             PlayerBase.instance.spawnedUnits.Remove(this);
     } */
}