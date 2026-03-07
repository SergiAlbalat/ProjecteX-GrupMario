using UnityEngine;
[RequireComponent (typeof(Animator))]
public class AnimationBehaviour : MonoBehaviour
{
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator> ();
    }
    public void Walk(float velocity)
    {
        _animator.SetFloat("Velocity", velocity);
    }
    public void Jump()
    {
        _animator.SetTrigger("Jump");
    }
}
