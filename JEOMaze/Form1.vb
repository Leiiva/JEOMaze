Imports System.Threading
Imports System.IO
Imports JEOMaze.Tokens

Public Class Form1
    Dim archivo As New Archivo()
    Dim analizador As New Analizador()
    Dim sint As New Sintactico()
    Dim lTokens As List(Of Tokens)
    Dim lerrores As List(Of ErrorSintactico) = New List(Of ErrorSintactico)
    Dim rango As New List(Of Integer)
    Dim lVariable As List(Of Variables) = New List(Of Variables)
    Public lCoordenadas As List(Of Coordenada) = New List(Of Coordenada)
    Public lObstaculos As List(Of Obstaculos) = New List(Of Obstaculos)
    Dim puntero, puntero2 As Integer
    Dim auxcount As Integer
    Dim nombrevariable As String
    Dim valorvariable As Integer
    Dim suma As Boolean = False
    Dim resta As Boolean = False
    Dim multiplicacion As Boolean = False
    Dim division As Boolean = False
    Dim total As Integer = 0
    Private Sub AbrirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AbrirToolStripMenuItem.Click
        Entrada.Text = archivo.Abrir()
    End Sub

    Private Sub GuardarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GuardarToolStripMenuItem.Click
        archivo.Guardar(Entrada.Lines)
    End Sub

    Private Sub GuardarComoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GuardarComoToolStripMenuItem.Click
        archivo.Guardar_Como(Entrada.Lines)
    End Sub

    Private Sub SalirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalirToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub AcercaDeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AcercaDeToolStripMenuItem.Click
        Dim acerca As String
        acerca = "Nombre: Alan Giovanni Guzmán Toledo" + vbCr + "Carnet: 201314733" + vbCr + "Curso: Lenguajes Formales de Programación" + vbCr + "Sección: A-"
        MessageBox.Show(acerca, "Acerca De...", MessageBoxButtons.OK)
    End Sub


    Public Sub coloreareditor(ByVal li As List(Of Tokens))
        puntero = 0
        puntero2 = 0
        For Each t As Tokens In li
            puntero2 += t.getTamaño
            Entrada.SelectionStart = puntero
            Entrada.SelectionLength = puntero2
            Entrada.SelectionColor = t.getColor
            puntero = puntero2
        Next
    End Sub

    Private Sub AnalisisToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AnalisisToolStripMenuItem.Click
        If (Entrada.Text <> "") Then
            lTokens = analizador.escnear(Entrada.Text)
            coloreareditor(lTokens)
            analizador.imprimirLista(lTokens)



            Dim mis_documentos As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            Dim archivo As String = "Tokens"
            Dim archivo2 As String = "Errores"
            Dim archivoHTML As String = mis_documentos & "\LFP_Proyecto2\" & archivo & ".html"
            Dim errorHTML As String = mis_documentos & "\LFP_Proyecto2\" & archivo2 & ".html"
            Dim escritor As StreamWriter
            Dim info As FileInfo = New FileInfo(archivoHTML)
            Dim info2 As FileInfo = New FileInfo(errorHTML)
            Dim tab As String = ""
            Dim conteyo As Integer = 0
            Dim tablaerrores As String = ""

            MsgBox("Listado de Tokens en HTML Creado exitosamente")
            For i As Integer = 0 To lTokens.Count - 1 Step 1
                tab = tab & "<Tr><Td>" & i + 1 & "<Td>" & lTokens(i).getTexto & "<Td>" & lTokens(i).getTipoString & "<Td>" & lTokens(i).getFila.ToString & "<Td>" & lTokens(i).getColumna.ToString & "" & vbNewLine
            Next
            'File.Delete(archivoHTML)
            'escritor = File.AppendText(archivoHTML)
            'escritor.WriteLine("<html>")
            'escritor.WriteLine("<meta charset=""UTF-8"">")
            'escritor.WriteLine("<head>")
            'escritor.WriteLine("<title>[LFP] Analizador Lexico </title>")
            'escritor.WriteLine("</head>")

            'escritor.WriteLine("<body bgcolor=""#2E2EFE""><font face=""Arial Black"">")
            'escritor.WriteLine("<center>")
            'escritor.WriteLine("<h1><font face=""Algerian"">Listado de Tokens</font></h1>")
            'escritor.WriteLine("</center>")
            'escritor.WriteLine("<h2>")
            'escritor.WriteLine("<center>")
            'escritor.WriteLine("<Table Border>")
            'escritor.WriteLine("<Tr>")
            'escritor.WriteLine("<Td> # token </td>")
            'escritor.WriteLine("<Td> Lexema </Td>")
            'escritor.WriteLine("<Td> Token </Td>")
            'escritor.WriteLine("<Td> Fila </Td>")
            'escritor.WriteLine("<Td> Columna </Td>")
            'escritor.WriteLine("</Tr>")
            'escritor.WriteLine(tab)
            'escritor.WriteLine("</Table")

            'escritor.WriteLine("</center")
            'escritor.WriteLine("</h2>")
            'escritor.WriteLine("</font></body>")
            'escritor.WriteLine("</html>")
            'escritor.Flush()
            'escritor.Close()

            If (analizador.getError() = False) Then
                tablaerrores = tablaerrores & "<Tr><Td>" & "------" & "<Td>" & "------" & "<Td>" & "------" & "<Td>" & "------" & "<Td>" & "------" & "" & vbNewLine
            Else
                MsgBox("El codigo ingresado contiene errores")
                For j As Integer = 0 To lTokens.Count - 1 Step 1
                    If (lTokens(j).getTipoString() = "Error Caracter Desconocido") Then
                        tablaerrores = tablaerrores & "<Tr><Td>" & conteyo + 1 & "<Td>" & lTokens(j).getTexto & "<Td>" & "Caracter Desconocido" & "<Td>" & lTokens(j).getFila.ToString & "<Td>" & lTokens(j).getColumna.ToString & "" & vbNewLine
                        conteyo = conteyo + 1
                    End If
                Next
            End If


            'File.Delete(errorHTML)
            'escritor = File.AppendText(errorHTML)
            'escritor.WriteLine("<html>")
            'escritor.WriteLine("<meta charset=""UTF-8"">")
            'escritor.WriteLine("<head>")
            'escritor.WriteLine("<title> [LFP] Errores Lexicos </title>")
            'escritor.WriteLine("</head>")

            'escritor.WriteLine("<body bgcolor=""#DF3A01""><font face=""Arial Black"">")
            'escritor.WriteLine("<center>")
            'escritor.WriteLine("<h1><font face=""Algerian"">Listado de Errores </font></h1>")
            'escritor.WriteLine("</center>")
            'escritor.WriteLine("<h2>")
            'escritor.WriteLine("<center>")
            'escritor.WriteLine("<Table Border>")
            'escritor.WriteLine("<Tr>")
            'escritor.WriteLine("<Td> # error </td>")
            'escritor.WriteLine("<Td> Error </Td>")
            'escritor.WriteLine("<Td> Descripción </Td>")
            'escritor.WriteLine("<Td> Fila </Td>")
            'escritor.WriteLine("<Td> Columna </Td>")
            'escritor.WriteLine("</Tr>")
            'escritor.WriteLine(tablaerrores)
            'escritor.WriteLine("</Table>")

            'escritor.WriteLine("</center>")
            'escritor.WriteLine("</h2>")
            'escritor.WriteLine("</font></body>")
            'escritor.WriteLine("</html>")
            'escritor.Flush()
            'escritor.Close()

        Else
                MsgBox("El campo esta vacio")
            End If

    End Sub

    Private Sub EmpezarToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles EmpezarToolStripMenuItem1.Click
        lVariable.Clear()

        For i As Integer = 0 To lTokens.Count - 1 Step 1

            'Agregar Variables
            If (lTokens(i).getTipo = Tokens.Tipo.VARIABLE) Then
                auxcount = i
                While (Not lTokens(auxcount).getTipo = Tokens.Tipo.PUNTO_Y_COMA)
                    If (lTokens(auxcount).getTipo = Tokens.Tipo.IDENTIFICADOR) Then
                        nombrevariable = lTokens(auxcount).getTexto
                        lVariable.Add(New Variables(nombrevariable, "0"))
                    End If
                    auxcount = auxcount + 1
                End While
            End If
            Dim valor As Integer
            'Agregar Valor a las Variables
            If (lTokens(i).getTipo = Tokens.Tipo.IDENTIFICADOR) Then
                If (lTokens(i + 1).getTipo = Tokens.Tipo.DOS_PUNTOS) Then
                    If (lTokens(i + 2).getTipo = Tokens.Tipo.IGUAL) Then
                        If (lTokens(i + 3).getTipo = Tokens.Tipo.NUMERO_ENTERO) Then
                            If (lTokens(i + 4).getTipo = Tokens.Tipo.PUNTO_Y_COMA) Then
                                For v As Integer = 0 To lVariable.Count - 1 Step 1
                                    If (lVariable(v).getNombre.Equals(lTokens(i).getTexto.ToString)) Then
                                        lVariable(v).setValor(lTokens(i + 3).getTexto.ToString)
                                    End If
                                Next
                                'SI HAY SIGNO
                            ElseIf (lTokens(i + 4).getTipo = Tokens.Tipo.MAS Or lTokens(i + 4).getTipo = Tokens.Tipo.MENOS Or lTokens(i + 4).getTipo = Tokens.Tipo.POR Or lTokens(i + 4).getTipo = Tokens.Tipo.DIVIDIR) Then
                                If (lTokens(i + 5).getTipo = Tipo.NUMERO_ENTERO) Then
                                    For v As Integer = 0 To lVariable.Count - 1 Step 1
                                        If (lVariable(v).getNombre.Equals(lTokens(i).getTexto.ToString)) Then
                                            lVariable(v).setValor(operar(Val(lTokens(i + 3).getTexto), lTokens(i + 4), Val(lTokens(i + 5).getTexto)).ToString)
                                        End If
                                    Next
                                ElseIf (lTokens(i + 5).getTipo = Tipo.IDENTIFICADOR) Then
                                    For v As Integer = 0 To lVariable.Count - 1 Step 1
                                        If (lVariable(v).getNombre.Equals(lTokens(i).getTexto.ToString)) Then
                                            lVariable(v).setValor(operar(Val(lTokens(i + 3).getTexto), lTokens(i + 4), Val(buscarvariable(lTokens(i + 5)))).ToString)
                                        End If
                                    Next
                                End If
                            End If
                        ElseIf (lTokens(i + 3).getTipo = Tokens.Tipo.IDENTIFICADOR) Then
                            If (lTokens(i + 4).getTipo = Tokens.Tipo.PUNTO_Y_COMA) Then
                                For w As Integer = 0 To lVariable.Count - 1 Step 1
                                    If (lVariable(w).getNombre.Equals(lTokens(i + 3).getTexto)) Then
                                        valor = Val(lVariable(w).getValor)
                                    End If
                                Next
                                For v As Integer = 0 To lVariable.Count - 1 Step 1
                                    If (lVariable(v).getNombre.Equals(lTokens(i).getTexto.ToString)) Then
                                        lVariable(v).setValor(valor)
                                    End If
                                Next
                                'SI HAY SIGNO
                            ElseIf (lTokens(i + 4).getTipo = Tokens.Tipo.MAS Or lTokens(i + 4).getTipo = Tokens.Tipo.MENOS Or lTokens(i + 4).getTipo = Tokens.Tipo.POR Or lTokens(i + 4).getTipo = Tokens.Tipo.DIVIDIR) Then
                                If (lTokens(i + 5).getTipo = Tipo.NUMERO_ENTERO) Then
                                    For v As Integer = 0 To lVariable.Count - 1 Step 1
                                        If (lVariable(v).getNombre.Equals(lTokens(i).getTexto.ToString)) Then
                                            lVariable(v).setValor(operar(Val(buscarvariable(lTokens(i + 3))), lTokens(i + 4), Val(lTokens(i + 5).getTexto)).ToString)
                                        End If
                                    Next
                                ElseIf (lTokens(i + 5).getTipo = Tipo.IDENTIFICADOR) Then
                                    For v As Integer = 0 To lVariable.Count - 1 Step 1
                                        If (lVariable(v).getNombre.Equals(lTokens(i).getTexto.ToString)) Then
                                            lVariable(v).setValor(operar(Val(buscarvariable(lTokens(i + 3))), lTokens(i + 4), Val(buscarvariable(lTokens(i + 5)))).ToString)
                                        End If
                                    Next
                                End If
                            End If

                        End If
                    End If
                    End If
                End If

            Dim x As Integer = 0
            Dim y As Integer = 0
            'Verificar Paso
            If (lTokens(i).getTipo = Tokens.Tipo.PASO) Then
                If (lTokens(i + 1).getTipo = Tokens.Tipo.CORCHETE_CERRADO) Then
                    If (lTokens(i + 2).getTipo = Tokens.Tipo.DOS_PUNTOS) Then
                        If (lTokens(i + 3).getTipo = Tokens.Tipo.PARENTESIS_ABIERTO) Then
                            'numero,numero
                            If (lTokens(i + 4).getTipo = Tokens.Tipo.NUMERO_ENTERO And lTokens(i + 6).getTipo = Tokens.Tipo.NUMERO_ENTERO) Then
                                lCoordenadas.Add(New Coordenada(Val(lTokens(i + 4).getTexto), Val(lTokens(i + 6).getTexto)))
                                'numero,variable
                            ElseIf (lTokens(i + 4).getTipo = Tokens.Tipo.NUMERO_ENTERO And lTokens(i + 6).getTipo = Tokens.Tipo.IDENTIFICADOR) Then
                                For v As Integer = 0 To lVariable.Count - 1 Step 1
                                    If (lVariable(v).getNombre.Equals(lTokens(i + 6).getTexto)) Then
                                        y = Val(lVariable(v).getValor)
                                    End If
                                Next
                                lCoordenadas.Add(New Coordenada(Val(lTokens(i + 4).getTexto), y))
                                'variable,numero
                            ElseIf (lTokens(i + 4).getTipo = Tokens.Tipo.IDENTIFICADOR And lTokens(i + 6).getTipo = Tokens.Tipo.NUMERO_ENTERO) Then
                                For v As Integer = 0 To lVariable.Count - 1 Step 1
                                    If (lVariable(v).getNombre.Equals(lTokens(i + 4).getTexto)) Then
                                        x = Val(lVariable(v).getValor)
                                    End If
                                Next
                                lCoordenadas.Add(New Coordenada(x, Val(lTokens(i + 6).getTexto)))
                                'variable,variable
                            ElseIf (lTokens(i + 4).getTipo = Tokens.Tipo.IDENTIFICADOR And lTokens(i + 6).getTipo = Tokens.Tipo.IDENTIFICADOR) Then
                                For v As Integer = 0 To lVariable.Count - 1 Step 1
                                    If (lVariable(v).getNombre.Equals(lTokens(i + 4).getTexto)) Then
                                        x = Val(lVariable(v).getValor)
                                    End If
                                Next
                                For w As Integer = 0 To lVariable.Count - 1 Step 1
                                    If (lVariable(w).getNombre.Equals(lTokens(i + 6).getTexto)) Then
                                        y = Val(lVariable(w).getValor)
                                    End If
                                Next
                                lCoordenadas.Add(New Coordenada(x, y))
                            End If
                        Else
                            MsgBox("Error de sintaxis en [Paso]:();")
                        End If
                    End If
                End If
            End If

            'Verificar Intervalo ]:(1000);
            If (lTokens(i).getTipo = Tokens.Tipo.INTERVALO) Then
                If (lTokens(i + 1).getTipo = Tokens.Tipo.CORCHETE_CERRADO) Then
                    If (lTokens(i + 2).getTipo = Tokens.Tipo.DOS_PUNTOS) Then
                        If (lTokens(i + 3).getTipo = Tokens.Tipo.PARENTESIS_ABIERTO) Then
                            If (lTokens(i + 4).getTipo = Tokens.Tipo.NUMERO_ENTERO) Then
                                If (lTokens(i + 5).getTipo = Tokens.Tipo.PARENTESIS_CERRADO) Then
                                    If (lTokens(i + 6).getTipo = Tokens.Tipo.PUNTO_Y_COMA) Then
                                        Module1.intervalo = Val(lTokens(i + 4).getTexto)
                                    Else
                                        MsgBox("Error de sintaxis en [Intervalo]:();")
                                    End If
                                Else
                                    MsgBox("Error de sintaxis en [Intervalo]:();")
                                End If
                            Else
                                MsgBox("Error de sintaxis en [Intervalo]:();")
                            End If
                        Else
                            MsgBox("Error de sintaxis en [Intervalo]:();")
                        End If
                    Else
                        MsgBox("Error de sintaxis en [Intervalo]:();")
                    End If
                Else
                    MsgBox("Error de sintaxis en [Intervalo]:();")
                End If
            End If
            Dim pasito As Integer
            'Verificar Caminata
            If (lTokens(i).getTipo = Tokens.Tipo.CAMINATA) Then
                If (lTokens(i + 1).getTipo = Tokens.Tipo.CORCHETE_CERRADO) Then
                    If (lTokens(i + 2).getTipo = Tokens.Tipo.DOS_PUNTOS) Then
                        If (lTokens(i + 3).getTipo = Tokens.Tipo.PARENTESIS_ABIERTO) Then
                            'numero, numero..numero
                            If (lTokens(i + 4).getTipo = Tokens.Tipo.NUMERO_ENTERO And lTokens(i + 5).getTipo = Tokens.Tipo.COMA And lTokens(i + 6).getTipo = Tokens.Tipo.NUMERO_ENTERO And lTokens(i + 9).getTipo = Tokens.Tipo.NUMERO_ENTERO) Then
                                If (Val(lTokens(i + 6).getTexto) < Val(lTokens(i + 9).getTexto)) Then
                                    pasito = 1
                                Else
                                    pasito = -1
                                End If
                                For c As Integer = Val(lTokens(i + 6).getTexto) To Val(lTokens(i + 9).getTexto) Step pasito
                                    lCoordenadas.Add(New Coordenada(Val(lTokens(i + 4).getTexto), c))
                                Next
                                'variable,numero..numero
                            ElseIf (lTokens(i + 4).getTipo = Tokens.Tipo.IDENTIFICADOR And lTokens(i + 5).getTipo = Tokens.Tipo.COMA And lTokens(i + 6).getTipo = Tokens.Tipo.NUMERO_ENTERO And lTokens(i + 9).getTipo = Tokens.Tipo.NUMERO_ENTERO) Then
                                For v As Integer = 0 To lVariable.Count - 1 Step 1
                                    If (lVariable(v).getNombre.Equals(lTokens(i + 4).getTexto)) Then
                                        x = Val(lVariable(v).getValor)
                                    End If
                                Next
                                If (Val(lTokens(i + 6).getTexto) < Val(lTokens(i + 9).getTexto)) Then
                                    pasito = 1
                                Else
                                    pasito = -1
                                End If
                                For c As Integer = Val(lTokens(i + 6).getTexto) To Val(lTokens(i + 9).getTexto) Step pasito
                                    lCoordenadas.Add(New Coordenada(x, c))
                                Next
                                'numero..numero,variable
                            ElseIf (lTokens(i + 4).getTipo = Tokens.Tipo.NUMERO_ENTERO And lTokens(i + 7).getTipo = Tokens.Tipo.NUMERO_ENTERO And lTokens(i + 8).getTipo = Tokens.Tipo.COMA And lTokens(i + 9).getTipo = Tokens.Tipo.IDENTIFICADOR) Then
                                For v As Integer = 0 To lVariable.Count - 1 Step 1
                                    If (lVariable(v).getNombre.Equals(lTokens(i + 9).getTexto)) Then
                                        y = Val(lVariable(v).getValor)
                                    End If
                                Next
                                If (Val(lTokens(i + 4).getTexto) < Val(lTokens(i + 7).getTexto)) Then
                                    pasito = 1
                                Else
                                    pasito = -1
                                End If
                                For c As Integer = Val(lTokens(i + 4).getTexto) To Val(lTokens(i + 7).getTexto) Step pasito
                                    lCoordenadas.Add(New Coordenada(c, y))
                                Next
                                'numero..numero,numero
                            ElseIf (lTokens(i + 4).getTipo = Tokens.Tipo.NUMERO_ENTERO And lTokens(i + 7).getTipo = Tokens.Tipo.NUMERO_ENTERO And lTokens(i + 8).getTipo = Tokens.Tipo.COMA And lTokens(i + 9).getTipo = Tokens.Tipo.NUMERO_ENTERO) Then
                                If (Val(lTokens(i + 4).getTexto) < Val(lTokens(i + 7).getTexto)) Then
                                    pasito = 1
                                Else
                                    pasito = -1
                                End If
                                For c As Integer = Val(lTokens(i + 4).getTexto) To Val(lTokens(i + 7).getTexto) Step pasito
                                    lCoordenadas.Add(New Coordenada(c, Val(lTokens(i + 9).getTexto)))
                                Next
                                'numero,variable..variable
                            ElseIf (lTokens(i + 4).getTipo = Tokens.Tipo.NUMERO_ENTERO And lTokens(i + 5).getTipo = Tokens.Tipo.COMA And lTokens(i + 6).getTipo = Tokens.Tipo.IDENTIFICADOR And lTokens(i + 9).getTipo = Tokens.Tipo.VARIABLE) Then
                                If (Val(buscarvariable(lTokens(i + 6))) < Val(buscarvariable(lTokens(i + 9)))) Then
                                    pasito = 1
                                Else
                                    pasito = -1
                                End If
                                For c As Integer = Val(buscarvariable(lTokens(i + 6))) To Val(buscarvariable(lTokens(i + 9))) Step pasito
                                    lCoordenadas.Add(New Coordenada(Val(lTokens(i + 4).getTexto), c))
                                Next
                                'numero,variable..numero
                            ElseIf (lTokens(i + 4).getTipo = Tokens.Tipo.NUMERO_ENTERO And lTokens(i + 5).getTipo = Tokens.Tipo.COMA And lTokens(i + 6).getTipo = Tokens.Tipo.IDENTIFICADOR And lTokens(i + 9).getTipo = Tokens.Tipo.NUMERO_ENTERO) Then
                                If (Val(buscarvariable(lTokens(i + 6))) < Val(lTokens(i + 9).getTexto)) Then
                                    pasito = 1
                                Else
                                    pasito = -1
                                End If
                                For c As Integer = Val(buscarvariable(lTokens(i + 6))) To Val(lTokens(i + 9).getTexto) Step pasito
                                    lCoordenadas.Add(New Coordenada(Val(lTokens(i + 4).getTexto), c))
                                Next
                                'numero..variable,variable
                            ElseIf (lTokens(i + 4).getTipo = Tokens.Tipo.NUMERO_ENTERO And lTokens(i + 7).getTipo = Tokens.Tipo.IDENTIFICADOR And lTokens(i + 8).getTipo = Tokens.Tipo.COMA And lTokens(i + 9).getTipo = Tokens.Tipo.IDENTIFICADOR) Then
                                If (Val(lTokens(i + 4).getTexto) < Val(buscarvariable(lTokens(i + 7)))) Then
                                    pasito = 1
                                Else
                                    pasito = -1
                                End If
                                For c As Integer = Val(lTokens(i + 4).getTexto) To Val(buscarvariable(lTokens(i + 7))) Step pasito
                                    lCoordenadas.Add(New Coordenada(c, Val(buscarvariable(lTokens(i + 9)))))
                                Next
                                'variable..variable,variable
                            ElseIf (lTokens(i + 4).getTipo = Tokens.Tipo.IDENTIFICADOR And lTokens(i + 7).getTipo = Tokens.Tipo.IDENTIFICADOR And lTokens(i + 8).getTipo = Tokens.Tipo.COMA And lTokens(i + 9).getTipo = Tokens.Tipo.IDENTIFICADOR) Then
                                If (Val(buscarvariable(lTokens(i + 4))) < Val(buscarvariable(lTokens(i + 7)))) Then
                                    pasito = 1
                                Else
                                    pasito = -1
                                End If
                                For c As Integer = Val(buscarvariable(lTokens(i + 4))) To Val(buscarvariable(lTokens(i + 7))) Step pasito
                                    lCoordenadas.Add(New Coordenada(c, Val(buscarvariable(lTokens(i + 9)))))
                                Next
                                'variable,variable..variable
                            ElseIf (lTokens(i + 4).getTipo = Tokens.Tipo.IDENTIFICADOR And lTokens(i + 5).getTipo = Tokens.Tipo.COMA And lTokens(i + 6).getTipo = Tokens.Tipo.IDENTIFICADOR And lTokens(i + 9).getTipo = Tokens.Tipo.IDENTIFICADOR) Then
                                If (Val(buscarvariable(lTokens(i + 6))) < Val(buscarvariable(lTokens(i + 9)))) Then
                                    pasito = 1
                                Else
                                    pasito = -1
                                End If
                                For c As Integer = Val(buscarvariable(lTokens(i + 6))) To Val(buscarvariable(lTokens(i + 9))) Step pasito
                                    lCoordenadas.Add(New Coordenada(Val(buscarvariable(lTokens(i + 4))), c))
                                Next
                                'otras opciones pero que hueva
                            End If
                        End If
                    End If
                End If
            End If

            'Verificar Ubicación Personaje
            If (lTokens(i).getTipo = Tokens.Tipo.UBICACION_PERSONAJE) Then
                If (lTokens(i + 1).getTipo = Tokens.Tipo.CORCHETE_CERRADO) Then
                    If (lTokens(i + 2).getTipo = Tokens.Tipo.DOS_PUNTOS) Then
                        If (lTokens(i + 3).getTipo = Tokens.Tipo.PARENTESIS_ABIERTO) Then
                            If (lTokens(i + 4).getTipo = Tokens.Tipo.NUMERO_ENTERO) Then
                                If (lTokens(i + 5).getTipo = Tokens.Tipo.COMA) Then
                                    If (lTokens(i + 6).getTipo = Tokens.Tipo.NUMERO_ENTERO) Then
                                        If (lTokens(i + 7).getTipo = Tokens.Tipo.PARENTESIS_CERRADO) Then
                                            If (lTokens(i + 8).getTipo = Tokens.Tipo.PUNTO_Y_COMA) Then
                                                Module1.x0personaje = Val(lTokens(i + 4).getTexto)
                                                Module1.y0personaje = Val(lTokens(i + 6).getTexto)
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
            'Verificar Ubicación Tesoro
            If (lTokens(i).getTipo = Tokens.Tipo.UBICACION_TESORO) Then
                If (lTokens(i + 1).getTipo = Tokens.Tipo.CORCHETE_CERRADO) Then
                    If (lTokens(i + 2).getTipo = Tokens.Tipo.DOS_PUNTOS) Then
                        If (lTokens(i + 3).getTipo = Tokens.Tipo.PARENTESIS_ABIERTO) Then
                            If (lTokens(i + 4).getTipo = Tokens.Tipo.NUMERO_ENTERO) Then
                                If (lTokens(i + 5).getTipo = Tokens.Tipo.COMA) Then
                                    If (lTokens(i + 6).getTipo = Tokens.Tipo.NUMERO_ENTERO) Then
                                        If (lTokens(i + 7).getTipo = Tokens.Tipo.PARENTESIS_CERRADO) Then
                                            If (lTokens(i + 8).getTipo = Tokens.Tipo.PUNTO_Y_COMA) Then
                                                Module1.x0tesoro = Val(lTokens(i + 4).getTexto)
                                                Module1.y0tesoro = Val(lTokens(i + 6).getTexto)
                                                Console.WriteLine("LA PUTA MADRE LASKDFJALSKDFJALSDKFJADF " & lTokens(i + 4).getTexto & "Y" & lTokens(i + 6).getTexto)
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If

            'Verificar Dimensiones
            If (lTokens(i).getTipo = Tokens.Tipo.DIMENSIONES) Then
                If (lTokens(i + 1).getTipo = Tokens.Tipo.CORCHETE_CERRADO) Then
                    If (lTokens(i + 2).getTipo = Tokens.Tipo.DOS_PUNTOS) Then
                        If (lTokens(i + 3).getTipo = Tokens.Tipo.PARENTESIS_ABIERTO) Then
                            If (lTokens(i + 4).getTipo = Tokens.Tipo.NUMERO_ENTERO) Then
                                If (lTokens(i + 5).getTipo = Tokens.Tipo.COMA) Then
                                    If (lTokens(i + 6).getTipo = Tokens.Tipo.NUMERO_ENTERO) Then
                                        If (lTokens(i + 7).getTipo = Tokens.Tipo.PARENTESIS_CERRADO) Then
                                            If (lTokens(i + 8).getTipo = Tokens.Tipo.PUNTO_Y_COMA) Then
                                                Module1.dimensionx = Val(lTokens(i + 4).getTexto)
                                                Module1.dimensiony = Val(lTokens(i + 6).getTexto)
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If

            'Verificar Obstaculos CASILLA
            If (lTokens(i).getTipo = Tokens.Tipo.CASILLA) Then
                If (lTokens(i + 1).getTipo = Tokens.Tipo.CORCHETE_CERRADO) Then
                    If (lTokens(i + 2).getTipo = Tokens.Tipo.DOS_PUNTOS) Then
                        If (lTokens(i + 3).getTipo = Tokens.Tipo.PARENTESIS_ABIERTO) Then
                            If (lTokens(i + 4).getTipo = Tokens.Tipo.NUMERO_ENTERO And lTokens(i + 6).getTipo = Tokens.Tipo.NUMERO_ENTERO) Then
                                lObstaculos.Add(New Obstaculos(Val(lTokens(i + 4).getTexto), Val(lTokens(i + 6).getTexto), 1))
                            ElseIf (lTokens(i + 4).getTipo = Tokens.Tipo.NUMERO_ENTERO And lTokens(i + 6).getTipo = Tokens.Tipo.IDENTIFICADOR) Then
                                For v As Integer = 0 To lVariable.Count - 1 Step 1
                                    If (lVariable(v).getNombre.Equals(lTokens(i + 6).getTexto)) Then
                                        y = Val(lVariable(v).getValor)
                                    End If
                                Next
                                lObstaculos.Add(New Obstaculos(Val(lTokens(i + 4).getTexto), y, 1))
                            ElseIf (lTokens(i + 4).getTipo = Tokens.Tipo.IDENTIFICADOR And lTokens(i + 6).getTipo = Tokens.Tipo.NUMERO_ENTERO) Then
                                For v As Integer = 0 To lVariable.Count - 1 Step 1
                                    If (lVariable(v).getNombre.Equals(lTokens(i + 4).getTexto)) Then
                                        x = Val(lVariable(v).getValor)
                                    End If
                                Next
                                lObstaculos.Add(New Obstaculos(x, Val(lTokens(i + 6).getTexto), 1))
                            ElseIf (lTokens(i + 4).getTipo = Tokens.Tipo.IDENTIFICADOR And lTokens(i + 6).getTipo = Tokens.Tipo.IDENTIFICADOR) Then
                                For v As Integer = 0 To lVariable.Count - 1 Step 1
                                    If (lVariable(v).getNombre.Equals(lTokens(i + 4).getTexto)) Then
                                        x = Val(lVariable(v).getValor)
                                    End If
                                Next
                                For w As Integer = 0 To lVariable.Count - 1 Step 1
                                    If (lVariable(w).getNombre.Equals(lTokens(i + 6).getTexto)) Then
                                        y = Val(lVariable(w).getValor)
                                    End If
                                Next
                                lObstaculos.Add(New Obstaculos(x, y, 1))
                            End If
                        End If
                    End If
                End If
            End If

            Dim a As Integer
            Dim b As Integer
            'Verificar Rango_Casillas
            If (lTokens(i).getTipo = Tokens.Tipo.RANGO_CASILLA) Then
                If (lTokens(i + 1).getTipo = Tokens.Tipo.CORCHETE_CERRADO) Then
                    If (lTokens(i + 2).getTipo = Tokens.Tipo.DOS_PUNTOS) Then
                        If (lTokens(i + 3).getTipo = Tokens.Tipo.PARENTESIS_ABIERTO) Then
                            'numero, numero..numero
                            If (lTokens(i + 4).getTipo = Tokens.Tipo.NUMERO_ENTERO And lTokens(i + 5).getTipo = Tokens.Tipo.COMA And lTokens(i + 6).getTipo = Tokens.Tipo.NUMERO_ENTERO And lTokens(i + 9).getTipo = Tokens.Tipo.NUMERO_ENTERO) Then
                                If (Val(lTokens(i + 6).getTexto) < Val(lTokens(i + 9).getTexto)) Then
                                    pasito = 1
                                Else
                                    pasito = -1
                                End If
                                For c As Integer = Val(lTokens(i + 6).getTexto) To Val(lTokens(i + 9).getTexto) Step pasito
                                    lObstaculos.Add(New Obstaculos(Val(lTokens(i + 4).getTexto), c, 1))
                                Next
                                'variable,numero..numero
                            ElseIf (lTokens(i + 4).getTipo = Tokens.Tipo.IDENTIFICADOR And lTokens(i + 5).getTipo = Tokens.Tipo.COMA And lTokens(i + 6).getTipo = Tokens.Tipo.NUMERO_ENTERO And lTokens(i + 9).getTipo = Tokens.Tipo.NUMERO_ENTERO) Then
                                If (Val(lTokens(i + 6).getTexto) < Val(lTokens(i + 9).getTexto)) Then
                                    pasito = 1
                                Else
                                    pasito = -1
                                End If
                                For v As Integer = 0 To lVariable.Count - 1 Step 1
                                    If (lVariable(v).getNombre.Equals(lTokens(i + 4).getTexto)) Then
                                        x = Val(lVariable(v).getValor)
                                    End If
                                Next
                                For c As Integer = Val(lTokens(i + 6).getTexto) To Val(lTokens(i + 9).getTexto) Step pasito
                                    lObstaculos.Add(New Obstaculos(x, c, 1))
                                Next
                                'numero..numero,variable
                            ElseIf (lTokens(i + 4).getTipo = Tokens.Tipo.NUMERO_ENTERO And lTokens(i + 7).getTipo = Tokens.Tipo.NUMERO_ENTERO And lTokens(i + 8).getTipo = Tokens.Tipo.COMA And lTokens(i + 9).getTipo = Tokens.Tipo.IDENTIFICADOR) Then
                                If (Val(lTokens(i + 4).getTexto) < Val(lTokens(i + 7).getTexto)) Then
                                    pasito = 1
                                Else
                                    pasito = -1
                                End If
                                For v As Integer = 0 To lVariable.Count - 1 Step 1
                                    If (lVariable(v).getNombre.Equals(lTokens(i + 9).getTexto)) Then
                                        y = Val(lVariable(v).getValor)
                                    End If
                                Next
                                For c As Integer = Val(lTokens(i + 4).getTexto) To Val(lTokens(i + 7).getTexto) Step pasito
                                    lObstaculos.Add(New Obstaculos(c, y, 1))
                                Next
                                'mas opciones
                                'variable..numero,variable
                            ElseIf (lTokens(i + 4).getTipo = Tokens.Tipo.IDENTIFICADOR And lTokens(i + 7).getTipo = Tokens.Tipo.NUMERO_ENTERO And lTokens(i + 8).getTipo = Tokens.Tipo.COMA And lTokens(i + 9).getTipo = Tokens.Tipo.IDENTIFICADOR) Then
                                If (Val(buscarvariable(lTokens(i + 4))) < Val(lTokens(i + 7).getTexto)) Then
                                    pasito = 1
                                Else
                                    pasito = -1
                                End If
                                For v As Integer = 0 To lVariable.Count - 1 Step 1
                                    If (lVariable(v).getNombre.Equals(lTokens(i + 4).getTexto)) Then
                                        a = Val(lVariable(v).getValor)
                                    End If
                                Next
                                For w As Integer = 0 To lVariable.Count - 1 Step 1
                                    If (lVariable(w).getNombre.Equals(lTokens(i + 9).getTexto)) Then
                                        y = Val(lVariable(w).getValor)
                                    End If
                                Next
                                For c As Integer = a To Val(lTokens(i + 7).getTexto) Step pasito
                                    lObstaculos.Add(New Obstaculos(c, y, 1))
                                Next
                                'variable..numero,numero
                            ElseIf (lTokens(i + 4).getTipo = Tokens.Tipo.IDENTIFICADOR And lTokens(i + 7).getTipo = Tokens.Tipo.NUMERO_ENTERO And lTokens(i + 8).getTipo = Tokens.Tipo.COMA And lTokens(i + 9).getTipo = Tokens.Tipo.NUMERO_ENTERO) Then
                                If (Val(buscarvariable(lTokens(i + 4))) < Val(lTokens(i + 7).getTexto)) Then
                                    pasito = 1
                                Else
                                    pasito = -1
                                End If
                                For v As Integer = 0 To lVariable.Count - 1 Step 1
                                    If (lVariable(v).getNombre.Equals(lTokens(i + 4).getTexto)) Then
                                        a = Val(lVariable(v).getValor)
                                    End If
                                Next
                                For c As Integer = a To Val(lTokens(i + 7).getTexto) Step pasito
                                    lObstaculos.Add(New Obstaculos(c, Val(lTokens(i + 9).getTexto), 1))
                                Next
                                'numero,numero..variable
                            ElseIf (lTokens(i + 4).getTipo = Tokens.Tipo.NUMERO_ENTERO And lTokens(i + 5).getTipo = Tokens.Tipo.COMA And lTokens(i + 6).getTipo = Tokens.Tipo.NUMERO_ENTERO And lTokens(i + 9).getTipo = Tokens.Tipo.IDENTIFICADOR) Then
                                If (Val(lTokens(i + 6).getTexto) < Val(buscarvariable(lTokens(i + 9)))) Then
                                    pasito = 1
                                Else
                                    pasito = -1
                                End If
                                For v As Integer = 0 To lVariable.Count - 1 Step 1
                                    If (lVariable(v).getNombre.Equals(lTokens(i + 9).getTexto)) Then
                                        b = Val(lVariable(v).getValor)
                                    End If
                                Next
                                For c As Integer = Val(lTokens(i + 6).getTexto) To b Step pasito
                                    lObstaculos.Add(New Obstaculos(Val(lTokens(i + 4).getTexto), c, 1))
                                Next

                                'otras opciones pero que hueva
                            End If
                        End If
                    End If
                End If
            End If







        Next
        imprimirvariables(lVariable)
        Module1.lObstaculos = Me.lObstaculos
        Module1.lCoordenadas = Me.lCoordenadas
        Form2.Show()
    End Sub

    Private Sub MostrarLaberintoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MostrarLaberintoToolStripMenuItem.Click
        lTokens.Add(New Tokens(Tokens.Tipo.ULTIMO, "", 0, 0, Drawing.Color.AliceBlue, 0))
        Dim sintactico As Sintactico = New Sintactico
        sintactico.parsear(lTokens)
        lerrores = sintactico.obtenererrores


        Dim mis_documentos As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        Dim archivo2 As String = "Errores sintacticos"
        Dim errorHTML As String = mis_documentos & "\LFP_Proyecto2\" & archivo2 & ".html"
        Dim escritor As StreamWriter
        Dim info2 As FileInfo = New FileInfo(errorHTML)
        Dim tab As String = ""
        Dim tablaerrores As String = ""

        If (lerrores.Count = 0) Then
            MsgBox("No hay errores sintacticos")
            tablaerrores = "<Tr><Td>" & 0 & "<Td>" & "No hay errores" & "<Td>" & "No hay Errores" & "" & vbNewLine
        ElseIf (lerrores.Count > 0) Then
            MsgBox("Lista de errores sintacticos Creados en HTML")
            Dim conteyo As Integer = 0
            For j As Integer = 0 To lerrores.Count - 1 Step 1
                tablaerrores = tablaerrores & "<Tr><Td>" & conteyo + 1 & "<Td>" & "Se esperaba " & lerrores(j).getTipo & "<Td>" & lerrores(j).getNumero & "" & vbNewLine
                conteyo = conteyo + 1

            Next
        End If
        'File.Delete(errorHTML)
        'escritor = File.AppendText(errorHTML)
        'escritor.WriteLine("<html>")
        'escritor.WriteLine("<meta charset=""UTF-8"">")
        'escritor.WriteLine("<head>")
        'escritor.WriteLine("<title> [LFP] Errores Sintacticos </title>")
        'escritor.WriteLine("</head>")

        'escritor.WriteLine("<body bgcolor=""#DF3A01""><font face=""Arial Black"">")
        'escritor.WriteLine("<center>")
        'escritor.WriteLine("<h1><font face=""Algerian"">Listado de Errores </font></h1>")
        'escritor.WriteLine("</center>")
        'escritor.WriteLine("<h2>")
        'escritor.WriteLine("<center>")
        'escritor.WriteLine("<Table Border>")
        'escritor.WriteLine("<Tr>")
        'escritor.WriteLine("<Td> # error </td>")
        'escritor.WriteLine("<Td> Error </Td>")
        'escritor.WriteLine("<Td> Posición </Td>")
        'escritor.WriteLine("</Tr>")
        'escritor.WriteLine(tablaerrores)
        'escritor.WriteLine("</Table>")

        'escritor.WriteLine("</center>")
        'escritor.WriteLine("</h2>")
        'escritor.WriteLine("</font></body>")
        'escritor.WriteLine("</html>")
        'escritor.Flush()
        'escritor.Close()


    End Sub

    Public Sub imprimirvariables(ByVal l As List(Of Variables))
        For Each t As Variables In l
            Console.WriteLine(t.getNombre & "->" & t.getValor)
        Next
    End Sub
    Public Function ObtenerLista() As List(Of Coordenada)
        Return lCoordenadas
    End Function

    Public Function operar(ByVal val1 As Integer, ByVal l As Tokens, ByVal val2 As Integer) As Integer
        Dim resultado As Integer = 0
        If (l.getTipo = Tipo.MAS) Then
            resultado = val1 + val2
            Return resultado
        ElseIf (l.getTipo = Tipo.MENOS) Then
            resultado = val1 - val2
            Return resultado
        ElseIf (l.getTipo = Tipo.POR) Then
            resultado = val1 * val2
            Return resultado
        ElseIf (l.getTipo = Tipo.DIVIDIR) Then
            resultado = val1 / val2
            Return resultado
        Else
            Return 0
        End If
    End Function
    Public Function buscarvariable(ByVal t As Tokens) As Integer
        Dim var As Integer = 0
        For i As Integer = 0 To lVariable.Count - 1 Step 1
            If (lVariable(i).getNombre.Equals(t.getTexto)) Then
                var = Val(lVariable(i).getValor)
            End If
        Next
        Return var
    End Function
End Class
