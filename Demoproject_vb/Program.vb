Imports System
Imports System.Threading.Tasks

Public Module Program
    Public Sub Main()
        MainAsync().GetAwaiter().GetResult()
    End Sub

    Public Async Function MainAsync() As Task
        Dim db As New Database()
        Await MainLoop(db)
    End Function

    Public Async Function MainLoop(db As Database) As Task
        While True
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
                    ' Add person
                    Try
                        Console.WriteLine("Enter Name:")
                        Dim name As String = Console.ReadLine()
                        If String.IsNullOrEmpty(name) Then Throw New Exception("Name cannot be empty.")

                        Console.WriteLine("Enter Age:")
                        Dim age As Integer = Integer.Parse(Console.ReadLine())
                        If age <= 0 Then Throw New Exception("Age must be a positive number.")

                        Console.WriteLine("Enter City:")
                        Dim city As String = Console.ReadLine()
                        If String.IsNullOrEmpty(city) Then Throw New Exception("City cannot be empty.")

                        Dim person As New Person(name, age, city)
                        db.AddPerson(person)
                    Catch ex As Exception
                        Console.WriteLine($"Error: {ex.Message}")
                    End Try

                Case "2"
                    db.ListPeople()

                Case "3"
                    ' Filter people with age range
                    Try
                        Console.WriteLine("Enter minimum age:")
                        Dim minAge As Integer = Integer.Parse(Console.ReadLine())

                        Console.WriteLine("Enter maximum age:")
                        Dim maxAge As Integer = Integer.Parse(Console.ReadLine())

                        db.FilterPeopleByAgeRange(minAge, maxAge)
                    Catch ex As Exception
                        Console.WriteLine($"Error: {ex.Message}")
                    End Try

                Case "4"
                    db.SortPeopleByAge()

                Case "5"
                    db.CalculateStatistics()

                Case "6"
                    ' Search by name initial
                    Try
                        Console.WriteLine("Enter initial character:")
                        Dim initial As Char = Console.ReadLine()(0)
                        db.SearchByInitial(initial)
                    Catch ex As Exception
                        Console.WriteLine($"Error: {ex.Message}")
                    End Try

                Case "7"
                    ' Speichern der Daten in eine Datei
                    Try
                        Console.WriteLine("Enter the file name to save data:")
                        Dim fileName As String = Console.ReadLine()
                        Await db.SaveToFileAsync(fileName) ' Hier wird der Dateiname übergeben
                    Catch ex As Exception
                        Console.WriteLine($"Error: {ex.Message}")
                    End Try

                Case "8"
                    ' Laden der Daten aus einer Datei
                    Try
                        Console.WriteLine("Enter the file name to load data:")
                        Dim fileName As String = Console.ReadLine()
                        Await db.LoadFromFileAsync(fileName) ' Hier wird der Dateiname übergeben
                    Catch ex As Exception
                        Console.WriteLine($"Error: {ex.Message}")
                    End Try

                Case "9"
                    ExitProgram()

                Case Else
                    Console.WriteLine("Invalid choice, try again.")
            End Select
        End While
    End Function

    Public Sub ExitProgram()
        Console.WriteLine("Exiting...")
        Environment.Exit(0)
    End Sub
End Module
