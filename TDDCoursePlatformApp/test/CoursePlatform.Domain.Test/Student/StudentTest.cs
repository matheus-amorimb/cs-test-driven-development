using Bogus;
using Bogus.Extensions.Brazil;
using CoursePlataform.Domain.Courses;
using CoursePlataform.Domain.Students;
using CoursePlataform.Domain.Utilities;
using CoursePlataform.DomainTest.Builders;
using FluentAssertions;
using Xunit.Abstractions;

namespace CoursePlataform.DomainTest.Courses;

public class StudentTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    private StudentDto _studentDto;
    private readonly Faker _faker;
    
    public StudentTest(ITestOutputHelper testOutputHelper)
    {
        _faker = new Faker();
        _testOutputHelper = testOutputHelper;
        _studentDto = new StudentDto(
            Name: _faker.Person.FullName,
            Cpf: _faker.Person.Cpf(),
            Email: _faker.Person.Email,
            TargetAudience: TargetAudience.Student
        );
    }
    
    [Fact]
    public void Constructor_ShouldCreateStudent()
    {
        var expectedStudent =  new Student(
            _studentDto.Name,
            _studentDto.Cpf,
            _studentDto.Email,
            _studentDto.TargetAudience
        );

        expectedStudent.Should().BeEquivalentTo(_studentDto);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Constructor_InvalidName_ShouldThrowArgumentException(string invalidName)
    {
        Action action = () =>  StudentBuilder.New().WithName(invalidName).Build();
        action.Should().Throw<ArgumentException>().WithMessage(Resource.InvalidName);
    }      
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Constructor_InvalidCpf_ShouldThrowArgumentException(string invalidCpf)
    {
        Action action = () =>  StudentBuilder.New().WithCpf(invalidCpf).Build();
        action.Should().Throw<ArgumentException>().WithMessage(Resource.InvalidCpf);
    }    
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Constructor_InvalidEmail_ShouldThrowArgumentException(string invalidEmail)
    {
        Action action = () =>  StudentBuilder.New().WithEmail(invalidEmail).Build();
        action.Should().Throw<ArgumentException>().WithMessage(Resource.InvalidEmail);
    }

    [Fact]
    public void ChangeName_ShouldUpdateStudentName()
    {
        var student = StudentBuilder.New().Build();
        string newName = _faker.Person.FullName;
        student.ChangeName(newName);

        student.Name.Should().Be(newName);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void ChangeName_InvalidName_ShouldThrowArgumentException(string invalidName)
    {

        Action action = () => StudentBuilder.New().Build().ChangeName(invalidName);
        action.Should().Throw<ArgumentException>().WithMessage(Resource.InvalidName);
    }

}

        