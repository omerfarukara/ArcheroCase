namespace _GameFolders.Scripts
{
    public class RogueDummy : BaseDummy
    {
        public override void Close()
        {
            base.Close();
            ObjectPool.Instance.Release(this);
        }
    }
}