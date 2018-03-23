Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.UI
Imports Windows.UI.Core

Public NotInheritable Class MainPage
    Inherits Page

    Private Sub Nv_Loaded(sender As Object, e As RoutedEventArgs)

        Dim recursos As New Resources.ResourceLoader()

        nvPrincipal.MenuItems.Add(NavigationViewItems.Generar(recursos.GetString("Skins"), New SymbolIcon(Symbol.Home), 0))
        nvPrincipal.MenuItems.Add(New NavigationViewItemSeparator)
        nvPrincipal.MenuItems.Add(NavigationViewItems.Generar(recursos.GetString("Config"), New SymbolIcon(Symbol.Setting), 1))

    End Sub

    Private Sub Nv_ItemInvoked(sender As NavigationView, args As NavigationViewItemInvokedEventArgs)

        Dim recursos As New Resources.ResourceLoader()

        Dim item As TextBlock = args.InvokedItem

        If item.Text = recursos.GetString("Skins") Then
            GridVisibilidad(gridApariencias, item.Text)
        ElseIf item.Text = recursos.GetString("Config") Then
            GridVisibilidad(gridConfig, item.Text)
        End If

    End Sub

    Private Sub Page_Loaded(sender As FrameworkElement, args As Object)

        'Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = "es-ES"
        'Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = "en-US"

        MasCosas.Generar()

        Dim recursos As New Resources.ResourceLoader()

        GridVisibilidad(gridApariencias, recursos.GetString("Skins"))
        nvPrincipal.IsPaneOpen = False

        Interfaz.GenerarListado(gvApariencias)
        Detector.Steam(False)

        '--------------------------------------------------------

        Dim transpariencia As New UISettings
        TransparienciaEfectosFinal(transpariencia.AdvancedEffectsEnabled)
        AddHandler transpariencia.AdvancedEffectsEnabledChanged, AddressOf TransparienciaEfectosCambia

    End Sub

    Private Sub TransparienciaEfectosCambia(sender As UISettings, e As Object)

        TransparienciaEfectosFinal(sender.AdvancedEffectsEnabled)

    End Sub

    Private Async Sub TransparienciaEfectosFinal(estado As Boolean)

        Await Dispatcher.RunAsync(CoreDispatcherPriority.High, Sub()
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

    Private Sub BotonTutorial_Click(sender As Object, e As RoutedEventArgs) Handles botonTutorial.Click

        Dim imagenTutorial As New ImageEx With {
            .Source = "Assets\tutorial.gif",
            .IsCacheEnabled = True
        }

        AmpliarCaptura(imagenTutorial)

    End Sub

    Private Sub BotonDescargaApariencia_Click(sender As Object, e As RoutedEventArgs) Handles botonDescargaApariencia.Click

        Dim boton As Button = sender
        Dim aparienciaElegida As Apariencia = boton.Tag
        Descarga.Iniciar(aparienciaElegida)

    End Sub

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

End Class
