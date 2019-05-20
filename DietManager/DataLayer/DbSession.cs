namespace DietManager.DataLayer
{
    // TODO: make it thread safe according to https://csharpindepth.com/articles/singleton
    public class DbSession
    {
        private static DbSession _instance;
        private AppDbContext _dbContext;


        private DbSession()
        {
            _dbContext = new AppDbContext();
        }

        public static DbSession Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DbSession();
                }
                return _instance;
            }
        }

        public AppDbContext GetAppDbcontext ()
        {
            return Instance._dbContext;
        }
    }
}
