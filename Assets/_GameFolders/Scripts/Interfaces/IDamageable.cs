namespace _GameFolders.Scripts.Interfaces
{
    public interface IDamageable
    {
        public float Health { get; set; }
        public void TakeDamage(int damageValue);
    }
}