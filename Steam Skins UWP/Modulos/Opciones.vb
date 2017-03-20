Module Opciones

    Dim listaOpciones As List(Of String)
    Dim ubicacionSteam As String
    Dim WithEvents backgroundWorkerAir As BackgroundWorker

    Public Sub Air(lista As List(Of String), steam As String)

        listaOpciones = lista
        ubicacionSteam = steam

        backgroundWorkerAir = New BackgroundWorker
        backgroundWorkerAir.RunWorkerAsync()

    End Sub

    Private Sub BackgroundWorkerAir_DoWork(sender As Object, e As DoWorkEventArgs) Handles backgroundWorkerAir.DoWork

        If File.Exists(ubicacionSteam + "\Air\config.ini") Then
            Dim contenidoConfig As String = File.ReadAllText(ubicacionSteam + "\Air\config.ini")

            If listaOpciones(0) = "Dark" Then
                contenidoConfig = contenidoConfig.Replace("include " + ChrW(34) + "resource/themes/_light.styles", "//include " + ChrW(34) + "resource/themes/_light.styles")
                contenidoConfig = contenidoConfig.Replace("//include " + ChrW(34) + "resource/themes/_dark.styles", "include " + ChrW(34) + "resource/themes/_dark.styles")
            End If

            If Not listaOpciones(1) = Nothing Then
                contenidoConfig = contenidoConfig.Replace("include " + ChrW(34) + "resource/colors/sky.styles", "//include " + ChrW(34) + "resource/colors/sky.styles")
                contenidoConfig = contenidoConfig.Replace("//include " + ChrW(34) + "resource/colors/" + listaOpciones(1).ToLower + ".styles", "include " + ChrW(34) + "resource/colors/" + listaOpciones(1).ToLower + ".styles")
            End If

            File.WriteAllText(ubicacionSteam + "\Air\config.ini", contenidoConfig)
        End If

    End Sub

    '-------------------------------------------------------------------------

    Dim WithEvents backgroundWorkerAirClassic As BackgroundWorker

    Public Sub AirClassic(lista As List(Of String), steam As String)

        listaOpciones = lista
        ubicacionSteam = steam

        backgroundWorkerAirClassic = New BackgroundWorker
        backgroundWorkerAirClassic.RunWorkerAsync()

    End Sub

    Private Sub BackgroundWorkerAirClassic_DoWork(sender As Object, e As DoWorkEventArgs) Handles backgroundWorkerAirClassic.DoWork

        If File.Exists(ubicacionSteam + "\Air-Classic\config.ini") Then
            Dim contenidoConfig As String = File.ReadAllText(ubicacionSteam + "\Air-Classic\config.ini")

            If Not listaOpciones(0) = Nothing Then
                contenidoConfig = contenidoConfig.Replace("include " + ChrW(34) + "resource/tweaks/color_blue.styles", "//include " + ChrW(34) + "resource/tweaks/color_blue.styles")
                contenidoConfig = contenidoConfig.Replace("//include " + ChrW(34) + "resource/tweaks/color_" + listaOpciones(0).ToLower + ".styles", "include " + ChrW(34) + "resource/tweaks/color_" + listaOpciones(0).ToLower + ".styles")
            End If

            If listaOpciones(1) = "Colorized" Then
                contenidoConfig = contenidoConfig.Replace("include " + ChrW(34) + "resource/tweaks/details_steamblue.styles", "//include " + ChrW(34) + "resource/tweaks/details_steamblue.styles")
                contenidoConfig = contenidoConfig.Replace("//include " + ChrW(34) + "resource/tweaks/details_colorized.styles", "include " + ChrW(34) + "resource/tweaks/details_colorized.styles")
            End If

            If Not listaOpciones(2) = Nothing Then
                contenidoConfig = contenidoConfig.Replace("include " + ChrW(34) + "resource/tweaks/bg_none.styles", "//include " + ChrW(34) + "resource/tweaks/bg_none.styles")
                contenidoConfig = contenidoConfig.Replace("//include " + ChrW(34) + "resource/tweaks/bg_" + listaOpciones(2).ToLower + ".styles", "include " + ChrW(34) + "resource/tweaks/bg_" + listaOpciones(2).ToLower + ".styles")
            End If

            File.WriteAllText(ubicacionSteam + "\Air-Classic\config.ini", contenidoConfig)
        End If

    End Sub

    '-------------------------------------------------------------------------

    Dim WithEvents backgroundWorkerMetro As BackgroundWorker

    Public Sub Metro(lista As List(Of String), steam As String)

        listaOpciones = lista
        ubicacionSteam = steam

        backgroundWorkerMetro = New BackgroundWorker
        backgroundWorkerMetro.RunWorkerAsync()

    End Sub

    Private Sub BackgroundWorkerMetro_DoWork(sender As Object, e As DoWorkEventArgs) Handles backgroundWorkerMetro.DoWork

        If File.Exists(ubicacionSteam + "\Metro\custom.styles") Then
            Dim contenidoConfig As String = File.ReadAllText(ubicacionSteam + "\Metro\custom.styles")

            If listaOpciones(0) = "Pink" Then
                contenidoConfig = contenidoConfig.Replace("colors{Focus=" + ChrW(34) + "102 36 226 255", "colors{Focus=" + ChrW(34) + "220 79 173 255")
            ElseIf listaOpciones(0) = "Red" Then
                contenidoConfig = contenidoConfig.Replace("colors{Focus=" + ChrW(34) + "102 36 226 255", "colors{Focus=" + ChrW(34) + "172 25 61 255")
            ElseIf listaOpciones(0) = "Orange" Then
                contenidoConfig = contenidoConfig.Replace("colors{Focus=" + ChrW(34) + "102 36 226 255", "colors{Focus=" + ChrW(34) + "255 143 50 255")
            ElseIf listaOpciones(0) = "Green" Then
                contenidoConfig = contenidoConfig.Replace("colors{Focus=" + ChrW(34) + "102 36 226 255", "colors{Focus=" + ChrW(34) + "130 186 0 255")
            ElseIf listaOpciones(0) = "Dark Green" Then
                contenidoConfig = contenidoConfig.Replace("colors{Focus=" + ChrW(34) + "102 36 226 255", "colors{Focus=" + ChrW(34) + "0 138 23 255")
            ElseIf listaOpciones(0) = "Cyan" Then
                contenidoConfig = contenidoConfig.Replace("colors{Focus=" + ChrW(34) + "102 36 226 255", "colors{Focus=" + ChrW(34) + "3 179 178 255")
            ElseIf listaOpciones(0) = "Dark Cyan" Then
                contenidoConfig = contenidoConfig.Replace("colors{Focus=" + ChrW(34) + "102 36 226 255", "colors{Focus=" + ChrW(34) + "0 130 152 255")
            ElseIf listaOpciones(0) = "Blue" Then
                contenidoConfig = contenidoConfig.Replace("colors{Focus=" + ChrW(34) + "102 36 226 255", "colors{Focus=" + ChrW(34) + "93 178 255 255")
            ElseIf listaOpciones(0) = "Dark Blue" Then
                contenidoConfig = contenidoConfig.Replace("colors{Focus=" + ChrW(34) + "102 36 226 255", "colors{Focus=" + ChrW(34) + "70 23 180 255")
            ElseIf listaOpciones(0) = "Dark Cyan" Then
                contenidoConfig = contenidoConfig.Replace("colors{Focus=" + ChrW(34) + "102 36 226 255", "colors{Focus=" + ChrW(34) + "140 0 149 255")
            End If

            File.WriteAllText(ubicacionSteam + "\Metro\custom.styles", contenidoConfig)
        End If

    End Sub

    '-------------------------------------------------------------------------

    Dim WithEvents backgroundWorkerMinimal As BackgroundWorker

    Public Sub Minimal(lista As List(Of String), steam As String)

        listaOpciones = lista
        ubicacionSteam = steam

        backgroundWorkerMinimal = New BackgroundWorker
        backgroundWorkerMinimal.RunWorkerAsync()

    End Sub

    Private Sub BackgroundWorkerMinimal_DoWork(sender As Object, e As DoWorkEventArgs) Handles backgroundWorkerMinimal.DoWork

        If File.Exists(ubicacionSteam + "\Minimal\settings.ini") Then
            Dim contenidoConfig As String = File.ReadAllText(ubicacionSteam + "\Minimal\settings.ini")

            If Not listaOpciones(0) = Nothing Then
                contenidoConfig = contenidoConfig.Replace("include " + ChrW(34) + "resource/styles/colors/black.styles", "include " + ChrW(34) + "resource/styles/colors/" + listaOpciones(0).ToLower + ".styles")
            End If

            File.WriteAllText(ubicacionSteam + "\Minimal\settings.ini", contenidoConfig)
        End If

    End Sub

    '-------------------------------------------------------------------------

    Dim WithEvents backgroundWorkerPressure2 As BackgroundWorker

    Public Sub Pressure2(lista As List(Of String), steam As String)

        listaOpciones = lista
        ubicacionSteam = steam

        backgroundWorkerPressure2 = New BackgroundWorker
        backgroundWorkerPressure2.RunWorkerAsync()

    End Sub

    Private Sub BackgroundWorkerPressure2_DoWork(sender As Object, e As DoWorkEventArgs) Handles backgroundWorkerPressure2.DoWork

        If File.Exists(ubicacionSteam + "\Pressure2\config.ini") Then
            Dim contenidoConfig As String = File.ReadAllText(ubicacionSteam + "\Pressure2\config.ini")

            If listaOpciones(0) = "No" Then
                contenidoConfig = contenidoConfig.Replace("include " + ChrW(34) + "includes/tweaks/grid/transparent.styles", "//include " + ChrW(34) + "includes/tweaks/grid/transparent.styles")
                contenidoConfig = contenidoConfig.Replace("//include " + ChrW(34) + "includes/tweaks/grid/not-transparent.styles", "include " + ChrW(34) + "includes/tweaks/grid/not-transparent.styles")
            End If

            If listaOpciones(1) = "No" Then
                contenidoConfig = contenidoConfig.Replace("include " + ChrW(34) + "includes/tweaks/gamebackgroundoverlay/enable.styles", "//include " + ChrW(34) + "includes/tweaks/gamebackgroundoverlay/enable.styles")
                contenidoConfig = contenidoConfig.Replace("//include " + ChrW(34) + "includes/tweaks/gamebackgroundoverlay/disable.styles", "include " + ChrW(34) + "includes/tweaks/gamebackgroundoverlay/disable.styles")
            End If

            File.WriteAllText(ubicacionSteam + "\Pressure2\config.ini", contenidoConfig)
        End If

    End Sub

    '-------------------------------------------------------------------------

    Dim WithEvents backgroundWorkerThreshold As BackgroundWorker

    Public Sub Threshold(lista As List(Of String), steam As String)

        listaOpciones = lista
        ubicacionSteam = steam

        backgroundWorkerThreshold = New BackgroundWorker
        backgroundWorkerThreshold.RunWorkerAsync()

    End Sub

    Private Sub BackgroundWorkerThreshold_DoWork(sender As Object, e As DoWorkEventArgs) Handles backgroundWorkerThreshold.DoWork

        If File.Exists(ubicacionSteam + "\Threshold\config.ini") Then
            Dim contenidoConfig As String = File.ReadAllText(ubicacionSteam + "\Threshold\config.ini")

            If Not listaOpciones(0) = Nothing Then
                contenidoConfig = contenidoConfig.Replace("include " + ChrW(34) + "resource/styles/colors/cobalt.styles", "//include " + ChrW(34) + "resource/styles/colors/cobalt.styles")
                contenidoConfig = contenidoConfig.Replace("//include " + ChrW(34) + "resource/styles/colors/" + listaOpciones(0).ToLower + ".styles", "include " + ChrW(34) + "resource/styles/colors/" + listaOpciones(0).ToLower + ".styles")
            End If

            If listaOpciones(1) = "Yes" Then
                contenidoConfig = contenidoConfig.Replace("include " + ChrW(34) + "resource/styles/colors/no_outline.styles", "//include " + ChrW(34) + "resource/styles/colors/no_outline.styles")
            End If

            If listaOpciones(2) = "Yes" Then
                contenidoConfig = contenidoConfig.Replace("include " + ChrW(34) + "resource/styles/colors/titlebar_black.styles", "//include " + ChrW(34) + "resource/styles/colors/titlebar_black.styles")
            End If

            File.WriteAllText(ubicacionSteam + "\Threshold\config.ini", contenidoConfig)
        End If

    End Sub

End Module
