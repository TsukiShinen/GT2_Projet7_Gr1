using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemy : MonoBehaviour
{
    [SerializeField]
    private float _knockback = 100f;
    [SerializeField]
    private int _additionalDamage = 0;

    public int damage { get; set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) { return; }
        if (!collision.CompareTag("Enemy")) { return; }

        collision.GetComponent<Entity>().Hit((collision.transform.position - transform.position).normalized * _knockback, damage + _additionalDamage);
    }
}
