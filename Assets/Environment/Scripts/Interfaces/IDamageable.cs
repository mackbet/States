
public interface IDamageable
{
    public Parameters Parameters { get; }
    public bool IsAlive { get; }
    public void TakeDamage(float value);
}
