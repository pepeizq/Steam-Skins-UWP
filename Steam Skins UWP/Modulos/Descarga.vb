Imports System.IO.Compression
Imports System.Text
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Windows.Networking.BackgroundTransfer
Imports Windows.Storage
Imports Windows.Storage.AccessCache
Imports Windows.UI.Core

Module Descarga

    Dim WithEvents Bw As BackgroundWorker

    Public Async Sub Iniciar(apariencia As Apariencia)

        If apariencia.Titulo = "Metro" Then
            Dim html As String = Await Decompiladores.HttpClient(New Uri(apariencia.EnlaceDescarga))

            Dim temp, temp2 As String
            Dim int, int2 As Integer

            If Not html = Nothing Then
                int = html.IndexOf(".zip")
                temp = html.Remove(int + 4, html.Length - (int + 4))

                int2 = temp.LastIndexOf("<a href=")
                temp2 = temp.Remove(0, int2 + 9)

                apariencia.EnlaceDescarga = "http://www.metroforsteam.com/" + temp2.Trim

                Dim carpeta As String = apariencia.Titulo + " " + temp2.Trim

                carpeta = carpeta.Replace("downloads/", Nothing)
                carpeta = carpeta.Replace(".zip", Nothing)

                apariencia.CarpetaDescarga = carpeta
            End If
        End If

        Dim carpetaSteam As StorageFolder = Nothing
        Dim ficheroDescargado As StorageFile = Nothing

        Try
            carpetaSteam = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteam")
        Catch ex As Exception

        End Try

        If Not apariencia Is Nothing Then
            If Not carpetaSteam Is Nothing Then

                Await Core.CoreApplication.GetCurrentView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, Sub()
                                                                                                                 TareaDescarga(apariencia, carpetaSteam)
                                                                                                             End Sub)
            End If
        End If

    End Sub

    Private Sub EstadoControles(estado As Boolean)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim gvApariencias As GridView = pagina.FindName("gvApariencias")
        gvApariencias.IsEnabled = estado

        Dim botonSteam As Button = pagina.FindName("botonSteamRuta")
        botonSteam.IsEnabled = estado

        Dim botonDescarga As Button = pagina.FindName("botonDescargaApariencia")
        botonDescarga.IsEnabled = estado

        Dim botonPersonalizacion As Button = pagina.FindName("botonPersonalizacion")
        botonPersonalizacion.IsEnabled = estado

        Dim prDescarga As ProgressRing = pagina.FindName("prDescarga")

        If estado = False Then
            prDescarga.Visibility = Visibility.Visible
        Else
            prDescarga.Visibility = Visibility.Collapsed
        End If

    End Sub

    Private Async Sub TareaDescarga(apariencia As Apariencia, carpetaSteam As StorageFolder)

        EstadoControles(False)

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

        Dim ficheroZip As IStorageFile = Nothing

        Try
            ficheroZip = Await ApplicationData.Current.LocalFolder.CreateFileAsync(apariencia.Titulo + ".zip", CreationCollisionOption.ReplaceExisting)
        Catch ex As Exception

        End Try

        If Not ficheroZip Is Nothing Then
            StorageApplicationPermissions.FutureAccessList.AddOrReplace(apariencia.Titulo + ".zip", ficheroZip)

            Dim descargador As BackgroundDownloader = New BackgroundDownloader
            Dim descarga As DownloadOperation = descargador.CreateDownload(New Uri(apariencia.EnlaceDescarga), ficheroZip)
            descarga.Priority = BackgroundTransferPriority.High
            Await descarga.StartAsync

            Dim ficheroDescargado As IStorageFile = descarga.ResultFile
            ficheroDescargado = Await StorageApplicationPermissions.FutureAccessList.GetFileAsync(apariencia.Titulo + ".zip")

            If Not ficheroDescargado Is Nothing Then
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance)

                Try
                    Directory.Delete(ApplicationData.Current.LocalFolder.Path + "\" + apariencia.CarpetaDescarga, True)
                Catch ex As Exception

                End Try

                Dim zip As New Ionic.Zip.ZipFile
                zip = Ionic.Zip.ZipFile.Read(ficheroDescargado.Path)
                zip.ExtractAll(ApplicationData.Current.LocalFolder.Path, Ionic.Zip.ExtractExistingFileAction.OverwriteSilently)

                Dim carpetaDescomprimida As StorageFolder = Await StorageFolder.GetFolderFromPathAsync(ApplicationData.Current.LocalFolder.Path + "\" + apariencia.CarpetaDescarga)

                If apariencia.Titulo = "Air" Then
                    Opciones.Air(carpetaDescomprimida.Path)
                ElseIf apariencia.Titulo = "Air Classic" Then
                    Opciones.AirClassic(carpetaDescomprimida.Path)
                ElseIf apariencia.Titulo = "Metro" Then
                    Opciones.Metro(carpetaDescomprimida.Path)
                ElseIf apariencia.Titulo = "Pressure2" Then
                    Opciones.Pressure2(carpetaDescomprimida.Path)
                ElseIf apariencia.Titulo = "Threshold" Then
                    Opciones.Threshold(carpetaDescomprimida.Path)
                End If

                Dim carpetaFinal As StorageFolder = Await carpetaSteamSkins.CreateFolderAsync(apariencia.Titulo, CreationCollisionOption.ReplaceExisting)

                Dim listaArchivos As IReadOnlyList(Of StorageFile) = Await carpetaDescomprimida.GetFilesAsync()

                For Each archivo In listaArchivos
                    Await archivo.CopyAsync(carpetaFinal)
                Next

                Dim listaCarpetas As IReadOnlyList(Of StorageFolder) = Await carpetaDescomprimida.GetFoldersAsync()

                If listaCarpetas.Count > 0 Then
                    For Each carpeta In listaCarpetas
                        Dim listaCarpetas2 As IReadOnlyList(Of StorageFolder) = Await carpeta.GetFoldersAsync()

                        If listaCarpetas2.Count > 0 Then
                            For Each carpeta2 In listaCarpetas2
                                Dim listaCarpetas3 As IReadOnlyList(Of StorageFolder) = Await carpeta2.GetFoldersAsync()

                                If listaCarpetas3.Count > 0 Then
                                    For Each carpeta3 In listaCarpetas3
                                        MovimientoFichero(carpeta3, carpeta.Name + "\" + carpeta2.Name + "\", carpetaFinal)
                                    Next
                                End If

                                MovimientoFichero(carpeta2, carpeta.Name + "\", carpetaFinal)
                            Next
                        End If

                        MovimientoFichero(carpeta, Nothing, carpetaFinal)
                    Next
                End If

                Dim recursos As New Resources.ResourceLoader
                Toast(recursos.GetString("InstallCompleted"), Nothing)
            End If
        End If

        EstadoControles(True)

    End Sub

    Private Async Sub MovimientoFichero(carpeta As StorageFolder, path As String, carpetaFinal As StorageFolder)

        Dim subCarpetaFinal As StorageFolder = Await carpetaFinal.CreateFolderAsync(path + carpeta.Name, CreationCollisionOption.OpenIfExists)

        If Not subCarpetaFinal Is Nothing Then
            Dim archivos As IReadOnlyList(Of StorageFile) = Await carpeta.GetFilesAsync(Search.CommonFileQuery.DefaultQuery)

            For Each archivo In archivos
                Await archivo.CopyAsync(subCarpetaFinal)
            Next
        End If

    End Sub

End Module
