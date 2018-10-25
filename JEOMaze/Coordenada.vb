Public Class Coordenada
    Public x As Integer
    Public y As Integer

    Public Sub New(c_x As Integer, c_y As Integer)
        x = c_x
        y = c_y
    End Sub

    Public Function getX() As String
        Return x
    End Function
    Public Function getY() As String
        Return y
    End Function
End Class
