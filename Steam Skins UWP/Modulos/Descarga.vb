Imports System.IO.Compression
Imports Windows.Networking.BackgroundTransfer
Imports Windows.Storage
Imports Windows.Storage.AccessCache

Module Descarga

    Dim recursos As Resources.ResourceLoader = New Resources.ResourceLoader()
    Dim WithEvents backgroundWorker As BackgroundWorker
    Dim nombreSkin As String
    Dim botonRutaSteam As Button
    Dim listaBotonesDescarga As List(Of Button)

    Dim textBlockInforme As TextBlock
    Dim progressInforme As ProgressRing
    Dim listaOpciones As List(Of String)
    Dim gridOpciones As Grid

    Dim ficheroDestino As StorageFile
    Dim ubicacionSteam As StorageFolder
    Dim fallo1 As Boolean = False
    Dim fallo2 As Boolean = False

    Public Async Sub Iniciar(skin As Skins, steam As StorageFolder, rutaSteam As Button, listaBotones As List(Of Button))

        backgroundWorker = New BackgroundWorker

        If backgroundWorker.IsBusy = False Then
            listaBotonesDescarga = listaBotones

            For Each boton As Button In listaBotonesDescarga
                boton.IsEnabled = False
            Next

            botonRutaSteam = rutaSteam
            botonRutaSteam.IsEnabled = False

            textBlockInforme = skin.textBlockInforme
            textBlockInforme.Text = recursos.GetString("Descarga Iniciar")

            progressInforme = skin.progressInforme
            progressInforme.Visibility = Visibility.Visible
            progressInforme.IsActive = True

            listaOpciones = skin.opciones
            gridOpciones = skin.opcionesGrid

            If Not gridOpciones Is Nothing Then
                gridOpciones.IsHitTestVisible = False
            End If

            nombreSkin = skin.titulo

            ubicacionSteam = Await StorageFolder.GetFolderFromPathAsync(steam.Path + "\skins")

            If ubicacionSteam Is Nothing Then
                ubicacionSteam = Await StorageFolder.GetFolderFromPathAsync(steam.Path + "\Skins")
            End If

            StorageApplicationPermissions.FutureAccessList.Add(ubicacionSteam)

            ficheroDestino = Await steam.CreateFileAsync(nombreSkin, CreationCollisionOption.ReplaceExisting)

            StorageApplicationPermissions.FutureAccessList.Add(ficheroDestino)

            Dim descargador As BackgroundDownloader = New BackgroundDownloader
            Dim descarga As DownloadOperation = descargador.CreateDownload(skin.enlace, ficheroDestino)

            Await descarga.StartAsync

            textBlockInforme.Text = recursos.GetString("Descarga Extraer")

            backgroundWorker.RunWorkerAsync()
        End If

    End Sub

    Private Sub backgroundWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles backgroundWorker.DoWork

        Try
            If Directory.Exists(ubicacionSteam.Path + "\" + nombreSkin) Then
                Directory.Delete(ubicacionSteam.Path + "\" + nombreSkin, True)
            End If
        Catch ex As Exception
            fallo2 = True
        End Try

        Try
            Using archivoZip As ZipArchive = ZipFile.Open(ficheroDestino.Path, ZipArchiveMode.Read)
                For Each archivo As ZipArchiveEntry In archivoZip.Entries
                    If Not archivo.FullName.EndsWith(".url", StringComparison.OrdinalIgnoreCase) Then
                        Dim nombreArchivo As String = archivo.FullName

                        If nombreArchivo.Contains("-master") Then
                            nombreArchivo = nombreArchivo.Replace("-master", Nothing)
                        End If

                        '-------------------------------------------------

                        If nombreArchivo.Contains("Air-for-Steam") Then
                            nombreArchivo = nombreArchivo.Replace("Air-for-Steam", nombreSkin)
                        End If

                        If nombreArchivo.Contains("Compact/Steam/skins/Compact") Then
                            nombreArchivo = nombreArchivo.Replace("Compact/Steam/skins/Compact", nombreSkin)
                        End If

                        If nombreArchivo.Contains("Metro for Steam") Then
                            Dim int As Integer = nombreArchivo.IndexOf("Metro for Steam")
                            Dim temp As String = nombreArchivo.Remove(0, int)
                            Dim int2 As Integer = temp.IndexOf("/")

                            If Not int2 = -1 Then
                                nombreArchivo = nombreArchivo.Remove(int, int2 - int)
                                nombreArchivo = nombreArchivo.Insert(int, nombreSkin)
                            End If
                        End If

                        If nombreArchivo.Contains("Minimal Steam UI V3") Then
                            nombreArchivo = nombreArchivo.Replace("Minimal Steam UI V3", nombreSkin)
                        End If

                        If nombreArchivo.Contains("Plexed") Then
                            Dim int As Integer = nombreArchivo.IndexOf("Plexed")
                            Dim temp As String = nombreArchivo.Remove(0, int)
                            Dim int2 As Integer = temp.IndexOf("/")

                            If Not int2 = -1 Then
                                nombreArchivo = nombreArchivo.Remove(int, int2 - int)
                                nombreArchivo = nombreArchivo.Insert(int, nombreSkin)
                            End If
                        End If

                        If nombreArchivo.Contains("Threshold-Skin") Then
                            nombreArchivo = nombreArchivo.Replace("Threshold-Skin", nombreSkin)
                        End If

                        '-------------------------------------------------

                        If Not nombreArchivo.Contains(".") Then
                            If Not Directory.Exists(ubicacionSteam.Path + "\" + nombreArchivo) Then
                                Directory.CreateDirectory(ubicacionSteam.Path + "\" + nombreArchivo)
                            End If
                        Else
                            If nombreArchivo.Contains("/.") = True Then
                                If Not Directory.Exists(ubicacionSteam.Path + "\" + nombreArchivo) Then
                                    Directory.CreateDirectory(ubicacionSteam.Path + "\" + nombreArchivo)
                                End If
                            Else
                                If Not File.Exists(ubicacionSteam.Path + "\" + nombreArchivo) Then
                                    If Not Directory.Exists(ubicacionSteam.Path + "\" + nombreArchivo) Then
                                        Directory.CreateDirectory(ubicacionSteam.Path + "\" + nombreSkin)
                                    End If

                                    archivo.ExtractToFile(ubicacionSteam.Path + "\" + nombreArchivo)
                                End If
                            End If
                        End If
                    End If
                Next
            End Using
        Catch ex As Exception
            fallo1 = True
        End Try

    End Sub

    Private Async Sub backgroundWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles backgroundWorker.RunWorkerCompleted

        If nombreSkin = "Air" Then
            Opciones.Air(listaOpciones, ubicacionSteam.Path)
        ElseIf nombreSkin = "Air-Classic" Then
            Opciones.AirClassic(listaOpciones, ubicacionSteam.Path)
        ElseIf nombreSkin = "Metro" Then
            Opciones.Metro(listaOpciones, ubicacionSteam.Path)
        ElseIf nombreSkin = "Minimal" Then
            Opciones.Minimal(listaOpciones, ubicacionSteam.Path)
        ElseIf nombreSkin = "Pressure2" Then
            Opciones.Pressure2(listaOpciones, ubicacionSteam.Path)
        ElseIf nombreSkin = "Threshold" Then
            Opciones.Threshold(listaOpciones, ubicacionSteam.Path)
        End If

        Await ficheroDestino.DeleteAsync()

        'Try
        '    Await Launcher.LaunchUriAsync(New Uri("steam://ExitSteam"))
        'Catch ex As Exception

        'End Try

        If fallo1 = True Then
            textBlockInforme.Text = recursos.GetString("Descarga Fallo 1")
        End If

        If fallo2 = True Then
            textBlockInforme.Text = recursos.GetString("Descarga Fallo 2")
        End If

        If fallo1 = False And fallo2 = False Then
            textBlockInforme.Text = recursos.GetString("Descarga Final")
        End If

        Toast("Steam Skins", textBlockInforme.Text)

        If Not gridOpciones Is Nothing Then
            gridOpciones.IsHitTestVisible = True
        End If

        progressInforme.Visibility = Visibility.Collapsed
        progressInforme.IsActive = False

        For Each boton As Button In listaBotonesDescarga
            boton.IsEnabled = True
        Next

        botonRutaSteam.IsEnabled = True

    End Sub

End Module
