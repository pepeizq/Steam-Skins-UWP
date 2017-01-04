Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.ApplicationModel.DataTransfer
Imports Windows.Storage
Imports Windows.Storage.AccessCache
Imports Windows.System
Imports Windows.UI

Public NotInheritable Class MainPage
    Inherits Page

    Dim skinAir, skinAirClassic, skinCompact, skinInvert, skinMetro, skinMinimal, skinPixelVision2, skinPlexed, skinPressure2, skinThreshold As Skins
    Dim listaBotonesDescarga As List(Of Button)

    Private Async Sub Page_Loaded(sender As FrameworkElement, args As Object)

        Dim barra As ApplicationViewTitleBar = ApplicationView.GetForCurrentView().TitleBar

        barra.BackgroundColor = Colors.DarkCyan
        barra.ForegroundColor = Colors.White
        barra.ButtonBackgroundColor = Colors.DarkCyan
        barra.ButtonForegroundColor = Colors.White

        '----------------------------------------------

        listaBotonesDescarga = New List(Of Button)
        listaBotonesDescarga.Add(buttonDescargaAir)
        listaBotonesDescarga.Add(buttonDescargaAirClassic)
        listaBotonesDescarga.Add(buttonDescargaCompact)
        listaBotonesDescarga.Add(buttonDescargaInvert)
        listaBotonesDescarga.Add(buttonDescargaMetro)
        listaBotonesDescarga.Add(buttonDescargaMinimal)
        listaBotonesDescarga.Add(buttonDescargaPixelVision2)
        listaBotonesDescarga.Add(buttonDescargaPlexed)
        listaBotonesDescarga.Add(buttonDescargaPressure2)
        listaBotonesDescarga.Add(buttonDescargaThreshold)

        '----------------------------------------------

        Dim recursos As Resources.ResourceLoader = New Resources.ResourceLoader()

        menuItemSkins.Label = recursos.GetString("Skins")
        menuItemConfig.Label = recursos.GetString("Boton Configuracion")
        menuItemVote.Label = recursos.GetString("Boton Votar")
        menuItemShare.Label = recursos.GetString("Boton Compartir")
        menuItemContact.Label = recursos.GetString("Boton Contactar")
        menuItemWeb.Label = recursos.GetString("Boton Web")

        tbConfig.Text = recursos.GetString("Boton Configuracion")
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

        buttonDescargaTextoPlexed.Text = recursos.GetString("Boton Descarga")
        tbScreenshotsPlexed.Text = recursos.GetString("Capturas")
        textBlockCreadoPlexed.Text = recursos.GetString("Creado Por")

        buttonDescargaTextoPressure2.Text = recursos.GetString("Boton Descarga")
        tbScreenshotsPressure2.Text = recursos.GetString("Capturas")
        tbOpcionesPressure2.Text = recursos.GetString("Opciones")
        textBlockCreadoPressure2.Text = recursos.GetString("Creado Por")

        buttonDescargaTextoThreshold.Text = recursos.GetString("Boton Descarga")
        tbScreenshotsThreshold.Text = recursos.GetString("Capturas")
        tbOpcionesThreshold.Text = recursos.GetString("Opciones")
        textBlockCreadoThreshold.Text = recursos.GetString("Creado Por")

        '----------------------------------------------

        Detector.Steam(tbSteamConfigPath, buttonSteamConfigPathTexto, False)

        Dim carpeta As StorageFolder = Nothing

        Try
            carpeta = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteam")
        Catch ex As Exception

        End Try

        If Not carpeta Is Nothing Then
            GridVisibilidad(gridSkins)
            GridSkinVisibilidad(gridSkinAir, buttonSeleccionAir)

            For Each boton As Button In listaBotonesDescarga
                boton.IsEnabled = True
            Next
        Else
            GridVisibilidad(gridConfig)
        End If

        '----------------------------------------------

        Dim coleccion As HamburgerMenuItemCollection = hamburgerMaestro.ItemsSource
        hamburgerMaestro.ItemsSource = Nothing
        hamburgerMaestro.ItemsSource = coleccion

        Dim coleccionOpciones As HamburgerMenuItemCollection = hamburgerMaestro.OptionsItemsSource
        hamburgerMaestro.OptionsItemsSource = Nothing
        hamburgerMaestro.OptionsItemsSource = coleccionOpciones

    End Sub

    '-----------------------------------------------------------------------------

    Public Sub GridVisibilidad(grid As Grid)

        gridSkins.Visibility = Visibility.Collapsed
        gridConfig.Visibility = Visibility.Collapsed
        gridWebContacto.Visibility = Visibility.Collapsed
        gridWeb.Visibility = Visibility.Collapsed
        gridCaptura.Visibility = Visibility.Collapsed

        grid.Visibility = Visibility.Visible

    End Sub

    Private Async Sub buttonSteamConfigPath_Click(sender As Object, e As RoutedEventArgs) Handles buttonSteamConfigPath.Click

        Detector.Steam(tbSteamConfigPath, buttonSteamConfigPathTexto, True)

        Dim carpeta As StorageFolder = Nothing

        Try
            carpeta = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteam")
        Catch ex As Exception

        End Try

        If Not carpeta Is Nothing Then
            GridVisibilidad(gridSkins)
            GridSkinVisibilidad(gridSkinAir, buttonSeleccionAir)

            For Each boton As Button In listaBotonesDescarga
                boton.IsEnabled = True
            Next
        Else
            GridVisibilidad(gridConfig)
        End If

    End Sub


    Private Sub hamburgerMaestro_ItemClick(sender As Object, e As ItemClickEventArgs) Handles hamburgerMaestro.ItemClick

        Dim menuItem As HamburgerMenuGlyphItem = TryCast(e.ClickedItem, HamburgerMenuGlyphItem)

        If menuItem.Tag = 1 Then
            GridVisibilidad(gridSkins)
            GridSkinVisibilidad(gridSkinAir, buttonSeleccionAir)
        End If

    End Sub

    Private Async Sub hamburgerMaestro_OptionsItemClick(sender As Object, e As ItemClickEventArgs) Handles hamburgerMaestro.OptionsItemClick

        Dim menuItem As HamburgerMenuGlyphItem = TryCast(e.ClickedItem, HamburgerMenuGlyphItem)

        If menuItem.Tag = 99 Then
            GridVisibilidad(gridConfig)
        ElseIf menuItem.Tag = 100 Then
            Await Launcher.LaunchUriAsync(New Uri("ms-windows-store:REVIEW?PFN=" + Package.Current.Id.FamilyName))
        ElseIf menuItem.Tag = 101 Then
            Dim datos As DataTransferManager = DataTransferManager.GetForCurrentView()
            AddHandler datos.DataRequested, AddressOf MainPage_DataRequested
            DataTransferManager.ShowShareUI()
        ElseIf menuItem.Tag = 102 Then
            GridVisibilidad(gridWebContacto)
        ElseIf menuItem.Tag = 103 Then
            GridVisibilidad(gridWeb)
        End If

    End Sub

    Private Sub MainPage_DataRequested(sender As DataTransferManager, e As DataRequestedEventArgs)

        Dim request As DataRequest = e.Request
        request.Data.SetText("Steam Skins")
        request.Data.Properties.Title = "Steam Skins"
        request.Data.Properties.Description = "Change the skin of Steam"

    End Sub

    '-----------------------------------------------------------------------------

    Private Sub AmpliarCaptura(imagen As ImageEx)

        GridVisibilidad(gridCaptura)
        imageCapturaExpandida.Source = imagen.Source

    End Sub

    Private Sub buttonVolver_Click(sender As Object, e As RoutedEventArgs) Handles buttonVolver.Click

        GridVisibilidad(gridSkins)

    End Sub

    Private Sub GridSkinVisibilidad(grid As Grid, button As Button)

        buttonSeleccionAir.Background = New SolidColorBrush(Microsoft.Toolkit.Uwp.ColorHelper.ToColor("#e3e3e3"))
        buttonSeleccionAir.BorderBrush = New SolidColorBrush(Colors.Transparent)
        buttonSeleccionAirClassic.Background = New SolidColorBrush(Microsoft.Toolkit.Uwp.ColorHelper.ToColor("#e3e3e3"))
        buttonSeleccionAirClassic.BorderBrush = New SolidColorBrush(Colors.Transparent)
        buttonSeleccionCompact.Background = New SolidColorBrush(Microsoft.Toolkit.Uwp.ColorHelper.ToColor("#e3e3e3"))
        buttonSeleccionCompact.BorderBrush = New SolidColorBrush(Colors.Transparent)
        buttonSeleccionInvert.Background = New SolidColorBrush(Microsoft.Toolkit.Uwp.ColorHelper.ToColor("#e3e3e3"))
        buttonSeleccionInvert.BorderBrush = New SolidColorBrush(Colors.Transparent)
        buttonSeleccionMetro.Background = New SolidColorBrush(Microsoft.Toolkit.Uwp.ColorHelper.ToColor("#e3e3e3"))
        buttonSeleccionMetro.BorderBrush = New SolidColorBrush(Colors.Transparent)
        buttonSeleccionMinimal.Background = New SolidColorBrush(Microsoft.Toolkit.Uwp.ColorHelper.ToColor("#e3e3e3"))
        buttonSeleccionMinimal.BorderBrush = New SolidColorBrush(Colors.Transparent)
        buttonSeleccionPixelVision2.Background = New SolidColorBrush(Microsoft.Toolkit.Uwp.ColorHelper.ToColor("#e3e3e3"))
        buttonSeleccionPixelVision2.BorderBrush = New SolidColorBrush(Colors.Transparent)
        buttonSeleccionPlexed.Background = New SolidColorBrush(Microsoft.Toolkit.Uwp.ColorHelper.ToColor("#e3e3e3"))
        buttonSeleccionPlexed.BorderBrush = New SolidColorBrush(Colors.Transparent)
        buttonSeleccionPressure2.Background = New SolidColorBrush(Microsoft.Toolkit.Uwp.ColorHelper.ToColor("#e3e3e3"))
        buttonSeleccionPressure2.BorderBrush = New SolidColorBrush(Colors.Transparent)
        buttonSeleccionThreshold.Background = New SolidColorBrush(Microsoft.Toolkit.Uwp.ColorHelper.ToColor("#e3e3e3"))
        buttonSeleccionThreshold.BorderBrush = New SolidColorBrush(Colors.Transparent)

        button.Background = New SolidColorBrush(Microsoft.Toolkit.Uwp.ColorHelper.ToColor("#bfbfbf"))
        button.BorderBrush = New SolidColorBrush(Colors.Black)

        gridSkinAir.Visibility = Visibility.Collapsed
        gridSkinAirClassic.Visibility = Visibility.Collapsed
        gridSkinCompact.Visibility = Visibility.Collapsed
        gridSkinInvert.Visibility = Visibility.Collapsed
        gridSkinMetro.Visibility = Visibility.Collapsed
        gridSkinMinimal.Visibility = Visibility.Collapsed
        gridSkinPixelVision2.Visibility = Visibility.Collapsed
        gridSkinPlexed.Visibility = Visibility.Collapsed
        gridSkinPressure2.Visibility = Visibility.Collapsed
        gridSkinThreshold.Visibility = Visibility.Collapsed

        grid.Visibility = Visibility.Visible

    End Sub

    'SKINAIR-----------------------------------------------------------------------------

    Private Sub buttonSeleccionAir_Click(sender As Object, e As RoutedEventArgs) Handles buttonSeleccionAir.Click

        GridSkinVisibilidad(gridSkinAir, buttonSeleccionAir)

    End Sub

    Private Async Sub buttonDescargaAir_Click(sender As Object, e As RoutedEventArgs) Handles buttonDescargaAir.Click

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

    Private Sub buttonSeleccionAirClassic_Click(sender As Object, e As RoutedEventArgs) Handles buttonSeleccionAirClassic.Click

        GridSkinVisibilidad(gridSkinAirClassic, buttonSeleccionAirClassic)

    End Sub

    Private Async Sub buttonDescargaAirClassic_Click(sender As Object, e As RoutedEventArgs) Handles buttonDescargaAirClassic.Click

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

    Private Sub buttonSeleccionCompact_Click(sender As Object, e As RoutedEventArgs) Handles buttonSeleccionCompact.Click

        GridSkinVisibilidad(gridSkinCompact, buttonSeleccionCompact)

    End Sub

    Private Async Sub buttonDescargaCompact_Click(sender As Object, e As RoutedEventArgs) Handles buttonDescargaCompact.Click

        skinCompact = New Skins("Compact",
                                  New Uri("https://github.com/badanka/Compact/archive/master.zip"),
                                  textBlockInformeCompact,
                                  progressInformeCompact,
                                  Nothing, Nothing)

        Dim carpetaSteam As StorageFolder = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteam")
        Descarga.Iniciar(skinCompact, carpetaSteam, buttonSteamConfigPath, listaBotonesDescarga)

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

    Private Sub buttonSeleccionInvert_Click(sender As Object, e As RoutedEventArgs) Handles buttonSeleccionInvert.Click

        GridSkinVisibilidad(gridSkinInvert, buttonSeleccionInvert)

    End Sub

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
                                  textBlockInformeInvert,
                                  progressInformeInvert,
                                  Nothing, Nothing)

        Dim carpetaSteam As StorageFolder = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteam")
        Descarga.Iniciar(skinInvert, carpetaSteam, buttonSteamConfigPath, listaBotonesDescarga)

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

    Private Sub buttonSeleccionMetro_Click(sender As Object, e As RoutedEventArgs) Handles buttonSeleccionMetro.Click

        GridSkinVisibilidad(gridSkinMetro, buttonSeleccionMetro)

    End Sub

    Private Async Sub buttonDescargaMetro_Click(sender As Object, e As RoutedEventArgs) Handles buttonDescargaMetro.Click

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

    Private Sub buttonSeleccionMinimal_Click(sender As Object, e As RoutedEventArgs) Handles buttonSeleccionMinimal.Click

        GridSkinVisibilidad(gridSkinMinimal, buttonSeleccionMinimal)

    End Sub

    Private Async Sub buttonDescargaMinimal_Click(sender As Object, e As RoutedEventArgs) Handles buttonDescargaMinimal.Click

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

    Private Sub buttonSeleccionPixelVision2_Click(sender As Object, e As RoutedEventArgs) Handles buttonSeleccionPixelVision2.Click

        GridSkinVisibilidad(gridSkinPixelVision2, buttonSeleccionPixelVision2)

    End Sub

    Private Async Sub buttonDescargaPixelVision2_Click(sender As Object, e As RoutedEventArgs) Handles buttonDescargaPixelVision2.Click

        skinPixelVision2 = New Skins("PixelVision2",
                                  New Uri("https://github.com/somini/Pixelvision2/archive/master.zip"),
                                  textBlockInformePixelVision2,
                                  progressInformePixelVision2,
                                  Nothing, Nothing)

        Dim carpetaSteam As StorageFolder = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteam")
        Descarga.Iniciar(skinPixelVision2, carpetaSteam, buttonSteamConfigPath, listaBotonesDescarga)

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

    Private Sub buttonSeleccionPlexed_Click(sender As Object, e As RoutedEventArgs) Handles buttonSeleccionPlexed.Click

        GridSkinVisibilidad(gridSkinPlexed, buttonSeleccionPlexed)

    End Sub

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
                                  textBlockInformePlexed,
                                  progressInformePlexed,
                                  Nothing, Nothing)

        Dim carpetaSteam As StorageFolder = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteam")
        Descarga.Iniciar(skinPlexed, carpetaSteam, buttonSteamConfigPath, listaBotonesDescarga)

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

    Private Sub buttonSeleccionPressure2_Click(sender As Object, e As RoutedEventArgs) Handles buttonSeleccionPressure2.Click

        GridSkinVisibilidad(gridSkinPressure2, buttonSeleccionPressure2)

    End Sub

    Private Async Sub buttonDescargaPressure2_Click(sender As Object, e As RoutedEventArgs) Handles buttonDescargaPressure2.Click

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

    Private Sub buttonSeleccionThreshold_Click(sender As Object, e As RoutedEventArgs) Handles buttonSeleccionThreshold.Click

        GridSkinVisibilidad(gridSkinThreshold, buttonSeleccionThreshold)

    End Sub

    Private Async Sub buttonDescargaThreshold_Click(sender As Object, e As RoutedEventArgs) Handles buttonDescargaThreshold.Click

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



End Class
