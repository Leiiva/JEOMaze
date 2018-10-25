Public Class Tokens
    'Declarar variables
    Private TipoToken As Tipo
    Private Texto As String
    Private fila As Integer
    Private columna As Integer
    Public Tamaño As Integer
    Public colorcito As Color
    'Enumerar tipo de toke
    Enum Tipo
        BLOQUE_LABERINTO
        BLOQUE_RUTA
        CAMINATA
        CASILLA
        COMA
        CORCHETE_ABIERTO
        CORCHETE_CERRADO
        DIMENSIONES
        DIVIDIR
        DOS_PUNTOS
        ERR
        IDENTIFICADOR
        IGUAL
        INTERVALO
        LLAVE_ABIERTA
        LLAVE_CERRADA
        MAS
        MENOS
        NUMERO_ENTERO
        OBSTACULOS
        PARENTESIS_ABIERTO
        PARENTESIS_CERRADO
        PASO
        POTENCIA
        POR
        PRINCIPAL
        PUNTO
        PUNTO_Y_COMA
        RANGO_CASILLA
        RUTA
        UBICACION_PERSONAJE
        UBICACION_TESORO
        ULTIMO
        VARIABLE
    End Enum
    Public Sub New(ByVal tipo As Tipo, ByVal auxiliar As String, ByVal filasa As Integer, ByVal columnasa As Integer, ByVal coloraso As Color, ByVal tamañaso As Integer)
        Me.TipoToken = tipo
        Me.Texto = auxiliar
        Me.fila = filasa
        Me.columna = columnasa
        Me.colorcito = coloraso
        Me.Tamaño = tamañaso
    End Sub
    Public Function getTipoString() As String
        Select Case TipoToken
            Case Tipo.BLOQUE_LABERINTO
                Return "Bloque de Laberinto"
            Case Tipo.BLOQUE_RUTA
                Return "Bloque de Ruta"
            Case Tipo.CAMINATA
                Return "Caminata"
            Case Tipo.CASILLA
                Return "Casilla"
            Case Tipo.COMA
                Return "Coma"
            Case Tipo.CORCHETE_ABIERTO
                Return "Corchete Abierto"
            Case Tipo.CORCHETE_CERRADO
                Return "Corchete Cerrado"
            Case Tipo.DIMENSIONES
                Return "Dimensiones"
            Case Tipo.DIVIDIR
                Return "Signo Dividir"
            Case Tipo.DOS_PUNTOS
                Return "Dos puntos"
            Case Tipo.ERR
                Return "Error Caracter Desconocido"
            Case Tipo.IDENTIFICADOR
                Return "Identificador"
            Case Tipo.IGUAL
                Return "Igual"
            Case Tipo.INTERVALO
                Return "Intervalo"
            Case Tipo.LLAVE_ABIERTA
                Return "Llave Abierta"
            Case Tipo.LLAVE_CERRADA
                Return "Llave Cerrada"
            Case Tipo.MAS
                Return "Mas"
            Case Tipo.MENOS
                Return "Menos"
            Case Tipo.NUMERO_ENTERO
                Return "Numero Entero"
            Case Tipo.OBSTACULOS
                Return "Obstaculos"
            Case Tipo.PARENTESIS_ABIERTO
                Return "Parentesis Abierto"
            Case Tipo.PARENTESIS_CERRADO
                Return "Parentesis Cerrado"
            Case Tipo.PASO
                Return "Paso"
            Case Tipo.POTENCIA
                Return "Signo de Potencia"
            Case Tipo.POR
                Return "Signo Por"
            Case Tipo.PRINCIPAL
                Return "Principal"
            Case Tipo.PUNTO
                Return "Punto"
            Case Tipo.PUNTO_Y_COMA
                Return "Punto y Coma"
            Case Tipo.RANGO_CASILLA
                Return "Rango de Casilla"
            Case Tipo.RUTA
                Return "Ruta"
            Case Tipo.UBICACION_PERSONAJE
                Return "Ubicación Personaje"
            Case Tipo.UBICACION_TESORO
                Return "Ubicación Tesoro"
            Case Tipo.ULTIMO
                Return "Ultimo"
            Case Tipo.VARIABLE
                Return "Variable"
            Case Else
                Return "Error Caracter Desconocido"
        End Select
    End Function
    Public Function getTexto() As String
        Return Texto
    End Function
    Public Function getTipo() As Tipo
        Return TipoToken
    End Function
    Public Function getColor() As Color
        Return colorcito
    End Function
    Public Function getTamaño() As String
        Return Tamaño
    End Function
    Public Function getFila() As Integer
        Return fila
    End Function
    Public Function getColumna() As Integer
        Return columna
    End Function
End Class
