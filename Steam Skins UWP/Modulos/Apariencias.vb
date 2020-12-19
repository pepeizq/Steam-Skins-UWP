Imports Microsoft.Toolkit.Uwp.UI.Animations
Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.System
Imports Windows.UI
Imports Windows.UI.Core

Module Apariencias

    Public Sub Cargar()

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim gvApariencias As AdaptiveGridView = pagina.FindName("gvApariencias")
        gvApariencias.Items.Clear()

        Dim air As New Apariencia("Air for Steam", "https://github.com/airforsteam/Air-for-Steam/archive/master.zip", "Air-for-Steam-master",
                                  "https://github.com/airforsteam/Air-for-Steam", "Assets\Apariencias\air1.png", "Assets\Apariencias\air2.png", "Assets\Apariencias\air3.png")

        gvApariencias.Items.Add(GenerarBoton(air))

        Dim airc As New Apariencia("Air Classic", "https://github.com/airforsteam/Air-Classic/archive/master.zip", "Air-Classic-master",
                                   "https://github.com/airforsteam/Air-Classic", "Assets\Apariencias\airc1.png", "Assets\Apariencias\airc2.png", "Assets\Apariencias\airc3.png")

        gvApariencias.Items.Add(GenerarBoton(airc))

        Dim compact As New Apariencia("Compact", "https://github.com/badanka/Compact/archive/master.zip", "Compact-master",
                                      "https://github.com/badanka/Compact", "Assets\Apariencias\comp1.png", "Assets\Apariencias\comp2.png", "Assets\Apariencias\comp3.png")

        gvApariencias.Items.Add(GenerarBoton(compact))

        Dim metroUnofficial As New Apariencia("Metro Unofficial", "https://github.com/redsigma/UPMetroSkin/archive/master.zip", "UPMetroSkin-master\Unofficial 4.x Patch\Main Files [Install First]",
                                              "https://github.com/redsigma/UPMetroSkin", "Assets\Apariencias\metrou1.png", "Assets\Apariencias\metrou2.png", "Assets\Apariencias\metro3.png")

        gvApariencias.Items.Add(GenerarBoton(metroUnofficial))

        Dim original As New Apariencia("Original", "https://github.com/ungstein/OG-Steam/archive/main.zip", "OG-Steam-main\OG-Steam\",
                                       "https://github.com/ungstein/OG-Steam", "Assets\Apariencias\ori1.png", "Assets\Apariencias\ori2.png", "Assets\Apariencias\ori3.png")

        gvApariencias.Items.Add(GenerarBoton(original))

        Dim pixelvision2 As New Apariencia("Pixelvision2", "https://github.com/somini/Pixelvision2/archive/master.zip", "Pixelvision2-master",
                                           "https://github.com/somini/Pixelvision2", "Assets\Apariencias\pixe1.png", "Assets\Apariencias\pixe2.png", "Assets\Apariencias\pixe3.png")

        gvApariencias.Items.Add(GenerarBoton(pixelvision2))

        Dim threshold As New Apariencia("Threshold", "https://github.com/Edgarware/Threshold-Skin/archive/master.zip", "Threshold-Skin-master",
                                        "https://github.com/Edgarware/Threshold-Skin", "Assets\Apariencias\thre1.png", "Assets\Apariencias\thre2.png", "Assets\Apariencias\thre3.png")

        gvApariencias.Items.Add(GenerarBoton(threshold))

        Dim thresholdMiku As New Apariencia("Threshold Miku", "https://github.com/Jack-Myth/Threshold-Miku/archive/master.zip", "Threshold-Miku-master",
                                            "https://github.com/Jack-Myth/Threshold-Miku", "Assets\Apariencias\miku1.png", "Assets\Apariencias\miku2.png", "Assets\Apariencias\miku3.png")

        gvApariencias.Items.Add(GenerarBoton(thresholdMiku))

    End Sub

    Private Function GenerarBoton(apariencia As Apariencia)

        Dim fondo As New SolidColorBrush With {
            .Color = App.Current.Resources("ColorCuarto"),
            .Opacity = 0.7
        }

        Dim boton As New Button With {
            .Padding = New Thickness(0, 0, 0, 0),
            .Background = fondo,
            .BorderBrush = New SolidColorBrush(App.Current.Resources("ColorPrimario")),
            .BorderThickness = New Thickness(1, 1, 1, 1),
            .Margin = New Thickness(10, 10, 10, 10),
            .Tag = apariencia
        }

        Dim grid As New Grid With {
            .Tag = apariencia
        }

        Dim row1 As New RowDefinition
        Dim row2 As New RowDefinition

        row1.Height = New GridLength(1, GridUnitType.Star)
        row2.Height = New GridLength(1, GridUnitType.Auto)

        grid.RowDefinitions.Add(row1)
        grid.RowDefinitions.Add(row2)

        Dim imagen As New ImageEx With {
            .IsCacheEnabled = True,
            .Stretch = Stretch.UniformToFill,
            .HorizontalAlignment = HorizontalAlignment.Stretch,
            .VerticalAlignment = VerticalAlignment.Stretch,
            .Source = apariencia.Imagen1,
            .EnableLazyLoading = True
        }

        imagen.SetValue(Grid.RowProperty, 0)
        grid.Children.Add(imagen)

        Dim tb As New TextBlock With {
            .Foreground = New SolidColorBrush(Colors.White),
            .Margin = New Thickness(0, 15, 0, 15),
            .VerticalAlignment = VerticalAlignment.Center,
            .HorizontalAlignment = HorizontalAlignment.Center,
            .Text = apariencia.Titulo,
            .FontSize = 17
        }

        tb.SetValue(Grid.RowProperty, 1)
        grid.Children.Add(tb)

        boton.Content = grid

        AddHandler boton.Click, AddressOf AparienciaClick
        AddHandler boton.PointerEntered, AddressOf UsuarioEntraBoton
        AddHandler boton.PointerExited, AddressOf UsuarioSaleBoton

        Return boton

    End Function

    Private Sub UsuarioEntraBoton(sender As Object, e As PointerRoutedEventArgs)

        Dim boton As Button = sender
        Dim grid As Grid = boton.Content
        Dim imagen As ImageEx = grid.Children(0)

        grid.Saturation(1).Scale(1.01, 1.01, grid.ActualWidth / 2, grid.ActualHeight / 2).Start()

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

    End Sub

    Private Sub UsuarioSaleBoton(sender As Object, e As PointerRoutedEventArgs)

        Dim boton As Button = sender
        Dim grid As Grid = boton.Content
        Dim imagen As ImageEx = grid.Children(0)

        grid.Saturation(1).Scale(1, 1, grid.ActualWidth / 2, grid.ActualHeight / 2).Start()

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

    End Sub

    Private Sub AparienciaClick(sender As Object, e As RoutedEventArgs)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim boton As Button = sender
        Dim apariencia As Apariencia = boton.Tag

        Dim gridAparienciaElegida As Grid = pagina.FindName("gridAparienciaElegida")
        Interfaz.Pestañas.Visibilidad_Pestañas(gridAparienciaElegida, apariencia.Titulo)

        Dim tbRegistro As TextBlock = pagina.FindName("tbRegistro")
        tbRegistro.Text = String.Empty

        Dim tbAparienciaSeleccionada As TextBlock = pagina.FindName("tbAparienciaSeleccionada")
        tbAparienciaSeleccionada.Text = apariencia.Titulo

        Dim botonDescarga As Button = pagina.FindName("botonDescargaApariencia")
        botonDescarga.Tag = apariencia

        RemoveHandler botonDescarga.Click, AddressOf DescargarClick
        AddHandler botonDescarga.Click, AddressOf DescargarClick

        RemoveHandler botonDescarga.PointerEntered, AddressOf Interfaz.EfectosHover.Entra_Boton_IconoTexto
        AddHandler botonDescarga.PointerEntered, AddressOf Interfaz.EfectosHover.Entra_Boton_IconoTexto

        RemoveHandler botonDescarga.PointerExited, AddressOf Interfaz.EfectosHover.Sale_Boton_IconoTexto
        AddHandler botonDescarga.PointerExited, AddressOf Interfaz.EfectosHover.Sale_Boton_IconoTexto

        Dim botonCodigoFuente As Button = pagina.FindName("botonCodigoFuente")
        botonCodigoFuente.Tag = apariencia.Github

        RemoveHandler botonCodigoFuente.Click, AddressOf CodigoFuenteClick
        AddHandler botonCodigoFuente.Click, AddressOf CodigoFuenteClick

        RemoveHandler botonCodigoFuente.PointerEntered, AddressOf Interfaz.EfectosHover.Entra_Boton_IconoTexto
        AddHandler botonCodigoFuente.PointerEntered, AddressOf Interfaz.EfectosHover.Entra_Boton_IconoTexto

        RemoveHandler botonCodigoFuente.PointerExited, AddressOf Interfaz.EfectosHover.Sale_Boton_IconoTexto
        AddHandler botonCodigoFuente.PointerExited, AddressOf Interfaz.EfectosHover.Sale_Boton_IconoTexto

        Dim imagen1 As ImageEx = pagina.FindName("imagenApariencia1")
        imagen1.Source = apariencia.Imagen1

        Dim botonImagenApariencia1 As Button = pagina.FindName("botonImagenApariencia1")

        RemoveHandler botonImagenApariencia1.Click, AddressOf AmpliarCapturaClick
        AddHandler botonImagenApariencia1.Click, AddressOf AmpliarCapturaClick

        RemoveHandler botonImagenApariencia1.PointerEntered, AddressOf Interfaz.EfectosHover.Entra_Basico
        AddHandler botonImagenApariencia1.PointerEntered, AddressOf Interfaz.EfectosHover.Entra_Basico

        RemoveHandler botonImagenApariencia1.PointerExited, AddressOf Interfaz.EfectosHover.Sale_Basico
        AddHandler botonImagenApariencia1.PointerExited, AddressOf Interfaz.EfectosHover.Sale_Basico

        Dim imagen2 As ImageEx = pagina.FindName("imagenApariencia2")
        imagen2.Source = apariencia.Imagen2

        Dim botonImagenApariencia2 As Button = pagina.FindName("botonImagenApariencia2")

        RemoveHandler botonImagenApariencia2.Click, AddressOf AmpliarCapturaClick
        AddHandler botonImagenApariencia2.Click, AddressOf AmpliarCapturaClick

        RemoveHandler botonImagenApariencia2.PointerEntered, AddressOf Interfaz.EfectosHover.Entra_Basico
        AddHandler botonImagenApariencia2.PointerEntered, AddressOf Interfaz.EfectosHover.Entra_Basico

        RemoveHandler botonImagenApariencia2.PointerExited, AddressOf Interfaz.EfectosHover.Sale_Basico
        AddHandler botonImagenApariencia2.PointerExited, AddressOf Interfaz.EfectosHover.Sale_Basico

        Dim imagen3 As ImageEx = pagina.FindName("imagenApariencia3")
        imagen3.Source = apariencia.Imagen3

        Dim botonImagenApariencia3 As Button = pagina.FindName("botonImagenApariencia3")

        RemoveHandler botonImagenApariencia3.Click, AddressOf AmpliarCapturaClick
        AddHandler botonImagenApariencia3.Click, AddressOf AmpliarCapturaClick

        RemoveHandler botonImagenApariencia3.PointerEntered, AddressOf Interfaz.EfectosHover.Entra_Basico
        AddHandler botonImagenApariencia3.PointerEntered, AddressOf Interfaz.EfectosHover.Entra_Basico

        RemoveHandler botonImagenApariencia3.PointerExited, AddressOf Interfaz.EfectosHover.Sale_Basico
        AddHandler botonImagenApariencia3.PointerExited, AddressOf Interfaz.EfectosHover.Sale_Basico

        Dim botonVolver As Button = pagina.FindName("botonVolver")

        RemoveHandler botonVolver.Click, AddressOf VolverClick
        AddHandler botonVolver.Click, AddressOf VolverClick

        RemoveHandler botonVolver.PointerEntered, AddressOf Interfaz.EfectosHover.Entra_Boton_IconoTexto
        AddHandler botonVolver.PointerEntered, AddressOf Interfaz.EfectosHover.Entra_Boton_IconoTexto

        RemoveHandler botonVolver.PointerExited, AddressOf Interfaz.EfectosHover.Sale_Boton_IconoTexto
        AddHandler botonVolver.PointerExited, AddressOf Interfaz.EfectosHover.Sale_Boton_IconoTexto

    End Sub

    Private Sub DescargarClick(sender As Object, e As RoutedEventArgs)

        Dim boton As Button = sender
        Dim aparienciaElegida As Apariencia = boton.Tag

        Descarga.Iniciar(aparienciaElegida)

    End Sub

    Private Async Sub CodigoFuenteClick(sender As Object, e As RoutedEventArgs)

        Dim boton As Button = sender

        Await Launcher.LaunchUriAsync(New Uri(boton.Tag))

    End Sub

    Private Sub AmpliarCapturaClick(sender As Object, e As RoutedEventArgs)

        Dim boton As Button = sender
        Dim imagen As ImageEx = boton.Content

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim gridCaptura As Grid = pagina.FindName("gridCaptura")
        Interfaz.Pestañas.Visibilidad_Pestañas(gridCaptura, Nothing)

        Dim imagenExpandida As ImageEx = pagina.FindName("imagenExpandida")
        imagenExpandida.Source = imagen.Source

    End Sub

    Private Sub VolverClick(sender As Object, e As RoutedEventArgs)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim gridApariencia As Grid = pagina.FindName("gridAparienciaElegida")
        Interfaz.Pestañas.Visibilidad_Pestañas(gridApariencia, Nothing)

    End Sub

End Module