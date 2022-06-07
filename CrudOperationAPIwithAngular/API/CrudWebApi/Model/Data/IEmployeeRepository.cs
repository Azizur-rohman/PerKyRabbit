namespace CrudWebApi.Model.Data
{
    public interface IEmployeeRepository
    {
        IEnumerable<TblEmployee> GetAll();
        TblEmployee Get(int id);
        void Delete(int id);
        void Update(int id, TblEmployee tblEmployee);
        void Create(TblEmployee tblEmployee);
        bool SaveChanges();
        bool TblEmployeeExists(int id);
    }
}
