using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider myCollider;
    private List<Collider> alreadyCollideWith = new List<Collider>();
    private int damage;
    private float knockBack;
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
            Debug.Log(other);
            health.DealDamage(damage);
        }
        if(other.TryGetComponent(out ForceReceiver forceReceiver))
        {
            Vector3 direction = (other.transform.position - myCollider.transform.position).normalized;
            forceReceiver.AddForce(direction * knockBack);//맞은 방향으로 팅겨나가기
        }
    }
    public void SetAttack(int damage, float knockBack)
    {
        this.damage = damage;
        this.knockBack = knockBack;
    }
}
