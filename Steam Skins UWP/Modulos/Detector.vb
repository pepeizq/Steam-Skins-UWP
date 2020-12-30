Imports Windows.Storage
Imports Windows.Storage.AccessCache
Imports Windows.Storage.Pickers

Module Detector

    Public Async Sub Steam(picker As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim tbRuta As TextBlock = pagina.FindName("tbSteamRuta")
        Dim botonRutaTexto As TextBlock = pagina.FindName("botonSteamRutaTexto")

        Dim recursos As New Resources.ResourceLoader()
        Dim carpeta As StorageFolder = Nothing

        Try
            If picker = True Then
                Dim carpetapicker As New FolderPicker()

                carpetapicker.FileTypeFilter.Add("*")
                carpetapicker.ViewMode = PickerViewMode.List

                carpeta = Await carpetapicker.PickSingleFolderAsync()
            Else
                carpeta = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteam")
            End If
        Catch ex As Exception

        End Try

        If Not carpeta Is Nothing Then
            Dim ejecutable As StorageFile = Nothing

            Try
                ejecutable = Await carpeta.GetFileAsync("Steam.exe")
            Catch ex As Exception

            End Try

            If Not ejecutable Is Nothing Then
                StorageApplicationPermissions.FutureAccessList.AddOrReplace("rutaSteam", carpeta)
                tbRuta.Text = carpeta.Path
                botonRutaTexto.Text = recursos.GetString("ConfigChange")

                Dim gridApariencias As Grid = pagina.FindName("gridApariencias")
                Interfaz.Pestañas.Visibilidad(gridApariencias, recursos.GetString("Skins"), Nothing)
            Else
                Dim gridConfig As Grid = pagina.FindName("gridConfig")
                Interfaz.Pestañas.Visibilidad(gridConfig, recursos.GetString("Config"), Nothing)
            End If
        Else
            Dim gridAviso As Grid = pagina.FindName("gridAviso")
            Interfaz.Pestañas.Visibilidad(gridAviso, Nothing, Nothing)
        End If
    End Sub

End Module
