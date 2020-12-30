Imports Windows.UI.Xaml.Media.Animation

Namespace Interfaz
    Module Pestañas

        Public Sub Visibilidad(gridMostrar As Grid, tag As String, origen As Object)

            Dim recursos As New Resources.ResourceLoader()

            Dim frame As Frame = Window.Current.Content
            Dim pagina As Page = frame.Content

            Dim tbTitulo As TextBlock = pagina.FindName("tbTitulo")
            tbTitulo.Text = recursos.GetString("Title") + " (" + Package.Current.Id.Version.Major.ToString + "." + Package.Current.Id.Version.Minor.ToString + "." + Package.Current.Id.Version.Build.ToString + "." + Package.Current.Id.Version.Revision.ToString + ")"

            If Not tag = Nothing Then
                tbTitulo.Text = tbTitulo.Text + " • " + tag
            End If

            Dim gridAviso As Grid = pagina.FindName("gridAviso")
            gridAviso.Visibility = Visibility.Collapsed

            Dim gridApariencias As Grid = pagina.FindName("gridApariencias")
            gridApariencias.Visibility = Visibility.Collapsed

            Dim gridAparienciaElegida As Grid = pagina.FindName("gridAparienciaElegida")
            gridAparienciaElegida.Visibility = Visibility.Collapsed

            Dim gridCaptura As Grid = pagina.FindName("gridCaptura")
            gridCaptura.Visibility = Visibility.Collapsed

            Dim gridTutorial As Grid = pagina.FindName("gridTutorial")
            gridTutorial.Visibility = Visibility.Collapsed

            Dim gridConfig As Grid = pagina.FindName("gridConfig")
            gridConfig.Visibility = Visibility.Collapsed

            gridMostrar.Visibility = Visibility.Visible

            '--------------------------------------------------------

            If Not origen Is Nothing Then
                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("animacion", origen)
                Dim animacion As ConnectedAnimation = ConnectedAnimationService.GetForCurrentView().GetAnimation("animacion")

                If Not animacion Is Nothing Then
                    animacion.Configuration = New DirectConnectedAnimationConfiguration
                    animacion.TryStart(gridMostrar)
                End If
            End If

        End Sub

    End Module
End Namespace

