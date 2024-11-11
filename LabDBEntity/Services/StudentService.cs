using LabDBEntity.DAL.Settings;
using LabDBEntity.DAL;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LabDBEntity.Services
{
    public class StudentService : IStudentService
    {
        private readonly IMongoCollection<Student> _studentsCollection;

        public StudentService(IOptions<MongoDBSettings> mongoDBSettings, IMongoClient mongoClient)
        {
            var mongoDatabase = mongoClient.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _studentsCollection = mongoDatabase.GetCollection<Student>("Students");
        }

        public async Task<List<Student>> GetAsync() =>
            await _studentsCollection.Find(s => true).ToListAsync();

        public async Task<Student> GetByIdAsync(string id) =>
            await _studentsCollection.Find(s => s.StudentId == int.Parse(id)).FirstOrDefaultAsync();

        public async Task CreateAsync(Student student) =>
            await _studentsCollection.InsertOneAsync(student);

        public async Task UpdateAsync(string id, Student updatedStudent) =>
            await _studentsCollection.ReplaceOneAsync(s => s.StudentId == int.Parse(id), updatedStudent);

        public async Task RemoveAsync(string id) =>
            await _studentsCollection.DeleteOneAsync(s => s.StudentId == int.Parse(id));

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
