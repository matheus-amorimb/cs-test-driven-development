namespace CoursePlatform.Data.Repositories.Interfaces;

public interface IUnitOfWork
{
    Task Commit();
}