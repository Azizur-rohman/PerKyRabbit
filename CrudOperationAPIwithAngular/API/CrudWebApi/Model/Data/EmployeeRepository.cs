using Microsoft.EntityFrameworkCore;

namespace CrudWebApi.Model.Data
{
    public class EmployeeRepository : IEmployeeRepository      
    {
        private readonly EmployeeContext _context;

        public EmployeeRepository(EmployeeContext context)
        {
            _context = context;
        }
        public void Create(TblEmployee TblEmployee)
        {
            _context.Add(TblEmployee);
        }

        public void Delete(int id)
        {
           _context.TblEmployees.Remove(_context.TblEmployees.FirstOrDefault(e => e.Id == id));
        }

        public TblEmployee Get(int id)
        {

            var employees = (from e in _context.TblEmployees
                             join d in _context.TblDesignations
                             on e.DesignationID equals d.Id

                             select new TblEmployee
                             {
                                 Id = e.Id,
                                 Name = e.Name,
                                 LastName = e.LastName,
                                 Email = e.Email,
                                 Age = e.Age,
                                 DesignationID = e.DesignationID,
                                 Designation = d.Designation,
                                 Doj = e.Doj,
                                 Gender = e.Gender,
                                 IsActive = e.IsActive,
                                 IsMarried = e.IsMarried
                             }
                           ).ToList();

            return employees.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<TblEmployee> GetAll()
        {

            var employees = (from e in _context.TblEmployees
                             join d in _context.TblDesignations
                             on e.DesignationID equals d.Id

                             select new TblEmployee
                             {
                                 Id = e.Id,
                                 Name = e.Name,
                                 LastName = e.LastName,
                                 Email = e.Email,
                                 Age = e.Age,
                                 DesignationID = e.DesignationID,
                                 Designation = d.Designation,
                                 Doj = e.Doj,
                                 Gender = e.Gender,
                                 IsActive = e.IsActive,
                                 IsMarried = e.IsMarried
                             }
                            ).ToList();




            return employees;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void Update(int id, TblEmployee tblEmployee)
        {
            _context.Entry(tblEmployee).State = EntityState.Modified;

        }

        bool IEmployeeRepository.TblEmployeeExists(int id)
        {
            return _context.TblEmployees.Any(e => e.Id == id);
        }
    }
}
