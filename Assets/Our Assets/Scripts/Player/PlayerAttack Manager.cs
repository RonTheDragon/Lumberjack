using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _rightHand;
    [SerializeField] private Transform _leftHand;
    private Weapon _meleeWeapon;
    private Weapon _rangedWeapon;

    [SerializeField] private Weapon _axe;

    private void Start()
    {
        SetMeleeWeapon(_axe);
    }

    public void SetMeleeWeapon(Weapon meleeWeapon)
    {
        _meleeWeapon = Instantiate(meleeWeapon, _rightHand);
        _meleeWeapon.InitializeWeapon(_animator);
        _meleeWeapon.gameObject.name = meleeWeapon.gameObject.name;
        _animator.Rebind();
    }
    public void SetRangedWeapon(Weapon rangedWeapon)
    {

    }

    public void OnMeleePress()
    {
        _meleeWeapon?.OnPress();
    }
    public void OnMeleeRelease()
    {
        _meleeWeapon?.OnRelease();
    }
}
