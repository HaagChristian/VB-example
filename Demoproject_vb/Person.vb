' Person.vb

' Represents a person with a name, age, and city
Public Class Person
    ' Public properties to store the name, age, and city of the person
    Public Property Name As String
    Public Property Age As Integer
    Public Property City As String

    ' Constructor to initialize a new Person object with specified values
    ' Parameters:
    '   name - The name of the person
    '   age - The age of the person (must be a positive integer)
    '   city - The city where the person resides
    Public Sub New(name As String, age As Integer, city As String)
        Me.Name = name   ' Sets the Name property
        Me.Age = age     ' Sets the Age property
        Me.City = city   ' Sets the City property
    End Sub
End Class
