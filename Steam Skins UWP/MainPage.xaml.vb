Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.ApplicationModel.Core
Imports Windows.UI
Imports Windows.UI.Core

Public NotInheritable Class MainPage
    Inherits Page

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

            Dim sv As ScrollViewer = gridMasCosas.Children(0)
            Dim gridRelleno As Grid = sv.Content
            Dim sp As StackPanel = gridRelleno.Children(0)
            Dim lv As ListView = sp.Children(0)

            MasCosas.Navegar(lv, "2", "https://pepeizqapps.com/")
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

        Interfaz.GenerarListado(gvApariencias)
        Detector.Steam(False)
        MasCosas.Generar()

        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper

        If Await helper.FileExistsAsync("metodo") = True Then
            cbConfigMetodo.SelectedIndex = Await helper.ReadFileAsync(Of String)("metodo")
        Else
            cbConfigMetodo.SelectedIndex = 0
        End If

        '--------------------------------------------------------

        AddHandler botonDescargaApariencia.PointerEntered, AddressOf UsuarioEntraBoton
        AddHandler botonDescargaApariencia.PointerExited, AddressOf UsuarioSaleBoton
        AddHandler botonPersonalizacion.PointerEntered, AddressOf UsuarioEntraBoton
        AddHandler botonPersonalizacion.PointerExited, AddressOf UsuarioSaleBoton

        AddHandler botonImagenApariencia1.PointerEntered, AddressOf UsuarioEntraBoton
        AddHandler botonImagenApariencia1.PointerExited, AddressOf UsuarioSaleBoton
        AddHandler botonImagenApariencia2.PointerEntered, AddressOf UsuarioEntraBoton
        AddHandler botonImagenApariencia2.PointerExited, AddressOf UsuarioSaleBoton
        AddHandler botonImagenApariencia3.PointerEntered, AddressOf UsuarioEntraBoton
        AddHandler botonImagenApariencia3.PointerExited, AddressOf UsuarioSaleBoton
        AddHandler botonImagenApariencia4.PointerEntered, AddressOf UsuarioEntraBoton
        AddHandler botonImagenApariencia4.PointerExited, AddressOf UsuarioSaleBoton

        AddHandler botonVolver.PointerEntered, AddressOf UsuarioSaleBoton
        AddHandler botonVolver.PointerExited, AddressOf UsuarioSaleBoton

        '--------------------------------------------------------

        Dim transpariencia As New UISettings
        TransparienciaEfectosFinal(transpariencia.AdvancedEffectsEnabled)
        AddHandler transpariencia.AdvancedEffectsEnabledChanged, AddressOf TransparienciaEfectosCambia

    End Sub

    Private Sub TransparienciaEfectosCambia(sender As UISettings, e As Object)

        TransparienciaEfectosFinal(sender.AdvancedEffectsEnabled)

    End Sub

    Private Async Sub TransparienciaEfectosFinal(estado As Boolean)

        Await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, Sub()
                                                                     If estado = True Then
                                                                         gridAparienciaElegida.Background = App.Current.Resources("GridAcrilico")
                                                                         gridCaptura.Background = App.Current.Resources("GridAcrilico")
                                                                         gridConfig.Background = App.Current.Resources("GridAcrilico")
                                                                         gridConfigSkins.Background = App.Current.Resources("GridTituloBackground")
                                                                         gridMasCosas.Background = App.Current.Resources("GridAcrilico")
                                                                     Else
                                                                         gridAparienciaElegida.Background = New SolidColorBrush(Colors.LightGray)
                                                                         gridCaptura.Background = New SolidColorBrush(Colors.LightGray)
                                                                         gridConfig.Background = New SolidColorBrush(Colors.LightGray)
                                                                         gridConfigSkins.Background = New SolidColorBrush(App.Current.Resources("ColorPrimario"))
                                                                         gridMasCosas.Background = New SolidColorBrush(Colors.LightGray)
                                                                     End If
                                                                 End Sub)

    End Sub

    Public Sub GridVisibilidad(grid As Grid, tag As String)

        tbTitulo.Text = Package.Current.DisplayName + " (" + Package.Current.Id.Version.Major.ToString + "." + Package.Current.Id.Version.Minor.ToString + "." + Package.Current.Id.Version.Build.ToString + "." + Package.Current.Id.Version.Revision.ToString + ") - " + tag

        gridAparienciaElegida.Visibility = Visibility.Collapsed
        gridCaptura.Visibility = Visibility.Collapsed
        gridConfig.Visibility = Visibility.Collapsed
        gridMasCosas.Visibility = Visibility.Collapsed

        grid.Visibility = Visibility.Visible

    End Sub

    Private Sub UsuarioEntraBoton(sender As Object, e As PointerRoutedEventArgs)

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

    End Sub

    Private Sub UsuarioSaleBoton(sender As Object, e As PointerRoutedEventArgs)

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

    End Sub

    'SKINS-----------------------------------------------------------------------------

    Private Sub BotonDescargaApariencia_Click(sender As Object, e As RoutedEventArgs) Handles botonDescargaApariencia.Click

        Dim boton As Button = sender
        Dim aparienciaElegida As Apariencia = boton.Tag
        Descarga.Iniciar(aparienciaElegida)

    End Sub





    ''SKINMINIMAL-----------------------------------------------------------------------------

    'Private Async Sub LvAparienciaMinimal1ItemClick(sender As Object, args As ItemClickEventArgs)

    '    Dim sp As StackPanel = args.ClickedItem

    '    If sp.Tag.ToString = 0 Then
    '        lvAparienciaMinimal1.IsEnabled = False

    '        Dim listaOpciones As New List(Of String)

    '        Dim opcionColor As ComboBoxItem = cbOpcionMinimalColor.SelectedValue
    '        listaOpciones.Add(opcionColor.Content.ToString)

    '        Dim html As String = Await Decompiladores.HttpClient(New Uri("http://gamebanana.com/guis/download/27756"))

    '        Dim temp, temp2 As String
    '        Dim int, int2 As Integer

    '        If Not html = Nothing Then
    '            int = html.IndexOf(".zip")
    '            temp = html.Remove(int + 4, html.Length - (int + 4))

    '            int2 = temp.LastIndexOf("https://")
    '            temp2 = temp.Remove(0, int2)
    '        Else
    '            temp2 = Nothing
    '        End If

    '        Dim apariencia As Apariencia = New Apariencia("Minimal",
    '                                                      New Uri(temp2),
    '                                                      tbInformeMinimal, prInformeMinimal,
    '                                                      listaOpciones, gridPersonalizacionMinimal)

    '        Descarga.Iniciar(apariencia)

    '    ElseIf sp.Tag.ToString = 1 Then

    '        If gridPersonalizacionMinimal.Visibility = Visibility.Collapsed Then
    '            lvPersonalizacionMinimal.Background = New SolidColorBrush(App.Current.Resources("ColorPrimario"))
    '            gridPersonalizacionMinimal.Margin = New Thickness(lvDescargaMinimal.ActualWidth + 5, 0, 0, 0)
    '            gridPersonalizacionMinimal.Visibility = Visibility.Visible

    '            ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("grid", lvPersonalizacionMinimal)

    '            Dim animacion As ConnectedAnimation = ConnectedAnimationService.GetForCurrentView().GetAnimation("grid")

    '            If Not animacion Is Nothing Then
    '                animacion.TryStart(gridPersonalizacionMinimal)
    '            End If
    '        Else
    '            lvPersonalizacionMinimal.Background = New SolidColorBrush(App.Current.Resources("ColorSecundario"))
    '            gridPersonalizacionMinimal.Visibility = Visibility.Collapsed
    '        End If

    '    End If

    'End Sub

    'Private Async Sub LvAparienciaMinimal2ItemClick(sender As Object, args As ItemClickEventArgs)

    '    Dim sp As StackPanel = args.ClickedItem

    '    If sp.Tag.ToString = 0 Then

    '        Await Launcher.LaunchUriAsync(New Uri("http://steamcommunity.com/groups/MinimalSteamUI"))

    '    End If

    'End Sub

    'Private Sub BotonImagenMinimal1_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenMinimal1.Click

    '    AmpliarCaptura(imagenMinimal1)

    'End Sub

    'Private Sub BotonImagenMinimal2_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenMinimal2.Click

    '    AmpliarCaptura(imagenMinimal2)

    'End Sub

    'Private Sub BotonImagenMinimal3_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenMinimal3.Click

    '    AmpliarCaptura(imagenMinimal3)

    'End Sub

    'Private Sub BotonImagenMinimal4_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenMinimal4.Click

    '    AmpliarCaptura(imagenMinimal4)

    'End Sub



    ''SKINTHRESHOLD-----------------------------------------------------------------------------

    'Private Sub LvAparienciaThreshold1ItemClick(sender As Object, args As ItemClickEventArgs)

    '    Dim sp As StackPanel = args.ClickedItem

    '    If sp.Tag.ToString = 0 Then
    '        Dim listaOpciones As New List(Of String)

    '        Dim opcionColor As ComboBoxItem = cbOpcionThresholdColor.SelectedValue
    '        listaOpciones.Add(opcionColor.Content.ToString)

    '        Dim opcionOutlines As ComboBoxItem = cbOpcionThresholdOutlines.SelectedValue
    '        listaOpciones.Add(opcionOutlines.Content.ToString)

    '        Dim opcionTitlebar As ComboBoxItem = cbOpcionThresholdColoredTitlebar.SelectedValue
    '        listaOpciones.Add(opcionTitlebar.Content.ToString)

    '        Dim apariencia As Apariencia = New Apariencia("Threshold",
    '                                                      New Uri("https://github.com/Edgarware/Threshold-Skin/archive/master.zip"),
    '                                                      tbInformeThreshold, prInformeThreshold,
    '                                                      listaOpciones, gridPersonalizacionThreshold)

    '        Descarga.Iniciar(apariencia)

    '    ElseIf sp.Tag.ToString = 1 Then

    '        If gridPersonalizacionThreshold.Visibility = Visibility.Collapsed Then
    '            lvPersonalizacionThreshold.Background = New SolidColorBrush(App.Current.Resources("ColorPrimario"))
    '            gridPersonalizacionThreshold.Margin = New Thickness(lvDescargaThreshold.ActualWidth + 5, 0, 0, 0)
    '            gridPersonalizacionThreshold.Visibility = Visibility.Visible

    '            ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("grid", lvPersonalizacionThreshold)

    '            Dim animacion As ConnectedAnimation = ConnectedAnimationService.GetForCurrentView().GetAnimation("grid")

    '            If Not animacion Is Nothing Then
    '                animacion.TryStart(gridPersonalizacionThreshold)
    '            End If
    '        Else
    '            lvPersonalizacionThreshold.Background = New SolidColorBrush(App.Current.Resources("ColorSecundario"))
    '            gridPersonalizacionThreshold.Visibility = Visibility.Collapsed
    '        End If

    '    End If

    'End Sub

    'Private Async Sub LvAparienciaThreshold2ItemClick(sender As Object, args As ItemClickEventArgs)

    '    Dim sp As StackPanel = args.ClickedItem

    '    If sp.Tag.ToString = 0 Then

    '        Await Launcher.LaunchUriAsync(New Uri("https://steamcommunity.com/groups/thresholdskin"))

    '    End If

    'End Sub

    Private Sub BotonImagenApariencia1_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenApariencia1.Click

        AmpliarCaptura(imagenApariencia1)

    End Sub

    Private Sub BotonImagenApariencia2_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenApariencia2.Click

        AmpliarCaptura(imagenApariencia2)

    End Sub

    Private Sub BotonImagenApariencia3_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenApariencia3.Click

        AmpliarCaptura(imagenApariencia3)

    End Sub

    Private Sub BotonImagenApariencia4_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenApariencia4.Click

        AmpliarCaptura(imagenApariencia4)

    End Sub

    'CAPTURA-----------------------------------------------------------------------------

    Private Sub AmpliarCaptura(imagen As ImageEx)

        gridCaptura.Visibility = Visibility.Visible
        imagenExpandida.Source = imagen.Source

    End Sub

    Private Sub BotonVolver_Click(sender As Object, e As RoutedEventArgs) Handles botonVolver.Click

        gridCaptura.Visibility = Visibility.Collapsed

    End Sub

    'CONFIG-----------------------------------------------------------------------------

    Private Sub BotonSteamRuta_Click(sender As Object, e As RoutedEventArgs) Handles botonSteamRuta.Click

        Detector.Steam(True)

    End Sub

    Private Async Sub CbConfigMetodo_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbConfigMetodo.SelectionChanged

        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
        Await helper.SaveFileAsync(Of String)("metodo", cbConfigMetodo.SelectedIndex)

    End Sub

End Class
