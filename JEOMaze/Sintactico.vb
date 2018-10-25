Imports JEOMaze.Tokens

Public Class Sintactico
    Dim numPreanalisis As Integer
    'Variable que representa el caracter de anticipación que posee el parser para realizar el análisis
    Dim preanalisis As Tokens
    Dim postanalisis As Tokens
    'Lista de Tokens que el parser recibe del analizador léxico
    Dim listaTokens As List(Of Tokens)
    Public lerrores As List(Of ErrorSintactico) = New List(Of ErrorSintactico)
    Dim listavariable As List(Of variables) = New List(Of variables)
    Public Sub parsear(l As List(Of Tokens))
        listaTokens = l
        preanalisis = listaTokens.Item(0)
        numPreanalisis = 0
        S()
    End Sub
    Private Sub S()
        match(Tipo.CORCHETE_ABIERTO)
        match(Tipo.PRINCIPAL)
        match(Tipo.CORCHETE_CERRADO)
        match(Tipo.DOS_PUNTOS)
        match(Tipo.LLAVE_ABIERTA)
        BloqueLab()
        BloqueRuta()
        match(Tipo.LLAVE_CERRADA)

    End Sub
    Private Sub BloqueLab()
        match(Tipo.CORCHETE_ABIERTO)
        match(Tipo.BLOQUE_LABERINTO)
        match(Tipo.CORCHETE_CERRADO)
        match(Tipo.DOS_PUNTOS)
        match(Tipo.LLAVE_ABIERTA)
        InsLaberinto()
        match(Tipo.LLAVE_CERRADA)
    End Sub
    Private Sub BloqueRuta()
        match(Tipo.CORCHETE_ABIERTO)
        match(Tipo.RUTA)
        match(Tipo.CORCHETE_CERRADO)
        match(Tipo.DOS_PUNTOS)
        match(Tipo.LLAVE_ABIERTA)
        InsRuta()
        match(Tipo.LLAVE_CERRADA)

    End Sub
    Private Sub InsRuta()
        postanalisis = listaTokens.Item(numPreanalisis + 1)
        If preanalisis.getTipo = Tipo.CORCHETE_ABIERTO And postanalisis.getTipo = Tipo.PASO Then
            'MsgBox("entro a paso")
            paso()
            InsRuta()
        ElseIf preanalisis.getTipo = Tipo.CORCHETE_ABIERTO And postanalisis.getTipo = Tipo.INTERVALO Then
            'MsgBox("entro a intervalo")
            intervalo()
            InsRuta()
        ElseIf preanalisis.getTipo = Tipo.CORCHETE_ABIERTO And postanalisis.getTipo = Tipo.CAMINATA Then
            'MsgBox("entro a caminata")
            caminata()
            InsRuta()
        ElseIf preanalisis.getTipo = Tipo.CORCHETE_ABIERTO And postanalisis.getTipo = Tipo.VARIABLE Then
            'MsgBox("entro a variable")
            variable()
            InsRuta()
        ElseIf preanalisis.getTipo = Tipo.IDENTIFICADOR Then
            'MsgBox("entro a ide")
            id()
            InsRuta()
        End If

    End Sub
    Private Sub intervalo()
        match(Tipo.CORCHETE_ABIERTO)
        match(tipo.intervalo)
        match(Tipo.CORCHETE_CERRADO)
        match(Tipo.DOS_PUNTOS)
        match(Tipo.PARENTESIS_ABIERTO)
        val_ar()
        match(Tipo.PARENTESIS_CERRADO)
        match(Tipo.PUNTO_Y_COMA)
    End Sub
    Private Sub paso()
        match(Tipo.CORCHETE_ABIERTO)
        match(tipo.paso)
        match(Tipo.CORCHETE_CERRADO)
        match(Tipo.DOS_PUNTOS)
        match(Tipo.PARENTESIS_ABIERTO)
        val_ar()
        match(tipo.Coma)
        val_ar()
        match(Tipo.PARENTESIS_CERRADO)
        match(Tipo.PUNTO_Y_COMA)


    End Sub
    Private Sub caminata()
        match(Tipo.CORCHETE_ABIERTO)
        match(tipo.caminata)
        match(Tipo.CORCHETE_CERRADO)
        match(Tipo.DOS_PUNTOS)
        match(Tipo.PARENTESIS_ABIERTO)
        val_ar()
        rang_c()
        match(Tipo.PARENTESIS_CERRADO)
        match(Tipo.PUNTO_Y_COMA)

    End Sub
    Private Sub rang_c()
        If preanalisis.getTipo = tipo.punto Then
            match(tipo.punto)
            match(tipo.punto)
            val_ar()
            match(tipo.Coma)
            val_ar()
        Else
            match(tipo.Coma)
            val_ar()
            match(tipo.punto)
            match(tipo.punto)
            val_ar()
        End If
    End Sub
    Private Sub InsLaberinto()
        postanalisis = listaTokens.Item(numPreanalisis + 1)
        If preanalisis.getTipo = Tipo.CORCHETE_ABIERTO And postanalisis.getTipo = Tipo.DIMENSIONES Then
            ' MsgBox("Entro a dimensiones")
            Dimensiones()
            InsLaberinto()
        ElseIf preanalisis.getTipo = Tipo.CORCHETE_ABIERTO And postanalisis.getTipo = Tipo.UBICACION_PERSONAJE Then
            UbicacionPer()
            InsLaberinto()
        ElseIf preanalisis.getTipo = Tipo.CORCHETE_ABIERTO And postanalisis.getTipo = Tipo.UBICACION_TESORO Then
            UbicacionTes()
            InsLaberinto()
        ElseIf preanalisis.getTipo = Tipo.CORCHETE_ABIERTO And postanalisis.getTipo = Tipo.OBSTACULOS Then
            ' MsgBox("Entro a obstaculos")
            Obstaculos()
            InsLaberinto()

        End If
    End Sub

    Private Sub Dimensiones()
        match(Tipo.CORCHETE_ABIERTO)
        match(tipo.Dimensiones)
        match(Tipo.CORCHETE_CERRADO)
        match(Tipo.DOS_PUNTOS)
        match(Tipo.PARENTESIS_ABIERTO)
        match(Tipo.NUMERO_ENTERO)
        match(tipo.Coma)
        match(Tipo.NUMERO_ENTERO)
        match(Tipo.PARENTESIS_CERRADO)
        match(Tipo.PUNTO_Y_COMA)
    End Sub
    Private Sub UbicacionPer()
        match(Tipo.CORCHETE_ABIERTO)
        match(Tipo.UBICACION_PERSONAJE)
        match(Tipo.CORCHETE_CERRADO)
        match(Tipo.DOS_PUNTOS)
        match(Tipo.PARENTESIS_ABIERTO)
        match(Tipo.NUMERO_ENTERO)
        match(tipo.Coma)
        match(Tipo.NUMERO_ENTERO)
        match(Tipo.PARENTESIS_CERRADO)
        match(Tipo.PUNTO_Y_COMA)
    End Sub
    Private Sub UbicacionTes()
        match(Tipo.CORCHETE_ABIERTO)
        match(Tipo.UBICACION_TESORO)
        match(Tipo.CORCHETE_CERRADO)
        match(Tipo.DOS_PUNTOS)
        match(Tipo.PARENTESIS_ABIERTO)
        match(Tipo.NUMERO_ENTERO)
        match(tipo.Coma)
        match(Tipo.NUMERO_ENTERO)
        match(Tipo.PARENTESIS_CERRADO)
        match(Tipo.PUNTO_Y_COMA)
    End Sub

    Private Sub Obstaculos()
        match(Tipo.CORCHETE_ABIERTO)
        match(tipo.Obstaculos)
        match(Tipo.CORCHETE_CERRADO)
        match(Tipo.DOS_PUNTOS)
        match(Tipo.LLAVE_ABIERTA)
        InsObs()
        match(Tipo.LLAVE_CERRADA)
    End Sub
    Private Sub InsObs()
        postanalisis = listaTokens.Item(numPreanalisis + 1)
        If preanalisis.getTipo = Tipo.CORCHETE_ABIERTO And postanalisis.getTipo = Tipo.CASILLA Then
            'MsgBox("Entro a casilla")
            Casilla()
            InsObs()
        ElseIf preanalisis.getTipo = Tipo.CORCHETE_ABIERTO And postanalisis.getTipo = Tipo.RANGO_CASILLA Then
            'MsgBox("Entro a rangocasillas")
            Rango()
            InsObs()
        ElseIf preanalisis.getTipo = Tipo.CORCHETE_ABIERTO And postanalisis.getTipo = Tipo.VARIABLE Then
            'MsgBox("Entro a variables")
            variable()
            InsObs()
        ElseIf preanalisis.getTipo = Tipo.IDENTIFICADOR Then
            'MsgBox("Entro a identificador")
            id()
            InsObs()
        End If
    End Sub
    Private Sub val_ar()
        If preanalisis.getTipo = tipo.identificador Then
            match(tipo.identificador)
        Else
            match(Tipo.NUMERO_ENTERO)

        End If
    End Sub
    Private Sub Casilla()
        match(Tipo.CORCHETE_ABIERTO)
        match(tipo.casilla)
        match(Tipo.CORCHETE_CERRADO)
        match(Tipo.DOS_PUNTOS)
        match(Tipo.PARENTESIS_ABIERTO)
        val_ar()
        match(tipo.Coma)
        val_ar()
        match(Tipo.PARENTESIS_CERRADO)
        match(Tipo.PUNTO_Y_COMA)
    End Sub
    Private Sub Rango()
        match(Tipo.CORCHETE_ABIERTO)
        match(Tipo.RANGO_CASILLA)
        match(Tipo.CORCHETE_CERRADO)
        match(Tipo.DOS_PUNTOS)
        match(Tipo.PARENTESIS_ABIERTO)
        val_ar()
        Rang()
        match(Tipo.PARENTESIS_CERRADO)
        match(Tipo.PUNTO_Y_COMA)
    End Sub
    Private Sub Rang()
        If preanalisis.getTipo = tipo.punto Then
            match(tipo.punto)
            match(tipo.punto)
            val_ar()
            match(tipo.Coma)
            Rang_d()
        Else
            match(tipo.Coma)
            val_ar()
            match(tipo.punto)
            match(tipo.punto)
            val_ar()
        End If
    End Sub
    Private Sub Rang_d()
        postanalisis = listaTokens.Item(numPreanalisis + 1)
        If postanalisis.getTipo = tipo.punto Then
            val_ar()
            match(tipo.punto)
            match(tipo.punto)
            val_ar()
        Else
            val_ar()
        End If
    End Sub


    Private Sub variable()
        match(Tipo.CORCHETE_ABIERTO)
        match(Tipo.VARIABLE)
        match(Tipo.CORCHETE_CERRADO)
        match(Tipo.DOS_PUNTOS)
        lista_varI()
        match(Tipo.PUNTO_Y_COMA)
    End Sub
    Private Sub lista_varI()
        match(tipo.identificador)
        lista_var()
    End Sub
    Private Sub lista_var()
        If preanalisis.getTipo = tipo.Coma Then
            match(tipo.Coma)
            lista_varI()
        Else
        End If
    End Sub
    Private Sub id()
        match(tipo.identificador)
        match(Tipo.DOS_PUNTOS)
        match(Tipo.IGUAL)
        Valor()
        match(Tipo.PUNTO_Y_COMA)
    End Sub
    Private Sub Valor()
        postanalisis = listaTokens.Item(numPreanalisis + 1)
        If preanalisis.getTipo = Tipo.NUMERO_ENTERO Or preanalisis.getTipo = Tipo.IDENTIFICADOR And postanalisis.getTipo = Tipo.PUNTO_Y_COMA Then
            val_ar()
        Else
            exp_ar()
        End If
    End Sub
    Private Sub exp_ar()
        val_ar()
        If preanalisis.getTipo = Tipo.MAS Then
            match(Tipo.MAS)
        ElseIf preanalisis.getTipo = Tipo.MENOS Then
            match(Tipo.MENOS)
        ElseIf preanalisis.getTipo = Tipo.POR Then
            match(Tipo.POR)
        ElseIf preanalisis.getTipo = Tipo.DIVIDIR Then
            match(Tipo.DIVIDIR)
        Else
            match(Tipo.POTENCIA)
        End If
        val_ar()
    End Sub

    Private Sub match(p As Tipo)
        If Not p = preanalisis.getTipo Then
            MsgBox("Se esperaba " + getTipoParaError(p).ToString + " " + numPreanalisis.ToString)
            lerrores.Add(New ErrorSintactico(getTipoParaError(p).ToString, numPreanalisis.ToString))
        End If
        If Not preanalisis.getTipo = Tipo.ULTIMO Then
            numPreanalisis += 1
            preanalisis = listaTokens.Item(numPreanalisis)
        End If
    End Sub
    Public Function obtenererrores() As List(Of ErrorSintactico)
        Return lerrores
    End Function
    Public Function getTipoParaError(p As Tipo) As String
        Select Case p
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
End Class