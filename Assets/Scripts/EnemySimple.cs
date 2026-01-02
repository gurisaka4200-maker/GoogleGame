using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemySimple : MonoBehaviour
{
    public int maxHealth = 100;
    public float chaseRange = 10f;
    public float attackRange = 1.5f;
    public int attackDamage = 10;
    public float attackCooldown = 1.2f;
    public Transform target;

    private int currentHealth;
    private NavMeshAgent agent;
    private float lastAttackTime = -99f;

    private void Awake()
    {
        currentHealth = maxHealth;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (!target) return;
        float dist = Vector3.Distance(transform.position, target.position);
        if (dist <= chaseRange)
        {
            agent.SetDestination(target.position);
            if (dist <= attackRange && Time.time - lastAttackTime >= attackCooldown)
            {
                lastAttackTime = Time.time;
                Attack();
            }
        }
        else
        {
            if (!agent.isStopped) agent.ResetPath();
        }
    }

    void Attack()
    {
        // Example: if player has a PlayerHealth script, call it.
        target.GetComponent<PlayerHealth>()?.TakeDamage(attackDamage);
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
