Imports Windows.Storage
Imports Windows.Storage.AccessCache
Imports Windows.Storage.Pickers

Module Detector

    Public Async Sub Steam(picker As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim tbConfigPath As TextBlock = pagina.FindName("tbSteamConfigPath")
        Dim buttonConfigPath As TextBlock = pagina.FindName("buttonSteamConfigPathTexto")

        Dim recursos As Resources.ResourceLoader = New Resources.ResourceLoader()
        Dim carpeta As StorageFolder = Nothing

        Try
            If picker = True Then
                Dim carpetapicker As FolderPicker = New FolderPicker()

                carpetapicker.FileTypeFilter.Add("*")
                carpetapicker.ViewMode = PickerViewMode.List

                carpeta = Await carpetapicker.PickSingleFolderAsync()
            Else
                carpeta = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteam")
            End If

            If Not carpeta Is Nothing Then
                Dim ejecutable As StorageFile = Nothing

                Try
                    ejecutable = Await carpeta.GetFileAsync("Steam.exe")
                Catch ex As Exception

                End Try

                If Not ejecutable Is Nothing Then
                    StorageApplicationPermissions.FutureAccessList.AddOrReplace("rutaSteam", carpeta)
                    tbConfigPath.Text = carpeta.Path
                    buttonConfigPath.Text = recursos.GetString("Boton Cambiar")

                    Dim botonAir As Button = pagina.FindName("buttonDescargaAir")
                    botonAir.IsEnabled = True

                    Dim botonAirClassic As Button = pagina.FindName("buttonDescargaAirClassic")
                    botonAirClassic.IsEnabled = True

                    Dim botonCompact As Button = pagina.FindName("buttonDescargaCompact")
                    botonCompact.IsEnabled = True

                    Dim botonInvert As Button = pagina.FindName("buttonDescargaInvert")
                    botonInvert.IsEnabled = True

                    Dim botonMetro As Button = pagina.FindName("buttonDescargaMetro")
                    botonMetro.IsEnabled = True

                    Dim botonMinimal As Button = pagina.FindName("buttonDescargaMinimal")
                    botonMinimal.IsEnabled = True

                    Dim botonPixelVision2 As Button = pagina.FindName("buttonDescargaPixelVision2")
                    botonPixelVision2.IsEnabled = True

                    Dim botonPressure2 As Button = pagina.FindName("buttonDescargaPressure2")
                    botonPressure2.IsEnabled = True

                    Dim botonThreshold As Button = pagina.FindName("buttonDescargaThreshold")
                    botonThreshold.IsEnabled = True
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

End Module
