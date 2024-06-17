using Bogus;
using Bogus.Extensions.Brazil;
using CoursePlataform.Domain.Courses;
using CoursePlataform.Domain.Students;
using Moq;

namespace CoursePlataform.DomainTest.Courses;

public class StudentStoreTest
{
    private readonly StudentDto _studentDto;
    private readonly StudentStorage _studentStorage;
    private readonly Mock<IStudentRepository> _studentRepositoryMock;

    public StudentStoreTest()
    {
        var fake = new Faker();
        _studentDto = new StudentDto(
            Name: fake.Person.FullName,
            Cpf: fake.Person.Cpf(),
            Email: fake.Person.Email,
            TargetAudience: TargetAudience.Student
        );
        _studentRepositoryMock = new Mock<IStudentRepository>();
        _studentStorage = new StudentStorage(_studentRepositoryMock.Object);
    }

    [Fact]
    public void MustStoreNewStudent()
    {
        _studentStorage.Add(_studentDto);
        _studentRepositoryMock.Verify(
            repository =>
                repository.Add(It.Is<Student>(student =>
                    student.Name == _studentDto.Name && student.Cpf == _studentDto.Cpf)), Times.AtLeast(1));
    }

    [Fact]
    public void MustNotAddStudentWIthCpfAlreadyInUse()
    {
        
    }
}