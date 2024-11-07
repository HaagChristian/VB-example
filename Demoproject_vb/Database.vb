' Database.vb
Imports System
Imports System.IO
Imports System.Linq
Imports System.Threading.Tasks

' Manages a collection of Person objects and provides various operations
Public Class Database
    ' A private list to store Person records
    Private _records As New List(Of Person)()

    ' Adds a person to the records list, after validating input values
    ' Parameters:
    '   person - The Person object to add
    Public Sub AddPerson(person As Person)
        ' Validate that the person's Name, Age, and City are correct
        If String.IsNullOrEmpty(person.Name) Then
            Console.WriteLine("Error: Name cannot be empty.")
            Return
        End If

        If person.Age <= 0 Then
            Console.WriteLine("Error: Age must be a positive number.")
            Return
        End If

        If String.IsNullOrEmpty(person.City) Then
            Console.WriteLine("Error: City cannot be empty.")
            Return
        End If

        ' Add the person to the list if all inputs are valid
        _records.Add(person)
        Console.WriteLine($"Person added: {person.Name}, {person.Age}, {person.City}")
    End Sub

    ' Asynchronously saves all records to a file
    ' Parameters:
    '   fileName - The name of the file to save data to
    Public Async Function SaveToFileAsync(fileName As String) As Task
        ' Check if there is any data to save
        If _records.Count = 0 Then
            Throw New Exception("No data available to save.")
        End If

        ' Format each person record as a CSV line (Name, Age, City)
        Dim lines = _records.Select(Function(p) $"{p.Name},{p.Age},{p.City}").ToArray()
        Await File.WriteAllLinesAsync(fileName, lines)
        Console.WriteLine($"Data saved to file {fileName} asynchronously.")
    End Function

    ' Asynchronously loads records from a file
    ' Parameters:
    '   fileName - The name of the file to load data from
    Public Async Function LoadFromFileAsync(fileName As String) As Task
        ' Check if the file exists before attempting to load data
        If File.Exists(fileName) Then
            Dim lines = Await File.ReadAllLinesAsync(fileName)
            _records.Clear() ' Clear existing records before loading new ones
            For Each line In lines
                ' Split each line by comma to extract Person details
                Dim parts = line.Split(","c)
                _records.Add(New Person(parts(0), Integer.Parse(parts(1)), parts(2)))
            Next
            Console.WriteLine($"Data loaded from file {fileName} asynchronously.")
        Else
            Console.WriteLine("File not found.")
        End If
    End Function

    ' Displays all people in the records list
    Public Sub ListPeople()
        If _records.Count = 0 Then
            Console.WriteLine("No records found.")
        Else
            For Each person In _records
                Console.WriteLine($"{person.Name}, {person.Age}, {person.City}")
            Next
        End If
    End Sub

    ' Sorts people in the records list by age in ascending order
    Public Sub SortPeopleByAge()
        _records = _records.OrderBy(Function(p) p.Age).ToList()
        Console.WriteLine("People sorted by Age.")
    End Sub

    ' Filters people within a specified age range and displays the results
    ' Parameters:
    '   minAge - The minimum age for the filter
    '   maxAge - The maximum age for the filter
    Public Sub FilterPeopleByAgeRange(minAge As Integer, maxAge As Integer)
        ' Find all people whose age is within the specified range
        Dim filteredPeople = _records.Where(Function(p) p.Age >= minAge AndAlso p.Age <= maxAge).ToList()
        If filteredPeople.Count = 0 Then
            Console.WriteLine("No people found in this age range.")
        Else
            For Each person In filteredPeople
                Console.WriteLine($"{person.Name}, {person.Age}, {person.City}")
            Next
        End If
    End Sub

    ' Calculates statistics like average age, oldest, and youngest person
    Public Sub CalculateStatistics()
        If _records.Count = 0 Then
            Console.WriteLine("No data available for statistics.")
            Return
        End If

        ' Calculate average age and find the oldest and youngest person
        Dim averageAge = _records.Average(Function(p) p.Age)
        Dim oldestPerson = _records.OrderByDescending(Function(p) p.Age).First()
        Dim youngestPerson = _records.OrderBy(Function(p) p.Age).First()

        ' Display the calculated statistics
        Console.WriteLine($"Average Age: {averageAge:F2}")
        Console.WriteLine($"Oldest Person: {oldestPerson.Name}, Age: {oldestPerson.Age}")
        Console.WriteLine($"Youngest Person: {youngestPerson.Name}, Age: {youngestPerson.Age}")
    End Sub

    ' Searches for people based on the initial character of their name
    ' Parameters:
    '   initial - The initial character to filter names by
    Public Sub SearchByInitial(initial As Char)
        ' Find all people whose names start with the specified character
        Dim filteredPeople =
                _records.Where(Function(p) p.Name.StartsWith(initial.ToString(), StringComparison.OrdinalIgnoreCase)).
                ToList()
        If filteredPeople.Count = 0 Then
            Console.WriteLine($"No people found with initial: {initial}")
        Else
            For Each person In filteredPeople
                Console.WriteLine($"{person.Name}, {person.Age}, {person.City}")
            Next
        End If
    End Sub

    ' Retrieves the list of all Person objects in the records
    Public Function GetRecords() As List(Of Person)
        Return _records
    End Function
End Class