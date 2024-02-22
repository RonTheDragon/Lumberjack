public class PlayerHealth : Health
{
    public override void Die()
    {
        base.Die();
        GetComponent<PlayerController>().enabled=false;
    }
}
