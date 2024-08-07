using UnityEngine;

public class BaseAnimator : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    public virtual void OnMove(bool isMove)
    {
        animator.SetBool("isMove", isMove);
    }
    public virtual void OnMove(Vector3 move)
    {
        animator.SetFloat("Speed", move.magnitude);
    }
    public virtual void OnAttack(bool isAttack)
    {
        animator.SetTrigger("isAttacking");
    }
    public virtual void OnAttack()
    {
        animator.SetTrigger("doAttack");
    }
    public virtual void OnDead(bool isDead)
    {
        animator.SetBool("isDead", isDead);
        animator.SetTrigger("death");
    }
    /// <summary>
    /// ���� �ִϸ����� ���¿��� �ִϸ��̼� ���̸� ������
    /// </summary>
    /// <param name="animationName"></param>
    /// <returns></returns>
    protected float GetAnimationLength(string animationName)
    {
        foreach (var clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == animationName) return clip.length;
        }
        return 0f;
    }
}
