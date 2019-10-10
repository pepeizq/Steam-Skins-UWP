Imports Windows.Storage
Imports Windows.Storage.AccessCache

Module Datos

    Public Async Sub Generar()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim tb As TextBox = pagina.FindName("tbDatosResultados")
        tb.Text = String.Empty

        Dim lista As List(Of EditorColores) = SetsColores()
        Dim colores As EditorColores = lista(0)

        Dim resultado1 As String = String.Empty
        Dim resultado2 As String = String.Empty

        For Each color In colores.Colores
            Dim html As String = Await Decompiladores.HttpClient(New Uri("https://www.colorhexa.com/" + color.Replace("#", Nothing)))

            If Not html = Nothing Then
                If html.Contains("<div id=" + ChrW(34) + "split-complementary") Then
                    Dim temp, temp2 As String
                    Dim int, int2 As Integer

                    int = html.IndexOf("<div id=" + ChrW(34) + "split-complementary")
                    temp = html.Remove(0, int)

                    int = temp.IndexOf("#")
                    temp = temp.Remove(0, int)

                    int2 = temp.IndexOf(";")
                    temp2 = temp.Remove(int2, temp.Length - int2)

                    resultado1 = resultado1 + ChrW(34) + temp2.Trim + ChrW(34) + ", "

                    Dim temp3, temp4 As String
                    Dim int3, int4 As Integer

                    int3 = html.IndexOf("<strong>Split Complementary Color</strong>")
                    temp3 = html.Remove(int3, html.Length - int3)

                    int3 = temp3.LastIndexOf("#")
                    temp3 = temp3.Remove(0, int3)

                    int4 = temp3.IndexOf("</code>")
                    temp4 = temp3.Remove(int4, temp3.Length - int4)

                    resultado2 = resultado2 + ChrW(34) + temp4.Trim + ChrW(34) + ", "
                End If
            End If
        Next

        tb.Text = resultado1 + Environment.NewLine + Environment.NewLine + resultado2

    End Sub

    Public Async Sub Generar2()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim tb As TextBox = pagina.FindName("tbDatosResultados")
        tb.Text = String.Empty

        Dim resultado1 As String = String.Empty

        Dim carpeta As StorageFolder = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteam")

        If Not carpeta Is Nothing Then
            Dim carpetaUI As StorageFolder = Await StorageFolder.GetFolderFromPathAsync(carpeta.Path + "\steamui\css")

            If Not carpetaUI Is Nothing Then
                Dim ficheroCSS1 As StorageFile = Await StorageFile.GetFileFromPathAsync(carpetaUI.Path + "\libraryroot.css")

                If Not ficheroCSS1 Is Nothing Then
                    Dim contenido As String = Await FileIO.ReadTextAsync(ficheroCSS1)

                    Dim i As Integer = 0
                    While i < 10000
                        If contenido.Contains("#") Then
                            Dim temp, temp2 As String
                            Dim int, int2 As Integer

                            int = contenido.IndexOf("#")
                            temp = contenido.Remove(0, int + 1)

                            contenido = temp

                            If temp.IndexOf(";") = 6 Then
                                int2 = temp.IndexOf(";")
                                temp2 = temp.Remove(int2, temp.Length - int2)

                                If Not resultado1.Contains(temp2.Trim) Then
                                    resultado1 = resultado1 + ChrW(34) + "#" + temp2.Trim + ChrW(34) + ", "
                                End If
                            End If
                        End If
                        i += 1
                    End While
                End If
            End If
        End If

        tb.Text = resultado1

    End Sub

End Module
