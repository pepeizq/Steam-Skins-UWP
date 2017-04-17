Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.ApplicationModel.DataTransfer
Imports Windows.Storage
Imports Windows.Storage.AccessCache
Imports Windows.System
Imports Windows.UI

Public NotInheritable Class MainPage
    Inherits Page

    Dim skinAir, skinAirClassic, skinCompact, skinInvert, skinMetro, skinMinimal, skinPixelVision2, skinPressure2, skinThreshold As Skins
    Dim listaBotonesDescarga As List(Of Button)

    Private Sub Page_Loaded(sender As FrameworkElement, args As Object)

        Dim barra As ApplicationViewTitleBar = ApplicationView.GetForCurrentView().TitleBar

        barra.BackgroundColor = Colors.DarkCyan
        barra.ForegroundColor = Colors.White
        barra.InactiveForegroundColor = Colors.White
        barra.ButtonBackgroundColor = Colors.DarkCyan
        barra.ButtonForegroundColor = Colors.White
        barra.ButtonInactiveForegroundColor = Colors.White

        '----------------------------------------------

        listaBotonesDescarga = New List(Of Button) From {
            buttonDescargaAir,
            buttonDescargaAirClassic,
            buttonDescargaCompact,
            buttonDescargaInvert,
            buttonDescargaMetro,
            buttonDescargaMinimal,
            buttonDescargaPixelVision2,
            buttonDescargaPressure2,
            buttonDescargaThreshold
        }

        '----------------------------------------------

        Dim recursos As Resources.ResourceLoader = New Resources.ResourceLoader()

        botonInicioTexto.Text = recursos.GetString("Boton Inicio")
        botonAparienciasTexto.Text = recursos.GetString("Skins")
        botonConfigTexto.Text = recursos.GetString("Boton Config")

        commadBarTop.DefaultLabelPosition = CommandBarDefaultLabelPosition.Right

        botonInicioVotarTexto.Text = recursos.GetString("Boton Votar")
        botonInicioCompartirTexto.Text = recursos.GetString("Boton Compartir")
        botonInicioContactoTexto.Text = recursos.GetString("Boton Contactar")
        botonInicioMasAppsTexto.Text = recursos.GetString("Boton Web")

        tbRSS.Text = recursos.GetString("RSS")

        tbConfig.Text = recursos.GetString("Boton Config")
        tbSteamConfigInstruccionesCliente.Text = recursos.GetString("Texto Steam Config Cliente")
        buttonSteamConfigPathTexto.Text = recursos.GetString("Boton Añadir")
        tbSteamConfigPath.Text = recursos.GetString("Texto Steam No Config")

        buttonVolverTexto.Text = recursos.GetString("Boton Volver")

        buttonDescargaTextoAir.Text = recursos.GetString("Boton Descarga")
        tbScreenshotsAir.Text = recursos.GetString("Capturas")
        tbOpcionesAir.Text = recursos.GetString("Opciones")
        textBlockCreadoAir.Text = recursos.GetString("Creado Por")

        buttonDescargaTextoAirClassic.Text = recursos.GetString("Boton Descarga")
        tbScreenshotsAirClassic.Text = recursos.GetString("Capturas")
        tbOpcionesAirClassic.Text = recursos.GetString("Opciones")
        textBlockCreadoAirClassic.Text = recursos.GetString("Creado Por")

        buttonDescargaTextoCompact.Text = recursos.GetString("Boton Descarga")
        tbScreenshotsCompact.Text = recursos.GetString("Capturas")
        textBlockCreadoCompact.Text = recursos.GetString("Creado Por")

        buttonDescargaTextoInvert.Text = recursos.GetString("Boton Descarga")
        tbScreenshotsInvert.Text = recursos.GetString("Capturas")
        textBlockCreadoInvert.Text = recursos.GetString("Creado Por")

        buttonDescargaTextoMetro.Text = recursos.GetString("Boton Descarga")
        tbScreenshotsMetro.Text = recursos.GetString("Capturas")
        tbOpcionesMetro.Text = recursos.GetString("Opciones")
        textBlockCreadoMetro.Text = recursos.GetString("Creado Por")

        buttonDescargaTextoMinimal.Text = recursos.GetString("Boton Descarga")
        tbScreenshotsMinimal.Text = recursos.GetString("Capturas")
        tbOpcionesMinimal.Text = recursos.GetString("Opciones")
        textBlockCreadoMinimal.Text = recursos.GetString("Creado Por")

        buttonDescargaTextoPixelVision2.Text = recursos.GetString("Boton Descarga")
        tbScreenshotsPixelVision2.Text = recursos.GetString("Capturas")
        textBlockCreadoPixelVision2.Text = recursos.GetString("Creado Por")

        buttonDescargaTextoPressure2.Text = recursos.GetString("Boton Descarga")
        tbScreenshotsPressure2.Text = recursos.GetString("Capturas")
        tbOpcionesPressure2.Text = recursos.GetString("Opciones")
        textBlockCreadoPressure2.Text = recursos.GetString("Creado Por")

        buttonDescargaTextoThreshold.Text = recursos.GetString("Boton Descarga")
        tbScreenshotsThreshold.Text = recursos.GetString("Capturas")
        tbOpcionesThreshold.Text = recursos.GetString("Opciones")
        textBlockCreadoThreshold.Text = recursos.GetString("Creado Por")

        '----------------------------------------------

        tbConsejoConfig.Text = recursos.GetString("Consejo Config")
        tbInicioGrid.Text = recursos.GetString("Grid Arranque")

        cbItemArranqueInicio.Content = recursos.GetString("Boton Inicio")
        cbItemArranqueApariencias.Content = recursos.GetString("Skins")
        cbItemArranqueConfig.Content = recursos.GetString("Boton Config")

        If ApplicationData.Current.LocalSettings.Values("cbarranque") = Nothing Then
            cbArranque.SelectedIndex = 0
            ApplicationData.Current.LocalSettings.Values("cbarranque") = "0"
        Else
            cbArranque.SelectedIndex = ApplicationData.Current.LocalSettings.Values("cbarranque")

            If cbArranque.SelectedIndex = 0 Then
                GridVisibilidad(gridInicio, botonInicio, Nothing)
            ElseIf cbArranque.SelectedIndex = 1 Then
                GridVisibilidad(gridSkins, botonApariencias, Nothing)
                GridSkinVisibilidad(gridSkinAir, buttonSeleccionAir)
            ElseIf cbArranque.SelectedIndex = 2 Then
                GridVisibilidad(Nothing, botonConfig, gridConfig)
            Else
                GridVisibilidad(gridInicio, botonInicio, Nothing)
            End If
        End If

        tbVersionApp.Text = "App " + SystemInformation.ApplicationVersion.Major.ToString + "." + SystemInformation.ApplicationVersion.Minor.ToString + "." + SystemInformation.ApplicationVersion.Build.ToString + "." + SystemInformation.ApplicationVersion.Revision.ToString
        tbVersionWindows.Text = "Windows " + SystemInformation.OperatingSystemVersion.Major.ToString + "." + SystemInformation.OperatingSystemVersion.Minor.ToString + "." + SystemInformation.OperatingSystemVersion.Build.ToString + "." + SystemInformation.OperatingSystemVersion.Revision.ToString

        '--------------------------------------------------------

        Try
            RSS.Generar()
        Catch ex As Exception

        End Try

        Detector.Steam(False)

    End Sub

    '-----------------------------------------------------------------------------

    Public Sub GridVisibilidad(grid As Grid, boton As AppBarButton, sp As StackPanel)

        gridInicio.Visibility = Visibility.Collapsed
        gridSkins.Visibility = Visibility.Collapsed
        gridConfig.Visibility = Visibility.Collapsed
        gridWeb.Visibility = Visibility.Collapsed
        gridCaptura.Visibility = Visibility.Collapsed

        If Not sp Is Nothing Then
            sp.Visibility = Visibility.Visible
        Else
            grid.Visibility = Visibility.Visible
        End If

        botonInicio.BorderBrush = New SolidColorBrush(Colors.Transparent)
        botonInicio.BorderThickness = New Thickness(0, 0, 0, 0)
        botonApariencias.BorderBrush = New SolidColorBrush(Colors.Transparent)
        botonApariencias.BorderThickness = New Thickness(0, 0, 0, 0)
        botonConfig.BorderBrush = New SolidColorBrush(Colors.Transparent)
        botonConfig.BorderThickness = New Thickness(0, 0, 0, 0)

        If Not boton Is Nothing Then
            boton.BorderBrush = New SolidColorBrush(Colors.White)
            boton.BorderThickness = New Thickness(0, 2, 0, 0)
        End If

    End Sub

    Private Sub BotonInicio_Click(sender As Object, e As RoutedEventArgs) Handles botonInicio.Click

        GridVisibilidad(gridInicio, botonInicio, Nothing)

    End Sub

    Private Sub BotonTilesSteam_Click(sender As Object, e As RoutedEventArgs) Handles botonApariencias.Click

        GridVisibilidad(gridSkins, botonApariencias, Nothing)
        GridSkinVisibilidad(gridSkinAir, buttonSeleccionAir)

    End Sub

    Private Sub BotonConfig_Click(sender As Object, e As RoutedEventArgs) Handles botonConfig.Click

        GridVisibilidad(Nothing, botonConfig, gridConfig)

    End Sub

    Private Async Sub BotonInicioVotar_Click(sender As Object, e As RoutedEventArgs) Handles botonInicioVotar.Click

        Await Launcher.LaunchUriAsync(New Uri("ms-windows-store:REVIEW?PFN=" + Package.Current.Id.FamilyName))

    End Sub

    Private Sub BotonInicioCompartir_Click(sender As Object, e As RoutedEventArgs) Handles botonInicioCompartir.Click

        Dim datos As DataTransferManager = DataTransferManager.GetForCurrentView()
        AddHandler datos.DataRequested, AddressOf MainPage_DataRequested
        DataTransferManager.ShowShareUI()

    End Sub

    Private Sub MainPage_DataRequested(sender As DataTransferManager, e As DataRequestedEventArgs)

        Dim request As DataRequest = e.Request
        request.Data.SetText("Download: https://www.microsoft.com/store/apps/9nblggh55b7f")
        request.Data.Properties.Title = "Steam Skins"
        request.Data.Properties.Description = "Change the skin of Steam"

    End Sub

    Private Sub BotonInicioContacto_Click(sender As Object, e As RoutedEventArgs) Handles botonInicioContacto.Click

        GridVisibilidad(gridWeb, Nothing, Nothing)

    End Sub

    Private Sub BotonInicioMasApps_Click(sender As Object, e As RoutedEventArgs) Handles botonInicioMasApps.Click

        If spMasApps.Visibility = Visibility.Visible Then
            spMasApps.Visibility = Visibility.Collapsed
        Else
            spMasApps.Visibility = Visibility.Visible
        End If

    End Sub

    Private Async Sub BotonAppSteamTiles_Click(sender As Object, e As RoutedEventArgs) Handles botonAppSteamTiles.Click

        Await Launcher.LaunchUriAsync(New Uri("ms-windows-store://pdp/?productid=9nblggh51sb3"))

    End Sub

    Private Async Sub BotonAppSteamDeals_Click(sender As Object, e As RoutedEventArgs) Handles botonAppSteamDeals.Click

        Await Launcher.LaunchUriAsync(New Uri("ms-windows-store://pdp/?productid=9p7836m1tw15"))

    End Sub

    Private Async Sub BotonAppSteamCategories_Click(sender As Object, e As RoutedEventArgs) Handles botonAppSteamCategories.Click

        Await Launcher.LaunchUriAsync(New Uri("ms-windows-store://pdp/?productid=9p54scg1n6bm"))

    End Sub

    Private Async Sub BotonAppSteamBridge_Click(sender As Object, e As RoutedEventArgs) Handles botonAppSteamBridge.Click

        Await Launcher.LaunchUriAsync(New Uri("ms-windows-store://pdp/?productid=9nblggh441c9"))

    End Sub

    Private Async Sub LvRSSUpdates_ItemClick(sender As Object, e As ItemClickEventArgs) Handles lvRSSUpdates.ItemClick

        Dim feed As FeedRSS = e.ClickedItem
        Await Launcher.LaunchUriAsync(feed.Enlace)

    End Sub

    Private Sub CbArranque_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbArranque.SelectionChanged

        ApplicationData.Current.LocalSettings.Values("cbarranque") = cbArranque.SelectedIndex

    End Sub

    Private Async Sub BotonSocialTwitter_Click(sender As Object, e As RoutedEventArgs) Handles botonSocialTwitter.Click

        Await Launcher.LaunchUriAsync(New Uri("https://twitter.com/pepeizqapps"))

    End Sub

    Private Async Sub BotonSocialGitHub_Click(sender As Object, e As RoutedEventArgs) Handles botonSocialGitHub.Click

        Await Launcher.LaunchUriAsync(New Uri("https://github.com/pepeizq"))

    End Sub

    Private Async Sub BotonSocialPaypal_Click(sender As Object, e As RoutedEventArgs) Handles botonSocialPaypal.Click

        Await Launcher.LaunchUriAsync(New Uri("https://paypal.me/pepeizq/1"))

    End Sub

    '-----------------------------------------------------------------------------

    Private Async Sub ButtonSteamConfigPath_Click(sender As Object, e As RoutedEventArgs) Handles buttonSteamConfigPath.Click

        Detector.Steam(True)

        Dim carpeta As StorageFolder = Nothing

        Try
            carpeta = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteam")
        Catch ex As Exception

        End Try

        If Not carpeta Is Nothing Then
            GridVisibilidad(gridSkins, botonApariencias, Nothing)
            GridSkinVisibilidad(gridSkinAir, buttonSeleccionAir)

            For Each boton As Button In listaBotonesDescarga
                boton.IsEnabled = True
            Next
        Else
            GridVisibilidad(Nothing, botonConfig, gridConfig)
        End If

    End Sub

    '-----------------------------------------------------------------------------

    Private Sub AmpliarCaptura(imagen As ImageEx)

        GridVisibilidad(gridCaptura, Nothing, Nothing)
        imageCapturaExpandida.Source = imagen.Source

    End Sub

    Private Sub ButtonVolver_Click(sender As Object, e As RoutedEventArgs) Handles buttonVolver.Click

        GridVisibilidad(gridSkins, botonApariencias, Nothing)

    End Sub

    Private Sub GridSkinVisibilidad(grid As Grid, boton As Button)

        buttonSeleccionAir.Background = New SolidColorBrush(Colors.Transparent)
        buttonSeleccionAir.BorderBrush = New SolidColorBrush(Colors.Transparent)
        buttonSeleccionAirClassic.Background = New SolidColorBrush(Colors.Transparent)
        buttonSeleccionAirClassic.BorderBrush = New SolidColorBrush(Colors.Transparent)
        buttonSeleccionCompact.Background = New SolidColorBrush(Colors.Transparent)
        buttonSeleccionCompact.BorderBrush = New SolidColorBrush(Colors.Transparent)
        buttonSeleccionInvert.Background = New SolidColorBrush(Colors.Transparent)
        buttonSeleccionInvert.BorderBrush = New SolidColorBrush(Colors.Transparent)
        buttonSeleccionMetro.Background = New SolidColorBrush(Colors.Transparent)
        buttonSeleccionMetro.BorderBrush = New SolidColorBrush(Colors.Transparent)
        buttonSeleccionMinimal.Background = New SolidColorBrush(Colors.Transparent)
        buttonSeleccionMinimal.BorderBrush = New SolidColorBrush(Colors.Transparent)
        buttonSeleccionPixelVision2.Background = New SolidColorBrush(Colors.Transparent)
        buttonSeleccionPixelVision2.BorderBrush = New SolidColorBrush(Colors.Transparent)
        buttonSeleccionPressure2.Background = New SolidColorBrush(Colors.Transparent)
        buttonSeleccionPressure2.BorderBrush = New SolidColorBrush(Colors.Transparent)
        buttonSeleccionThreshold.Background = New SolidColorBrush(Colors.Transparent)
        buttonSeleccionThreshold.BorderBrush = New SolidColorBrush(Colors.Transparent)

        boton.Background = New SolidColorBrush(Colors.DarkCyan)
        boton.BorderBrush = New SolidColorBrush(Colors.White)

        gridSkinAir.Visibility = Visibility.Collapsed
        gridSkinAirClassic.Visibility = Visibility.Collapsed
        gridSkinCompact.Visibility = Visibility.Collapsed
        gridSkinInvert.Visibility = Visibility.Collapsed
        gridSkinMetro.Visibility = Visibility.Collapsed
        gridSkinMinimal.Visibility = Visibility.Collapsed
        gridSkinPixelVision2.Visibility = Visibility.Collapsed
        gridSkinPressure2.Visibility = Visibility.Collapsed
        gridSkinThreshold.Visibility = Visibility.Collapsed

        grid.Visibility = Visibility.Visible

    End Sub

    'SKINAIR-----------------------------------------------------------------------------

    Private Sub ButtonSeleccionAir_Click(sender As Object, e As RoutedEventArgs) Handles buttonSeleccionAir.Click

        GridSkinVisibilidad(gridSkinAir, buttonSeleccionAir)

    End Sub

    Private Async Sub ButtonDescargaAir_Click(sender As Object, e As RoutedEventArgs) Handles buttonDescargaAir.Click

        Dim listaOpciones As New List(Of String)

        Dim opcionTheme As ComboBoxItem = comboBoxOpcionAirTheme.SelectedValue
        listaOpciones.Add(opcionTheme.Content.ToString)

        Dim opcionColor As ComboBoxItem = comboBoxOpcionAirColor.SelectedValue
        listaOpciones.Add(opcionColor.Content.ToString)

        skinAir = New Skins("Air",
                                  New Uri("https://github.com/Outsetini/Air-for-Steam/archive/master.zip"),
                                  textBlockInformeAir,
                                  progressInformeAir,
                                  listaOpciones,
                                  gridOpcionesAir)

        Dim carpetaSteam As StorageFolder = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteam")
        Descarga.Iniciar(skinAir, carpetaSteam, buttonSteamConfigPath, listaBotonesDescarga)

    End Sub

    Private Async Sub ButtonWebAir_Click(sender As Object, e As RoutedEventArgs) Handles buttonWebAir.Click

        Await Launcher.LaunchUriAsync(New Uri("http://airforsteam.com"))

    End Sub

    Private Async Sub ButtonPatreonAir_Click(sender As Object, e As RoutedEventArgs) Handles buttonPatreonAir.Click

        Await Launcher.LaunchUriAsync(New Uri("https://www.patreon.com/inhibitor"))

    End Sub

    Private Sub ButtonImagePreview1Air_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview1Air.Click

        AmpliarCaptura(imagePreview1Air)

    End Sub

    Private Sub ButtonImagePreview2Air_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview2Air.Click

        AmpliarCaptura(imagePreview2Air)

    End Sub

    Private Sub ButtonImagePreview3Air_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview3Air.Click

        AmpliarCaptura(imagePreview3Air)

    End Sub

    Private Sub ButtonImagePreview4Air_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview4Air.Click

        AmpliarCaptura(imagePreview4Air)

    End Sub

    'SKINAIRCLASSIC-----------------------------------------------------------------------------

    Private Sub ButtonSeleccionAirClassic_Click(sender As Object, e As RoutedEventArgs) Handles buttonSeleccionAirClassic.Click

        GridSkinVisibilidad(gridSkinAirClassic, buttonSeleccionAirClassic)

    End Sub

    Private Async Sub ButtonDescargaAirClassic_Click(sender As Object, e As RoutedEventArgs) Handles buttonDescargaAirClassic.Click

        Dim listaOpciones As New List(Of String)

        Dim opcionColor As ComboBoxItem = comboBoxOpcionAirClassicColor.SelectedValue
        listaOpciones.Add(opcionColor.Content.ToString)

        Dim opcionGameDetails As ComboBoxItem = comboBoxOpcionAirClassicGamesDetails.SelectedValue
        listaOpciones.Add(opcionGameDetails.Content.ToString)

        Dim opcionBackground As ComboBoxItem = comboBoxOpcionAirClassicBackground.SelectedValue
        listaOpciones.Add(opcionBackground.Content.ToString)

        skinAirClassic = New Skins("Air-Classic",
                                  New Uri("https://github.com/Outsetini/Air-Classic/archive/master.zip"),
                                  textBlockInformeAirClassic,
                                  progressInformeAirClassic,
                                  listaOpciones,
                                  gridOpcionesAirClassic)

        Dim carpetaSteam As StorageFolder = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteam")
        Descarga.Iniciar(skinAirClassic, carpetaSteam, buttonSteamConfigPath, listaBotonesDescarga)

    End Sub

    Private Async Sub ButtonWebAirClassic_Click(sender As Object, e As RoutedEventArgs) Handles buttonWebAirClassic.Click

        Await Launcher.LaunchUriAsync(New Uri("http://airforsteam.com"))

    End Sub

    Private Async Sub ButtonPatreonAirClassic_Click(sender As Object, e As RoutedEventArgs) Handles buttonPatreonAirClassic.Click

        Await Launcher.LaunchUriAsync(New Uri("https://www.patreon.com/inhibitor"))

    End Sub

    Private Sub ButtonImagePreview1AirClassic_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview1AirClassic.Click

        AmpliarCaptura(imagePreview1AirClassic)

    End Sub

    Private Sub ButtonImagePreview2AirClassic_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview2AirClassic.Click

        AmpliarCaptura(imagePreview2AirClassic)

    End Sub

    Private Sub ButtonImagePreview3AirClassic_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview3AirClassic.Click

        AmpliarCaptura(imagePreview3AirClassic)

    End Sub

    Private Sub ButtonImagePreview4AirClassic_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview4AirClassic.Click

        AmpliarCaptura(imagePreview4AirClassic)

    End Sub

    'SKINCOMPACT-----------------------------------------------------------------------------

    Private Sub ButtonSeleccionCompact_Click(sender As Object, e As RoutedEventArgs) Handles buttonSeleccionCompact.Click

        GridSkinVisibilidad(gridSkinCompact, buttonSeleccionCompact)

    End Sub

    Private Async Sub ButtonDescargaCompact_Click(sender As Object, e As RoutedEventArgs) Handles buttonDescargaCompact.Click

        skinCompact = New Skins("Compact",
                                  New Uri("https://github.com/badanka/Compact/archive/master.zip"),
                                  textBlockInformeCompact,
                                  progressInformeCompact,
                                  Nothing, Nothing)

        Dim carpetaSteam As StorageFolder = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteam")
        Descarga.Iniciar(skinCompact, carpetaSteam, buttonSteamConfigPath, listaBotonesDescarga)

    End Sub

    Private Async Sub ButtonWebCompact_Click(sender As Object, e As RoutedEventArgs) Handles buttonWebCompact.Click

        Await Launcher.LaunchUriAsync(New Uri("http://badanka.github.io/Compact/"))

    End Sub

    Private Sub ButtonImagePreview1Compact_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview1Compact.Click

        AmpliarCaptura(imagePreview1Compact)

    End Sub

    Private Sub ButtonImagePreview2Compact_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview2Compact.Click

        AmpliarCaptura(imagePreview2Compact)

    End Sub

    Private Sub ButtonImagePreview3Compact_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview3Compact.Click

        AmpliarCaptura(imagePreview3Compact)

    End Sub

    Private Sub ButtonImagePreview4Compact_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview4Compact.Click

        AmpliarCaptura(imagePreview4Compact)

    End Sub

    'SKININVERT-----------------------------------------------------------------------------

    Private Sub ButtonSeleccionInvert_Click(sender As Object, e As RoutedEventArgs) Handles buttonSeleccionInvert.Click

        GridSkinVisibilidad(gridSkinInvert, buttonSeleccionInvert)

    End Sub

    Private Async Sub ButtonDescargaInvert_Click(sender As Object, e As RoutedEventArgs) Handles buttonDescargaInvert.Click

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
                                  textBlockInformeInvert,
                                  progressInformeInvert,
                                  Nothing, Nothing)

        Dim carpetaSteam As StorageFolder = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteam")
        Descarga.Iniciar(skinInvert, carpetaSteam, buttonSteamConfigPath, listaBotonesDescarga)

    End Sub

    Private Async Sub ButtonWebInvert_Click(sender As Object, e As RoutedEventArgs) Handles buttonWebInvert.Click

        Await Launcher.LaunchUriAsync(New Uri("http://gamebanana.com/guis/28814"))

    End Sub

    Private Async Sub ButtonPaypalInvert_Click(sender As Object, e As RoutedEventArgs) Handles buttonPaypalInvert.Click

        Await Launcher.LaunchUriAsync(New Uri("https://www.paypal.com/sk/cgi-bin/webscr?cmd=_flow&SESSION=XyLR0yV_beniveGJ0ONtunwPaUfnuwR7BtktA-E2xhApEH_8hG8e2s_Frm0&dispatch=5885d80a13c0db1f8e263663d3faee8d4fe1dd75ca3bd4f11d72275b28239088"))

    End Sub

    Private Sub ButtonImagePreview1Invert_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview1Invert.Click

        AmpliarCaptura(imagePreview1Invert)

    End Sub

    Private Sub ButtonImagePreview2Invert_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview2Invert.Click

        AmpliarCaptura(imagePreview2Invert)

    End Sub

    Private Sub ButtonImagePreview3Invert_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview3Invert.Click

        AmpliarCaptura(imagePreview3Invert)

    End Sub

    Private Sub ButtonImagePreview4Invert_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview4Invert.Click

        AmpliarCaptura(imagePreview4Invert)

    End Sub

    'SKINMETRO-----------------------------------------------------------------------------

    Private Sub ButtonSeleccionMetro_Click(sender As Object, e As RoutedEventArgs) Handles buttonSeleccionMetro.Click

        GridSkinVisibilidad(gridSkinMetro, buttonSeleccionMetro)

    End Sub

    Private Async Sub ButtonDescargaMetro_Click(sender As Object, e As RoutedEventArgs) Handles buttonDescargaMetro.Click

        buttonDescargaMetro.IsEnabled = False

        Dim listaOpciones As New List(Of String)

        Dim opcionColor As ComboBoxItem = comboBoxOpcionMetroColor.SelectedValue
        listaOpciones.Add(opcionColor.Content.ToString)

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
                                  textBlockInformeMetro,
                                  progressInformeMetro,
                                  listaOpciones,
                                  gridOpcionesMetro)

        Dim carpetaSteam As StorageFolder = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteam")
        Descarga.Iniciar(skinMetro, carpetaSteam, buttonSteamConfigPath, listaBotonesDescarga)

    End Sub

    Private Async Sub ButtonWebMetro_Click(sender As Object, e As RoutedEventArgs) Handles buttonWebMetro.Click

        Await Launcher.LaunchUriAsync(New Uri("http://www.metroforsteam.com"))

    End Sub

    Private Async Sub ButtonPaypalMetro_Click(sender As Object, e As RoutedEventArgs) Handles buttonPaypalMetro.Click

        Await Launcher.LaunchUriAsync(New Uri("https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=BDL2J3MEETZ3J&lc=US&item_name=Metro%20for%20Steam&item_number=metroforsteam&currency_code=USD&bn=PP%2dDonationsBF%3abtn_donate_LG%2egif%3aNonHosted"))

    End Sub

    Private Async Sub ButtonPatreonMetro_Click(sender As Object, e As RoutedEventArgs) Handles buttonPatreonMetro.Click

        Await Launcher.LaunchUriAsync(New Uri("http://www.patreon.com/dommini"))

    End Sub

    Private Sub ButtonImagePreview1Metro_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview1Metro.Click

        AmpliarCaptura(imagePreview1Metro)

    End Sub

    Private Sub ButtonImagePreview2Metro_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview2Metro.Click

        AmpliarCaptura(imagePreview2Metro)

    End Sub

    Private Sub ButtonImagePreview3Metro_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview3Metro.Click

        AmpliarCaptura(imagePreview3Metro)

    End Sub

    Private Sub ButtonImagePreview4Metro_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview4Metro.Click

        AmpliarCaptura(imagePreview4Metro)

    End Sub

    'SKINMINIMAL-----------------------------------------------------------------------------

    Private Sub ButtonSeleccionMinimal_Click(sender As Object, e As RoutedEventArgs) Handles buttonSeleccionMinimal.Click

        GridSkinVisibilidad(gridSkinMinimal, buttonSeleccionMinimal)

    End Sub

    Private Async Sub ButtonDescargaMinimal_Click(sender As Object, e As RoutedEventArgs) Handles buttonDescargaMinimal.Click

        buttonDescargaMinimal.IsEnabled = False

        Dim listaOpciones As New List(Of String)

        Dim opcionColor As ComboBoxItem = comboBoxOpcionMinimalColor.SelectedValue
        listaOpciones.Add(opcionColor.Content.ToString)

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
                                  textBlockInformeMinimal,
                                  progressInformeMinimal,
                                  listaOpciones,
                                  gridOpcionesMinimal)

        Dim carpetaSteam As StorageFolder = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteam")
        Descarga.Iniciar(skinMinimal, carpetaSteam, buttonSteamConfigPath, listaBotonesDescarga)

    End Sub

    Private Async Sub ButtonWebMinimal_Click(sender As Object, e As RoutedEventArgs) Handles buttonWebMinimal.Click

        Await Launcher.LaunchUriAsync(New Uri("http://steamcommunity.com/groups/MinimalSteamUI"))

    End Sub

    Private Sub ButtonImagePreview1Minimal_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview1Minimal.Click

        AmpliarCaptura(imagePreview1Minimal)

    End Sub

    Private Sub ButtonImagePreview2Minimal_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview2Minimal.Click

        AmpliarCaptura(imagePreview2Minimal)

    End Sub

    Private Sub ButtonImagePreview3Minimal_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview3Minimal.Click

        AmpliarCaptura(imagePreview3Minimal)

    End Sub

    Private Sub ButtonImagePreview4Minimal_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview4Minimal.Click

        AmpliarCaptura(imagePreview4Minimal)

    End Sub

    'SKINPIXELVISION2-----------------------------------------------------------------------------

    Private Sub ButtonSeleccionPixelVision2_Click(sender As Object, e As RoutedEventArgs) Handles buttonSeleccionPixelVision2.Click

        GridSkinVisibilidad(gridSkinPixelVision2, buttonSeleccionPixelVision2)

    End Sub

    Private Async Sub ButtonDescargaPixelVision2_Click(sender As Object, e As RoutedEventArgs) Handles buttonDescargaPixelVision2.Click

        skinPixelVision2 = New Skins("PixelVision2",
                                  New Uri("https://github.com/somini/Pixelvision2/archive/master.zip"),
                                  textBlockInformePixelVision2,
                                  progressInformePixelVision2,
                                  Nothing, Nothing)

        Dim carpetaSteam As StorageFolder = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteam")
        Descarga.Iniciar(skinPixelVision2, carpetaSteam, buttonSteamConfigPath, listaBotonesDescarga)

    End Sub

    Private Async Sub ButtonWebPixelVision2_Click(sender As Object, e As RoutedEventArgs) Handles buttonWebPixelVision2.Click

        Await Launcher.LaunchUriAsync(New Uri("https://steamcommunity.com/groups/pixelvision2"))

    End Sub

    Private Sub ButtonImagePreview1PixelVision2_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview1PixelVision2.Click

        AmpliarCaptura(imagePreview1PixelVision2)

    End Sub

    Private Sub ButtonImagePreview2PixelVision2_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview2PixelVision2.Click

        AmpliarCaptura(imagePreview2PixelVision2)

    End Sub

    Private Sub ButtonImagePreview3PixelVision2_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview3PixelVision2.Click

        AmpliarCaptura(imagePreview3PixelVision2)

    End Sub

    Private Sub ButtonImagePreview4PixelVision2_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview4PixelVision2.Click

        AmpliarCaptura(imagePreview4PixelVision2)

    End Sub

    'SKINPRESSURE2-----------------------------------------------------------------------------

    Private Sub ButtonSeleccionPressure2_Click(sender As Object, e As RoutedEventArgs) Handles buttonSeleccionPressure2.Click

        GridSkinVisibilidad(gridSkinPressure2, buttonSeleccionPressure2)

    End Sub

    Private Async Sub ButtonDescargaPressure2_Click(sender As Object, e As RoutedEventArgs) Handles buttonDescargaPressure2.Click

        Dim listaOpciones As New List(Of String)

        Dim opcionGridTransparent As ComboBoxItem = comboBoxOpcionPressure2GridTransparent.SelectedValue
        listaOpciones.Add(opcionGridTransparent.Content.ToString)

        Dim opcionOverlayBackground As ComboBoxItem = comboBoxOpcionPressure2OverlayBackground.SelectedValue
        listaOpciones.Add(opcionOverlayBackground.Content.ToString)

        skinPressure2 = New Skins("Pressure2",
                                  New Uri("https://github.com/DirtDiglett/Pressure2/archive/master.zip"),
                                  textBlockInformePressure2,
                                  progressInformePressure2,
                                  listaOpciones,
                                  gridOpcionesPressure2)

        Dim carpetaSteam As StorageFolder = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteam")
        Descarga.Iniciar(skinPressure2, carpetaSteam, buttonSteamConfigPath, listaBotonesDescarga)

    End Sub

    Private Async Sub ButtonWebPressure2_Click(sender As Object, e As RoutedEventArgs) Handles buttonWebPressure2.Click

        Await Launcher.LaunchUriAsync(New Uri("http://www.pressureforsteam.com"))

    End Sub

    Private Async Sub ButtonPaypalPressure2_Click(sender As Object, e As RoutedEventArgs) Handles buttonPaypalPressure2.Click

        Await Launcher.LaunchUriAsync(New Uri("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=WAC672ATU68E4"))

    End Sub

    Private Async Sub ButtonPatreonPressure2_Click(sender As Object, e As RoutedEventArgs) Handles buttonPatreonPressure2.Click

        Await Launcher.LaunchUriAsync(New Uri("https://www.patreon.com/dirtdiglett"))

    End Sub

    Private Sub ButtonImagePreview1Pressure2_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview1Pressure2.Click

        AmpliarCaptura(imagePreview1Pressure2)

    End Sub

    Private Sub ButtonImagePreview2Pressure2_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview2Pressure2.Click

        AmpliarCaptura(imagePreview2Pressure2)

    End Sub

    Private Sub ButtonImagePreview3Pressure2_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview3Pressure2.Click

        AmpliarCaptura(imagePreview3Pressure2)

    End Sub

    Private Sub ButtonImagePreview4Pressure2_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview4Pressure2.Click

        AmpliarCaptura(imagePreview4Pressure2)

    End Sub

    'SKINTHRESHOLD-----------------------------------------------------------------------------

    Private Sub ButtonSeleccionThreshold_Click(sender As Object, e As RoutedEventArgs) Handles buttonSeleccionThreshold.Click

        GridSkinVisibilidad(gridSkinThreshold, buttonSeleccionThreshold)

    End Sub

    Private Async Sub ButtonDescargaThreshold_Click(sender As Object, e As RoutedEventArgs) Handles buttonDescargaThreshold.Click

        Dim listaOpciones As New List(Of String)

        Dim opcionColor As ComboBoxItem = comboBoxOpcionThresholdColor.SelectedValue
        listaOpciones.Add(opcionColor.Content.ToString)

        Dim opcionOutlines As ComboBoxItem = comboBoxOpcionThresholdOutlines.SelectedValue
        listaOpciones.Add(opcionOutlines.Content.ToString)

        Dim opcionTitlebar As ComboBoxItem = comboBoxOpcionThresholdColoredTitlebar.SelectedValue
        listaOpciones.Add(opcionTitlebar.Content.ToString)

        skinThreshold = New Skins("Threshold",
                                  New Uri("https://github.com/Edgarware/Threshold-Skin/archive/master.zip"),
                                  textBlockInformeThreshold,
                                  progressInformeThreshold,
                                  listaOpciones,
                                  gridOpcionesThreshold)

        Dim carpetaSteam As StorageFolder = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteam")
        Descarga.Iniciar(skinThreshold, carpetaSteam, buttonSteamConfigPath, listaBotonesDescarga)

    End Sub

    Private Async Sub ButtonWebThreshold_Click(sender As Object, e As RoutedEventArgs) Handles buttonWebThreshold.Click

        Await Launcher.LaunchUriAsync(New Uri("https://steamcommunity.com/groups/thresholdskin"))

    End Sub

    Private Sub ButtonImagePreview1Threshold_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview1Threshold.Click

        AmpliarCaptura(imagePreview1Threshold)

    End Sub

    Private Sub ButtonImagePreview2Threshold_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview2Threshold.Click

        AmpliarCaptura(imagePreview2Threshold)

    End Sub

    Private Sub ButtonImagePreview3Threshold_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview3Threshold.Click

        AmpliarCaptura(imagePreview3Threshold)

    End Sub

    Private Sub ButtonImagePreview4Threshold_Click(sender As Object, e As RoutedEventArgs) Handles buttonImagePreview4Threshold.Click

        AmpliarCaptura(imagePreview4Threshold)

    End Sub

End Class
