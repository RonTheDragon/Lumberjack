public class PlayerHealth : Health
{
    public override void Die()
    {
        base.Die();
        GetComponent<PlayerMovement>().enabled=false;
    }
}
