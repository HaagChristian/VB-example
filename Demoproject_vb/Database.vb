' Database.vb
Imports System
Imports System.IO
Imports System.Linq
Imports System.Threading.Tasks

Public Class Database
    Private records As New List(Of Person)()

    ' Methode zum Hinzufügen einer Person
    Public Sub AddPerson(person As Person)
        ' Überprüfen, ob die Person gültige Werte hat
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

        ' Person hinzufügen, wenn alle Eingaben gültig sind
        records.Add(person)
        Console.WriteLine($"Person added: {person.Name}, {person.Age}, {person.City}")
    End Sub

    ' Asynchrone Methode zum Speichern der Daten in einer Datei mit benutzerdefiniertem Pfad
    Public Async Function SaveToFileAsync(fileName As String) As Task
        ' Überprüfen, ob Daten vorhanden sind
        If records.Count = 0 Then
            Throw New Exception("No data available to save.")
        End If
        
        Dim lines = records.Select(Function(p) $"{p.Name},{p.Age},{p.City}").ToArray()
        Await File.WriteAllLinesAsync(fileName, lines)
        Console.WriteLine($"Data saved to file {fileName} asynchronously.")
    End Function

    ' Asynchrone Methode zum Laden der Daten aus einer Datei mit benutzerdefiniertem Pfad
    Public Async Function LoadFromFileAsync(fileName As String) As Task
        If File.Exists(fileName) Then
            Dim lines = Await File.ReadAllLinesAsync(fileName)
            records.Clear()
            For Each line In lines
                Dim parts = line.Split(","c)
                records.Add(New Person(parts(0), Integer.Parse(parts(1)), parts(2)))
            Next
            Console.WriteLine($"Data loaded from file {fileName} asynchronously.")
        Else
            Console.WriteLine("File not found.")
        End If
    End Function

    ' Methode zum Auflisten der Personen
    Public Sub ListPeople()
        If records.Count = 0 Then
            Console.WriteLine("No records found.")
        Else
            For Each person In records
                Console.WriteLine($"{person.Name}, {person.Age}, {person.City}")
            Next
        End If
    End Sub

    ' Methode zum Sortieren der Personen nach Alter
    Public Sub SortPeopleByAge()
        records = records.OrderBy(Function(p) p.Age).ToList()
        Console.WriteLine("People sorted by Age.")
    End Sub

    ' Methode zum Filtern der Personen nach Altersbereich
    Public Sub FilterPeopleByAgeRange(minAge As Integer, maxAge As Integer)
        Dim filteredPeople = records.Where(Function(p) p.Age >= minAge AndAlso p.Age <= maxAge).ToList()
        If filteredPeople.Count = 0 Then
            Console.WriteLine("No people found in this age range.")
        Else
            For Each person In filteredPeople
                Console.WriteLine($"{person.Name}, {person.Age}, {person.City}")
            Next
        End If
    End Sub

    ' Methode zum Berechnen von Statistiken wie Durchschnittsalter, älteste und jüngste Person
    Public Sub CalculateStatistics()
        If records.Count = 0 Then
            Console.WriteLine("No data available for statistics.")
            Return
        End If

        Dim averageAge = records.Average(Function(p) p.Age)
        Dim oldestPerson = records.OrderByDescending(Function(p) p.Age).First()
        Dim youngestPerson = records.OrderBy(Function(p) p.Age).First()

        Console.WriteLine($"Average Age: {averageAge:F2}")
        Console.WriteLine($"Oldest Person: {oldestPerson.Name}, Age: {oldestPerson.Age}")
        Console.WriteLine($"Youngest Person: {youngestPerson.Name}, Age: {youngestPerson.Age}")
    End Sub

    ' Methode zum Suchen von Personen nach dem Anfangsbuchstaben des Namens
    Public Sub SearchByInitial(initial As Char)
        Dim filteredPeople = records.Where(Function(p) p.Name.StartsWith(initial.ToString(), StringComparison.OrdinalIgnoreCase)).ToList()
        If filteredPeople.Count = 0 Then
            Console.WriteLine($"No people found with initial: {initial}")
        Else
            For Each person In filteredPeople
                Console.WriteLine($"{person.Name}, {person.Age}, {person.City}")
            Next
        End If
    End Sub

    ' Öffentliche Methode zum Abrufen der Liste der Personen
    Public Function GetRecords() As List(Of Person)
        Return records
    End Function
End Class
