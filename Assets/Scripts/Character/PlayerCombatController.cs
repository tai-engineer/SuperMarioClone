using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    public Transform attackHitBox;
    public float attackRadius = 1f;
    public float attackDamage = 0.5f;
    public LayerMask damagableLayer;
    [HideInInspector]
    public bool isAttacking = false;
    public void StartMeleeAttack()
    {
        isAttacking = true;
    }

    public void FinishMeleeAttack()
    {
        isAttacking = false;
    }
    public void CheckMeleeAttackHitBox()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackHitBox.position, damagableLayer);

        foreach(Collider2D collider in colliders)
        {
            //collider.transform.parent.SendMessage("TakeDamage", attackDamage);
            Debug.Log("Attack Hit: " + collider.gameObject.name);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackHitBox.position, attackRadius);
    }

}
