Imports NUnit.Framework

<TestFixture>
Public Class DatabaseTests
    ' Test for the AddPerson method
    <Test>
    Public Sub AddPerson_ShouldLogPersonAdded()
        ' Arrange: Create a new database instance and a person object
        Dim database As New Database()
        Dim person As New Person("Max Mustermann", 30, "Bad Mergentheim")
        
        ' Act: Add the person to the database
        database.AddPerson(person)
        Dim res = database.GetRecords()
        
        ' Assert: Verify that the person has been successfully added
        Assert.AreEqual(1, res.Count) ' Expect one person in the database
        Assert.AreEqual("Max Mustermann", res(0).Name) ' Verify that the person's name is correct
    End Sub

    ' Test for the FilterPeopleByAgeRange method
    <Test>
    Public Sub FilterPeopleByAgeRange_ShouldReturnCorrectResults()
        ' Arrange: Create a new database instance and add two persons
        Dim database As New Database()
        database.AddPerson(New Person("Max Mustermann", 30, "Bad Mergentheim"))
        database.AddPerson(New Person("Erika Musterfrau", 40, "Bad Mergentheim"))
        
        ' Act: Filter people by age range (30 to 35)
        database.FilterPeopleByAgeRange(30, 35)
        Dim filteredPeople = database.GetRecords()
        
        ' Assert: Verify that both persons are within the age range
        Assert.AreEqual(2, filteredPeople.Count) ' Expect two people in the filtered list
        Assert.AreEqual("Max Mustermann", filteredPeople(0).Name) ' Verify the first person
    End Sub

    ' Test for the SortPeopleByAge method
    <Test>
    Public Sub SortPeopleByAge_ShouldSortPeopleByAgeAscending()
        ' Arrange: Create a new database instance and add three persons with different ages
        Dim database As New Database()
        database.AddPerson(New Person("Max Mustermann", 30, "Bad Mergentheim"))
        database.AddPerson(New Person("Erika Musterfrau", 40, "Bad Mergentheim"))
        database.AddPerson(New Person("John Doe", 25, "Bad Mergentheim"))

        ' Act: Sort the people by age in ascending order
        database.SortPeopleByAge()

        ' Assert: Verify that the people are sorted by age in ascending order
        Dim sortedPeople = database.GetRecords()
        Assert.AreEqual("John Doe", sortedPeople(0).Name) ' The youngest person should be first
        Assert.AreEqual("Max Mustermann", sortedPeople(1).Name) ' The second youngest person should be second
        Assert.AreEqual("Erika Musterfrau", sortedPeople(2).Name) ' The oldest person should be last
    End Sub

    ' Test for the CalculateStatistics method
    <Test>
    Public Sub CalculateStatistics_ShouldReturnCorrectStatistics()
        ' Arrange: Create a new database instance and add three persons with different ages
        Dim database As New Database()
        database.AddPerson(New Person("Max Mustermann", 30, "Bad Mergentheim"))
        database.AddPerson(New Person("Erika Musterfrau", 40, "Bad Mergentheim"))
        database.AddPerson(New Person("John Doe", 25, "Bad Mergentheim"))

        ' Act: Calculate the statistics (average age, oldest, and youngest person)
        database.CalculateStatistics()
        Dim res = database.GetRecords()

        ' Assert: Verify that the statistics are correct
        ' Check if the average age is calculated correctly
        Assert.AreEqual(31.666666666666668d, res.Average(Function(p) p.Age)) ' Average age is approximately 31.67
        ' Check if the youngest person's age is correctly identified (25 years old)
        Assert.AreEqual(25, res.OrderBy(Function(p) p.Age).First().Age) ' The youngest person should be 25 years old
        ' Check if the oldest person's age is correctly identified (40 years old)
        Assert.AreEqual(40, res.OrderByDescending(Function(p) p.Age).First().Age) ' The oldest person should be 40 years old
    End Sub
End Class
