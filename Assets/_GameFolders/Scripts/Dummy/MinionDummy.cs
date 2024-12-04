namespace _GameFolders.Scripts
{
    public class MinionDummy : BaseDummy
    {
        public override void Close()
        {
            base.Close();
            ObjectPool.Instance.Release(this);
        }  }
}