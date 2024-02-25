using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField] private ShotgunAttack NormalAttack;
    [SerializeField] private ShotgunAttack ChargedAttack;
    [SerializeField] private string _bulletName;
    [SerializeField] private Transform _bulletSpawnPoint;
    private ObjectPooler pooler;
    private void Start()
    {
        pooler = GameManager.Instance.ObjectPooler;
    }
    public override void OnPress()
    {
        _isHeldDown = true;
    }

    public override void OnRelease()
    {
        if (_currentCooldown <= 0)
        {
            _isHeldDown = false;
            ShotgunProjectile bullet = pooler.SpawnFromPool(_bulletName, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation).GetComponent<ShotgunProjectile>();
            if (_charge >= _maxCharge)
            {
                bullet.Initialize(ChargedAttack.Damage, ChargedAttack.BulletSpeed, ChargedAttack.GrowSpeed, ChargedAttack.TimeInAir);
                _currentCooldown = ChargedAttack.Cooldown;
            }
            else
            {
                bullet.Initialize(NormalAttack.Damage, NormalAttack.BulletSpeed, NormalAttack.GrowSpeed, NormalAttack.TimeInAir);
                _currentCooldown = NormalAttack.Cooldown;
            }
            _charge = 0;
        }
    }

    public override bool IsWeaponBusy()
    {
        return false;
    }
}
