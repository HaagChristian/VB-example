Imports System
Imports System.IO
Imports System.Linq
Imports System.Threading.Tasks

' Person Klasse
Public Class Person
    Public Property Name As String
    Public Property Age As Integer
    Public Property City As String

    Public Sub New(name As String, age As Integer, city As String)
        Me.Name = name
        Me.Age = age
        Me.City = city
    End Sub
End Class

' Datenbank Klasse
Public Class Database
    Private records As New List(Of Person)()

    ' Methode zum Hinzufügen einer Person
    Public Sub AddPerson(person As Person)
        records.Add(person)
        Console.WriteLine($"Person added: {person.Name}, {person.Age}, {person.City}")
    End Sub

    ' Asynchrone Methode zum Speichern der Daten in einer Datei
    Public Async Function SaveToFileAsync(fileName As String) As Task
        ' Überprüfen, ob Daten vorhanden sind
        If records.Count = 0 Then
            Throw New Exception("No data available to save.")
        End If
        
        Dim lines = records.Select(Function(p) $"{p.Name},{p.Age},{p.City}").ToArray()
        Await File.WriteAllLinesAsync(fileName, lines)
        Console.WriteLine($"Data saved to file {fileName} asynchronously.")
    End Function

    ' Asynchrone Methode zum Laden der Daten aus einer Datei
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

' Hauptprogramm
Public Module Program
    ' Die synchrone Main-Methode ruft die asynchrone MainAsync-Methode auf
    Public Sub Main()
        MainAsync().GetAwaiter().GetResult()
    End Sub

    ' Hauptmethode als Async für die Verwendung von Await
    Public Async Function MainAsync() As Task
        Dim db As New Database()
        Await MainLoop(db)
    End Function

    Public Async Function MainLoop(db As Database) As Task
        While True
            ' Optionen anzeigen
            Console.WriteLine(vbCrLf & "Select an option:")
            Console.WriteLine("1. Add Person")
            Console.WriteLine("2. List People")
            Console.WriteLine("3. Filter People by Age Range")
            Console.WriteLine("4. Sort People by Age")
            Console.WriteLine("5. Calculate Age Statistics")
            Console.WriteLine("6. Search by Name Initial")
            Console.WriteLine("7. Save Data to File (Async)")
            Console.WriteLine("8. Load Data from File (Async)")
            Console.WriteLine("9. Exit")

            Dim choice As String = Console.ReadLine()

            Select Case choice
                Case "1"
                    Await AddPerson(db)
                Case "2"
                    db.ListPeople()
                Case "3"
                    Await FilterPeopleByAgeRange(db)
                Case "4"
                    db.SortPeopleByAge()
                Case "5"
                    db.CalculateStatistics()
                Case "6"
                    Await SearchByInitial(db)
                Case "7"
                    Await SaveDataToFile(db)
                Case "8"
                    Await LoadDataFromFile(db)
                Case "9"
                    ExitProgram()
                Case Else
                    Console.WriteLine("Invalid choice, try again.")
            End Select
        End While
    End Function

    ' Funktion zum Hinzufügen einer Person
    Public Async Function AddPerson(db As Database) As Task
        Try
            Console.WriteLine("Enter Name:")
            Dim name As String = Console.ReadLine()
            If String.IsNullOrEmpty(name) Then Throw New Exception("Name cannot be empty.")
            
            Console.WriteLine("Enter Age:")
            Dim age As Integer = Integer.Parse(Console.ReadLine())
            If age <= 0 Then Throw New Exception("Age must be positive.")
            
            Console.WriteLine("Enter City:")
            Dim city As String = Console.ReadLine()
            If String.IsNullOrEmpty(city) Then Throw New Exception("City cannot be empty.")

            Dim person As New Person(name, age, city)
            db.AddPerson(person)
        Catch ex As Exception
            Console.WriteLine($"Error: {ex.Message}")
        End Try
    End Function

    ' Funktion zum Filtern der Personen nach Altersbereich
    Public Async Function FilterPeopleByAgeRange(db As Database) As Task
        Try
            Console.WriteLine("Enter minimum age:")
            Dim minAge As Integer = Integer.Parse(Console.ReadLine())
            Console.WriteLine("Enter maximum age:")
            Dim maxAge As Integer = Integer.Parse(Console.ReadLine())
            db.FilterPeopleByAgeRange(minAge, maxAge)
        Catch ex As Exception
            Console.WriteLine($"Error: {ex.Message}")
        End Try
    End Function

    ' Funktion zum Suchen nach Initialen
    Public Async Function SearchByInitial(db As Database) As Task
        Try
            Console.WriteLine("Enter initial character:")
            Dim initial As Char = Console.ReadLine()(0)
            db.SearchByInitial(initial)
        Catch ex As Exception
            Console.WriteLine($"Error: {ex.Message}")
        End Try
    End Function

    ' Speicherfunktion mit Überprüfung auf vorhandene Daten
    Public Async Function SaveDataToFile(db As Database) As Task
        Try
            ' Überprüfen, ob Daten vorhanden sind
            If db.GetRecords().Count = 0 Then
                Console.WriteLine("No data available to save.")
                Return
            End If

            Console.WriteLine("Do you want to specify a custom path? (Y/N):")
            Dim choice As String = Console.ReadLine().ToUpper()

            Dim filePath As String
            If choice = "Y" Then
                Console.WriteLine("Enter the full file path to save data:")
                filePath = Console.ReadLine()
                If String.IsNullOrEmpty(filePath) Then
                    Throw New Exception("File path cannot be empty.")
                End If
            Else
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "data.txt") ' Standardpfad im Code-Verzeichnis
                Console.WriteLine($"No path specified, saving as {filePath}")
            End If

            Await db.SaveToFileAsync(filePath)
        Catch ex As Exception
            Console.WriteLine($"Error: {ex.Message}")
        End Try
    End Function

    ' Ladefunktion für die Daten
    Public Async Function LoadDataFromFile(db As Database) As Task
        Try
            Console.WriteLine("Do you want to specify a custom path? (Y/N):")
            Dim choice As String = Console.ReadLine().ToUpper()

            Dim filePath As String
            If choice = "Y" Then
                Console.WriteLine("Enter the full file path to load data:")
                filePath = Console.ReadLine()
                If String.IsNullOrEmpty(filePath) Then
                    Throw New Exception("File path cannot be empty.")
                End If
            Else
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "data.txt") ' Standardpfad im Code-Verzeichnis
                Console.WriteLine($"No path specified, loading from {filePath}")
            End If

            Await db.LoadFromFileAsync(filePath)
        Catch ex As Exception
            Console.WriteLine($"Error: {ex.Message}")
        End Try
    End Function

    ' Programm beenden
    Public Sub ExitProgram()
        Console.WriteLine("Exiting...")
        Environment.Exit(0)
    End Sub
End Module
