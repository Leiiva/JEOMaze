Imports System.Threading
Public Class Form2
    Dim lTokens As List(Of Tokens)
    Dim lPasos As New List(Of Coordenada)
    Dim lObstaculos As New List(Of Obstaculos)
    'Variable que almacena la posición de la casilla en x
    Public personajex As Integer = Module1.x0personaje
    'Variable que almacena la posición de la casilla en y
    Public personajey As Integer = Module1.y0personaje
    'Variable que almacena la posición del tesoro en x
    Public tesorox As Integer = Module1.x0tesoro
    'Variable que almacena la posición del tesoro en y
    Public tesoroy As Integer = Module1.y0tesoro
    'Variable que almacena la dimension en x del mapa
    Public dimensionx As Integer = Module1.dimensionx
    'variable que almacena la dimensión en y del mapa
    Public dimensiony As Integer = Module1.dimensiony

    Public obstaculos(dimensionx, dimensiony) As Integer


    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DoubleBuffered = True ' Seteamos doble buffer para que las imagenes no parpadeen al refrescar el Form
        Me.Size = New System.Drawing.Size(51 * dimensionx, 54.5 * dimensiony) ' Cambiamos el tamaño para que quepa la imagen de fondo que mide 500 x 500
        'Llenar la matriz de 0
        For i = 0 To obstaculos.GetUpperBound(0)
            For j = 0 To obstaculos.GetUpperBound(1)
                obstaculos(i, j) = 0
            Next
        Next
        'Llenar la matriz de 1
        lObstaculos = Module1.lObstaculos
        For d As Integer = 0 To lObstaculos.Count - 1 Step 1
            obstaculos(Val(lObstaculos(d).getX), Val(lObstaculos(d).getY)) = Val(lObstaculos(d).getObs)
        Next

    End Sub
    Private Sub Form2_Paint(sender As Object, pe As PaintEventArgs) Handles _
   MyBase.Paint
        'Cada vez que llamemos al método refresh del Form, se ejecutará este método, que es el del evento paint
        'Definimos un objeto Graphics que se usará para dibujar sobre el formulario
        Dim g As Graphics = pe.Graphics

        'Dibujamos el fondo
        Dim imagen As New Bitmap("obstaculos.jpg")
        imagen.SetResolution(51 * dimensionx, 54.5 * dimensiony)
        g.DrawImage(imagen, 0, 0)

        
        'Dibujamos los obstaculos
        imagen = New Bitmap("obstaculos.jpg")
        For i = 0 To obstaculos.GetUpperBound(0)
            For j = 0 To obstaculos.GetUpperBound(1)
                If (obstaculos(i, j) = 1) Then
                    g.DrawImage(imagen, 50 * i, 50 * j) 'Lo multiplico por 50 porque la imagen obstaculo es de 50*50
                End If
            Next
        Next
        'Dibujamos el tesoro
        imagen = New Bitmap("tesoro.jpg")
        g.DrawImage(imagen, tesorox * 50, tesoroy * 50)
        'Dibujamos la casilla según el lugar en el que esté
        imagen = New Bitmap("personaje.jpg")
        g.DrawImage(imagen, 50 * personajex, 50 * personajey) 'Lo multiplico por 50 porque la imagen casilla es de 50*50
    End Sub
    Private Sub animacion(lista As List(Of Coordenada))
        For Each c In lista
            'Actualizamos la posición en x segun la coordenada del elemento de la lista
            personajex = c.getX
            'Actualizamos la posición en y segun la coordenada del elemento de la lista
            personajey = c.getY
            'Esperamos 200 milisegundos
            Thread.Sleep(Module1.intervalo)
            'Refrescamos el formulario para que se repinte el fondo y la casilla en sus nuevas coordenadas
            Me.Refresh()
        Next
        If (personajex = tesorox And personajey = tesoroy) Then
            MsgBox("EXITOOO")
        Else
            MsgBox("FRACASOOO")
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        lPasos = Module1.lCoordenadas
        imprimirpasos(lPasos)
        imprimirobstaculos(lObstaculos)
        animacion(lPasos)
    End Sub
    Public Sub imprimirpasos(ByVal l As List(Of Coordenada))
        For Each t As Coordenada In l
            Console.WriteLine(t.getX & "->" & t.getY)
        Next
    End Sub
    Public Sub imprimirobstaculos(ByVal l As List(Of Obstaculos))
        For Each t As Obstaculos In l
            Console.WriteLine(t.getX & "->" & t.getY)
        Next
    End Sub
End Class