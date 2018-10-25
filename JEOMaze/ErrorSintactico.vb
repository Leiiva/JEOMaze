Public Class ErrorSintactico
    Public tipo As String
    Public numero As String

    Public Sub New(c_x As String, c_y As String)
        tipo = c_x
        numero = c_y
    End Sub
    Public Function getTipo() As String
        Return tipo
    End Function
    Public Function getNumero() As String
        Return numero
    End Function
End Class
