Imports System.IO

Public Class Archivo
    Public archivo_actual As String

    Public Function Abrir() As String
        Dim linea As String = ""
        Dim ventana As New OpenFileDialog
        ventana.Filter = "Archivo LBT(*.LBT)|*.LBT"
        ventana.Multiselect = False
        ventana.CheckFileExists = False
        ventana.Title = "Selecciona un Archivo"
        ventana.ShowDialog()
        If ventana.FileName <> "" Then
            archivo_actual = ventana.FileName
            Dim Lector As New StreamReader(archivo_actual)
            While Lector.Peek() >= 0
                linea += Lector.ReadLine() + vbCrLf
            End While
            Lector.Close()
        End If
        Return linea
    End Function
    Public Sub Guardar_Como(ByVal texto() As String)
        Dim guardarcomo As New SaveFileDialog
        With guardarcomo
            .Title = "Guardar Como..."
            .Filter = "Archivo LBT(*.LBT)|*.LBT"
            .ShowDialog()
            If .FileName <> "" Then
                archivo_actual = .FileName
                Dim escritor As New StreamWriter(archivo_actual)
                For Each linea As String In texto
                    escritor.WriteLine(linea)
                Next
                escritor.Close()
            End If
        End With
    End Sub
    Public Sub Guardar(ByVal texto() As String)
        If (File.Exists(archivo_actual)) Then
            Dim escritor As New StreamWriter(archivo_actual)
            For Each linea As String In texto
                escritor.WriteLine(linea)
            Next
            escritor.Close()
        Else
            Guardar_Como(texto)
        End If
    End Sub
End Class
