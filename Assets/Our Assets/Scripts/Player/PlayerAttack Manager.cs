using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _rightHand;
    [SerializeField] private Transform _leftHand;
    [SerializeField] private Transform _backSlot;
    private Weapon _meleeWeapon;
    private Weapon _rangedWeapon;

    private bool _isMeleeInHands = true;

    [SerializeField] private Weapon _axe;
    [SerializeField] private Weapon _shotgun;

    private void Start()
    {
        SetRangedWeapon(_shotgun);
        SetMeleeWeapon(_axe);
    }

    public void SetMeleeWeapon(Weapon meleeWeapon)
    {
        if (!_isMeleeInHands)
        {
            SwitchWeapons();
        }
        Destroy(_meleeWeapon?.gameObject); // Destroy existing weapon
        _meleeWeapon = InstantiateWeapon(meleeWeapon, _rightHand);
    }

    public void SetRangedWeapon(Weapon rangedWeapon)
    {
        if (_isMeleeInHands)
        {
            SwitchWeapons();
        }
        Destroy(_rangedWeapon?.gameObject); // Destroy existing weapon
        _rangedWeapon = InstantiateWeapon(rangedWeapon, _rightHand);
    }

    private Weapon InstantiateWeapon(Weapon weaponPrefab, Transform parentTransform)
    {
        Weapon newWeapon = Instantiate(weaponPrefab, parentTransform);
        newWeapon.InitializeWeapon(_animator);
        newWeapon.gameObject.name = weaponPrefab.gameObject.name;
        _animator.Rebind();
        return newWeapon;
    }

    public void OnMeleePress()
    {
        if (!_isMeleeInHands)
        {
            if (IsWeaponBusy(_rangedWeapon))
            {
                    return;
            }
            SwitchWeapons();
        }
        _meleeWeapon?.OnPress();
    }

    public void OnMeleeRelease()
    {
        if (_isMeleeInHands)
        {
            _meleeWeapon?.OnRelease();
        }
    }

    public void OnRangedPress()
    {
        if (_isMeleeInHands)
        {
            if (IsWeaponBusy(_meleeWeapon))
            {
                    return;
            }
            SwitchWeapons();
        }
        _rangedWeapon?.OnPress();
    }

    public void OnRangedRelease()
    {
        if (!_isMeleeInHands)
        {
            _rangedWeapon?.OnRelease();
        }
    }

    private void SwitchWeapons()
    {
        if (_isMeleeInHands)
        {
            _meleeWeapon?.ClearAttacks();

            SetWeaponTransform(_rangedWeapon, _rightHand);
            SetWeaponTransform(_meleeWeapon, _backSlot);
            _meleeWeapon?.FixPositionOnBack();

            _isMeleeInHands = false;
        }
        else
        {
            _rangedWeapon?.ClearAttacks();

            SetWeaponTransform(_meleeWeapon, _rightHand);
            SetWeaponTransform(_rangedWeapon, _backSlot);
            _rangedWeapon?.FixPositionOnBack();

            _isMeleeInHands = true;
        }
    }

    private void SetWeaponTransform(Weapon weapon, Transform parent)
    {
        if (weapon != null)
        {
            weapon.transform.SetParent(parent, false);
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;
        }
    }

    private bool IsWeaponBusy(Weapon weapon)
    {
        if (weapon != null)
        {
            if (weapon.IsWeaponBusy())
            {
                return true;
            }
        }
        return false;
    }

}
