using UnityEngine;

public class Axe : Weapon
{
    [SerializeField] private PlayerAttack[] _axeCombo = new PlayerAttack[3];
    [SerializeField] private PlayerAttack _axeCharged;
    [SerializeField] private MeleeHitbox _hitCollider;
    [SerializeField] private float _maxCharge;
    private float _charge;
    private bool _isHeldDown;
    private int _currentCombo;
    private float _currentCooldown;

    private void Update()
    {
        WeaponCharging();
        WeaponCooldownTime();
    }

    private void WeaponCharging()
    {
        if (_isHeldDown && _charge< _maxCharge)
        {
            _charge += Time.deltaTime;
        }
        else if (_charge > _maxCharge)
        {
            _charge = _maxCharge;
        }
    }

    private void WeaponCooldownTime()
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

    public override void OnPress()
    {
        _isHeldDown=true;
    }

    public override void OnRelease()
    {
        if (_currentCooldown > 0)
        { 
            _charge = 0; 
            return; 
        }

        _isHeldDown = false;
        if (_charge>= _maxCharge)
        {
            AxeAttack(_axeCharged);
        }
        else
        {
            AxeAttack(_axeCombo[_currentCombo]);
            _currentCombo++;
            if (_currentCombo >= _axeCombo.Length)
            {
                _currentCombo = 0;
            }
        }
        _charge = 0;
    }

    private void AxeAttack(PlayerAttack attack)
    {
        _animator.SetTrigger(attack.AnimationName);
        _hitCollider.SetDamage(attack.Damage);
        _currentCooldown = attack.Cooldown;
    }
}
