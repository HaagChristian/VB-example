Imports NUnit.Framework

<TestFixture>
Public Class PersonTests
    ' Test for the constructor of the Person class to ensure it initializes properties correctly
    <Test>
    Public Sub Constructor_ShouldInitializeProperties()
        ' Arrange: Create a new Person object with specific values for Name, Age, and City
        Dim person As New Person("Jane Doe", 25, "Berlin")

        ' Act: The constructor initializes the properties (this is done in the creation step)

        ' Assert: Verify that the person's properties are correctly initialized
        Assert.That(person.Name, [Is].EqualTo("Jane Doe")) ' Verify that the Name property is set correctly
        Assert.That(person.Age, [Is].EqualTo(25)) ' Verify that the Age property is set correctly
        Assert.That(person.City, [Is].EqualTo("Berlin")) ' Verify that the City property is set correctly
    End Sub
End Class
