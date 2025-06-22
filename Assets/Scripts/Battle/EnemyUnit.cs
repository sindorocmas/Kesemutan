using UnityEngine;
using System.Collections.Generic;

public enum EnemyType { Grasshopper, Spider, Scorpion }
public enum EnemyState { Moving, Attacking }

public class EnemyUnit : MonoBehaviour
{
    [Header("Enemy Settings")]
    public EnemyType type;

    //debug
    //soalnya klo di satu frame, >1 unit ant ngebunuh musuh yang sama,
    //tiap unit ant bakal bikin musuhnya drop sugar --> abusable dgn spam ant unit
    private bool isDead = false;

    public int maxHP;
    public int currentHP;
    public int attackDamage;
    public float moveSpeed = 2f;
    public float attackRange = 1.5f;
    public float attackCooldown = 1.5f;
    public float detectionRange = 10f;

    [Header("Target Settings")]
    public LayerMask antLayerMask;
    public PlayerBase playerBase;

    private EnemyState currentState = EnemyState.Moving;
    private AntUnit targetAnt;
    private float lastAttackTime;
    private bool shouldTargetBase = false;
    private List<AntUnit> allAntUnits = new List<AntUnit>();


    private void Start()
    {
        currentHP = maxHP;
        playerBase = FindObjectOfType<PlayerBase>();
        UpdateAntUnitsList();
        InvokeRepeating("UpdateAntUnitsList", 1f, 1f); // Update list setiap 1 detik
    }

    private void UpdateAntUnitsList()
    {
        allAntUnits = new List<AntUnit>(FindObjectsOfType<AntUnit>());
        shouldTargetBase = allAntUnits.Count == 0;
    }

    private void Update()
    {
        if (shouldTargetBase)
        {
            MoveTowardsBase();
            return;
        }

        switch (currentState)
        {
            case EnemyState.Moving:
                FindAntTarget();
                MoveTowardsTarget();
                break;

            case EnemyState.Attacking:
                HandleAttack();
                break;
        }
    }

    private void FindAntTarget()
    {
        if (allAntUnits.Count == 0)
        {
            shouldTargetBase = true;
            return;
        }

        // Bersihkan null units
        allAntUnits.RemoveAll(unit => unit == null);

        float closestDistance = Mathf.Infinity;
        targetAnt = null;

        foreach (AntUnit ant in allAntUnits)
        {
            if (ant == null) continue;

            float distance = Vector2.Distance(transform.position, ant.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                targetAnt = ant;
            }
        }
    }

    private void MoveTowardsTarget()
    {
        if (targetAnt != null)
        {
            Vector2 direction = (targetAnt.transform.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, targetAnt.transform.position) <= attackRange)
            {
                currentState = EnemyState.Attacking;
            }
        }
        else
        {
            shouldTargetBase = true;
        }
    }

    private void MoveTowardsBase()
    {
        if (playerBase == null) return;

        Vector2 direction = (playerBase.transform.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        // Serang base jika sudah dekat
        if (Vector2.Distance(transform.position, playerBase.transform.position) <= 1f)
        {
            AttackBase();
        }
    }

    private void AttackBase()
    {
        playerBase.TakeDamage(attackDamage);
        Die(false);
    }

    private void HandleAttack()
    {
        if (targetAnt == null)
        {
            currentState = EnemyState.Moving;
            return;
        }

        if (Time.time - lastAttackTime >= attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }

        if (Vector2.Distance(transform.position, targetAnt.transform.position) > attackRange)
        {
            currentState = EnemyState.Moving;
        }
    }

    private void Attack()
    {
        if (targetAnt != null)
        {
            targetAnt.TakeDamage(attackDamage);
            //Debug.Log($"{type} menyerang unit semut!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBase"))
        {
            if (playerBase != null)
            {
                isDead = true;
                playerBase.TakeDamage(attackDamage);
                Die(false);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        //biar healthnya ga negative --> bisa drop sugar lebih dr sekali
        if (isDead)
        {
            return;
        }

        currentHP -= damage;
        if (currentHP <= 0)
        {
            isDead = true; 
            Die(true);
        }
    }

    private void Die(bool isKilled)
    {
        EnemySpawner.instance.EnemyDied();

        //SAMBUNG KE GULA (FEI)
        if (isKilled && DropManager.instance != null)
        {
            DropManager.instance.DropItem(transform.position, type);
            Debug.Log("Dropped sugar cubes");
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        if (shouldTargetBase && playerBase != null)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(transform.position, playerBase.transform.position);
        }
    }
}