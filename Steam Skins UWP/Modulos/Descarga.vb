Imports System.IO.Compression
Imports Windows.Networking.BackgroundTransfer
Imports Windows.Storage
Imports Windows.Storage.AccessCache

Module Descarga

    Dim recursos As Resources.ResourceLoader = New Resources.ResourceLoader()
    Dim WithEvents backgroundWorker As BackgroundWorker
    Dim nombreSkin As String
    Dim botonDescarga, botonRutaSteam As Button
    Dim textBlockInforme As TextBlock
    Dim progressInforme As ProgressRing
    Dim ficheroDestino As StorageFile
    Dim ubicacionSteam As StorageFolder
    Dim fallo1 As Boolean = False

    Public Async Sub Iniciar(skin As Skins, steam As StorageFolder, rutaSteam As Button)

        botonDescarga = skin.botonDescarga
        botonDescarga.IsEnabled = False

        botonRutaSteam = rutaSteam
        botonRutaSteam.IsEnabled = False

        textBlockInforme = skin.textBlockInforme
        textBlockInforme.Text = recursos.GetString("Descarga Iniciar")

        progressInforme = skin.progressInforme
        progressInforme.Visibility = Visibility.Visible
        progressInforme.IsActive = True

        nombreSkin = skin.titulo

        ficheroDestino = Await KnownFolders.PicturesLibrary.CreateFileAsync(nombreSkin, CreationCollisionOption.ReplaceExisting)
        Dim descargador As BackgroundDownloader = New BackgroundDownloader
        Dim descarga As DownloadOperation = descargador.CreateDownload(skin.enlace, ficheroDestino)

        Await descarga.StartAsync

        ubicacionSteam = steam

        StorageApplicationPermissions.FutureAccessList.Add(ficheroDestino)
        StorageApplicationPermissions.FutureAccessList.Add(ubicacionSteam)

        textBlockInforme.Text = recursos.GetString("Descarga Extraer")
        backgroundWorker = New BackgroundWorker
        backgroundWorker.RunWorkerAsync()

    End Sub

    Private Sub backgroundWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles backgroundWorker.DoWork

        Try

            Using archivoZip As ZipArchive = ZipFile.Open(ficheroDestino.Path, ZipArchiveMode.Read)
                For Each archivo As ZipArchiveEntry In archivoZip.Entries
                    If Not archivo.FullName.EndsWith(".url", StringComparison.OrdinalIgnoreCase) Then
                        Dim nombreArchivo As String = archivo.FullName

                        If nombreArchivo.IndexOf("/") = (nombreArchivo.Length - 1) Then
                            nombreArchivo = nombreArchivo.Remove(nombreArchivo.Length - 1, 1)
                        End If

                        If Not nombreArchivo.IndexOf(".") = (nombreArchivo.Length - 4) Then
                            Directory.CreateDirectory(ubicacionSteam.Path + "\" + nombreArchivo)
                        Else
                            archivo.ExtractToFile(ubicacionSteam.Path + "\" + nombreArchivo)
                        End If
                    End If
                Next
            End Using
        Catch ex As Exception
            fallo1 = True
        End Try

    End Sub

    Private Async Sub backgroundWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles backgroundWorker.RunWorkerCompleted

        Await ficheroDestino.DeleteAsync()

        'Try
        '    Await Launcher.LaunchUriAsync(New Uri("steam://ExitSteam"))
        'Catch ex As Exception

        'End Try

        If fallo1 = False Then
            textBlockInforme.Text = recursos.GetString("Descarga Final")
        Else
            textBlockInforme.Text = recursos.GetString("Descarga Fallo 1")
        End If

        progressInforme.Visibility = Visibility.Collapsed
        progressInforme.IsActive = False

        botonDescarga.IsEnabled = True
        botonRutaSteam.IsEnabled = True

    End Sub

End Module
