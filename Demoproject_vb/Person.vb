' Person.vb
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
