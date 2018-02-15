Imports Microsoft.Services.Store.Engagement
Imports Microsoft.Toolkit.Uwp.Helpers
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.ApplicationModel.Core
Imports Windows.System
Imports Windows.UI
Imports Windows.UI.Xaml.Media.Animation

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
            NavegarMasCosas(lvMasCosasMasApps, "https://pepeizqapps.com/")
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

        '--------------------------------------------------------

        Dim transpariencia As New UISettings
        AddHandler transpariencia.AdvancedEffectsEnabledChanged, AddressOf TransparienciaEfectosCambia

    End Sub

    Private Sub TransparienciaEfectosCambia(sender As UISettings, e As Object)

        If sender.AdvancedEffectsEnabled = True Then
            gridCaptura.Background = New SolidColorBrush(App.Current.Resources("GridAcrilico"))
            gridConfig.Background = New SolidColorBrush(App.Current.Resources("GridAcrilico"))
            gridConfigSkins.Background = New SolidColorBrush(App.Current.Resources("GridTituloBackground"))
            gridMasCosas.Background = New SolidColorBrush(App.Current.Resources("GridAcrilico"))
        Else
            gridCaptura.Background = New SolidColorBrush(Colors.LightGray)
            gridConfig.Background = New SolidColorBrush(Colors.LightGray)
            gridConfigSkins.Background = New SolidColorBrush(App.Current.Resources("ColorPrimario"))
            gridMasCosas.Background = New SolidColorBrush(Colors.LightGray)
        End If

    End Sub

    Public Sub GridVisibilidad(grid As Grid, tag As String)

        tbTitulo.Text = Package.Current.DisplayName + " (" + Package.Current.Id.Version.Major.ToString + "." + Package.Current.Id.Version.Minor.ToString + "." + Package.Current.Id.Version.Build.ToString + "." + Package.Current.Id.Version.Revision.ToString + ") - " + tag

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

        botonAparienciaAir.Background = New SolidColorBrush(App.Current.Resources("ColorSecundario"))
        botonAparienciaAirClassic.Background = New SolidColorBrush(App.Current.Resources("ColorSecundario"))
        botonAparienciaCompact.Background = New SolidColorBrush(App.Current.Resources("ColorSecundario"))
        botonAparienciaInvert.Background = New SolidColorBrush(App.Current.Resources("ColorSecundario"))
        botonAparienciaMetro.Background = New SolidColorBrush(App.Current.Resources("ColorSecundario"))
        botonAparienciaMinimal.Background = New SolidColorBrush(App.Current.Resources("ColorSecundario"))
        botonAparienciaPixelVision2.Background = New SolidColorBrush(App.Current.Resources("ColorSecundario"))
        botonAparienciaPressure2.Background = New SolidColorBrush(App.Current.Resources("ColorSecundario"))
        botonAparienciaThreshold.Background = New SolidColorBrush(App.Current.Resources("ColorSecundario"))

        gridAparienciaAir.Visibility = Visibility.Collapsed
        gridAparienciaAirClassic.Visibility = Visibility.Collapsed
        gridAparienciaCompact.Visibility = Visibility.Collapsed
        gridAparienciaInvert.Visibility = Visibility.Collapsed
        gridAparienciaMetro.Visibility = Visibility.Collapsed
        gridAparienciaMinimal.Visibility = Visibility.Collapsed
        gridAparienciaPixelVision2.Visibility = Visibility.Collapsed
        gridAparienciaPressure2.Visibility = Visibility.Collapsed
        gridAparienciaThreshold.Visibility = Visibility.Collapsed

        Dim sp As StackPanel = args.ClickedItem

        If sp.Tag.ToString = 0 Then

            tbTitulo.Text = Package.Current.DisplayName + " (" + Package.Current.Id.Version.Major.ToString + "." + Package.Current.Id.Version.Minor.ToString + "." + Package.Current.Id.Version.Build.ToString + "." + Package.Current.Id.Version.Revision.ToString + ") - Air"
            botonAparienciaAir.Background = New SolidColorBrush(App.Current.Resources("ColorPrimario"))
            gridAparienciaAir.Visibility = Visibility.Visible

        ElseIf sp.Tag.ToString = 1 Then

            tbTitulo.Text = Package.Current.DisplayName + " (" + Package.Current.Id.Version.Major.ToString + "." + Package.Current.Id.Version.Minor.ToString + "." + Package.Current.Id.Version.Build.ToString + "." + Package.Current.Id.Version.Revision.ToString + ") - Air Classic"
            botonAparienciaAirClassic.Background = New SolidColorBrush(App.Current.Resources("ColorPrimario"))
            gridAparienciaAirClassic.Visibility = Visibility.Visible

        ElseIf sp.Tag.ToString = 2 Then

            tbTitulo.Text = Package.Current.DisplayName + " (" + Package.Current.Id.Version.Major.ToString + "." + Package.Current.Id.Version.Minor.ToString + "." + Package.Current.Id.Version.Build.ToString + "." + Package.Current.Id.Version.Revision.ToString + ") - Compact"
            botonAparienciaCompact.Background = New SolidColorBrush(App.Current.Resources("ColorPrimario"))
            gridAparienciaCompact.Visibility = Visibility.Visible

        ElseIf sp.Tag.ToString = 3 Then

            tbTitulo.Text = Package.Current.DisplayName + " (" + Package.Current.Id.Version.Major.ToString + "." + Package.Current.Id.Version.Minor.ToString + "." + Package.Current.Id.Version.Build.ToString + "." + Package.Current.Id.Version.Revision.ToString + ") - Invert"
            botonAparienciaInvert.Background = New SolidColorBrush(App.Current.Resources("ColorPrimario"))
            gridAparienciaInvert.Visibility = Visibility.Visible

        ElseIf sp.Tag.ToString = 4 Then

            tbTitulo.Text = Package.Current.DisplayName + " (" + Package.Current.Id.Version.Major.ToString + "." + Package.Current.Id.Version.Minor.ToString + "." + Package.Current.Id.Version.Build.ToString + "." + Package.Current.Id.Version.Revision.ToString + ") - Metro"
            botonAparienciaMetro.Background = New SolidColorBrush(App.Current.Resources("ColorPrimario"))
            gridAparienciaMetro.Visibility = Visibility.Visible

        ElseIf sp.Tag.ToString = 5 Then

            tbTitulo.Text = Package.Current.DisplayName + " (" + Package.Current.Id.Version.Major.ToString + "." + Package.Current.Id.Version.Minor.ToString + "." + Package.Current.Id.Version.Build.ToString + "." + Package.Current.Id.Version.Revision.ToString + ") - Minimal"
            botonAparienciaMinimal.Background = New SolidColorBrush(App.Current.Resources("ColorPrimario"))
            gridAparienciaMinimal.Visibility = Visibility.Visible

        ElseIf sp.Tag.ToString = 6 Then

            tbTitulo.Text = Package.Current.DisplayName + " (" + Package.Current.Id.Version.Major.ToString + "." + Package.Current.Id.Version.Minor.ToString + "." + Package.Current.Id.Version.Build.ToString + "." + Package.Current.Id.Version.Revision.ToString + ") - PixelVision²"
            botonAparienciaPixelVision2.Background = New SolidColorBrush(App.Current.Resources("ColorPrimario"))
            gridAparienciaPixelVision2.Visibility = Visibility.Visible

        ElseIf sp.Tag.ToString = 7 Then

            tbTitulo.Text = Package.Current.DisplayName + " (" + Package.Current.Id.Version.Major.ToString + "." + Package.Current.Id.Version.Minor.ToString + "." + Package.Current.Id.Version.Build.ToString + "." + Package.Current.Id.Version.Revision.ToString + ") - Pressure²"
            botonAparienciaPressure2.Background = New SolidColorBrush(App.Current.Resources("ColorPrimario"))
            gridAparienciaPressure2.Visibility = Visibility.Visible

        ElseIf sp.Tag.ToString = 8 Then

            tbTitulo.Text = Package.Current.DisplayName + " (" + Package.Current.Id.Version.Major.ToString + "." + Package.Current.Id.Version.Minor.ToString + "." + Package.Current.Id.Version.Build.ToString + "." + Package.Current.Id.Version.Revision.ToString + ") - Threshold"
            botonAparienciaThreshold.Background = New SolidColorBrush(App.Current.Resources("ColorPrimario"))
            gridAparienciaThreshold.Visibility = Visibility.Visible

        End If

    End Sub

    'SKINAIR-----------------------------------------------------------------------------

    Private Sub LvAparienciaAir1ItemClick(sender As Object, args As ItemClickEventArgs)

        Dim sp As StackPanel = args.ClickedItem

        If sp.Tag.ToString = 0 Then

            Dim listaOpciones As New List(Of String)

            Dim opcionTheme As ComboBoxItem = cbOpcionAirTheme.SelectedValue
            listaOpciones.Add(opcionTheme.Content.ToString)

            Dim opcionColor As ComboBoxItem = cbOpcionAirColor.SelectedValue
            listaOpciones.Add(opcionColor.Content.ToString)

            Dim apariencia As Apariencia = New Apariencia("Air",
                                                          New Uri("https://github.com/Outsetini/Air-for-Steam/archive/master.zip"),
                                                          tbInformeAir, prInformeAir,
                                                          listaOpciones, gridPersonalizacionAir)

            Descarga.Iniciar(apariencia)

        ElseIf sp.Tag.ToString = 1 Then

            If gridPersonalizacionAir.Visibility = Visibility.Collapsed Then
                lvPersonalizacionAir.Background = New SolidColorBrush(App.Current.Resources("ColorPrimario"))
                gridPersonalizacionAir.Margin = New Thickness(lvDescargaAir.ActualWidth + 5, 0, 0, 0)
                gridPersonalizacionAir.Visibility = Visibility.Visible

                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("grid", lvPersonalizacionAir)

                Dim animacion As ConnectedAnimation = ConnectedAnimationService.GetForCurrentView().GetAnimation("grid")

                If Not animacion Is Nothing Then
                    animacion.TryStart(gridPersonalizacionAir)
                End If
            Else
                lvPersonalizacionAir.Background = New SolidColorBrush(App.Current.Resources("ColorSecundario"))
                gridPersonalizacionAir.Visibility = Visibility.Collapsed
            End If

        End If

    End Sub

    Private Async Sub LvAparienciaAir2ItemClick(sender As Object, args As ItemClickEventArgs)

        Dim sp As StackPanel = args.ClickedItem

        If sp.Tag.ToString = 0 Then

            Await Launcher.LaunchUriAsync(New Uri("http://airforsteam.com"))

        ElseIf sp.Tag.ToString = 1 Then

            Await Launcher.LaunchUriAsync(New Uri("https://www.patreon.com/inhibitor"))

        End If

    End Sub

    Private Sub BotonImagenAir1_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenAir1.Click

        AmpliarCaptura(imagenAir1)

    End Sub

    Private Sub BotonImagenAir2_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenAir2.Click

        AmpliarCaptura(imagenAir2)

    End Sub

    Private Sub BotonImagenAir3_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenAir3.Click

        AmpliarCaptura(imagenAir3)

    End Sub

    Private Sub BotonImagenAir4_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenAir4.Click

        AmpliarCaptura(imagenAir4)

    End Sub

    'SKINAIRCLASSIC-----------------------------------------------------------------------------

    Private Sub LvAparienciaAirClassic1ItemClick(sender As Object, args As ItemClickEventArgs)

        Dim sp As StackPanel = args.ClickedItem

        If sp.Tag.ToString = 0 Then
            Dim listaOpciones As New List(Of String)

            Dim opcionColor As ComboBoxItem = cbAirClassicColor.SelectedValue
            listaOpciones.Add(opcionColor.Content.ToString)

            Dim opcionGameDetails As ComboBoxItem = cbOpcionAirClassicGamesDetails.SelectedValue
            listaOpciones.Add(opcionGameDetails.Content.ToString)

            Dim opcionBackground As ComboBoxItem = cbOpcionAirClassicBackground.SelectedValue
            listaOpciones.Add(opcionBackground.Content.ToString)

            Dim apariencia As Apariencia = New Apariencia("Air-Classic",
                                                          New Uri("https://github.com/Outsetini/Air-Classic/archive/master.zip"),
                                                          tbInformeAirClassic, prInformeAirClassic,
                                                          listaOpciones, gridPersonalizacionAirClassic)

            Descarga.Iniciar(apariencia)

        ElseIf sp.Tag.ToString = 1 Then

            If gridPersonalizacionAirClassic.Visibility = Visibility.Collapsed Then
                lvPersonalizacionAirClassic.Background = New SolidColorBrush(App.Current.Resources("ColorPrimario"))
                gridPersonalizacionAirClassic.Margin = New Thickness(lvDescargaAirClassic.ActualWidth + 5, 0, 0, 0)
                gridPersonalizacionAirClassic.Visibility = Visibility.Visible

                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("grid", lvPersonalizacionAirClassic)

                Dim animacion As ConnectedAnimation = ConnectedAnimationService.GetForCurrentView().GetAnimation("grid")

                If Not animacion Is Nothing Then
                    animacion.TryStart(gridPersonalizacionAirClassic)
                End If
            Else
                lvPersonalizacionAirClassic.Background = New SolidColorBrush(App.Current.Resources("ColorSecundario"))
                gridPersonalizacionAirClassic.Visibility = Visibility.Collapsed
            End If

        End If

    End Sub

    Private Async Sub LvAparienciaAirClassic2ItemClick(sender As Object, args As ItemClickEventArgs)

        Dim sp As StackPanel = args.ClickedItem

        If sp.Tag.ToString = 0 Then

            Await Launcher.LaunchUriAsync(New Uri("http://airforsteam.com"))

        ElseIf sp.Tag.ToString = 1 Then

            Await Launcher.LaunchUriAsync(New Uri("https://www.patreon.com/inhibitor"))

        End If

    End Sub

    Private Sub BotonImagenAirClassic1_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenAirClassic1.Click

        AmpliarCaptura(imagenAirClassic1)

    End Sub

    Private Sub BotonImagenAirClassic2_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenAirClassic2.Click

        AmpliarCaptura(imagenAirClassic2)

    End Sub

    Private Sub BotonImagenAirClassic3_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenAirClassic3.Click

        AmpliarCaptura(imagenAirClassic3)

    End Sub

    Private Sub BotonImagenAirClassic4_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenAirClassic4.Click

        AmpliarCaptura(imagenAirClassic4)

    End Sub

    'SKINCOMPACT-----------------------------------------------------------------------------

    Private Sub LvAparienciaCompact1ItemClick(sender As Object, args As ItemClickEventArgs)

        Dim sp As StackPanel = args.ClickedItem

        If sp.Tag.ToString = 0 Then
            Dim apariencia As Apariencia = New Apariencia("Compact",
                                                          New Uri("https://github.com/badanka/Compact/archive/master.zip"),
                                                          tbInformeCompact, prInformeCompact,
                                                          Nothing, Nothing)

            Descarga.Iniciar(apariencia)

        End If

    End Sub

    Private Async Sub LvAparienciaCompact2ItemClick(sender As Object, args As ItemClickEventArgs)

        Dim sp As StackPanel = args.ClickedItem

        If sp.Tag.ToString = 0 Then

            Await Launcher.LaunchUriAsync(New Uri("http://badanka.github.io/Compact/"))

        End If

    End Sub

    Private Sub BotonImagenCompact1_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenCompact1.Click

        AmpliarCaptura(imagenCompact1)

    End Sub

    Private Sub BotonImagenCompact2_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenCompact2.Click

        AmpliarCaptura(imagenCompact2)

    End Sub

    Private Sub BotonImagenCompact3_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenCompact3.Click

        AmpliarCaptura(imagenCompact3)

    End Sub

    Private Sub BotonImagenCompact4_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenCompact4.Click

        AmpliarCaptura(imagenCompact4)

    End Sub

    'SKININVERT-----------------------------------------------------------------------------

    Private Async Sub LvAparienciaInvert1ItemClick(sender As Object, args As ItemClickEventArgs)

        lvAparienciaInvert1.IsEnabled = False

        Dim html As String = Await Decompiladores.HttpClient(New Uri("http://gamebanana.com/guis/download/28814"))

        Dim temp, temp2 As String
        Dim int, int2 As Integer

        If Not html = Nothing Then
            int = html.IndexOf(".zip")
            temp = html.Remove(int + 4, html.Length - (int + 4))

            int2 = temp.LastIndexOf("https://")
            temp2 = temp.Remove(0, int2)
        Else
            temp2 = Nothing
        End If

        Dim sp As StackPanel = args.ClickedItem

        If sp.Tag.ToString = 0 Then
            Dim apariencia As Apariencia = New Apariencia("Invert",
                                                          New Uri(temp2),
                                                          tbInformeInvert, prInformeInvert,
                                                          Nothing, Nothing)

            Descarga.Iniciar(apariencia)

        End If

    End Sub

    Private Async Sub LvAparienciaInvert2ItemClick(sender As Object, args As ItemClickEventArgs)

        Dim sp As StackPanel = args.ClickedItem

        If sp.Tag.ToString = 0 Then

            Await Launcher.LaunchUriAsync(New Uri("http://gamebanana.com/guis/28814"))

        ElseIf sp.Tag.ToString = 1 Then

            Await Launcher.LaunchUriAsync(New Uri("https://www.paypal.com/sk/cgi-bin/webscr?cmd=_flow&SESSION=XyLR0yV_beniveGJ0ONtunwPaUfnuwR7BtktA-E2xhApEH_8hG8e2s_Frm0&dispatch=5885d80a13c0db1f8e263663d3faee8d4fe1dd75ca3bd4f11d72275b28239088"))

        End If

    End Sub

    Private Sub BotonImagenInvert1_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenInvert1.Click

        AmpliarCaptura(imagenInvert1)

    End Sub

    Private Sub BotonImagenInvert2_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenInvert2.Click

        AmpliarCaptura(imagenInvert2)

    End Sub

    Private Sub BotonImagenInvert3_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenInvert3.Click

        AmpliarCaptura(imagenInvert3)

    End Sub

    Private Sub BotonImagenInvert4_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenInvert4.Click

        AmpliarCaptura(imagenInvert4)

    End Sub

    'SKINMETRO-----------------------------------------------------------------------------

    Private Async Sub LvAparienciaMetro1ItemClick(sender As Object, args As ItemClickEventArgs)

        Dim sp As StackPanel = args.ClickedItem

        If sp.Tag.ToString = 0 Then
            lvAparienciaMetro1.IsEnabled = False

            Dim listaOpciones As New List(Of String)

            Dim opcionColor As ComboBoxItem = cbOpcionMetroColor.SelectedValue
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

            Dim apariencia As Apariencia = New Apariencia("Metro",
                                                          New Uri(temp2),
                                                          tbInformeMetro, prInformeMetro,
                                                          listaOpciones, gridPersonalizacionMetro)

            Descarga.Iniciar(apariencia)

        ElseIf sp.Tag.ToString = 1 Then

            If gridPersonalizacionMetro.Visibility = Visibility.Collapsed Then
                lvPersonalizacionMetro.Background = New SolidColorBrush(App.Current.Resources("ColorPrimario"))
                gridPersonalizacionMetro.Margin = New Thickness(lvDescargaMetro.ActualWidth + 5, 0, 0, 0)
                gridPersonalizacionMetro.Visibility = Visibility.Visible

                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("grid", lvPersonalizacionMetro)

                Dim animacion As ConnectedAnimation = ConnectedAnimationService.GetForCurrentView().GetAnimation("grid")

                If Not animacion Is Nothing Then
                    animacion.TryStart(gridPersonalizacionMetro)
                End If
            Else
                lvPersonalizacionMetro.Background = New SolidColorBrush(App.Current.Resources("ColorSecundario"))
                gridPersonalizacionMetro.Visibility = Visibility.Collapsed
            End If

        End If

    End Sub

    Private Async Sub LvAparienciaMetro2ItemClick(sender As Object, args As ItemClickEventArgs)

        Dim sp As StackPanel = args.ClickedItem

        If sp.Tag.ToString = 0 Then

            Await Launcher.LaunchUriAsync(New Uri("http://www.metroforsteam.com"))

        ElseIf sp.Tag.ToString = 1 Then

            Await Launcher.LaunchUriAsync(New Uri("https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=BDL2J3MEETZ3J&lc=US&item_name=Metro%20for%20Steam&item_number=metroforsteam&currency_code=USD&bn=PP%2dDonationsBF%3abtn_donate_LG%2egif%3aNonHosted"))

        ElseIf sp.Tag.ToString = 2 Then

            Await Launcher.LaunchUriAsync(New Uri("http://www.patreon.com/dommini"))

        End If

    End Sub

    Private Sub BotonImagenMetro1_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenMetro1.Click

        AmpliarCaptura(imagenMetro1)

    End Sub

    Private Sub BotonImagenMetro2_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenMetro2.Click

        AmpliarCaptura(imagenMetro2)

    End Sub

    Private Sub BotonImagenMetro3_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenMetro3.Click

        AmpliarCaptura(imagenMetro3)

    End Sub

    Private Sub BotonImagenMetro4_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenMetro4.Click

        AmpliarCaptura(imagenMetro4)

    End Sub

    'SKINMINIMAL-----------------------------------------------------------------------------

    Private Async Sub LvAparienciaMinimal1ItemClick(sender As Object, args As ItemClickEventArgs)

        Dim sp As StackPanel = args.ClickedItem

        If sp.Tag.ToString = 0 Then
            lvAparienciaMinimal1.IsEnabled = False

            Dim listaOpciones As New List(Of String)

            Dim opcionColor As ComboBoxItem = cbOpcionMinimalColor.SelectedValue
            listaOpciones.Add(opcionColor.Content.ToString)

            Dim html As String = Await Decompiladores.HttpClient(New Uri("http://gamebanana.com/guis/download/27756"))

            Dim temp, temp2 As String
            Dim int, int2 As Integer

            If Not html = Nothing Then
                int = html.IndexOf(".zip")
                temp = html.Remove(int + 4, html.Length - (int + 4))

                int2 = temp.LastIndexOf("https://")
                temp2 = temp.Remove(0, int2)
            Else
                temp2 = Nothing
            End If

            Dim apariencia As Apariencia = New Apariencia("Minimal",
                                                          New Uri(temp2),
                                                          tbInformeMinimal, prInformeMinimal,
                                                          listaOpciones, gridPersonalizacionMinimal)

            Descarga.Iniciar(apariencia)

        ElseIf sp.Tag.ToString = 1 Then

            If gridPersonalizacionMinimal.Visibility = Visibility.Collapsed Then
                lvPersonalizacionMinimal.Background = New SolidColorBrush(App.Current.Resources("ColorPrimario"))
                gridPersonalizacionMinimal.Margin = New Thickness(lvDescargaMinimal.ActualWidth + 5, 0, 0, 0)
                gridPersonalizacionMinimal.Visibility = Visibility.Visible

                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("grid", lvPersonalizacionMinimal)

                Dim animacion As ConnectedAnimation = ConnectedAnimationService.GetForCurrentView().GetAnimation("grid")

                If Not animacion Is Nothing Then
                    animacion.TryStart(gridPersonalizacionMinimal)
                End If
            Else
                lvPersonalizacionMinimal.Background = New SolidColorBrush(App.Current.Resources("ColorSecundario"))
                gridPersonalizacionMinimal.Visibility = Visibility.Collapsed
            End If

        End If

    End Sub

    Private Async Sub LvAparienciaMinimal2ItemClick(sender As Object, args As ItemClickEventArgs)

        Dim sp As StackPanel = args.ClickedItem

        If sp.Tag.ToString = 0 Then

            Await Launcher.LaunchUriAsync(New Uri("http://steamcommunity.com/groups/MinimalSteamUI"))

        End If

    End Sub

    Private Sub BotonImagenMinimal1_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenMinimal1.Click

        AmpliarCaptura(imagenMinimal1)

    End Sub

    Private Sub BotonImagenMinimal2_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenMinimal2.Click

        AmpliarCaptura(imagenMinimal2)

    End Sub

    Private Sub BotonImagenMinimal3_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenMinimal3.Click

        AmpliarCaptura(imagenMinimal3)

    End Sub

    Private Sub BotonImagenMinimal4_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenMinimal4.Click

        AmpliarCaptura(imagenMinimal4)

    End Sub

    'SKINPIXELVISION2-----------------------------------------------------------------------------

    Private Sub LvAparienciaPixelVision21ItemClick(sender As Object, args As ItemClickEventArgs)

        Dim sp As StackPanel = args.ClickedItem

        If sp.Tag.ToString = 0 Then
            Dim apariencia As Apariencia = New Apariencia("PixelVision2",
                                                          New Uri("https://github.com/somini/Pixelvision2/archive/master.zip"),
                                                          tbInformePixelVision2, prInformePixelVision2,
                                                          Nothing, Nothing)

            Descarga.Iniciar(apariencia)

        End If

    End Sub

    Private Async Sub LvAparienciaPixelVision22ItemClick(sender As Object, args As ItemClickEventArgs)

        Dim sp As StackPanel = args.ClickedItem

        If sp.Tag.ToString = 0 Then

            Await Launcher.LaunchUriAsync(New Uri("https://steamcommunity.com/groups/pixelvision2"))

        End If

    End Sub

    Private Sub BotonImagenPixelVision21_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenPixelVision21.Click

        AmpliarCaptura(imagenPixelVision21)

    End Sub

    Private Sub BotonImagenPixelVision22_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenPixelVision22.Click

        AmpliarCaptura(imagenPixelVision22)

    End Sub

    Private Sub BotonImagenPixelVision23_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenPixelVision23.Click

        AmpliarCaptura(imagenPixelVision23)

    End Sub

    Private Sub BotonImagenPixelVision24_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenPixelVision24.Click

        AmpliarCaptura(imagenPixelVision24)

    End Sub

    'SKINPRESSURE2-----------------------------------------------------------------------------

    Private Sub LvAparienciaPressure21ItemClick(sender As Object, args As ItemClickEventArgs)

        Dim sp As StackPanel = args.ClickedItem

        If sp.Tag.ToString = 0 Then
            Dim listaOpciones As New List(Of String)

            Dim opcionGridTransparent As ComboBoxItem = cbOpcionPressure2GridTransparent.SelectedValue
            listaOpciones.Add(opcionGridTransparent.Content.ToString)

            Dim opcionOverlayBackground As ComboBoxItem = cbOpcionPressure2OverlayBackground.SelectedValue
            listaOpciones.Add(opcionOverlayBackground.Content.ToString)

            Dim apariencia As Apariencia = New Apariencia("Pressure2",
                                                          New Uri("https://github.com/DirtDiglett/Pressure2/archive/master.zip"),
                                                          tbInformePressure2, prInformePressure2,
                                                          listaOpciones, gridPersonalizacionPressure2)

            Descarga.Iniciar(apariencia)

        ElseIf sp.Tag.ToString = 1 Then

            If gridPersonalizacionPressure2.Visibility = Visibility.Collapsed Then
                lvPersonalizacionPressure2.Background = New SolidColorBrush(App.Current.Resources("ColorPrimario"))
                gridPersonalizacionPressure2.Margin = New Thickness(lvDescargaPressure2.ActualWidth + 5, 0, 0, 0)
                gridPersonalizacionPressure2.Visibility = Visibility.Visible

                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("grid", lvPersonalizacionPressure2)

                Dim animacion As ConnectedAnimation = ConnectedAnimationService.GetForCurrentView().GetAnimation("grid")

                If Not animacion Is Nothing Then
                    animacion.TryStart(gridPersonalizacionPressure2)
                End If
            Else
                lvPersonalizacionPressure2.Background = New SolidColorBrush(App.Current.Resources("ColorSecundario"))
                gridPersonalizacionPressure2.Visibility = Visibility.Collapsed
            End If

        End If

    End Sub

    Private Async Sub LvAparienciaPressure22ItemClick(sender As Object, args As ItemClickEventArgs)

        Dim sp As StackPanel = args.ClickedItem

        If sp.Tag.ToString = 0 Then

            Await Launcher.LaunchUriAsync(New Uri("http://www.pressureforsteam.com"))

        ElseIf sp.Tag.ToString = 1 Then

            Await Launcher.LaunchUriAsync(New Uri("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=WAC672ATU68E4"))

        ElseIf sp.Tag.ToString = 2 Then

            Await Launcher.LaunchUriAsync(New Uri("https://www.patreon.com/dirtdiglett"))

        End If

    End Sub

    Private Sub BotonImagenPressure21_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenPressure21.Click

        AmpliarCaptura(imagenPressure21)

    End Sub

    Private Sub BotonImagenPressure22_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenPressure22.Click

        AmpliarCaptura(imagenPressure22)

    End Sub

    Private Sub BotonImagenPressure23_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenPressure23.Click

        AmpliarCaptura(imagenPressure23)

    End Sub

    Private Sub BotonImagenPressure24_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenPressure24.Click

        AmpliarCaptura(imagenPressure24)

    End Sub

    'SKINTHRESHOLD-----------------------------------------------------------------------------

    Private Sub LvAparienciaThreshold1ItemClick(sender As Object, args As ItemClickEventArgs)

        Dim sp As StackPanel = args.ClickedItem

        If sp.Tag.ToString = 0 Then
            Dim listaOpciones As New List(Of String)

            Dim opcionColor As ComboBoxItem = cbOpcionThresholdColor.SelectedValue
            listaOpciones.Add(opcionColor.Content.ToString)

            Dim opcionOutlines As ComboBoxItem = cbOpcionThresholdOutlines.SelectedValue
            listaOpciones.Add(opcionOutlines.Content.ToString)

            Dim opcionTitlebar As ComboBoxItem = cbOpcionThresholdColoredTitlebar.SelectedValue
            listaOpciones.Add(opcionTitlebar.Content.ToString)

            Dim apariencia As Apariencia = New Apariencia("Threshold",
                                                          New Uri("https://github.com/Edgarware/Threshold-Skin/archive/master.zip"),
                                                          tbInformeThreshold, prInformeThreshold,
                                                          listaOpciones, gridPersonalizacionThreshold)

            Descarga.Iniciar(apariencia)

        ElseIf sp.Tag.ToString = 1 Then

            If gridPersonalizacionThreshold.Visibility = Visibility.Collapsed Then
                lvPersonalizacionThreshold.Background = New SolidColorBrush(App.Current.Resources("ColorPrimario"))
                gridPersonalizacionThreshold.Margin = New Thickness(lvDescargaThreshold.ActualWidth + 5, 0, 0, 0)
                gridPersonalizacionThreshold.Visibility = Visibility.Visible

                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("grid", lvPersonalizacionThreshold)

                Dim animacion As ConnectedAnimation = ConnectedAnimationService.GetForCurrentView().GetAnimation("grid")

                If Not animacion Is Nothing Then
                    animacion.TryStart(gridPersonalizacionThreshold)
                End If
            Else
                lvPersonalizacionThreshold.Background = New SolidColorBrush(App.Current.Resources("ColorSecundario"))
                gridPersonalizacionThreshold.Visibility = Visibility.Collapsed
            End If

        End If

    End Sub

    Private Async Sub LvAparienciaThreshold2ItemClick(sender As Object, args As ItemClickEventArgs)

        Dim sp As StackPanel = args.ClickedItem

        If sp.Tag.ToString = 0 Then

            Await Launcher.LaunchUriAsync(New Uri("https://steamcommunity.com/groups/thresholdskin"))

        End If

    End Sub

    Private Sub BotonImagenThreshold1_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenThreshold1.Click

        AmpliarCaptura(imagenThreshold1)

    End Sub

    Private Sub BotonImagenThreshold2_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenThreshold2.Click

        AmpliarCaptura(imagenThreshold2)

    End Sub

    Private Sub BotonImagenThreshold3_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenThreshold3.Click

        AmpliarCaptura(imagenThreshold3)

    End Sub

    Private Sub BotonImagenThreshold4_Click(sender As Object, e As RoutedEventArgs) Handles botonImagenThreshold4.Click

        AmpliarCaptura(imagenThreshold4)

    End Sub

    'CAPTURA-----------------------------------------------------------------------------

    Private Sub AmpliarCaptura(imagen As ImageEx)

        Dim recursos As Resources.ResourceLoader = New Resources.ResourceLoader()
        GridVisibilidad(gridCaptura, recursos.GetString("Screenshot"))
        imagenExpandida.Source = imagen.Source

    End Sub

    Private Sub BotonVolver_Click(sender As Object, e As RoutedEventArgs) Handles botonVolver.Click

        Dim recursos As Resources.ResourceLoader = New Resources.ResourceLoader()
        GridVisibilidad(gridApariencias, recursos.GetString("Skins"))

    End Sub

    'CONFIG-----------------------------------------------------------------------------

    Private Sub BotonSteamRuta_Click(sender As Object, e As RoutedEventArgs) Handles botonSteamRuta.Click

        Detector.Steam(True)

    End Sub

    Private Async Sub CbConfigMetodo_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbConfigMetodo.SelectionChanged

        Dim helper As LocalObjectStorageHelper = New LocalObjectStorageHelper
        Await helper.SaveFileAsync(Of String)("metodo", cbConfigMetodo.SelectedIndex)

    End Sub

    'MASCOSAS-----------------------------------------

    Private Async Sub LvMasCosasItemClick(sender As Object, args As ItemClickEventArgs)

        Dim sp As StackPanel = args.ClickedItem

        If sp.Tag.ToString = 0 Then

            Await Launcher.LaunchUriAsync(New Uri("ms-windows-store:REVIEW?PFN=" + Package.Current.Id.FamilyName))

        ElseIf sp.Tag.ToString = 1 Then

            NavegarMasCosas(lvMasCosasMasApps, "https://pepeizqapps.com/")

        ElseIf sp.Tag.ToString = 3 Then

            NavegarMasCosas(lvMasCosasContacto, "https://pepeizqapps.com/contact/")

        ElseIf sp.Tag.ToString = 4 Then

            If StoreServicesFeedbackLauncher.IsSupported = True Then
                Dim ejecutador As StoreServicesFeedbackLauncher = StoreServicesFeedbackLauncher.GetDefault()
                Await ejecutador.LaunchAsync()
            Else
                NavegarMasCosas(lvMasCosasReportarFallo, "https://pepeizqapps.com/contact/")
            End If

        ElseIf sp.Tag.ToString = 5 Then

            NavegarMasCosas(lvMasCosasTraduccion, "https://poeditor.com/join/project/LcYHFvuAzA")

        ElseIf sp.Tag.ToString = 6 Then

            NavegarMasCosas(lvMasCosasCodigoFuente, "https://github.com/pepeizq/Steam-Skins-UWP")

        End If

    End Sub

    Private Sub NavegarMasCosas(lvItem As ListViewItem, url As String)

        lvMasCosasMasApps.Background = New SolidColorBrush(App.Current.Resources("ColorSecundario"))
        lvMasCosasContacto.Background = New SolidColorBrush(App.Current.Resources("ColorSecundario"))
        lvMasCosasReportarFallo.Background = New SolidColorBrush(App.Current.Resources("ColorSecundario"))
        lvMasCosasTraduccion.Background = New SolidColorBrush(App.Current.Resources("ColorSecundario"))
        lvMasCosasCodigoFuente.Background = New SolidColorBrush(App.Current.Resources("ColorSecundario"))

        lvItem.Background = New SolidColorBrush(App.Current.Resources("ColorPrimario"))

        pbMasCosas.Visibility = Visibility.Visible

        wvMasCosas.Navigate(New Uri(url))

    End Sub

    Private Sub WvMasCosas_NavigationCompleted(sender As WebView, args As WebViewNavigationCompletedEventArgs) Handles wvMasCosas.NavigationCompleted

        pbMasCosas.Visibility = Visibility.Collapsed

    End Sub

End Class
