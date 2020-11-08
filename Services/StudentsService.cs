using System.Collections.Generic;
using MongoDB.Driver;
using StudentsAPI.Models;

namespace StudentsAPI.Services
{
    public class StudentsService
    {
        private readonly IMongoCollection<Student> _students;

        public StudentsService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _students = database.GetCollection<Student>(settings.CollectionName);
        }

        public List<Student> Get() => _students.Find(student => true).ToList();

        public Student Get(string id) => _students.Find<Student>(student => student.Id == id).FirstOrDefault();

        public Student Create(Student student)
        {
            _students.InsertOne(student);
            return student;
        }

        public void Update(string id, Student studentIn) => _students.FindOneAndReplace(student => student.Id == id, studentIn);

        public void Remove(Student studentIn) => _students.DeleteOne(student => student.Id == studentIn.Id);

        public void Remove(string id) => _students.DeleteOne(student => student.Id == id);
    }
}