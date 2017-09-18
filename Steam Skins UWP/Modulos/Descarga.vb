Imports System.IO.Compression
Imports Microsoft.Toolkit.Uwp.Helpers
Imports SharpCompress.Readers
Imports Windows.Networking.BackgroundTransfer
Imports Windows.Storage
Imports Windows.Storage.AccessCache

Module Descarga

    Dim WithEvents Bw As BackgroundWorker

    Public Async Sub Iniciar(apariencia As Apariencia)

        Dim carpetaSteam As StorageFolder = Nothing

        Try
            carpetaSteam = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteam")
        Catch ex As Exception

        End Try

        If Not apariencia Is Nothing Then
            If Not carpetaSteam Is Nothing Then
                Bw = New BackgroundWorker With {
                    .WorkerSupportsCancellation = True
                }

                If Bw.IsBusy = False Then
                    EstadoControles(False, apariencia)

                    Dim recursos As Resources.ResourceLoader = New Resources.ResourceLoader()

                    apariencia.Informe.Text = recursos.GetString("Downloading")
                    apariencia.Progreso.Visibility = Visibility.Visible
                    apariencia.Progreso.IsActive = True

                    Dim carpetaSteamSkins As StorageFolder = Nothing

                    Try
                        carpetaSteamSkins = Await StorageFolder.GetFolderFromPathAsync(carpetaSteam.Path + "\skins")
                    Catch ex As Exception

                    End Try

                    If carpetaSteamSkins Is Nothing Then
                        Try
                            carpetaSteamSkins = Await StorageFolder.GetFolderFromPathAsync(carpetaSteam.Path + "\Skins")
                        Catch ex As Exception

                        End Try
                    End If

                    If Not carpetaSteamSkins Is Nothing Then
                        StorageApplicationPermissions.FutureAccessList.AddOrReplace("rutaSteamSkins", carpetaSteamSkins)

                        Dim ficheroApariencia As StorageFile = Await carpetaSteamSkins.CreateFileAsync(apariencia.Titulo, CreationCollisionOption.ReplaceExisting)
                        StorageApplicationPermissions.FutureAccessList.AddOrReplace(apariencia.Titulo, ficheroApariencia)

                        Dim descargador As BackgroundDownloader = New BackgroundDownloader
                        Dim descarga As DownloadOperation = descargador.CreateDownload(apariencia.Enlace, ficheroApariencia)
                        Await descarga.StartAsync

                        apariencia.Informe.Text = recursos.GetString("Extracting")

                        Bw.RunWorkerAsync(apariencia)
                    Else
                        Toast(recursos.GetString("Error1"), "0x1")
                        Bw.CancelAsync()
                        EstadoControles(True, apariencia)
                    End If
                End If
            End If
        End If

    End Sub

    Private Async Sub Bw_DoWork(sender As Object, e As DoWorkEventArgs) Handles Bw.DoWork

        Dim recursos As Resources.ResourceLoader = New Resources.ResourceLoader()

        Dim carpetaSteam As StorageFolder = Nothing

        Try
            carpetaSteam = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteamSkins")
        Catch ex As Exception

        End Try

        Dim apariencia As Apariencia = e.Argument

        If Not apariencia Is Nothing Then
            If Not carpetaSteam Is Nothing Then
                Try
                    If Directory.Exists(carpetaSteam.Path + "\" + apariencia.Titulo) Then
                        Directory.Delete(carpetaSteam.Path + "\" + apariencia.Titulo, True)
                    End If
                Catch ex As Exception
                    Toast(recursos.GetString("Error2"), "0x2")
                    Bw.CancelAsync()
                    EstadoControles(True, apariencia)
                End Try

                Dim ficheroApariencia As StorageFile = Nothing

                Try
                    ficheroApariencia = Await StorageApplicationPermissions.FutureAccessList.GetFileAsync(apariencia.Titulo)
                Catch ex As Exception

                End Try

                If Not ficheroApariencia Is Nothing Then
                    Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
                    Dim metodo As String = 0

                    If Await helper.FileExistsAsync("metodo") = True Then
                        metodo = Await helper.ReadFileAsync(Of String)("metodo")
                    End If

                    If metodo = 0 Then
                        Using archivoZip As ZipArchive = ZipFile.Open(ficheroApariencia.Path, ZipArchiveMode.Read)
                            For Each archivo As ZipArchiveEntry In archivoZip.Entries
                                If Not archivo.FullName.EndsWith(".url", StringComparison.OrdinalIgnoreCase) Then
                                    Dim nombreArchivo As String = archivo.FullName

                                    If nombreArchivo.Contains("-master") Then
                                        nombreArchivo = nombreArchivo.Replace("-master", Nothing)
                                    End If

                                    '-------------------------------------------------

                                    If nombreArchivo.Contains("Air-for-Steam") Then
                                        nombreArchivo = nombreArchivo.Replace("Air-for-Steam", apariencia.Titulo)
                                    End If

                                    If nombreArchivo.Contains("Compact/Steam/skins/Compact") Then
                                        nombreArchivo = nombreArchivo.Replace("Compact/Steam/skins/Compact", apariencia.Titulo)
                                    End If

                                    If nombreArchivo.Contains("Metro for Steam") Then
                                        Dim int As Integer = nombreArchivo.IndexOf("Metro for Steam")
                                        Dim temp As String = nombreArchivo.Remove(0, int)
                                        Dim int2 As Integer = temp.IndexOf("/")

                                        If Not int2 = -1 Then
                                            nombreArchivo = nombreArchivo.Remove(int, int2 - int)
                                            nombreArchivo = nombreArchivo.Insert(int, apariencia.Titulo)
                                        End If
                                    End If

                                    If nombreArchivo.Contains("Minimal Steam UI V3") Then
                                        nombreArchivo = nombreArchivo.Replace("Minimal Steam UI V3", apariencia.Titulo)
                                    End If

                                    If nombreArchivo.Contains("Threshold-Skin") Then
                                        nombreArchivo = nombreArchivo.Replace("Threshold-Skin", apariencia.Titulo)
                                    End If

                                    '-------------------------------------------------

                                    nombreArchivo = nombreArchivo.Replace("/", "\")

                                    If Not nombreArchivo.Contains(".") Then
                                        If Not Directory.Exists(carpetaSteam.Path + "\" + nombreArchivo) Then
                                            Try
                                                Directory.CreateDirectory(carpetaSteam.Path + "\" + nombreArchivo)
                                            Catch ex As Exception

                                            End Try
                                        End If
                                    Else
                                        If nombreArchivo.Contains("/.") = True Then
                                            If Not Directory.Exists(carpetaSteam.Path + "\" + nombreArchivo) Then
                                                Try
                                                    Directory.CreateDirectory(carpetaSteam.Path + "\" + nombreArchivo)
                                                Catch ex As Exception

                                                End Try
                                            End If
                                        Else
                                            If Not File.Exists(carpetaSteam.Path + "\" + nombreArchivo) Then
                                                If Not Directory.Exists(carpetaSteam.Path + "\" + nombreArchivo) Then
                                                    Try
                                                        Directory.CreateDirectory(carpetaSteam.Path + "\" + nombreArchivo)
                                                    Catch ex As Exception

                                                    End Try
                                                End If

                                                Try
                                                    archivo.ExtractToFile(carpetaSteam.Path + "\" + nombreArchivo)
                                                Catch ex As Exception

                                                End Try
                                            End If
                                        End If
                                    End If
                                End If
                            Next
                        End Using

                    ElseIf metodo = 1 Then

                        Dim rstream As Streams.IRandomAccessStream = Await ficheroApariencia.OpenReadAsync
                        Using stream As Stream = rstream.AsStreamForRead
                            Dim lector As IReader = ReaderFactory.Open(stream)

                            Dim opciones As New ExtractionOptions With {
                                .ExtractFullPath = True,
                                .Overwrite = True
                            }

                            While lector.MoveToNextEntry = True
                                Try
                                    lector.WriteEntryToDirectory(carpetaSteam.Path, opciones)
                                Catch ex As Exception

                                End Try
                            End While
                        End Using
                    End If

                    e.Result = apariencia
                End If
            End If
        End If

    End Sub

    Private Async Sub Bw_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles Bw.RunWorkerCompleted

        'If nombreSkin = "Air" Then
        '    Opciones.Air(listaOpciones, ubicacionSteam.Path)
        'ElseIf nombreSkin = "Air-Classic" Then
        '    Opciones.AirClassic(listaOpciones, ubicacionSteam.Path)
        'ElseIf nombreSkin = "Metro" Then
        '    Opciones.Metro(listaOpciones, ubicacionSteam.Path)
        'ElseIf nombreSkin = "Minimal" Then
        '    Opciones.Minimal(listaOpciones, ubicacionSteam.Path)
        'ElseIf nombreSkin = "Pressure2" Then
        '    Opciones.Pressure2(listaOpciones, ubicacionSteam.Path)
        'ElseIf nombreSkin = "Threshold" Then
        '    Opciones.Threshold(listaOpciones, ubicacionSteam.Path)
        'End If


        'Try
        '    Await Launcher.LaunchUriAsync(New Uri("steam://ExitSteam"))
        'Catch ex As Exception

        'End Try

        'If fallo1 = True Then
        '    textBlockInforme.Text = recursos.GetString("Descarga Fallo 1")
        'End If

        'If fallo2 = True Then
        '    textBlockInforme.Text = recursos.GetString("Descarga Fallo 2")
        'End If

        'If fallo1 = False And fallo2 = False Then
        '    textBlockInforme.Text = recursos.GetString("Descarga Final")
        'End If

        'Toast("Steam Skins", textBlockInforme.Text)

        Dim apariencia As Apariencia = e.Result
        Dim ficheroApariencia As StorageFile = Nothing

        Try
            ficheroApariencia = Await StorageApplicationPermissions.FutureAccessList.GetFileAsync(apariencia.Titulo)
        Catch ex As Exception

        End Try

        If Not ficheroApariencia Is Nothing Then
            Await ficheroApariencia.DeleteAsync()
        End If

        Dim carpetaSteam As StorageFolder = Nothing

        Try
            carpetaSteam = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteamSkins")
        Catch ex As Exception

        End Try

        If Not carpetaSteam Is Nothing Then
            If Not apariencia Is Nothing Then
                If apariencia.Titulo = "Air" Then
                    Dim carpeta As StorageFolder = Await StorageFolder.GetFolderFromPathAsync(carpetaSteam.Path + "Air-for-Steam-master")
                    Await carpeta.RenameAsync("Air")
                End If

                EstadoControles(True, apariencia)
            End If
        End If

    End Sub

    Private Sub EstadoControles(estado As Boolean, apariencia As Apariencia)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim botonSteam As Button = pagina.FindName("botonSteamRuta")
        botonSteam.IsEnabled = estado

        Dim lvAir As ListView = pagina.FindName("lvAparienciaAir1")
        lvAir.IsEnabled = estado

        If estado = True Then
            If Not apariencia Is Nothing Then
                apariencia.Informe.Text = String.Empty
                apariencia.Progreso.Visibility = Visibility.Collapsed
                apariencia.Progreso.IsActive = False
            End If
        End If

    End Sub

End Module
