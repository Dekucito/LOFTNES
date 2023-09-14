using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEfects : MonoBehaviour
{
    public Collider2D swordCollider;
    public float KnockbackForce = 500f;

    internal Vector3 parentPosition;

    internal Vector2 directon;
    internal Vector2 Knockback;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            parentPosition = gameObject.GetComponentInParent<Transform>().position;
            directon = (collision.gameObject.transform.position - parentPosition).normalized;

            Knockback = directon * KnockbackForce;
        }
    }
}
