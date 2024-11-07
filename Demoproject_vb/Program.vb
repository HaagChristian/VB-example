Imports System
Imports System.Threading.Tasks

' Entry point of the program
Public Module Program
    Public Sub Main()
        ' Start the asynchronous main loop and wait for its completion
        MainAsync().GetAwaiter().GetResult()
    End Sub

    ' Main async method to initialize the database and start the main loop
    Private Async Function MainAsync() As Task
        Dim db As New Database() ' Initialize the database
        Await MainLoop(db) ' Start the main loop for user interaction
    End Function

    ' Main loop to interact with the user and provide menu options
    Private Async Function MainLoop(db As Database) As Task
        While True
            ' Display menu options to the user
            ' vbCrLf -> line break
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

            ' Read user choice
            Dim choice As String = Console.ReadLine()

            ' Perform action based on user choice
            Select Case choice
                Case "1"
                    ' Option to add a new person
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

                        ' Create a new person object and add to the database
                        Dim person As New Person(name, age, city)
                        db.AddPerson(person)
                    Catch ex As Exception
                        Console.WriteLine($"Error: {ex.Message}")
                    End Try

                Case "2"
                    ' Option to list all people in the database
                    db.ListPeople()

                Case "3"
                    ' Option to filter people within an age range
                    Try
                        Console.WriteLine("Enter minimum age:")
                        Dim minAge As Integer = Integer.Parse(Console.ReadLine())

                        Console.WriteLine("Enter maximum age:")
                        Dim maxAge As Integer = Integer.Parse(Console.ReadLine())

                        ' Filter people by the specified age range
                        db.FilterPeopleByAgeRange(minAge, maxAge)
                    Catch ex As Exception
                        Console.WriteLine($"Error: {ex.Message}")
                    End Try

                Case "4"
                    ' Option to sort people by age in ascending order
                    db.SortPeopleByAge()

                Case "5"
                    ' Option to calculate and display age statistics
                    db.CalculateStatistics()

                Case "6"
                    ' Option to search people by the initial letter of their name
                    Try
                        Console.WriteLine("Enter initial character:")
                        Dim initial As Char = Console.ReadLine()(0)
                        db.SearchByInitial(initial)
                    Catch ex As Exception
                        Console.WriteLine($"Error: {ex.Message}")
                    End Try

                Case "7"
                    ' Option to save data to a file asynchronously
                    Try
                        Console.WriteLine("Enter the file name to save data:")
                        Dim fileName As String = Console.ReadLine()
                        Await db.SaveToFileAsync(fileName) ' Save with specified file name
                    Catch ex As Exception
                        Console.WriteLine($"Error: {ex.Message}")
                    End Try

                Case "8"
                    ' Option to load data from a file asynchronously
                    Try
                        Console.WriteLine("Enter the file name to load data:")
                        Dim fileName As String = Console.ReadLine()
                        Await db.LoadFromFileAsync(fileName) ' Load with specified file name
                    Catch ex As Exception
                        Console.WriteLine($"Error: {ex.Message}")
                    End Try

                Case "9"
                    ' Exit the program
                    ExitProgram()

                Case Else
                    ' Invalid option handling
                    Console.WriteLine("Invalid choice, try again.")
            End Select
        End While
    End Function

    ' Method to exit the program
    Private Sub ExitProgram()
        Console.WriteLine("Exiting...")
        Environment.Exit(0) ' Terminates the application
    End Sub
End Module
