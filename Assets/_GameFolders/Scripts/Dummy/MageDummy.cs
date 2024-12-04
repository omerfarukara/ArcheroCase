namespace _GameFolders.Scripts
{
    public class MageDummy : BaseDummy
    {
        public override void Close()
        {
            base.Close();
            ObjectPool.Instance.Release(this);
        }
    }
}