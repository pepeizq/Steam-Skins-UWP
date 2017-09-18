Imports Microsoft.Services.Store.Engagement
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.ApplicationModel.Core
Imports Windows.Storage
Imports Windows.Storage.AccessCache
Imports Windows.System
Imports Windows.UI

Public NotInheritable Class MainPage
    Inherits Page

    Dim skinAir, skinAirClassic, skinCompact, skinInvert, skinMetro, skinMinimal, skinPixelVision2, skinPressure2, skinThreshold As Apariencia

    Private Sub Nv_Loaded(sender As Object, e As RoutedEventArgs)

        Dim recursos As New Resources.ResourceLoader()

        nvPrincipal.MenuItems.Add(NavigationViewItems.Generar(recursos.GetString("Skins"), New SymbolIcon(Symbol.Home), 0))
        nvPrincipal.MenuItems.Add(New NavigationViewItemSeparator)
        nvPrincipal.MenuItems.Add(NavigationViewItems.Generar(recursos.GetString("Config"), New SymbolIcon(Symbol.Setting), 1))
        nvPrincipal.MenuItems.Add(NavigationViewItems.Generar(recursos.GetString("MoreThings"), New SymbolIcon(Symbol.More), 2))

    End Sub

    Private Sub Nv_ItemInvoked(sender As NavigationView, args As NavigationViewItemInvokedEventArgs)

        Dim recursos As Resources.ResourceLoader = New Resources.ResourceLoader()

        Dim item As TextBlock = args.InvokedItem

        If item.Text = recursos.GetString("Skins") Then
            GridVisibilidad(gridApariencias, item.Text)
        ElseIf item.Text = recursos.GetString("Config") Then
            GridVisibilidad(gridConfig, item.Text)
        ElseIf item.Text = recursos.GetString("MoreThings") Then
            GridVisibilidad(gridMasCosas, item.Text)
        End If

    End Sub

    Private Async Sub Page_Loaded(sender As FrameworkElement, args As Object)

        'Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = "es-ES"
        'Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = "en-US"

        Dim coreBarra As CoreApplicationViewTitleBar = CoreApplication.GetCurrentView.TitleBar
        coreBarra.ExtendViewIntoTitleBar = True

        Dim barra As ApplicationViewTitleBar = ApplicationView.GetForCurrentView().TitleBar
        barra.ButtonBackgroundColor = Colors.Transparent
        barra.ButtonForegroundColor = Colors.White
        barra.ButtonInactiveBackgroundColor = Colors.Transparent

        '--------------------------------------------------------

        Dim recursos As Resources.ResourceLoader = New Resources.ResourceLoader()

        GridVisibilidad(gridApariencias, recursos.GetString("Skins"))
        nvPrincipal.IsPaneOpen = False
        Detector.Steam(False)

        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper

        If Await helper.FileExistsAsync("metodo") = True Then
            cbConfigMetodo.SelectedIndex = Await helper.ReadFileAsync(Of String)("metodo")
        Else
            cbConfigMetodo.SelectedIndex = 0
        End If


        '----------------------------------------------

        'botonAparienciasTexto.Text = recursos.GetString("Skins")
        'botonConfigTexto.Text = recursos.GetString("Boton Config")
        'botonVotarTexto.Text = recursos.GetString("Boton Votar")
        'botonMasCosasTexto.Text = recursos.GetString("Boton Cosas")

        'botonMasAppsTexto.Text = recursos.GetString("Boton Web")
        'botonContactoTexto.Text = recursos.GetString("Boton Contacto")
        'botonReportarTexto.Text = recursos.GetString("Boton Reportar")
        'botonCodigoFuenteTexto.Text = recursos.GetString("Boton Codigo Fuente")

        'botonConfigAparienciasTexto.Text = recursos.GetString("Skins")
        'tbSteamConfigInstruccionesCliente.Text = recursos.GetString("Texto Steam Config Cliente")
        'buttonSteamConfigPathTexto.Text = recursos.GetString("Boton Añadir")
        'tbSteamConfigPath.Text = recursos.GetString("Texto Steam No Config")

        'buttonVolverTexto.Text = recursos.GetString("Boton Volver")

        'buttonDescargaTextoAir.Text = recursos.GetString("Boton Descarga")
        'tbScreenshotsAir.Text = recursos.GetString("Capturas")
        'tbOpcionesAir.Text = recursos.GetString("Opciones")
        'textBlockCreadoAir.Text = recursos.GetString("Creado Por")

        'buttonDescargaTextoAirClassic.Text = recursos.GetString("Boton Descarga")
        'tbScreenshotsAirClassic.Text = recursos.GetString("Capturas")
        'tbOpcionesAirClassic.Text = recursos.GetString("Opciones")
        'textBlockCreadoAirClassic.Text = recursos.GetString("Creado Por")

        'buttonDescargaTextoCompact.Text = recursos.GetString("Boton Descarga")
        'tbScreenshotsCompact.Text = recursos.GetString("Capturas")
        'textBlockCreadoCompact.Text = recursos.GetString("Creado Por")

        'buttonDescargaTextoInvert.Text = recursos.GetString("Boton Descarga")
        'tbScreenshotsInvert.Text = recursos.GetString("Capturas")
        'textBlockCreadoInvert.Text = recursos.GetString("Creado Por")

        'buttonDescargaTextoMetro.Text = recursos.GetString("Boton Descarga")
        'tbScreenshotsMetro.Text = recursos.GetString("Capturas")
        'tbOpcionesMetro.Text = recursos.GetString("Opciones")
        'textBlockCreadoMetro.Text = recursos.GetString("Creado Por")

        'buttonDescargaTextoMinimal.Text = recursos.GetString("Boton Descarga")
        'tbScreenshotsMinimal.Text = recursos.GetString("Capturas")
        'tbOpcionesMinimal.Text = recursos.GetString("Opciones")
        'textBlockCreadoMinimal.Text = recursos.GetString("Creado Por")

        'buttonDescargaTextoPixelVision2.Text = recursos.GetString("Boton Descarga")
        'tbScreenshotsPixelVision2.Text = recursos.GetString("Capturas")
        'textBlockCreadoPixelVision2.Text = recursos.GetString("Creado Por")

        'buttonDescargaTextoPressure2.Text = recursos.GetString("Boton Descarga")
        'tbScreenshotsPressure2.Text = recursos.GetString("Capturas")
        'tbOpcionesPressure2.Text = recursos.GetString("Opciones")
        'textBlockCreadoPressure2.Text = recursos.GetString("Creado Por")

        'buttonDescargaTextoThreshold.Text = recursos.GetString("Boton Descarga")
        'tbScreenshotsThreshold.Text = recursos.GetString("Capturas")
        'tbOpcionesThreshold.Text = recursos.GetString("Opciones")
        'textBlockCreadoThreshold.Text = recursos.GetString("Creado Por")

    End Sub

    Public Sub GridVisibilidad(grid As Grid, tag As String)

        tbTitulo.Text = "Steam Skins (" + SystemInformation.ApplicationVersion.Major.ToString + "." + SystemInformation.ApplicationVersion.Minor.ToString + "." + SystemInformation.ApplicationVersion.Build.ToString + "." + SystemInformation.ApplicationVersion.Revision.ToString + ") - " + tag

        gridApariencias.Visibility = Visibility.Collapsed
        gridCaptura.Visibility = Visibility.Collapsed
        gridConfig.Visibility = Visibility.Collapsed
        gridMasCosas.Visibility = Visibility.Collapsed

        grid.Visibility = Visibility.Visible

    End Sub

    'SKINS-----------------------------------------------------------------------------

    Private Sub LvAparienciasItemClick(sender As Object, args As ItemClickEventArgs)

        If panelMensajeApariencias.Visibility = Visibility.Visible Then
            panelMensajeApariencias.Visibility = Visibility.Collapsed
        End If

        botonAparienciaAir.Background = New SolidColorBrush(Colors.CadetBlue)

        gridAparienciaAir.Visibility = Visibility.Collapsed

        Dim sp As StackPanel = args.ClickedItem

        If sp.Tag.ToString = 0 Then

            botonAparienciaAir.Background = New SolidColorBrush(Colors.DarkCyan)
            gridAparienciaAir.Visibility = Visibility.Visible

        End If

    End Sub

    'SKINAIR-----------------------------------------------------------------------------

    Private Sub LvAparienciaAir1ItemClick(sender As Object, args As ItemClickEventArgs)

        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
        Dim sp As StackPanel = args.ClickedItem

        If sp.Tag.ToString = 0 Then

            Dim listaOpciones As New List(Of String)

            Dim opcionTheme As ComboBoxItem = comboBoxOpcionAirTheme.SelectedValue
            listaOpciones.Add(opcionTheme.Content.ToString)

            Dim opcionColor As ComboBoxItem = comboBoxOpcionAirColor.SelectedValue
            listaOpciones.Add(opcionColor.Content.ToString)

            Dim apariencia As Apariencia = New Apariencia("Air",
                                                    New Uri("https://github.com/Outsetini/Air-for-Steam/archive/master.zip"),
                                                    tbInformeAir,
                                                    prInformeAir,
                                                    listaOpciones,
                                                    gridOpcionesAir)

            Descarga.Iniciar(apariencia)

        End If

    End Sub

    'CONFIG-----------------------------------------------------------------------------

    Private Async Sub BotonSteamRuta_Click(sender As Object, e As RoutedEventArgs) Handles botonSteamRuta.Click

        Detector.Steam(True)

        Dim carpeta As StorageFolder = Nothing

        Try
            carpeta = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteam")
        Catch ex As Exception

        End Try

        Dim recursos As Resources.ResourceLoader = New Resources.ResourceLoader()

        If Not carpeta Is Nothing Then
            'GridVisibilidad(gridApariencias, botonApariencias, recursos.GetString("Skins"))
            'GridSkinVisibilidad(gridSkinAir, buttonSeleccionAir)

            'For Each boton As Button In listaBotonesDescarga
            '    boton.IsEnabled = True
            'Next
        Else
            'GridVisibilidad(gridConfig, botonConfig, recursos.GetString("Boton Config"))
        End If

    End Sub

    Private Async Sub CbConfigMetodo_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbConfigMetodo.SelectionChanged

        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
        Await helper.SaveFileAsync(Of String)("metodo", cbConfigMetodo.SelectedIndex)

    End Sub

    '-----------------------------------------------------------------------------

    Private Sub AmpliarCaptura(imagen As ImageEx)

        Dim recursos As Resources.ResourceLoader = New Resources.ResourceLoader()
        'GridVisibilidad(gridCaptura, Nothing, recursos.GetString("Captura"))
        imageCapturaExpandida.Source = imagen.Source

    End Sub

    Private Sub ButtonVolver_Click(sender As Object, e As RoutedEventArgs) Handles buttonVolver.Click

        Dim recursos As Resources.ResourceLoader = New Resources.ResourceLoader()
        'GridVisibilidad(gridApariencias, botonApariencias, recursos.GetString("Skins"))

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

    'Private Sub ButtonSeleccionAirClassic_Click(sender As Object, e As RoutedEventArgs) Handles buttonSeleccionAirClassic.Click

    '    GridSkinVisibilidad(gridSkinAirClassic, buttonSeleccionAirClassic)

    'End Sub

    Private Async Sub ButtonDescargaAirClassic_Click(sender As Object, e As RoutedEventArgs) Handles buttonDescargaAirClassic.Click

        Dim listaOpciones As New List(Of String)

        Dim opcionColor As ComboBoxItem = comboBoxOpcionAirClassicColor.SelectedValue
        listaOpciones.Add(opcionColor.Content.ToString)

        Dim opcionGameDetails As ComboBoxItem = comboBoxOpcionAirClassicGamesDetails.SelectedValue
        listaOpciones.Add(opcionGameDetails.Content.ToString)

        Dim opcionBackground As ComboBoxItem = comboBoxOpcionAirClassicBackground.SelectedValue
        listaOpciones.Add(opcionBackground.Content.ToString)

        skinAirClassic = New Apariencia("Air-Classic",
                                  New Uri("https://github.com/Outsetini/Air-Classic/archive/master.zip"),
                                  textBlockInformeAirClassic,
                                  progressInformeAirClassic,
                                  listaOpciones,
                                  gridOpcionesAirClassic)

        Dim carpetaSteam As StorageFolder = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteam")
        'Descarga.Iniciar(skinAirClassic, carpetaSteam, buttonSteamConfigPath, listaBotonesDescarga)

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

    'Private Sub ButtonSeleccionCompact_Click(sender As Object, e As RoutedEventArgs) Handles buttonSeleccionCompact.Click

    '    GridSkinVisibilidad(gridSkinCompact, buttonSeleccionCompact)

    'End Sub

    Private Async Sub ButtonDescargaCompact_Click(sender As Object, e As RoutedEventArgs) Handles buttonDescargaCompact.Click

        skinCompact = New Apariencia("Compact",
                                  New Uri("https://github.com/badanka/Compact/archive/master.zip"),
                                  textBlockInformeCompact,
                                  progressInformeCompact,
                                  Nothing, Nothing)

        Dim carpetaSteam As StorageFolder = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteam")
        'Descarga.Iniciar(skinCompact, carpetaSteam, buttonSteamConfigPath, listaBotonesDescarga)

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

    'Private Sub ButtonSeleccionInvert_Click(sender As Object, e As RoutedEventArgs) Handles buttonSeleccionInvert.Click

    '    GridSkinVisibilidad(gridSkinInvert, buttonSeleccionInvert)

    'End Sub

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

        skinInvert = New Apariencia("Invert",
                                  New Uri(temp2),
                                  textBlockInformeInvert,
                                  progressInformeInvert,
                                  Nothing, Nothing)

        Dim carpetaSteam As StorageFolder = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteam")
        'Descarga.Iniciar(skinInvert, carpetaSteam, buttonSteamConfigPath, listaBotonesDescarga)

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

    'Private Sub ButtonSeleccionMetro_Click(sender As Object, e As RoutedEventArgs) Handles buttonSeleccionMetro.Click

    '    GridSkinVisibilidad(gridSkinMetro, buttonSeleccionMetro)

    'End Sub

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

        skinMetro = New Apariencia("Metro",
                                  New Uri(temp2),
                                  textBlockInformeMetro,
                                  progressInformeMetro,
                                  listaOpciones,
                                  gridOpcionesMetro)

        Dim carpetaSteam As StorageFolder = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteam")
        'Descarga.Iniciar(skinMetro, carpetaSteam, buttonSteamConfigPath, listaBotonesDescarga)

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

    'Private Sub ButtonSeleccionMinimal_Click(sender As Object, e As RoutedEventArgs) Handles buttonSeleccionMinimal.Click

    '    GridSkinVisibilidad(gridSkinMinimal, buttonSeleccionMinimal)

    'End Sub

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

        skinMinimal = New Apariencia("Minimal",
                                  New Uri(temp2),
                                  textBlockInformeMinimal,
                                  progressInformeMinimal,
                                  listaOpciones,
                                  gridOpcionesMinimal)

        Dim carpetaSteam As StorageFolder = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteam")
        'Descarga.Iniciar(skinMinimal, carpetaSteam, buttonSteamConfigPath, listaBotonesDescarga)

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

    'Private Sub ButtonSeleccionPixelVision2_Click(sender As Object, e As RoutedEventArgs) Handles buttonSeleccionPixelVision2.Click

    '    GridSkinVisibilidad(gridSkinPixelVision2, buttonSeleccionPixelVision2)

    'End Sub

    Private Async Sub ButtonDescargaPixelVision2_Click(sender As Object, e As RoutedEventArgs) Handles buttonDescargaPixelVision2.Click

        skinPixelVision2 = New Apariencia("PixelVision2",
                                  New Uri("https://github.com/somini/Pixelvision2/archive/master.zip"),
                                  textBlockInformePixelVision2,
                                  progressInformePixelVision2,
                                  Nothing, Nothing)

        Dim carpetaSteam As StorageFolder = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteam")
        'Descarga.Iniciar(skinPixelVision2, carpetaSteam, buttonSteamConfigPath, listaBotonesDescarga)

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

    'Private Sub ButtonSeleccionPressure2_Click(sender As Object, e As RoutedEventArgs) Handles buttonSeleccionPressure2.Click

    '    GridSkinVisibilidad(gridSkinPressure2, buttonSeleccionPressure2)

    'End Sub

    Private Async Sub ButtonDescargaPressure2_Click(sender As Object, e As RoutedEventArgs) Handles buttonDescargaPressure2.Click

        Dim listaOpciones As New List(Of String)

        Dim opcionGridTransparent As ComboBoxItem = comboBoxOpcionPressure2GridTransparent.SelectedValue
        listaOpciones.Add(opcionGridTransparent.Content.ToString)

        Dim opcionOverlayBackground As ComboBoxItem = comboBoxOpcionPressure2OverlayBackground.SelectedValue
        listaOpciones.Add(opcionOverlayBackground.Content.ToString)

        skinPressure2 = New Apariencia("Pressure2",
                                  New Uri("https://github.com/DirtDiglett/Pressure2/archive/master.zip"),
                                  textBlockInformePressure2,
                                  progressInformePressure2,
                                  listaOpciones,
                                  gridOpcionesPressure2)

        Dim carpetaSteam As StorageFolder = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteam")
        'Descarga.Iniciar(skinPressure2, carpetaSteam, buttonSteamConfigPath, listaBotonesDescarga)

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

    'Private Sub ButtonSeleccionThreshold_Click(sender As Object, e As RoutedEventArgs) Handles buttonSeleccionThreshold.Click

    '    GridSkinVisibilidad(gridSkinThreshold, buttonSeleccionThreshold)

    'End Sub

    Private Async Sub ButtonDescargaThreshold_Click(sender As Object, e As RoutedEventArgs) Handles buttonDescargaThreshold.Click

        Dim listaOpciones As New List(Of String)

        Dim opcionColor As ComboBoxItem = comboBoxOpcionThresholdColor.SelectedValue
        listaOpciones.Add(opcionColor.Content.ToString)

        Dim opcionOutlines As ComboBoxItem = comboBoxOpcionThresholdOutlines.SelectedValue
        listaOpciones.Add(opcionOutlines.Content.ToString)

        Dim opcionTitlebar As ComboBoxItem = comboBoxOpcionThresholdColoredTitlebar.SelectedValue
        listaOpciones.Add(opcionTitlebar.Content.ToString)

        skinThreshold = New Apariencia("Threshold",
                                  New Uri("https://github.com/Edgarware/Threshold-Skin/archive/master.zip"),
                                  textBlockInformeThreshold,
                                  progressInformeThreshold,
                                  listaOpciones,
                                  gridOpcionesThreshold)

        Dim carpetaSteam As StorageFolder = Await StorageApplicationPermissions.FutureAccessList.GetFolderAsync("rutaSteam")
        'Descarga.Iniciar(skinThreshold, carpetaSteam, buttonSteamConfigPath, listaBotonesDescarga)

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

    'MASCOSAS-----------------------------------------

    Private Async Sub LvMasCosasItemClick(sender As Object, args As ItemClickEventArgs)

        Dim sp As StackPanel = args.ClickedItem

        If sp.Tag.ToString = 0 Then

            Await Launcher.LaunchUriAsync(New Uri("ms-windows-store:REVIEW?PFN=" + Package.Current.Id.FamilyName))

        ElseIf sp.Tag.ToString = 1 Then

            wvMasCosas.Navigate(New Uri("https://pepeizqapps.com/"))

        ElseIf sp.Tag.ToString = 2 Then

            wvMasCosas.Navigate(New Uri("https://pepeizqapps.com/contact/"))

        ElseIf sp.Tag.ToString = 3 Then

            If StoreServicesFeedbackLauncher.IsSupported = True Then
                Dim ejecutador As StoreServicesFeedbackLauncher = StoreServicesFeedbackLauncher.GetDefault()
                Await ejecutador.LaunchAsync()
            Else
                wvMasCosas.Navigate(New Uri("https://pepeizqapps.com/contact/"))
            End If

        ElseIf sp.Tag.ToString = 4 Then

            wvMasCosas.Navigate(New Uri("https://poeditor.com/join/project/YaZAR0uIW4"))

        ElseIf sp.Tag.ToString = 5 Then

            wvMasCosas.Navigate(New Uri("https://github.com/pepeizq/Steam-Skins-UWP"))

        ElseIf sp.Tag.ToString = 6 Then

            wvMasCosas.Navigate(New Uri("https://pepeizqapps.com/thanks/"))

        End If

    End Sub

End Class
