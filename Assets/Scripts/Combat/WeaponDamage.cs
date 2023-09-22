using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider myCollider;
    private List<Collider> alreadyCollideWith = new List<Collider>();
    private int damage;
    private void OnEnable()
    {
        alreadyCollideWith.Clear();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other == myCollider) return;//본인이면 return
        if (alreadyCollideWith.Contains(other))
        {
            return;
        }
        alreadyCollideWith.Add(other);
        if (other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(damage);
        }
    }
    public void SetAttack(int damage)
    {
        this.damage = damage;
    }
}
