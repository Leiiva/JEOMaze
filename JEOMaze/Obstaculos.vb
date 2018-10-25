Public Class Obstaculos
    Public coordenadax As Integer
    Public coordenaday As Integer
    Public obs As Integer

    Public Sub New(ByVal x As Integer, ByVal y As Integer, ByVal o As Integer)
        coordenadax = x
        coordenaday = y
        obs = o
    End Sub

    Public Function getX() As Integer
        Return coordenadax
    End Function
    Public Function getY() As Integer
        Return coordenaday
    End Function
    Public Function getObs() As Integer
        Return obs
    End Function
End Class
