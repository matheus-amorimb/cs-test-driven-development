using Bogus;
using Bogus.Extensions.Brazil;
using CoursePlataform.Domain.Courses;
using CoursePlataform.Domain.Students;
using CoursePlataform.DomainTest.Builders;
using FluentAssertions;
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
    public void Add_StudentSuccessfully()
    {
        _studentStorage.Add(_studentDto);
        _studentRepositoryMock.Verify(
            repository =>
                repository.Add(It.Is<Student>(student =>
                    student.Name == _studentDto.Name && student.Cpf == _studentDto.Cpf)), Times.AtLeast(1));
    }

    [Fact]
    public async Task Add_DuplicateStudentCpf_ThrowsArgumentException()
    {
        var studentAlreadySaved = StudentBuilder.New().WithCpf(_studentDto.Cpf).Build();
        _studentRepositoryMock.Setup(repository => repository.GetByCpf(_studentDto.Cpf)).ReturnsAsync(studentAlreadySaved);

        Func<Task> action = async () => await _studentStorage.Add(_studentDto);

        await action.Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public async Task Update_StudentSuccessfully()
    {
        var studentToUpdate = StudentBuilder.New().WithCpf(_studentDto.Cpf).Build();
        _studentRepositoryMock.Setup(repository => repository.GetByCpf(_studentDto.Cpf)).ReturnsAsync(studentToUpdate);
        
        await _studentStorage.Update(_studentDto);

        studentToUpdate.Name.Should().Be(_studentDto.Name);
        studentToUpdate.Email.Should().Be(_studentDto.Email);

    }

    [Fact]
    public async Task Update_StudentInvalidCpf_ThrowsArgumentException()
    {
        Student student = null;
        _studentRepositoryMock.Setup(r => r.GetByCpf(_studentDto.Cpf)).ReturnsAsync(student);

        Func<Task> action = async () => await _studentStorage.Update(_studentDto);

        await action.Should().ThrowAsync<ArgumentException>();

    }
    
}