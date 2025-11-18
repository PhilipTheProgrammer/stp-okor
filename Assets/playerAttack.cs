using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public float attackRange = 0.5f;
    public int attackDamage = 20;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    [Header("References")]
    public PlayerMovement playerMovement;
    private Animator animator;

    private bool isAttacking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        // pokud PlayerMovement je na stejném GameObjectu:
        if (playerMovement == null)
            playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && isAttacking == false)
        {
            Attack();
        }
    }

    void Attack()
    {
        isAttacking = true;
        // Spustí animaci
        animator.SetTrigger("Attack");
       
    }

    // Animation Event volá se pøesnì v momentu, kdy má být damage
    public void DealDamage()
    {
        Debug.Log("hit ");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>()?.TakeDamage(attackDamage);
           
        }
        
    }

    public void PlayerAttack_DisableMovement()
    {
        if (playerMovement != null)
            playerMovement.canMove = false;

    }

    public void PlayerAttack_EnableMovement()
    {
        
            playerMovement.canMove = true;

        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
