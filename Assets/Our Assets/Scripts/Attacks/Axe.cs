using UnityEngine;

public class Axe : Weapon
{
    [SerializeField] private PlayerAttack[] _axeCombo = new PlayerAttack[3];
    [SerializeField] private PlayerAttack _axeCharged;
    [SerializeField] private MeleeHitbox _hitCollider;
    private int _currentCombo;
    [SerializeField] private float _loseComboTime=0.5f;
    private float _comboTimeLeft;

    protected new void Update()
    {
        base.Update();
        ComboTimer();
    }

    private void ComboTimer()
    {
        if (_currentCombo > 0)
        {
            if (_comboTimeLeft > 0)
            {
                _comboTimeLeft -=Time.deltaTime;
            }
            else
            {
                _currentCombo = 0; _comboTimeLeft = 0;
            }
        }
    }

    public override void OnPress()
    {
        if (_currentCooldown <= 0)
        {
            _isHeldDown = true;
        }
    }

    public override void OnRelease()
    {
        if (_currentCooldown > 0)
        {
            ClearCharge();
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
            else
            {
                _comboTimeLeft = _axeCombo[_currentCombo].Cooldown + _loseComboTime;
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

    public override void ClearAttacks()
    {
        base.ClearAttacks();
        _currentCombo = 0;
    }
}
