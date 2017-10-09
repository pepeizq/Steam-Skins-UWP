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

                        Dim carpetaBorrar As StorageFolder = Nothing

                        Try
                            carpetaBorrar = Await StorageFolder.GetFolderFromPathAsync(carpetaSteamSkins.Path + "\" + apariencia.Titulo)
                        Catch ex As Exception

                        End Try

                        If Not carpetaBorrar Is Nothing Then
                            Try
                                Await carpetaBorrar.DeleteAsync
                            Catch ex As Exception
                                Toast("Error 0x4", Nothing)
                            End Try
                        End If

                        Dim ficheroApariencia As StorageFile = Nothing

                        Try
                            ficheroApariencia = Await carpetaSteamSkins.CreateFileAsync(apariencia.Titulo + ".zip", CreationCollisionOption.ReplaceExisting)
                        Catch ex As Exception

                        End Try

                        If Not ficheroApariencia Is Nothing Then
                            StorageApplicationPermissions.FutureAccessList.AddOrReplace(apariencia.Titulo + ".zip", ficheroApariencia)

                            Dim descargador As BackgroundDownloader = New BackgroundDownloader
                            Dim descarga As DownloadOperation = descargador.CreateDownload(apariencia.Enlace, ficheroApariencia)
                            Await descarga.StartAsync

                            apariencia.Informe.Text = recursos.GetString("Extracting")

                            Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
                            Dim metodo As String = 0

                            If Await helper.FileExistsAsync("metodo") = True Then
                                metodo = Await helper.ReadFileAsync(Of String)("metodo")
                            End If

                            If metodo = 0 Then
                                Dim rstream As Streams.IRandomAccessStream = Await ficheroApariencia.OpenReadAsync
                                Dim tarea As Task = Task.Factory.StartNew(Function() Descomprimir2(rstream, carpetaSteamSkins))
                                tarea.Wait()
                            ElseIf metodo = 1 Then
                                Dim tarea As Task = Task.Factory.StartNew(Function() Descomprimir1(ficheroApariencia, carpetaSteamSkins))
                                tarea.Wait()
                            End If

                            Try
                                Await ficheroApariencia.DeleteAsync()
                            Catch ex As Exception
                                Toast("Error 0x3", Nothing)
                            End Try

                            Try
                                If apariencia.Titulo = "Air" Then
                                    Dim carpetaSkin As StorageFolder = Await StorageFolder.GetFolderFromPathAsync(carpetaSteamSkins.Path + "\Air-for-Steam-master")
                                    Await carpetaSkin.RenameAsync("Air")
                                ElseIf apariencia.Titulo = "Air-Classic" Then
                                    Dim carpetaSkin As StorageFolder = Await StorageFolder.GetFolderFromPathAsync(carpetaSteamSkins.Path + "\Air-Classic-master")
                                    Await carpetaSkin.RenameAsync("Air-Classic")
                                ElseIf apariencia.Titulo = "Compact" Then
                                    Dim carpetaSkin As StorageFolder = Await StorageFolder.GetFolderFromPathAsync(carpetaSteamSkins.Path + "\Compact-master")
                                    Await carpetaSkin.RenameAsync("Compact")
                                ElseIf apariencia.Titulo = "Minimal" Then
                                    Dim carpetaSkin As StorageFolder = Await StorageFolder.GetFolderFromPathAsync(carpetaSteamSkins.Path + "\Minimal Steam UI V3")
                                    Await carpetaSkin.RenameAsync("Minimal")
                                ElseIf apariencia.Titulo = "PixelVision2" Then
                                    Dim carpetaSkin As StorageFolder = Await StorageFolder.GetFolderFromPathAsync(carpetaSteamSkins.Path + "\PixelVision2-master")
                                    Await carpetaSkin.RenameAsync("PixelVision2")
                                ElseIf apariencia.Titulo = "Pressure2" Then
                                    Dim carpetaSkin As StorageFolder = Await StorageFolder.GetFolderFromPathAsync(carpetaSteamSkins.Path + "\Pressure2-master")
                                    Await carpetaSkin.RenameAsync("Pressure2")
                                ElseIf apariencia.Titulo = "Threshold" Then
                                    Dim carpetaSkin As StorageFolder = Await StorageFolder.GetFolderFromPathAsync(carpetaSteamSkins.Path + "\Threshold-Skin-master")
                                    Await carpetaSkin.RenameAsync("Threshold")
                                End If
                            Catch ex As Exception
                                Toast("Error 0x5", Nothing)
                            End Try

                            If apariencia.Titulo = "Air" Then
                                Opciones.Air(apariencia.Opciones, carpetaSteamSkins.Path)
                            ElseIf apariencia.Titulo = "Air-Classic" Then
                                Opciones.AirClassic(apariencia.Opciones, carpetaSteamSkins.Path)
                            ElseIf apariencia.Titulo = "Metro" Then
                                Opciones.Metro(apariencia.Opciones, carpetaSteamSkins.Path)
                            ElseIf apariencia.Titulo = "Minimal" Then
                                Opciones.Minimal(apariencia.Opciones, carpetaSteamSkins.Path)
                            ElseIf apariencia.Titulo = "Pressure2" Then
                                Opciones.Pressure2(apariencia.Opciones, carpetaSteamSkins.Path)
                            ElseIf apariencia.Titulo = "Threshold" Then
                                Opciones.Threshold(apariencia.Opciones, carpetaSteamSkins.Path)
                            End If

                            Toast(recursos.GetString("InstallCompleted"), Nothing)
                            EstadoControles(True, apariencia)
                        Else
                            Toast("Error 0x2", Nothing)
                            EstadoControles(True, apariencia)
                        End If
                    Else
                        Toast("Error 0x1", Nothing)
                        EstadoControles(True, apariencia)
                    End If
                End If
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

        Dim lvAirClassic As ListView = pagina.FindName("lvAparienciaAirClassic1")
        lvAirClassic.IsEnabled = estado

        Dim lvCompact As ListView = pagina.FindName("lvAparienciaCompact1")
        lvCompact.IsEnabled = estado

        Dim lvInvert As ListView = pagina.FindName("lvAparienciaInvert1")
        lvInvert.IsEnabled = estado

        Dim lvMetro As ListView = pagina.FindName("lvAparienciaMetro1")
        lvMetro.IsEnabled = estado

        Dim lvMinimal As ListView = pagina.FindName("lvAparienciaMinimal1")
        lvMinimal.IsEnabled = estado

        Dim lvPixelVision2 As ListView = pagina.FindName("lvAparienciaPixelVision21")
        lvPixelVision2.IsEnabled = estado

        Dim lvPressure2 As ListView = pagina.FindName("lvAparienciaPressure21")
        lvPressure2.IsEnabled = estado

        Dim lvThreshold As ListView = pagina.FindName("lvAparienciaThreshold1")
        lvThreshold.IsEnabled = estado

        If Not apariencia Is Nothing Then
            If estado = True Then
                apariencia.Informe.Text = String.Empty
                apariencia.Progreso.Visibility = Visibility.Collapsed
                apariencia.Progreso.IsActive = False
            End If

            If Not apariencia.OpcionesGrid Is Nothing Then
                apariencia.OpcionesGrid.IsHitTestVisible = estado
            End If
        End If

    End Sub

    Private Function Descomprimir1(ficheroApariencia As StorageFile, carpetaSteam As StorageFolder)

        Using archivoZip As ZipArchive = ZipFile.Open(ficheroApariencia.Path, ZipArchiveMode.Read)
            For Each archivo As ZipArchiveEntry In archivoZip.Entries
                If Not archivo.FullName.EndsWith(".url", StringComparison.OrdinalIgnoreCase) Then
                    Dim nombreArchivo As String = archivo.FullName

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

        Return Nothing
    End Function

    Private Function Descomprimir2(rstream As Streams.IRandomAccessStream, carpetaSteam As StorageFolder) As Task

        Using stream As Stream = rstream.AsStreamForRead
            Dim lector As IReader = ReaderFactory.Open(stream)

            Dim opciones As New ExtractionOptions With {
                .ExtractFullPath = True,
                .Overwrite = True,
                .PreserveAttributes = True
            }

            While lector.MoveToNextEntry = True
                Try
                    lector.WriteEntryToDirectory(carpetaSteam.Path, opciones)
                Catch ex As Exception

                End Try
            End While
        End Using

        Return Nothing
    End Function

End Module
