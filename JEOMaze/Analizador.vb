Imports JEOMaze.Tokens

Public Class Analizador
    'Variable que representa la lista de tokens
    Public salida As List(Of Tokens)
    'Variable que representa el estado actual
    Private estado As Integer
    'Variable que representa el lexema que actualmente se esta acumulando
    Private auxLex As String
    'Variable que representa la fila que se esta analizando
    Private fila As Integer
    'Variable que representa la columna en la que se encuentra el caracter
    Private columna As Integer
    'Variable para calcular el tamaño del token y colorear
    Dim tamaño As Integer = 0
    'Variable para verificar errores
    Dim Errorlexico As Boolean = False
    Public Function getError() As Boolean
        Return Errorlexico
    End Function
    Private Sub addToken(ByRef tipo As Tipo, ByVal fila As String, ByVal columna As String, ByVal color As Color)
        salida.Add(New Tokens(tipo, auxLex, fila, columna, color, tamaño))
        auxLex = ""
        estado = 0
        tamaño = 0
    End Sub
    Public Sub imprimirLista(ByVal l As List(Of Tokens))
        For Each t As Tokens In l
            Console.WriteLine(t.getTexto() & "<-->" & t.getTipoString() & "<-->" & t.getFila() & "<-->" & t.getColumna())
        Next
    End Sub

    Public Function escnear(ByVal entrada As String) As List(Of Tokens)
        'Le agrego caracter de fin de cadena porque hay lexemas que aceptan con 
        'el primer caracter del siguiente lexema y si este caracter no existe entonces
        'perdemos el lexema

        entrada = entrada + "#"
        salida = New List(Of Tokens)
        estado = 0
        auxLex = ""
        fila = 1
        columna = 0
        tamaño = 0
        Dim c As Char
        'Ciclo que recorre de izquierda a derecha caracter por caracter la cadena de entrada
        For i As Integer = 0 To entrada.Length - 1 Step 1
            c = entrada.Chars(i)
            'Select en el que cada caso representa cada uno de los estados del conjunto de estados
            Select Case estado
                Case 0
                    tamaño += 1
                    'Para cada caso (o estado) hay un if elseif elseif ... else que representan el conjunto de transiciones que 
                    'salen de dicho estado, por ejemplo, estando en el estado 0 si el caracter reconocido es un dígito entonces, 
                    'pasamos al estado 1 y acumulamos el caracter reconocido en auxLex, que es el auxiliar de lexemas.
                    If (c = "[") Then
                        auxLex += c
                        columna = columna + 1
                        addToken(Tipo.CORCHETE_ABIERTO, fila, columna, Color.SkyBlue)

                    ElseIf (c = "]") Then
                        auxLex += c
                        columna = columna + 1
                        addToken(Tipo.CORCHETE_CERRADO, fila, columna, Color.SkyBlue)

                    ElseIf (c = ":") Then
                        auxLex += c
                        columna = columna + 1
                        addToken(Tipo.DOS_PUNTOS, fila, columna, Color.Orange)

                    ElseIf (c = "{") Then
                        auxLex += c
                        columna = columna + 1
                        addToken(Tipo.LLAVE_ABIERTA, fila, columna, Color.Pink)

                    ElseIf (c = "}") Then
                        auxLex += c
                        columna = columna + 1
                        addToken(Tipo.LLAVE_CERRADA, fila, columna, Color.Pink)

                    ElseIf (c = ";") Then
                        auxLex += c
                        columna = columna + 1
                        addToken(Tipo.PUNTO_Y_COMA, fila, columna, Color.Purple)

                    ElseIf (c = ",") Then
                        auxLex += c
                        columna = columna + 1
                        addToken(Tipo.COMA, fila, columna, Color.Orange)

                    ElseIf (c = ".") Then
                        auxLex += c
                        columna = columna + 1
                        addToken(Tipo.PUNTO, fila, columna, Color.OrangeRed)

                    ElseIf (c = "(") Then
                        auxLex += c
                        columna = columna + 1
                        addToken(Tipo.PARENTESIS_ABIERTO, fila, columna, Color.Green)

                    ElseIf (c = ")") Then
                        auxLex += c
                        columna = columna + 1
                        addToken(Tipo.PARENTESIS_CERRADO, fila, columna, Color.Green)

                    ElseIf (c = "=") Then
                        auxLex += c
                        columna = columna + 1
                        addToken(Tipo.IGUAL, fila, columna, Color.Orange)

                    ElseIf (c = "+") Then
                        auxLex += c
                        columna = columna + 1
                        addToken(Tipo.MAS, fila, columna, Color.Orange)

                    ElseIf (c = "-") Then
                        auxLex += c
                        columna = columna + 1
                        addToken(Tipo.MENOS, fila, columna, Color.Orange)

                    ElseIf (c = "/") Then
                        auxLex += c
                        columna = columna + 1
                        addToken(Tipo.DIVIDIR, fila, columna, Color.Orange)

                    ElseIf (c = "*") Then
                        auxLex += c
                        columna = columna + 1
                        addToken(Tipo.POR, fila, columna, Color.Orange)

                    ElseIf (Char.IsDigit(c)) Then
                        estado = 4
                        auxLex += c

                    ElseIf Char.IsLetter(c) Then
                        auxLex += c
                        estado = 1
                        'MsgBox("Hay letra")

                    ElseIf c = vbCr Or c = vbCrLf Or c = vbLf Then
                        fila = fila + 1
                        columna = 0

                    ElseIf (c = " " Or c = vbTab Or Char.IsWhiteSpace(c)) Then
                        estado = 0

                    Else
                        If (c = "#" And i = entrada.Length() - 1) Then
                            'Hemos concluido el análisis léxico.
                            Console.WriteLine("Hemos concluido el análisis léxico satisfactoriamente")
                        Else
                            columna = columna + 1
                            auxLex += c
                            addToken(Tipo.ERR, fila, columna, Color.Black)
                            Console.WriteLine("Error léxico con: " + c)
                            Errorlexico = True
                            estado = 0
                        End If
                    End If

                Case 1
                    tamaño += 1
                    If (Char.IsLetter(c)) Then
                        estado = 1
                        auxLex += c
                        'MsgBox("hay mas letras")
                    ElseIf (c = "_") Then
                        estado = 2
                        auxLex += c
                        ' MsgBox("entre en _")
                    ElseIf (Char.IsDigit(c)) Then
                        estado = 3
                        auxLex += c
                    Else
                        If auxLex = "Principal" Then
                            tamaño = tamaño - 1
                            columna = columna + 1
                            addToken(Tipo.PRINCIPAL, fila, columna, Drawing.Color.Blue)
                            i -= 1
                        ElseIf auxLex = "Laberinto" Then
                            tamaño = tamaño - 1
                            columna = columna + 1
                            addToken(Tipo.BLOQUE_LABERINTO, fila, columna, Drawing.Color.Blue)
                            i -= 1
                        ElseIf auxLex = "Dimensiones" Then
                            tamaño = tamaño - 1
                            columna = columna + 1
                            addToken(Tipo.DIMENSIONES, fila, columna, Drawing.Color.Blue)
                            i -= 1
                        ElseIf auxLex = "Ubicación_personaje" Then
                            tamaño = tamaño - 1
                            columna = columna + 1
                            addToken(Tipo.UBICACION_PERSONAJE, fila, columna, Drawing.Color.Blue)
                            i -= 1
                        ElseIf auxLex = "Ubicación_tesoro" Then
                            tamaño = tamaño - 1
                            columna = columna + 1
                            addToken(Tipo.UBICACION_TESORO, fila, columna, Drawing.Color.Blue)
                            i -= 1
                        ElseIf auxLex = "Obstáculos" Then
                            tamaño = tamaño - 1
                            columna = columna + 1
                            addToken(Tipo.OBSTACULOS, fila, columna, Drawing.Color.Blue)
                            i -= 1
                        ElseIf auxLex = "Casilla" Then
                            tamaño = tamaño - 1
                            columna = columna + 1
                            addToken(Tipo.CASILLA, fila, columna, Drawing.Color.Blue)
                            i -= 1
                        ElseIf auxLex = "Rango_Casillas" Then
                            tamaño = tamaño - 1
                            columna = columna + 1
                            addToken(Tipo.RANGO_CASILLA, fila, columna, Drawing.Color.Blue)
                            i -= 1
                        ElseIf auxLex = "Ruta" Then
                            tamaño = tamaño - 1
                            columna = columna + 1
                            addToken(Tipo.RUTA, fila, columna, Drawing.Color.Blue)
                            i -= 1
                        ElseIf auxLex = "Intervalo" Then
                            tamaño = tamaño - 1
                            columna = columna + 1
                            addToken(Tipo.INTERVALO, fila, columna, Drawing.Color.Blue)
                            i -= 1
                        ElseIf auxLex = "Paso" Then
                            tamaño = tamaño - 1
                            columna = columna + 1
                            addToken(Tipo.PASO, fila, columna, Drawing.Color.Blue)
                            i -= 1
                        ElseIf auxLex = "Caminata" Then
                            tamaño = tamaño - 1
                            columna = columna + 1
                            addToken(Tipo.CAMINATA, fila, columna, Drawing.Color.Blue)
                            i -= 1
                        ElseIf auxLex = "Variable" Then
                            tamaño = tamaño - 1
                            columna = columna + 1
                            addToken(Tipo.VARIABLE, fila, columna, Drawing.Color.Blue)
                            i -= 1
                        Else
                            tamaño = tamaño - 1
                            columna = columna + 1
                            ' columna = columna - auxLex.Length + 1
                            addToken(Tipo.IDENTIFICADOR, fila, columna, Color.Blue)
                            i -= 1
                        End If
                    End If

                Case 2
                    tamaño += 1
                    If (Char.IsLetter(c) Or c = "_") Then
                        estado = 2
                        auxLex += c
                    Else
                        If auxLex = "Principal" Then
                            tamaño = tamaño - 1
                            columna = columna + 1
                            addToken(Tipo.PRINCIPAL, fila, columna, Drawing.Color.Blue)
                            i -= 1
                        ElseIf auxLex = "Laberinto" Then
                            tamaño = tamaño - 1
                            columna = columna + 1
                            addToken(Tipo.BLOQUE_LABERINTO, fila, columna, Drawing.Color.Blue)
                            i -= 1
                        ElseIf auxLex = "Dimensiones" Then
                            tamaño = tamaño - 1
                            columna = columna + 1
                            addToken(Tipo.DIMENSIONES, fila, columna, Drawing.Color.Blue)
                            i -= 1
                        ElseIf auxLex = "Ubicación_personaje" Then
                            tamaño = tamaño - 1
                            columna = columna + 1
                            addToken(Tipo.UBICACION_PERSONAJE, fila, columna, Drawing.Color.Blue)
                            i -= 1
                        ElseIf auxLex = "Ubicación_tesoro" Then
                            tamaño = tamaño - 1
                            columna = columna + 1
                            addToken(Tipo.UBICACION_TESORO, fila, columna, Drawing.Color.Blue)
                            i -= 1
                        ElseIf auxLex = "Obstáculos" Then
                            tamaño = tamaño - 1
                            columna = columna + 1
                            addToken(Tipo.OBSTACULOS, fila, columna, Drawing.Color.Blue)
                            i -= 1
                        ElseIf auxLex = "Casilla" Then
                            tamaño = tamaño - 1
                            columna = columna + 1
                            addToken(Tipo.CASILLA, fila, columna, Drawing.Color.Blue)
                            i -= 1
                        ElseIf auxLex = "Rango_Casillas" Then
                            tamaño = tamaño - 1
                            columna = columna + 1
                            addToken(Tipo.RANGO_CASILLA, fila, columna, Drawing.Color.Blue)
                            i -= 1
                        ElseIf auxLex = "Ruta" Then
                            tamaño = tamaño - 1
                            columna = columna + 1
                            addToken(Tipo.RUTA, fila, columna, Drawing.Color.Blue)
                            i -= 1
                        ElseIf auxLex = "Intervalo" Then
                            tamaño = tamaño - 1
                            columna = columna + 1
                            addToken(Tipo.INTERVALO, fila, columna, Drawing.Color.Blue)
                            i -= 1
                        ElseIf auxLex = "Paso" Then
                            tamaño = tamaño - 1
                            columna = columna + 1
                            addToken(Tipo.PASO, fila, columna, Drawing.Color.Blue)
                            i -= 1
                        ElseIf auxLex = "Caminata" Then
                            tamaño = tamaño - 1
                            columna = columna + 1
                            addToken(Tipo.CAMINATA, fila, columna, Drawing.Color.Blue)
                            i -= 1
                        ElseIf auxLex = "Variable" Then
                            tamaño = tamaño - 1
                            columna = columna + 1
                            addToken(Tipo.VARIABLE, fila, columna, Drawing.Color.Blue)
                            i -= 1
                        Else
                            tamaño = tamaño - 1
                            columna = columna + 1
                            ' columna = columna - auxLex.Length + 1
                            addToken(Tipo.IDENTIFICADOR, fila, columna, Color.Blue)
                            i -= 1
                        End If
                    End If
                Case 3
                    tamaño += 1
                    If (Char.IsLetter(c) Or Char.IsDigit(c)) Then
                        ' columna = columna + 1
                        estado = 3
                        auxLex += c
                    Else

                        tamaño = tamaño - 1
                        columna = columna + 1
                        ' columna = columna - auxLex.Length + 1
                        addToken(Tipo.IDENTIFICADOR, fila, columna, Color.Blue)
                        i -= 1
                    End If
                Case 4
                    tamaño += 1
                    If (Char.IsDigit(c)) Then
                        columna = columna + 1
                        estado = 4
                        auxLex += c
                    Else
                        columna = columna + 1
                        columna = columna - auxLex.Length + 1
                        tamaño = tamaño - 1
                        addToken(Tipo.NUMERO_ENTERO, fila, columna, Color.Red)
                        i -= 1
                    End If
            End Select
        Next
        Return salida
    End Function
End Class
