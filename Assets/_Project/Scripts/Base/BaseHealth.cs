using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    public bool isDead;
    [SerializeField] protected float maxHp;
    [SerializeField] protected float curHp;
    protected CapsuleCollider myCollider;
    protected LayerMask layer;
    public virtual void SufferDamage(float damgage)
    {
        if(isDead) return;
        Debug.Log($"�������� {damgage} ����");
        curHp -= damgage;
        if(curHp <= 0) Dead();
    }
    protected virtual void Dead()
    {
        myCollider.enabled = false;
        isDead = true;
        layer = LayerMask.NameToLayer("Death");
    }
}