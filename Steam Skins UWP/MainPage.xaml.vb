Imports Windows.ApplicationModel.DataTransfer
Imports Windows.Storage
Imports Windows.Storage.AccessCache
Imports Windows.Storage.Pickers
Imports Windows.System
Imports Windows.UI

Public NotInheritable Class MainPage
    Inherits Page

    Dim skinAir, skinAirClassic, skinCompact, skinInvert, skinMetro, skinMinimal, skinPixelVision2, skinPlexed, skinPressure2, skinThreshold As Skins

    Private Sub Page_Loading(sender As FrameworkElement, args As Object)

        Dim barra As ApplicationViewTitleBar = ApplicationView.GetForCurrentView().TitleBar

        barra.BackgroundColor = Colors.DarkCyan
        barra.ForegroundColor = Colors.White
        barra.ButtonBackgroundColor = Colors.DarkCyan
        barra.ButtonForegroundColor = Colors.White

        '----------------------------------------------

        Dim recursos As Resources.ResourceLoader = New Resources.ResourceLoader()

        buttonRutaSteamTexto.Text = recursos.GetString("Boton Ruta Steam 1")
        buttonVolverTexto.Text = recursos.GetString("Boton Volver")
        buttonVotacionesTexto.Text = recursos.GetString("Boton Votar")
        buttonCompartirTexto.Text = recursos.GetString("Boton Compartir")
        buttonContactarTexto.Text = recursos.GetString("Boton Contactar")
        buttonWebTexto.Text = recursos.GetString("Boton Web")
        textBlockPublicidad.Text = recursos.GetString("Publicidad")

        buttonDescargaTextoAir.Text = recursos.GetString("Boton Descarga")
        tbScreenshotsAir.Text = recursos.GetString("Capturas")
        textBlockCreadoAir.Text = recursos.GetString("Creado Por")

        buttonDescargaTextoAirClassic.Text = recursos.GetString("Boton Descarga")
        tbScreenshotsAirClassic.Text = recursos.GetString("Capturas")
        textBlockCreadoAirClassic.Text = recursos.GetString("Creado Por")

        buttonDescargaTextoCompact.Text = recursos.GetString("Boton Descarga")
        tbScreenshotsCompact.Text = recursos.GetString("Capturas")
        textBlockCreadoCompact.Text = recursos.GetString("Creado Por")

        buttonDescargaTextoInvert.Text = recursos.GetString("Boton Descarga")
        tbScreenshotsInvert.Text = recursos.GetString("Capturas")
        textBlockCreadoInvert.Text = recursos.GetString("Creado Por")

        buttonDescargaTextoMetro.Text = recursos.GetString("Boton Descarga")
        tbScreenshotsMetro.Text = recursos.GetString("Capturas")
        textBlockCreadoMetro.Text = recursos.GetString("Creado Por")

        buttonDescargaTextoMinimal.Text = recursos.GetString("Boton Descarga")
        tbScreenshotsMinimal.Text = recursos.GetString("Capturas")
        textBlockCreadoMinimal.Text = recursos.GetString("Creado Por")

        buttonDescargaTextoPixelVision2.Text = recursos.GetString("Boton Descarga")
        tbScreenshotsPixelVision2.Text = recursos.GetString("Capturas")
        textBlockCreadoPixelVision2.Text = recursos.GetString("Creado Por")

        buttonDescargaTextoPlexed.Text = recursos.GetString("Boton Descarga")
        tbScreenshotsPlexed.Text = recursos.GetString("Capturas")
        textBlockCreadoPlexed.Text = recursos.GetString("Creado Por")

        buttonDescargaTextoPressure2.Text = recursos.GetString("Boton Descarga")
        tbScreenshotsPressure2.Text = recursos.GetString("Capturas")
        textBlockCreadoPressure2.Text = recursos.GetString("Creado Por")

        buttonDescargaTextoThreshold.Text = recursos.GetString("Boton Descarga")
        tbScreenshotsThreshold.Text = recursos.GetString("Capturas")
        textBlockCreadoThreshold.Text = recursos.GetString("Creado Por")

        '----------------------------------------------

        ComprobacionRutaSteam()

        Dim seleccion As Random = New Random
        Dim resultadoRandom As Integer = seleccion.Next(0, pivotSkins.Items.Count)
        pivotSkins.SelectedIndex = resultadoRandom

    End Sub

    'RUTASTEAM-----------------------------------------------------------------------------

    Private Async Sub buttonRutaSteam_Click(sender As Object, e As RoutedEventArgs) Handles buttonRutaSteam.Click

        Try
            Dim picker As FolderPicker = New FolderPicker()

            picker.FileTypeFilter.Add("*")
            picker.ViewMode = PickerViewMode.List

            Dim carpeta As StorageFolder = Await picker.PickSingleFolderAsync()

            If carpeta.Path.Contains("skins") Then
                StorageApplicationPermissions.FutureAccessList.AddOrReplace("rutaSteam", carpeta)
                ComprobacionRutaSteam()
            Else
                Dim recursos As Resources.ResourceLoader = New Resources.ResourceLoader()
                MessageBox.EnseñarMensaje(recursos.GetString("Seleccion Carpeta Fallo 1"))
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Async Sub ComprobacionRutaSteam()

        Dim carpetaComprobar As StorageFolder = Nothing

        Try
            carpetaComprobar = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteam")
        Catch ex As Exception

        End Try

        If Not carpetaComprobar Is Nothing Then
            buttonDescargaAir.IsEnabled = True
            buttonDescargaAirClassic.IsEnabled = True
            buttonDescargaCompact.IsEnabled = True
            buttonDescargaInvert.IsEnabled = True
            buttonDescargaMetro.IsEnabled = True
            buttonDescargaMinimal.IsEnabled = True
            buttonDescargaPixelVision2.IsEnabled = True
            buttonDescargaPlexed.IsEnabled = True
            buttonDescargaPressure2.IsEnabled = True
            buttonDescargaThreshold.IsEnabled = True

            Dim recursos As Resources.ResourceLoader = New Resources.ResourceLoader()

            buttonRutaSteamTexto.Text = recursos.GetString("Boton Ruta Steam 2")
        End If

    End Sub

    'AMPLIARCAPTURA-----------------------------------------------------------------------------

    Private Sub AmpliarCaptura(imagen As Image)

        pivotSkins.Visibility = Visibility.Collapsed
        gridCaptura.Visibility = Visibility.Visible

        buttonVolver.Visibility = Visibility.Visible
        buttonRutaSteam.Visibility = Visibility.Collapsed

        imageCapturaExpandida.Source = imagen.Source

    End Sub

    'SKINAIR-----------------------------------------------------------------------------

    Private Async Sub buttonDescargaAir_Click(sender As Object, e As RoutedEventArgs) Handles buttonDescargaAir.Click

        skinAir = New Skins("Air-for-Steam",
                                  New Uri("https://github.com/Outsetini/Air-for-Steam/archive/master.zip"),
                                  buttonDescargaAir,
                                  textBlockInformeAir,
                                  progressInformeAir)

        Dim carpetaSteam As StorageFolder = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteam")
        Descarga.Iniciar(skinAir, carpetaSteam, buttonRutaSteam)

    End Sub

    Private Async Sub buttonWebAir_Click(sender As Object, e As RoutedEventArgs) Handles buttonWebAir.Click

        Await Launcher.LaunchUriAsync(New Uri("http://airforsteam.com"))

    End Sub

    Private Async Sub buttonPatreonAir_Click(sender As Object, e As RoutedEventArgs) Handles buttonPatreonAir.Click

        Await Launcher.LaunchUriAsync(New Uri("https://www.patreon.com/inhibitor"))

    End Sub

    Private Sub buttonImagePreview1Air_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview1Air.Click

        AmpliarCaptura(imagePreview1Air)

    End Sub

    Private Sub buttonImagePreview2Air_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview2Air.Click

        AmpliarCaptura(imagePreview2Air)

    End Sub

    Private Sub buttonImagePreview3Air_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview3Air.Click

        AmpliarCaptura(imagePreview3Air)

    End Sub

    Private Sub buttonImagePreview4Air_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview4Air.Click

        AmpliarCaptura(imagePreview4Air)

    End Sub

    'SKINAIRCLASSIC-----------------------------------------------------------------------------

    Private Async Sub buttonDescargaAirClassic_Click(sender As Object, e As RoutedEventArgs) Handles buttonDescargaAirClassic.Click

        skinAirClassic = New Skins("Air-Classic",
                                  New Uri("https://github.com/Outsetini/Air-Classic/archive/master.zip"),
                                  buttonDescargaAirClassic,
                                  textBlockInformeAirClassic,
                                  progressInformeAirClassic)

        Dim carpetaSteam As StorageFolder = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteam")
        Descarga.Iniciar(skinAirClassic, carpetaSteam, buttonRutaSteam)

    End Sub

    Private Async Sub buttonWebAirClassic_Click(sender As Object, e As RoutedEventArgs) Handles buttonWebAirClassic.Click

        Await Launcher.LaunchUriAsync(New Uri("http://airforsteam.com"))

    End Sub

    Private Async Sub buttonPatreonAirClassic_Click(sender As Object, e As RoutedEventArgs) Handles buttonPatreonAirClassic.Click

        Await Launcher.LaunchUriAsync(New Uri("https://www.patreon.com/inhibitor"))

    End Sub

    Private Sub buttonImagePreview1AirClassic_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview1AirClassic.Click

        AmpliarCaptura(imagePreview1AirClassic)

    End Sub

    Private Sub buttonImagePreview2AirClassic_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview2AirClassic.Click

        AmpliarCaptura(imagePreview2AirClassic)

    End Sub

    Private Sub buttonImagePreview3AirClassic_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview3AirClassic.Click

        AmpliarCaptura(imagePreview3AirClassic)

    End Sub

    Private Sub buttonImagePreview4AirClassic_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview4AirClassic.Click

        AmpliarCaptura(imagePreview4AirClassic)

    End Sub

    'SKINCOMPACT-----------------------------------------------------------------------------

    Private Async Sub buttonDescargaCompact_Click(sender As Object, e As RoutedEventArgs) Handles buttonDescargaCompact.Click

        skinCompact = New Skins("Compact",
                                  New Uri("https://github.com/badanka/Compact/archive/master.zip"),
                                  buttonDescargaCompact,
                                  textBlockInformeCompact,
                                  progressInformeCompact)

        Dim carpetaSteam As StorageFolder = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteam")
        Descarga.Iniciar(skinCompact, carpetaSteam, buttonRutaSteam)

    End Sub

    Private Async Sub buttonWebCompact_Click(sender As Object, e As RoutedEventArgs) Handles buttonWebCompact.Click

        Await Launcher.LaunchUriAsync(New Uri("http://badanka.github.io/Compact/"))

    End Sub

    Private Sub buttonImagePreview1Compact_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview1Compact.Click

        AmpliarCaptura(imagePreview1Compact)

    End Sub

    Private Sub buttonImagePreview2Compact_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview2Compact.Click

        AmpliarCaptura(imagePreview2Compact)

    End Sub

    Private Sub buttonImagePreview3Compact_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview3Compact.Click

        AmpliarCaptura(imagePreview3Compact)

    End Sub

    Private Sub buttonImagePreview4Compact_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview4Compact.Click

        AmpliarCaptura(imagePreview4Compact)

    End Sub

    'SKININVERT-----------------------------------------------------------------------------

    Private Async Sub buttonDescargaInvert_Click(sender As Object, e As RoutedEventArgs) Handles buttonDescargaInvert.Click

        buttonDescargaInvert.IsEnabled = False

        Dim html As String = Await Decompiladores.HttpClient(New Uri("http://gamebanana.com/guis/download/28814"))

        Dim temp, temp2 As String
        Dim int, int2 As Integer

        If Not html = Nothing Then
            int = html.IndexOf(".zip")
            temp = html.Remove(int + 4, html.Length - (int + 4))

            int2 = temp.LastIndexOf("http://")
            temp2 = temp.Remove(0, int2)
        Else
            temp2 = Nothing
        End If

        skinInvert = New Skins("Invert",
                                  New Uri(temp2),
                                  buttonDescargaInvert,
                                  textBlockInformeInvert,
                                  progressInformeInvert)

        Dim carpetaSteam As StorageFolder = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteam")
        Descarga.Iniciar(skinInvert, carpetaSteam, buttonRutaSteam)

    End Sub

    Private Async Sub buttonWebInvert_Click(sender As Object, e As RoutedEventArgs) Handles buttonWebInvert.Click

        Await Launcher.LaunchUriAsync(New Uri("http://gamebanana.com/guis/28814"))

    End Sub

    Private Async Sub buttonPaypalInvert_Click(sender As Object, e As RoutedEventArgs) Handles buttonPaypalInvert.Click

        Await Launcher.LaunchUriAsync(New Uri("https://www.paypal.com/sk/cgi-bin/webscr?cmd=_flow&SESSION=XyLR0yV_beniveGJ0ONtunwPaUfnuwR7BtktA-E2xhApEH_8hG8e2s_Frm0&dispatch=5885d80a13c0db1f8e263663d3faee8d4fe1dd75ca3bd4f11d72275b28239088"))

    End Sub

    Private Sub buttonImagePreview1Invert_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview1Invert.Click

        AmpliarCaptura(imagePreview1Invert)

    End Sub

    Private Sub buttonImagePreview2Invert_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview2Invert.Click

        AmpliarCaptura(imagePreview2Invert)

    End Sub

    Private Sub buttonImagePreview3Invert_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview3Invert.Click

        AmpliarCaptura(imagePreview3Invert)

    End Sub

    Private Sub buttonImagePreview4Invert_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview4Invert.Click

        AmpliarCaptura(imagePreview4Invert)

    End Sub

    'SKINMETRO-----------------------------------------------------------------------------

    Private Async Sub buttonDescargaMetro_Click(sender As Object, e As RoutedEventArgs) Handles buttonDescargaMetro.Click

        buttonDescargaMetro.IsEnabled = False

        Dim html As String = Await Decompiladores.HttpClient(New Uri("http://www.metroforsteam.com"))

        Dim temp, temp2 As String
        Dim int, int2 As Integer

        If Not html = Nothing Then
            int = html.IndexOf(".zip")
            temp = html.Remove(int + 4, html.Length - (int + 4))

            int2 = temp.LastIndexOf("<a href=")
            temp2 = temp.Remove(0, int2 + 9)

            temp2 = "http://www.metroforsteam.com/" + temp2
        Else
            temp2 = Nothing
        End If

        skinMetro = New Skins("Metro",
                                  New Uri(temp2),
                                  buttonDescargaMetro,
                                  textBlockInformeMetro,
                                  progressInformeMetro)

        Dim carpetaSteam As StorageFolder = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteam")
        Descarga.Iniciar(skinMetro, carpetaSteam, buttonRutaSteam)

    End Sub

    Private Async Sub buttonWebMetro_Click(sender As Object, e As RoutedEventArgs) Handles buttonWebMetro.Click

        Await Launcher.LaunchUriAsync(New Uri("http://www.metroforsteam.com"))

    End Sub

    Private Async Sub buttonPaypalMetro_Click(sender As Object, e As RoutedEventArgs) Handles buttonPaypalMetro.Click

        Await Launcher.LaunchUriAsync(New Uri("https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=BDL2J3MEETZ3J&lc=US&item_name=Metro%20for%20Steam&item_number=metroforsteam&currency_code=USD&bn=PP%2dDonationsBF%3abtn_donate_LG%2egif%3aNonHosted"))

    End Sub

    Private Async Sub buttonPatreonMetro_Click(sender As Object, e As RoutedEventArgs) Handles buttonPatreonMetro.Click

        Await Launcher.LaunchUriAsync(New Uri("http://www.patreon.com/dommini"))

    End Sub

    Private Sub buttonImagePreview1Metro_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview1Metro.Click

        AmpliarCaptura(imagePreview1Metro)

    End Sub

    Private Sub buttonImagePreview2Metro_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview2Metro.Click

        AmpliarCaptura(imagePreview2Metro)

    End Sub

    Private Sub buttonImagePreview3Metro_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview3Metro.Click

        AmpliarCaptura(imagePreview3Metro)

    End Sub

    Private Sub buttonImagePreview4Metro_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview4Metro.Click

        AmpliarCaptura(imagePreview4Metro)

    End Sub

    'SKINMINIMAL-----------------------------------------------------------------------------

    Private Async Sub buttonDescargaMinimal_Click(sender As Object, e As RoutedEventArgs) Handles buttonDescargaMinimal.Click

        buttonDescargaMinimal.IsEnabled = False

        Dim html As String = Await Decompiladores.HttpClient(New Uri("http://gamebanana.com/guis/download/27756"))

        Dim temp, temp2 As String
        Dim int, int2 As Integer

        If Not html = Nothing Then
            int = html.IndexOf(".zip")
            temp = html.Remove(int + 4, html.Length - (int + 4))

            int2 = temp.LastIndexOf("http://")
            temp2 = temp.Remove(0, int2)
        Else
            temp2 = Nothing
        End If

        skinMinimal = New Skins("Minimal",
                                  New Uri(temp2),
                                  buttonDescargaMinimal,
                                  textBlockInformeMinimal,
                                  progressInformeMinimal)

        Dim carpetaSteam As StorageFolder = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteam")
        Descarga.Iniciar(skinMinimal, carpetaSteam, buttonRutaSteam)

    End Sub

    Private Async Sub buttonWebMinimal_Click(sender As Object, e As RoutedEventArgs) Handles buttonWebMinimal.Click

        Await Launcher.LaunchUriAsync(New Uri("http://steamcommunity.com/groups/MinimalSteamUI"))

    End Sub

    Private Sub buttonImagePreview1Minimal_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview1Minimal.Click

        AmpliarCaptura(imagePreview1Minimal)

    End Sub

    Private Sub buttonImagePreview2Minimal_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview2Minimal.Click

        AmpliarCaptura(imagePreview2Minimal)

    End Sub

    Private Sub buttonImagePreview3Minimal_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview3Minimal.Click

        AmpliarCaptura(imagePreview3Minimal)

    End Sub

    Private Sub buttonImagePreview4Minimal_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview4Minimal.Click

        AmpliarCaptura(imagePreview4Minimal)

    End Sub

    'SKINPIXELVISION2-----------------------------------------------------------------------------

    Private Async Sub buttonDescargaPixelVision2_Click(sender As Object, e As RoutedEventArgs) Handles buttonDescargaPixelVision2.Click

        skinPixelVision2 = New Skins("PixelVision2",
                                  New Uri("https://github.com/somini/Pixelvision2/archive/master.zip"),
                                  buttonDescargaPixelVision2,
                                  textBlockInformePixelVision2,
                                  progressInformePixelVision2)

        Dim carpetaSteam As StorageFolder = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteam")
        Descarga.Iniciar(skinPixelVision2, carpetaSteam, buttonRutaSteam)

    End Sub

    Private Async Sub buttonWebPixelVision2_Click(sender As Object, e As RoutedEventArgs) Handles buttonWebPixelVision2.Click

        Await Launcher.LaunchUriAsync(New Uri("https://steamcommunity.com/groups/pixelvision2"))

    End Sub

    Private Sub buttonImagePreview1PixelVision2_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview1PixelVision2.Click

        AmpliarCaptura(imagePreview1PixelVision2)

    End Sub

    Private Sub buttonImagePreview2PixelVision2_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview2PixelVision2.Click

        AmpliarCaptura(imagePreview2PixelVision2)

    End Sub

    Private Sub buttonImagePreview3PixelVision2_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview3PixelVision2.Click

        AmpliarCaptura(imagePreview3PixelVision2)

    End Sub

    Private Sub buttonImagePreview4PixelVision2_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview4PixelVision2.Click

        AmpliarCaptura(imagePreview4PixelVision2)

    End Sub

    'SKINPLEXED-----------------------------------------------------------------------------

    Private Async Sub buttonDescargaPlexed_Click(sender As Object, e As RoutedEventArgs) Handles buttonDescargaPlexed.Click

        buttonDescargaPlexed.IsEnabled = False

        Dim html As String = Await Decompiladores.HttpClient(New Uri("http://gamebanana.com/guis/download/30097"))

        Dim temp, temp2 As String
        Dim int, int2 As Integer

        If Not html = Nothing Then
            int = html.IndexOf(".zip")
            temp = html.Remove(int + 4, html.Length - (int + 4))

            int2 = temp.LastIndexOf("http://")
            temp2 = temp.Remove(0, int2)
        Else
            temp2 = Nothing
        End If

        skinPlexed = New Skins("Plexed",
                                  New Uri(temp2),
                                  buttonDescargaPlexed,
                                  textBlockInformePlexed,
                                  progressInformePlexed)

        Dim carpetaSteam As StorageFolder = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteam")
        Descarga.Iniciar(skinPlexed, carpetaSteam, buttonRutaSteam)

    End Sub

    Private Async Sub buttonWebPlexed_Click(sender As Object, e As RoutedEventArgs) Handles buttonWebPlexed.Click

        Await Launcher.LaunchUriAsync(New Uri("http://steamcommunity.com/groups/PlexedSkin"))

    End Sub

    Private Sub buttonImagePreview1Plexed_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview1Plexed.Click

        AmpliarCaptura(imagePreview1Plexed)

    End Sub

    Private Sub buttonImagePreview2Plexed_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview2Plexed.Click

        AmpliarCaptura(imagePreview2Plexed)

    End Sub

    Private Sub buttonImagePreview3Plexed_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview3Plexed.Click

        AmpliarCaptura(imagePreview3Plexed)

    End Sub

    Private Sub buttonImagePreview4Plexed_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview4Plexed.Click

        AmpliarCaptura(imagePreview4Plexed)

    End Sub

    'SKINPRESSURE2-----------------------------------------------------------------------------

    Private Async Sub buttonDescargaPressure2_Click(sender As Object, e As RoutedEventArgs) Handles buttonDescargaPressure2.Click

        skinPressure2 = New Skins("Pressure2",
                                  New Uri("https://github.com/DirtDiglett/Pressure2/archive/master.zip"),
                                  buttonDescargaPressure2,
                                  textBlockInformePressure2,
                                  progressInformePressure2)

        Dim carpetaSteam As StorageFolder = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteam")
        Descarga.Iniciar(skinPressure2, carpetaSteam, buttonRutaSteam)

    End Sub

    Private Async Sub buttonWebPressure2_Click(sender As Object, e As RoutedEventArgs) Handles buttonWebPressure2.Click

        Await Launcher.LaunchUriAsync(New Uri("http://www.pressureforsteam.com"))

    End Sub

    Private Async Sub buttonPaypalPressure2_Click(sender As Object, e As RoutedEventArgs) Handles buttonPaypalPressure2.Click

        Await Launcher.LaunchUriAsync(New Uri("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=WAC672ATU68E4"))

    End Sub

    Private Async Sub buttonPatreonPressure2_Click(sender As Object, e As RoutedEventArgs) Handles buttonPatreonPressure2.Click

        Await Launcher.LaunchUriAsync(New Uri("https://www.patreon.com/dirtdiglett"))

    End Sub

    Private Sub buttonImagePreview1Pressure2_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview1Pressure2.Click

        AmpliarCaptura(imagePreview1Pressure2)

    End Sub

    Private Sub buttonImagePreview2Pressure2_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview2Pressure2.Click

        AmpliarCaptura(imagePreview2Pressure2)

    End Sub

    Private Sub buttonImagePreview3Pressure2_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview3Pressure2.Click

        AmpliarCaptura(imagePreview3Pressure2)

    End Sub

    Private Sub buttonImagePreview4Pressure2_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview4Pressure2.Click

        AmpliarCaptura(imagePreview4Pressure2)

    End Sub

    'SKINTHRESHOLD-----------------------------------------------------------------------------

    Private Async Sub buttonDescargaThreshold_Click(sender As Object, e As RoutedEventArgs) Handles buttonDescargaThreshold.Click

        skinThreshold = New Skins("Threshold",
                                  New Uri("https://github.com/Edgarware/Threshold-Skin/archive/master.zip"),
                                  buttonDescargaThreshold,
                                  textBlockInformeThreshold,
                                  progressInformeThreshold)

        Dim carpetaSteam As StorageFolder = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteam")
        Descarga.Iniciar(skinThreshold, carpetaSteam, buttonRutaSteam)

    End Sub

    Private Async Sub buttonWebThreshold_Click(sender As Object, e As RoutedEventArgs) Handles buttonWebThreshold.Click

        Await Launcher.LaunchUriAsync(New Uri("https://steamcommunity.com/groups/thresholdskin"))

    End Sub

    Private Sub buttonImagePreview1Threshold_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview1Threshold.Click

        AmpliarCaptura(imagePreview1Threshold)

    End Sub

    Private Sub buttonImagePreview2Threshold_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview2Threshold.Click

        AmpliarCaptura(imagePreview2Threshold)

    End Sub

    Private Sub buttonImagePreview3Threshold_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview3Threshold.Click

        AmpliarCaptura(imagePreview3Threshold)

    End Sub

    Private Sub buttonImagePreview4Threshold_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview4Threshold.Click

        AmpliarCaptura(imagePreview4Threshold)

    End Sub

    'VOTAR-----------------------------------------------------------------------------

    Private Async Sub buttonVotaciones_Click(sender As Object, e As RoutedEventArgs) Handles buttonVotaciones.Click

        Await Launcher.LaunchUriAsync(New Uri("ms-windows-store:REVIEW?PFN=" + Package.Current.Id.FamilyName))

    End Sub

    'COMPARTIR-----------------------------------------------------------------------------

    Private Sub buttonCompartir_Click(sender As Object, e As RoutedEventArgs) Handles buttonCompartir.Click

        Dim datos As DataTransferManager = DataTransferManager.GetForCurrentView()
        AddHandler datos.DataRequested, AddressOf MainPage_DataRequested
        DataTransferManager.ShowShareUI()

    End Sub

    Private Sub MainPage_DataRequested(sender As DataTransferManager, e As DataRequestedEventArgs)

        Dim request As DataRequest = e.Request
        request.Data.SetText("Steam Skins")
        request.Data.Properties.Title = "Steam Skins"
        request.Data.Properties.Description = "Change the skin of Steam"

    End Sub

    'CONTACTAR-----------------------------------------------------------------------------

    Private Sub buttonContactar_Click(sender As Object, e As RoutedEventArgs) Handles buttonContactar.Click

        gridWebContacto.Visibility = Visibility.Visible
        pivotSkins.Visibility = Visibility.Collapsed

        buttonVolver.Visibility = Visibility.Visible
        buttonRutaSteam.Visibility = Visibility.Collapsed

    End Sub

    'VOLVER-----------------------------------------------------------------------------

    Private Sub buttonVolver_Click(sender As Object, e As RoutedEventArgs) Handles buttonVolver.Click

        gridWebContacto.Visibility = Visibility.Collapsed
        pivotSkins.Visibility = Visibility.Visible
        gridCaptura.Visibility = Visibility.Collapsed

        buttonVolver.Visibility = Visibility.Collapsed
        buttonRutaSteam.Visibility = Visibility.Visible

    End Sub

    'WEB-----------------------------------------------------------------------------

    Private Async Sub buttonWeb_Click(sender As Object, e As RoutedEventArgs) Handles buttonWeb.Click

        Await Launcher.LaunchUriAsync(New Uri("https://pepeizqapps.com/"))

    End Sub


End Class
