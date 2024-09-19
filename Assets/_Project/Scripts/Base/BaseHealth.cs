using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    public bool isDead;
    [SerializeField] protected float maxHp;
    [SerializeField] protected float curHp;
    protected CapsuleCollider myCollider;
    public virtual void SufferDamage(float damgage)
    {
        if(isDead) return;
        Debug.Log($"데미지를 {damgage} 입음");
        curHp -= damgage;
        if(curHp <= 0) Dead();
    }
    protected virtual void Dead()
    {
        myCollider.enabled = false;
        isDead = true;
        gameObject.layer = LayerMask.NameToLayer("Death");
    }
}
