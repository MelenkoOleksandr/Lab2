using LabDBEntity.DAL;

namespace LabDBEntity.Services
{
    public interface IStudentService
    {
        Task<List<Student>> GetAsync();
        Task<Student> GetByIdAsync(string id);
        Task CreateAsync(Student student);
        Task UpdateAsync(string id, Student updatedStudent);
        Task RemoveAsync(string id);
    }
}
