namespace _GameFolders.Scripts
{
    public class WarriorDummy : BaseDummy
    {
        public override void Close()
        {
            base.Close();
            ObjectPool.Instance.Release(this);
        }
    }
}