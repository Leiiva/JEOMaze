Public Class Variables
    Private Nombre As String
    Private Valor As String

    Public Sub New(ByVal Nombre As String, ByVal Valor As String)
        Me.Nombre = Nombre
        Me.Valor = Valor
    End Sub

    Public Function getNombre() As String
        Return Nombre
    End Function

    Public Function getValor() As String
        Return Valor
    End Function

    Public Sub setValor(ByVal val As String)
        Me.Valor = val
    End Sub
    Public Sub setNombre(ByVal nom As String)
        Me.Nombre = nom
    End Sub
End Class
