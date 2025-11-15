using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.UI;

public class PlayerManager : MonoBehaviour
{

    public Transform attackPoint;
    public float attackRadius;

    public LayerMask enemyLayers;

    private InputAction attackAction;
    private void Start()
    {
        attackAction = InputSystem.actions.FindAction("Attack");

    }

    private void Update()
    {
        if (attackAction.WasPressedThisFrame())
        {
            Attack();
        }
    }

    private void Attack()
    {
        Debug.Log("attack");

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRadius, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            HealthSystem health = enemy.GetComponent<HealthSystem>();

            Debug.Log(enemy.name);

            if (health != null)
            {
                Debug.Log("enemy takes damage;");
                health.TakeDamage(1);
            } else
            {
                Debug.Log("No health object");
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

}
