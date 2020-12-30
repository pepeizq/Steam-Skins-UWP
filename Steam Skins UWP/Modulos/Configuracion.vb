Module Configuracion

    Public Sub Cargar()

        Dim recursos As New Resources.ResourceLoader()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim botonAbrirConfig As Button = pagina.FindName("botonAbrirConfig")

        AddHandler botonAbrirConfig.Click, AddressOf AbrirConfigClick
        AddHandler botonAbrirConfig.PointerEntered, AddressOf Interfaz.EfectosHover.Entra_Boton_IconoTexto
        AddHandler botonAbrirConfig.PointerExited, AddressOf Interfaz.EfectosHover.Sale_Boton_IconoTexto

        Dim botonRutaTexto As TextBlock = pagina.FindName("botonSteamRutaTexto")
        botonRutaTexto.Text = recursos.GetString("ConfigSearch")

        Dim botonSteamRuta As Button = pagina.FindName("botonSteamRuta")

        AddHandler botonSteamRuta.Click, AddressOf BuscarCarpetaClick
        AddHandler botonSteamRuta.PointerEntered, AddressOf Interfaz.EfectosHover.Entra_Boton_IconoTexto
        AddHandler botonSteamRuta.PointerExited, AddressOf Interfaz.EfectosHover.Sale_Boton_IconoTexto

    End Sub

    Private Sub AbrirConfigClick(sender As Object, e As RoutedEventArgs)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim gridConfig As Grid = pagina.FindName("gridConfig")

        Dim recursos As New Resources.ResourceLoader()
        Interfaz.Pestañas.Visibilidad(gridConfig, recursos.GetString("Config"), sender)

    End Sub

    Private Sub BuscarCarpetaClick(sender As Object, e As RoutedEventArgs)

        Detector.Steam(True)

    End Sub

End Module
