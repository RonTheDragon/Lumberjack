using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected Animator _animator;   
    abstract public void OnPress();
    abstract public void OnRelease();

    public void InitializeWeapon(Animator animator)
    {
        _animator = animator;
    }
}
