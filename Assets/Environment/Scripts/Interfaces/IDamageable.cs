
public interface IDamageable
{
    public int TeamId { get; }
    public bool IsAlive { get; }
    public void TakeDamage(float value);
}
