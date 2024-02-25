using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected Animator _animator;
    protected bool _isHeldDown;
    protected float _currentCooldown;
    [SerializeField] protected float _maxCharge;
    protected float _charge;

    [SerializeField] protected PositionOnBack _positionOnBack;

    protected void Update()
    {
        WeaponCharging();
        WeaponCooldownTime();
    }

    abstract public void OnPress();
    abstract public void OnRelease();

    protected void WeaponCharging()
    {
        if (_isHeldDown && _charge < _maxCharge)
        {
            _charge += Time.deltaTime;
        }
        else if (_charge > _maxCharge)
        {
            _charge = _maxCharge;
        }
    }

    protected void WeaponCooldownTime()
    {
        if (_currentCooldown > 0)
        {
            _currentCooldown -= Time.deltaTime;
        }
        else if (_currentCooldown < 0)
        {
            _currentCooldown = 0;
        }
    }

    public void InitializeWeapon(Animator animator)
    {
        _animator = animator;
    }

    [ContextMenu("Test Position On Back")]
    public virtual void FixPositionOnBack()
    {
        transform.localPosition = _positionOnBack.Position;
        transform.localRotation = Quaternion.Euler(_positionOnBack.Rotation);
    } 

    public virtual bool IsWeaponBusy()
    {
        if (_isHeldDown || _currentCooldown>0) return true;
        return false;
    }

    public virtual void ClearAttacks()
    {
        ClearCharge();
    }

    protected void ClearCharge()
    {
        _charge = 0;
        _isHeldDown = false;
    }

    [System.Serializable]
    protected class PositionOnBack
    {
        public Vector3 Position = Vector3.zero;
        public Vector3 Rotation = Vector3.zero;
    }
}
