Imports Windows.ApplicationModel.DataTransfer
Imports Windows.Storage
Imports Windows.Storage.AccessCache
Imports Windows.Storage.Pickers
Imports Windows.System
Imports Windows.UI

Public NotInheritable Class MainPage
    Inherits Page

    Dim skinCompact, skinPixelVision2, skinPressure2, skinThreshold As Skins

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

        buttonDescargaTextoCompact.Text = recursos.GetString("Boton Descarga")
        tbScreenshotsCompact.Text = recursos.GetString("Capturas")
        textBlockCreadoCompact.Text = recursos.GetString("Creado Por")

        buttonDescargaTextoPixelVision2.Text = recursos.GetString("Boton Descarga")
        tbScreenshotsPixelVision2.Text = recursos.GetString("Capturas")
        textBlockCreadoPixelVision2.Text = recursos.GetString("Creado Por")

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
            buttonDescargaCompact.IsEnabled = True
            buttonDescargaPixelVision2.IsEnabled = True
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

    'SKINPRESSURE2-----------------------------------------------------------------------------

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
