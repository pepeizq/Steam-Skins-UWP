Public NotInheritable Class MainPage
    Inherits Page

    Private Sub Nv_Loaded(sender As Object, e As RoutedEventArgs)

        Dim recursos As New Resources.ResourceLoader()

        nvPrincipal.MenuItems.Add(Interfaz.NavigationViewItems.Generar(recursos.GetString("Skins"), FontAwesome5.EFontAwesomeIcon.Solid_Home))
        nvPrincipal.MenuItems.Add(Interfaz.NavigationViewItems.Generar(recursos.GetString("Tutorial2"), FontAwesome5.EFontAwesomeIcon.Solid_InfoCircle))
        nvPrincipal.MenuItems.Add(Interfaz.NavigationViewItems.Generar(recursos.GetString("Config"), FontAwesome5.EFontAwesomeIcon.Solid_Cog))
        nvPrincipal.MenuItems.Add(New NavigationViewItemSeparator)
        nvPrincipal.MenuItems.Add(Interfaz.NavigationViewItems.Generar(recursos.GetString("MoreSteam"), FontAwesome5.EFontAwesomeIcon.Brands_Steam))
        nvPrincipal.MenuItems.Add(Interfaz.NavigationViewItems.Generar(recursos.GetString("MoreThings"), FontAwesome5.EFontAwesomeIcon.Solid_Cube))

    End Sub

    Private Sub Nv_ItemInvoked(sender As NavigationView, args As NavigationViewItemInvokedEventArgs)

        Dim recursos As New Resources.ResourceLoader()

        Dim item As TextBlock = args.InvokedItem

        If Not item Is Nothing Then
            If item.Text = recursos.GetString("Skins") Then
                Interfaz.Pestañas.Visibilidad_Pestañas(gridApariencias, item.Text)
            ElseIf item.Text = recursos.GetString("Tutorial2") Then
                Interfaz.Pestañas.Visibilidad_Pestañas(gridTutorial, item.Text)
            ElseIf item.Text = recursos.GetString("Config") Then
                Interfaz.Pestañas.Visibilidad_Pestañas(gridConfig, item.Text)
            ElseIf item.Text = recursos.GetString("MoreSteam") Then
                Interfaz.Pestañas.Visibilidad_Pestañas(gridMasSteam, item.Text)
            ElseIf item.Text = recursos.GetString("MoreThings") Then
                Interfaz.Pestañas.Visibilidad_Pestañas(gridMasCosas, item.Text)
            End If
        End If

    End Sub

    Private Sub Page_Loaded(sender As FrameworkElement, args As Object)

        'Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = "es-ES"
        'Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = "en-US"

        Detector.Steam(False)
        Configuracion.Cargar()
        Apariencias.Cargar()
        Interfaz.MasSteam.Cargar()
        MasCosas.Cargar()

    End Sub

End Class
