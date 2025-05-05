using UnityEngine;

public class AnimatorPingPong : MonoBehaviour
{
    public Animator animator;
    public string animationStateName;
    public float speed = 1f;

    private bool reversing = false;

    void Update()
    {
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

        // When reaching end or start, reverse direction
        if (!reversing && state.normalizedTime >= 1f)
        {
            reversing = true;
            animator.speed = -speed;
        }
        else if (reversing && state.normalizedTime <= 0f)
        {
            reversing = false;
            animator.speed = speed;
        }
    }

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        animator.Play(animationStateName, 0, 0f);
        animator.speed = speed;
    }
}
