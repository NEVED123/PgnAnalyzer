namespace PgnAnalyzer.MyTest; //<---Any test you write must be in the MyTest namespace to be run via "analyzer test"

//This framework uses NUnit for testing. See https://github.com/nunit/nunit-csharp-samples

public class Template_Test
{
    [SetUp]
    public void SetUp()
    {
        //Code to be run before each test
    }

    [Test]
    public void Test()
    {
        //Test code with Assert statements. 
        //For more on NUnit and Assertions, visit https://docs.nunit.org/articles/nunit/writing-tests/assertions/assertions.html
        Assert.Pass();
    }
}