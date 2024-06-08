namespace CoursePlataform.DomainTest.Utilities;

public static class AssertExtension
{
    public static void WithMessage(this ArgumentException argumentException, string message)
    {
        if (argumentException.Message == message)
        {
            Assert.True(true);
        }
        else
        {
            Assert.False(true);
        }
        
    }
}