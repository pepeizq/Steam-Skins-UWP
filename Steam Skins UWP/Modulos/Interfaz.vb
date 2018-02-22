Imports Microsoft.Toolkit.Uwp.UI.Controls
Imports Windows.Storage
Imports Windows.UI
Imports Windows.UI.Core

Module Interfaz

    Public Sub GenerarListado(gv As GridView)

        Dim airListado1 As New List(Of String) From {
            "Light", "Dark"
        }

        Dim airListado2 As New List(Of String) From {
            "Sky", "Sea", "Breeze", "Slate", "Truffle", "Gunmetal", "Silver", "Grass",
            "Rose", "Cinnabar", "Lavender", "Lilac", "Deeppurple", "Steamblue", "Youtubered"
        }

        Dim air As New Apariencia("Air", "https://github.com/Outsetini/Air-for-Steam/archive/master.zip", "Air-for-Steam-master",
                                   Nothing, "https://www.patreon.com/inhibitor", "http://airforsteam.com",
                                   "Theme", airListado1, "Color", airListado2, Nothing, Nothing, Nothing, Nothing,
                                   "Assets\Skins\air1.PNG", "Assets\Skins\air2.PNG", "Assets\Skins\air3.PNG", "Assets\Skins\air4.PNG")

        gv.Items.Add(GenerarListadoItem(air))

        '------------------------------------------------

        Dim airClassicListado1 As New List(Of String) From {
            "Blue", "Bubblegum", "Cinnamon", "Green", "Happyorange", "Navy", "Night", "Orange", "Padawan",
            "Royal", "Silver", "Teal", "Watermelon"}

        Dim airClassicListado2 As New List(Of String) From {
            "Steamblue", "Colorized"}

        Dim airClassicListado3 As New List(Of String) From {
            "None", "Noise", "Dots", "Ribbon"}

        Dim airClassic As New Apariencia("Air Classic", "https://github.com/Outsetini/Air-Classic/archive/master.zip", "Air-Classic-master",
                                          Nothing, "https://www.patreon.com/inhibitor", "http://airforsteam.com",
                                          "Color", airClassicListado1, "Games Details", airClassicListado2, "Background", airClassicListado3, Nothing, Nothing,
                                          "Assets\Skins\airclassic1.PNG", "Assets\Skins\airclassic2.PNG", "Assets\Skins\airclassic3.PNG", "Assets\Skins\airclassic4.PNG")

        gv.Items.Add(GenerarListadoItem(airClassic))

        '------------------------------------------------

        Dim blueSteel As New Apariencia("Blue Steel", "https://gamebanana.com/dl/20283", "Blue Steel",
                                        "https://www.paypal.com/us/cgi-bin/webscr?cmd=_flow&SESSION=gU9Q_xoI6qn8m_-QTAMVYqZk9Mn24pPmf_JMeWbpBB7tB1lJ9voLT3ewCF8&dispatch=5885d80a13c0db1f8e263663d3faee8dc3f308debf7330dd8d0b0a9f21afd7d3&rapidsState=Donation__DonationFlow___StateDonationLogin&rapidsStateSignature=99f4d0df56b1d7c5f803efd0556e52c65ab59680",
                                        Nothing, "http://steamcommunity.com/groups/bluesteelskin",
                                        Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                        "Assets\Skins\blue1.PNG", "Assets\Skins\blue2.PNG", "Assets\Skins\blue3.PNG", "Assets\Skins\blue4.PNG")

        gv.Items.Add(GenerarListadoItem(blueSteel))

        '------------------------------------------------

        Dim compact As New Apariencia("Compact", "https://github.com/badanka/Compact/archive/master.zip", "Compact-master",
                                      Nothing, Nothing, "http://badanka.github.io/Compact/",
                                      Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                      "Assets\Skins\compact1.PNG", "Assets\Skins\compact2.PNG", "Assets\Skins\compact3.PNG", "Assets\Skins\compact4.PNG")

        gv.Items.Add(GenerarListadoItem(compact))

        '------------------------------------------------

        Dim invert As New Apariencia("Invert", "https://gamebanana.com/dl/22503", "Invert",
                                     "https://www.paypal.com/sk/cgi-bin/webscr?cmd=_flow&SESSION=XyLR0yV_beniveGJ0ONtunwPaUfnuwR7BtktA-E2xhApEH_8hG8e2s_Frm0&dispatch=5885d80a13c0db1f8e263663d3faee8d4fe1dd75ca3bd4f11d72275b28239088",
                                     Nothing, "http://gamebanana.com/guis/28814",
                                     Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                     "Assets\Skins\invert1.PNG", "Assets\Skins\invert2.PNG", "Assets\Skins\invert3.PNG", "Assets\Skins\invert4.PNG")

        gv.Items.Add(GenerarListadoItem(invert))

        '------------------------------------------------

        Dim flatGreen As New Apariencia("Flat Green", "https://github.com/jonnyboy0719/Flat-Green-Steam/archive/master.zip", "Flat-Green-Steam-master\OFGSremake",
                                        Nothing, Nothing, "https://github.com/jonnyboy0719/Flat-Green-Steam/",
                                        Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                        "Assets\Skins\flat1.PNG", "Assets\Skins\flat2.PNG", "Assets\Skins\flat3.PNG", "Assets\Skins\flat4.PNG")

        gv.Items.Add(GenerarListadoItem(flatGreen))

        '------------------------------------------------

        Dim metroListado1 As New List(Of String) From {
            "Blue", "Cyan", "Dark Blue", "Dark Cyan", "Dark Green", "Green", "Orange", "Pink", "Purple", "Red"
        }

        Dim metro As New Apariencia("Metro", "http://www.metroforsteam.com", Nothing,
                                    "https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=BDL2J3MEETZ3J&lc=US&item_name=Metro%20for%20Steam&item_number=metroforsteam&currency_code=USD&bn=PP%2dDonationsBF%3abtn_donate_LG%2egif%3aNonHosted",
                                    "http://www.patreon.com/dommini", "http://www.metroforsteam.com",
                                    "Color", metroListado1, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                    "Assets\Skins\metro1.PNG", "Assets\Skins\metro2.PNG", "Assets\Skins\metro3.PNG", "Assets\Skins\metro4.PNG")

        gv.Items.Add(GenerarListadoItem(metro))

        '------------------------------------------------

        Dim pixelVision2 As New Apariencia("PixelVision2", "https://github.com/somini/Pixelvision2/archive/master.zip", "Pixelvision2-master",
                                           Nothing, Nothing, "https://steamcommunity.com/groups/pixelvision2",
                                           Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                           "Assets\Skins\pixel1.PNG", "Assets\Skins\pixel2.PNG", "Assets\Skins\pixel3.PNG", "Assets\Skins\pixel4.PNG")

        gv.Items.Add(GenerarListadoItem(pixelVision2))

        '------------------------------------------------

        Dim pressure2Listado1 As New List(Of String) From {
            "Yes", "No"
        }

        Dim pressure2Listado2 As New List(Of String) From {
            "Yes", "No"
        }

        Dim pressure2 As New Apariencia("Pressure2", "https://github.com/DirtDiglett/Pressure2/archive/master.zip", "Pressure2-master",
                                        "https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=WAC672ATU68E4", "https://www.patreon.com/dirtdiglett", "http://www.pressureforsteam.com",
                                        "Grid Uninstalled Transparency", pressure2Listado1, "Overlay Background", pressure2Listado2, Nothing, Nothing, Nothing, Nothing,
                                        "Assets\Skins\pre1.PNG", "Assets\Skins\pre2.PNG", "Assets\Skins\pre3.PNG", "Assets\Skins\pre4.PNG")

        gv.Items.Add(GenerarListadoItem(pressure2))

        '------------------------------------------------

        Dim steam2013 As New Apariencia("Steam 2013", "https://gamebanana.com/dl/21134", "Steam2013",
                                        "https://www.paypal.com/donate/?token=nyrcJMkXG5jZDPMwZoL3NalPOEbjFtBtCpMEHd3Dz4lRKcBs8VimWtppKQRpNwlJSvH_U0&country.x=FI&locale.x=",
                                        Nothing, "https://gamebanana.com/guis/30190",
                                        Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing,
                                        "Assets\Skins\20131.PNG", "Assets\Skins\20132.PNG", "Assets\Skins\20133.PNG", "Assets\Skins\20134.PNG")

        gv.Items.Add(GenerarListadoItem(steam2013))

        '------------------------------------------------

        Dim thresholdListado1 As New List(Of String) From {
            "Cobalt", "Cyan", "Green", "Orange", "Purple", "Red"
        }

        Dim thresholdListado2 As New List(Of String) From {
            "No", "Yes"
        }

        Dim thresholdListado3 As New List(Of String) From {
            "No", "Yes"
        }

        Dim threshold As New Apariencia("Threshold", "https://github.com/Edgarware/Threshold-Skin/archive/master.zip", "Threshold-Skin-master",
                                        Nothing, Nothing, "https://steamcommunity.com/groups/thresholdskin",
                                        "Color", thresholdListado1, "Outlines", thresholdListado2, "Colored Titlebar", thresholdListado3, Nothing, Nothing,
                                        "Assets\Skins\thr1.PNG", "Assets\Skins\thr2.PNG", "Assets\Skins\thr3.PNG", "Assets\Skins\thr4.PNG")

        gv.Items.Add(GenerarListadoItem(threshold))

        '------------------------------------------------

        AddHandler gv.ItemClick, AddressOf GvItemClick

    End Sub

    Private Function GenerarListadoItem(apariencia As Apariencia)

        Dim item As New GridViewItem With {
            .Padding = New Thickness(0, 0, 0, 0),
            .Width = 350,
            .Height = 250,
            .Background = New SolidColorBrush(App.Current.Resources("ColorSecundario")),
            .BorderBrush = New SolidColorBrush(App.Current.Resources("ColorSecundario")),
            .BorderThickness = New Thickness(1, 1, 1, 1),
            .Margin = New Thickness(10, 10, 10, 10)
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
            .Source = apariencia.Imagen1
        }

        imagen.SetValue(Grid.RowProperty, 0)
        grid.Children.Add(imagen)

        Dim tb As New TextBlock With {
            .Foreground = New SolidColorBrush(Colors.White),
            .Margin = New Thickness(0, 10, 0, 10),
            .VerticalAlignment = VerticalAlignment.Center,
            .HorizontalAlignment = HorizontalAlignment.Center,
            .Text = apariencia.Titulo
        }

        tb.SetValue(Grid.RowProperty, 1)
        grid.Children.Add(tb)

        item.Content = grid

        AddHandler item.PointerEntered, AddressOf UsuarioEntraBoton
        AddHandler item.PointerExited, AddressOf UsuarioSaleBoton

        Return item

    End Function

    Private Sub UsuarioEntraBoton(sender As Object, e As PointerRoutedEventArgs)

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Hand, 1)

    End Sub

    Private Sub UsuarioSaleBoton(sender As Object, e As PointerRoutedEventArgs)

        Window.Current.CoreWindow.PointerCursor = New CoreCursor(CoreCursorType.Arrow, 1)

    End Sub

    Private Sub GvItemClick(sender As Object, e As ItemClickEventArgs)

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim item As Grid = e.ClickedItem
        Dim apariencia As Apariencia = item.Tag

        Dim tbTitulo As TextBlock = pagina.FindName("tbTitulo")
        tbTitulo.Text = Package.Current.DisplayName + " (" + Package.Current.Id.Version.Major.ToString + "." + Package.Current.Id.Version.Minor.ToString + "." + Package.Current.Id.Version.Build.ToString + "." + Package.Current.Id.Version.Revision.ToString + ") - " + apariencia.Titulo

        Dim gridAparienciaElegida As Grid = pagina.FindName("gridAparienciaElegida")
        gridAparienciaElegida.Visibility = Visibility.Visible

        Dim tbAparienciaSeleccionada As TextBlock = pagina.FindName("tbAparienciaSeleccionada")
        tbAparienciaSeleccionada.Text = apariencia.Titulo

        Dim mostrarDonacion As Boolean = False

        If Not apariencia.DonacionPaypal = Nothing Then
            mostrarDonacion = True
        End If

        If Not apariencia.DonacionPatreon = Nothing Then
            mostrarDonacion = True
        End If

        If mostrarDonacion = True Then
            Dim spDonacion As StackPanel = pagina.FindName("spDonacion")
            spDonacion.Visibility = Visibility.Visible

            If Not apariencia.DonacionPaypal = Nothing Then
                Dim hlBoton As HyperlinkButton = pagina.FindName("hlBotonDonacionPaypal")
                hlBoton.Visibility = Visibility.Visible
                hlBoton.NavigateUri = New Uri(apariencia.DonacionPaypal)
            End If

            If Not apariencia.DonacionPatreon = Nothing Then
                Dim hlBoton As HyperlinkButton = pagina.FindName("hlBotonDonacionPatreon")
                hlBoton.Visibility = Visibility.Visible
                hlBoton.NavigateUri = New Uri(apariencia.DonacionPatreon)
            End If
        End If

        If Not apariencia.WebCreador = Nothing Then
            Dim hlBoton As HyperlinkButton = pagina.FindName("hlBotonWebCreador")
            hlBoton.Visibility = Visibility.Visible
            hlBoton.NavigateUri = New Uri(apariencia.WebCreador)
        End If

        Dim botonDescarga As Button = pagina.FindName("botonDescargaApariencia")
        botonDescarga.Tag = apariencia

        Dim imagen1 As ImageEx = pagina.FindName("imagenApariencia1")
        imagen1.Source = apariencia.Imagen1

        Dim imagen2 As ImageEx = pagina.FindName("imagenApariencia2")
        imagen2.Source = apariencia.Imagen2

        Dim imagen3 As ImageEx = pagina.FindName("imagenApariencia3")
        imagen3.Source = apariencia.Imagen3

        Dim imagen4 As ImageEx = pagina.FindName("imagenApariencia4")
        imagen4.Source = apariencia.Imagen4

        Dim botonPersonalizacion As Button = pagina.FindName("botonPersonalizacion")

        If apariencia.Opcion1 = Nothing Then
            botonPersonalizacion.Visibility = Visibility.Collapsed
        ElseIf Not apariencia.Opcion1 = Nothing Then
            botonPersonalizacion.Visibility = Visibility.Visible

            Dim menu As New MenuFlyout With {
                .Placement = FlyoutPlacementMode.Bottom
            }

            Dim submenu1 As New MenuFlyoutSubItem With {
                .Text = apariencia.Opcion1
            }

            Dim i As Integer = 0
            For Each opcion In apariencia.OpcionListado1
                Dim subopcion As New ToggleMenuFlyoutItem With {
                    .Text = opcion,
                    .Tag = submenu1
                }

                AddHandler subopcion.Click, AddressOf SubopcionClick

                If i = 0 Then
                    subopcion.IsChecked = True
                End If

                submenu1.Items.Add(subopcion)
                i += 1
            Next

            menu.Items.Add(submenu1)

            If Not apariencia.Opcion2 = Nothing Then
                Dim submenu2 As New MenuFlyoutSubItem With {
                    .Text = apariencia.Opcion2
                }

                i = 0
                For Each opcion In apariencia.OpcionListado2
                    Dim subopcion As New ToggleMenuFlyoutItem With {
                        .Text = opcion,
                        .Tag = submenu2
                    }

                    AddHandler subopcion.Click, AddressOf SubopcionClick

                    If i = 0 Then
                        subopcion.IsChecked = True
                    End If

                    submenu2.Items.Add(subopcion)
                    i += 1
                Next

                menu.Items.Add(submenu2)
            End If

            If Not apariencia.Opcion3 = Nothing Then
                Dim submenu3 As New MenuFlyoutSubItem With {
                    .Text = apariencia.Opcion3
                }

                i = 0
                For Each opcion In apariencia.OpcionListado3
                    Dim subopcion As New ToggleMenuFlyoutItem With {
                        .Text = opcion,
                        .Tag = submenu3
                    }

                    AddHandler subopcion.Click, AddressOf SubopcionClick

                    If i = 0 Then
                        subopcion.IsChecked = True
                    End If

                    submenu3.Items.Add(subopcion)
                    i += 1
                Next

                menu.Items.Add(submenu3)
            End If

            If Not apariencia.Opcion4 = Nothing Then
                Dim submenu4 As New MenuFlyoutSubItem With {
                    .Text = apariencia.Opcion4
                }

                i = 0
                For Each opcion In apariencia.OpcionListado3
                    Dim subopcion As New ToggleMenuFlyoutItem With {
                        .Text = opcion,
                        .Tag = submenu4
                    }

                    AddHandler subopcion.Click, AddressOf SubopcionClick

                    If i = 0 Then
                        subopcion.IsChecked = True
                    End If

                    submenu4.Items.Add(subopcion)
                    i += 1
                Next

                menu.Items.Add(submenu4)
            End If

            botonPersonalizacion.Flyout = menu
        End If

    End Sub

    Private Sub SubopcionClick(sender As Object, e As RoutedEventArgs)

        Dim subopcion As ToggleMenuFlyoutItem = sender
        Dim menu As MenuFlyoutSubItem = subopcion.Tag

        For Each item As ToggleMenuFlyoutItem In menu.Items
            item.IsChecked = False
        Next

        subopcion.IsChecked = True

        Dim frame As Frame = Window.Current.Content
        Dim pagina As Page = frame.Content

        Dim apariencia As String = Nothing

        Dim tbAparienciaSeleccionada As TextBlock = pagina.FindName("tbAparienciaSeleccionada")
        apariencia = tbAparienciaSeleccionada.Text + menu.Text

        ApplicationData.Current.LocalSettings.Values(apariencia) = subopcion.Text

    End Sub

End Module