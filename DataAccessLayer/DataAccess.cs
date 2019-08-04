using Utils.Helpers;

namespace DataAccessLayer
{
    public abstract class DataAccess
    {
        protected static readonly string connectionString = "Data Source=DESKTOP-KJ1KTE2\\SQLEXPRESS; Initial Catalog=Encuestas; Persist Security Info=False; User ID=user; Password=user";
        protected string tableName { get; private set; }

        public DataAccess()
        {
            tableName = this.NameOf().Replace("DataAccess","");
        }
    }
}
