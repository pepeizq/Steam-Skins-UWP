Imports Microsoft.Toolkit.Uwp.Helpers
Imports Windows.Storage
Imports Windows.Storage.AccessCache
Imports Windows.UI

Module Editor

    Public Sub Cargar()

        Dim lista As List(Of EditorColores) = SetsColores()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim cbEditor As ComboBox = pagina.FindName("cbEditor")

        For Each colores In lista
            cbEditor.Items.Add(colores.Nombre)
        Next

        If ApplicationData.Current.LocalSettings.Values("modo_editor") = Nothing Then
            ApplicationData.Current.LocalSettings.Values("modo_editor") = 0
        End If

        cbEditor.SelectedIndex = ApplicationData.Current.LocalSettings.Values("modo_editor")

    End Sub

    Public Async Sub Cambiar(modo As Integer)

        Dim lista As List(Of EditorColores) = SetsColores()

        Dim coloresNuevo As EditorColores = lista(modo)
        AñadirColoresLista(coloresNuevo)
        Dim coloresViejo As EditorColores = lista(ApplicationData.Current.LocalSettings.Values("modo_editor"))

        Dim carpeta As StorageFolder = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteam")

        If Not carpeta Is Nothing Then
            Dim carpetaUI As StorageFolder = Await StorageFolder.GetFolderFromPathAsync(carpeta.Path + "\steamui\css")

            If Not carpetaUI Is Nothing Then
                Dim ficheroCSS1 As StorageFile = Await StorageFile.GetFileFromPathAsync(carpetaUI.Path + "\libraryroot.css")

                If Not ficheroCSS1 Is Nothing Then
                    Dim contenido As String = Await FileIO.ReadTextAsync(ficheroCSS1)

                    Dim i As Integer = 0
                    While i < coloresNuevo.Colores.Count
                        contenido = contenido.Replace(coloresViejo.Colores(i), coloresNuevo.Colores(i))
                        i += 1
                    End While

                    Await FileIO.WriteTextAsync(ficheroCSS1, contenido)
                End If
            End If
        End If

        ApplicationData.Current.LocalSettings.Values("modo_editor") = modo

    End Sub

    Private Sub AñadirColoresLista(colores As EditorColores)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim gvColores As GridView = pagina.FindName("gvEditorColores")
        gvColores.Items.Clear()

        For Each color In colores.Colores
            Dim sp As New StackPanel With {
                .Orientation = Orientation.Horizontal,
                .Padding = New Thickness(10, 10, 10, 10)
            }

            Dim boton As New Button With {
                .Width = 120,
                .Height = 40,
                .Background = New SolidColorBrush(color.ToColor),
                .BorderBrush = New SolidColorBrush(Colors.White),
                .BorderThickness = New Thickness(1, 1, 1, 1)
            }

            sp.Children.Add(boton)

            gvColores.Items.Add(sp)
        Next

    End Sub

End Module
