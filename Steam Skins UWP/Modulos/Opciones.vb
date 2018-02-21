Imports Windows.Storage

Module Opciones

    Public Sub Air(carpetaFichero As String)

        If File.Exists(carpetaFichero + "\config.ini") Then
            Dim contenidoConfig As String = File.ReadAllText(carpetaFichero + "\config.ini")

            If ApplicationData.Current.LocalSettings.Values("AirTheme") = "Dark" Then
                contenidoConfig = contenidoConfig.Replace("include " + ChrW(34) + "resource/themes/_light.styles", "//include " + ChrW(34) + "resource/themes/_light.styles")
                contenidoConfig = contenidoConfig.Replace("//include " + ChrW(34) + "resource/themes/_dark.styles", "include " + ChrW(34) + "resource/themes/_dark.styles")
            End If

            If Not ApplicationData.Current.LocalSettings.Values("AirColor") = Nothing Then
                contenidoConfig = contenidoConfig.Replace("include " + ChrW(34) + "resource/colors/sky.styles", "//include " + ChrW(34) + "resource/colors/sky.styles")
                contenidoConfig = contenidoConfig.Replace("//include " + ChrW(34) + "resource/colors/" + ApplicationData.Current.LocalSettings.Values("AirColor").ToLower + ".styles", "include " + ChrW(34) + "resource/colors/" + ApplicationData.Current.LocalSettings.Values("AirColor").ToLower + ".styles")
            End If

            File.WriteAllText(carpetaFichero + "\config.ini", contenidoConfig)
        End If

    End Sub

    Public Sub AirClassic(carpetaFichero As String)

        If File.Exists(carpetaFichero + "\config.ini") Then
            Dim contenidoConfig As String = File.ReadAllText(carpetaFichero + "\config.ini")

            If Not ApplicationData.Current.LocalSettings.Values("Air ClassicColor") = Nothing Then
                contenidoConfig = contenidoConfig.Replace("include " + ChrW(34) + "resource/tweaks/color_blue.styles", "//include " + ChrW(34) + "resource/tweaks/color_blue.styles")
                contenidoConfig = contenidoConfig.Replace("//include " + ChrW(34) + "resource/tweaks/color_" + ApplicationData.Current.LocalSettings.Values("Air ClassicColor").ToLower + ".styles", "include " + ChrW(34) + "resource/tweaks/color_" + ApplicationData.Current.LocalSettings.Values("Air ClassicColor").ToLower + ".styles")
            End If

            If ApplicationData.Current.LocalSettings.Values("Air ClassicGames Details") = "Colorized" Then
                contenidoConfig = contenidoConfig.Replace("include " + ChrW(34) + "resource/tweaks/details_steamblue.styles", "//include " + ChrW(34) + "resource/tweaks/details_steamblue.styles")
                contenidoConfig = contenidoConfig.Replace("//include " + ChrW(34) + "resource/tweaks/details_colorized.styles", "include " + ChrW(34) + "resource/tweaks/details_colorized.styles")
            End If

            If Not ApplicationData.Current.LocalSettings.Values("Air ClassicBackground") = Nothing Then
                contenidoConfig = contenidoConfig.Replace("include " + ChrW(34) + "resource/tweaks/bg_none.styles", "//include " + ChrW(34) + "resource/tweaks/bg_none.styles")
                contenidoConfig = contenidoConfig.Replace("//include " + ChrW(34) + "resource/tweaks/bg_" + ApplicationData.Current.LocalSettings.Values("Air ClassicBackground").ToLower + ".styles", "include " + ChrW(34) + "resource/tweaks/bg_" + ApplicationData.Current.LocalSettings.Values("Air ClassicBackground").ToLower + ".styles")
            End If

            File.WriteAllText(carpetaFichero + "\config.ini", contenidoConfig)
        End If

    End Sub

    Public Sub Metro(carpetaFichero As String)

        If File.Exists(carpetaFichero + "\custom.styles") Then
            Dim contenidoConfig As String = File.ReadAllText(carpetaFichero + "\custom.styles")

            If ApplicationData.Current.LocalSettings.Values("MetroColor") = "Pink" Then
                contenidoConfig = contenidoConfig.Replace("colors{Focus=" + ChrW(34) + "102 36 226 255", "colors{Focus=" + ChrW(34) + "220 79 173 255")
            ElseIf ApplicationData.Current.LocalSettings.Values("MetroColor") = "Red" Then
                contenidoConfig = contenidoConfig.Replace("colors{Focus=" + ChrW(34) + "102 36 226 255", "colors{Focus=" + ChrW(34) + "172 25 61 255")
            ElseIf ApplicationData.Current.LocalSettings.Values("MetroColor") = "Orange" Then
                contenidoConfig = contenidoConfig.Replace("colors{Focus=" + ChrW(34) + "102 36 226 255", "colors{Focus=" + ChrW(34) + "255 143 50 255")
            ElseIf ApplicationData.Current.LocalSettings.Values("MetroColor") = "Green" Then
                contenidoConfig = contenidoConfig.Replace("colors{Focus=" + ChrW(34) + "102 36 226 255", "colors{Focus=" + ChrW(34) + "130 186 0 255")
            ElseIf ApplicationData.Current.LocalSettings.Values("MetroColor") = "Dark Green" Then
                contenidoConfig = contenidoConfig.Replace("colors{Focus=" + ChrW(34) + "102 36 226 255", "colors{Focus=" + ChrW(34) + "0 138 23 255")
            ElseIf ApplicationData.Current.LocalSettings.Values("MetroColor") = "Cyan" Then
                contenidoConfig = contenidoConfig.Replace("colors{Focus=" + ChrW(34) + "102 36 226 255", "colors{Focus=" + ChrW(34) + "3 179 178 255")
            ElseIf ApplicationData.Current.LocalSettings.Values("MetroColor") = "Dark Cyan" Then
                contenidoConfig = contenidoConfig.Replace("colors{Focus=" + ChrW(34) + "102 36 226 255", "colors{Focus=" + ChrW(34) + "0 130 152 255")
            ElseIf ApplicationData.Current.LocalSettings.Values("MetroColor") = "Blue" Then
                contenidoConfig = contenidoConfig.Replace("colors{Focus=" + ChrW(34) + "102 36 226 255", "colors{Focus=" + ChrW(34) + "93 178 255 255")
            ElseIf ApplicationData.Current.LocalSettings.Values("MetroColor") = "Dark Blue" Then
                contenidoConfig = contenidoConfig.Replace("colors{Focus=" + ChrW(34) + "102 36 226 255", "colors{Focus=" + ChrW(34) + "70 23 180 255")
            ElseIf ApplicationData.Current.LocalSettings.Values("MetroColor") = "Dark Cyan" Then
                contenidoConfig = contenidoConfig.Replace("colors{Focus=" + ChrW(34) + "102 36 226 255", "colors{Focus=" + ChrW(34) + "140 0 149 255")
            End If

            File.WriteAllText(carpetaFichero + "\custom.styles", contenidoConfig)
        End If

    End Sub



    ''-------------------------------------------------------------------------

    'Dim WithEvents backgroundWorkerMinimal As BackgroundWorker

    'Public Sub Minimal(lista As List(Of String), steam As String)

    '    listaOpciones = lista
    '    ubicacionSteam = steam

    '    backgroundWorkerMinimal = New BackgroundWorker
    '    backgroundWorkerMinimal.RunWorkerAsync()

    'End Sub

    'Private Sub BackgroundWorkerMinimal_DoWork(sender As Object, e As DoWorkEventArgs) Handles backgroundWorkerMinimal.DoWork

    '    If File.Exists(ubicacionSteam + "\Minimal\settings.ini") Then
    '        Dim contenidoConfig As String = File.ReadAllText(ubicacionSteam + "\Minimal\settings.ini")

    '        If Not listaOpciones(0) = Nothing Then
    '            contenidoConfig = contenidoConfig.Replace("include " + ChrW(34) + "resource/styles/colors/black.styles", "include " + ChrW(34) + "resource/styles/colors/" + listaOpciones(0).ToLower + ".styles")
    '        End If

    '        File.WriteAllText(ubicacionSteam + "\Minimal\settings.ini", contenidoConfig)
    '    End If

    'End Sub

    ''-------------------------------------------------------------------------

    Public Sub Pressure2(carpetaFichero As String)

        If File.Exists(carpetaFichero + "\config.ini") Then
            Dim contenidoConfig As String = File.ReadAllText(carpetaFichero + "\config.ini")

            If ApplicationData.Current.LocalSettings.Values("Pressure2Grid Uninstalled Transparency") = "No" Then
                contenidoConfig = contenidoConfig.Replace("include " + ChrW(34) + "includes/tweaks/grid/transparent.styles", "//include " + ChrW(34) + "includes/tweaks/grid/transparent.styles")
                contenidoConfig = contenidoConfig.Replace("//include " + ChrW(34) + "includes/tweaks/grid/not-transparent.styles", "include " + ChrW(34) + "includes/tweaks/grid/not-transparent.styles")
            End If

            If ApplicationData.Current.LocalSettings.Values("Pressure2Overlay Background") = "No" Then
                contenidoConfig = contenidoConfig.Replace("include " + ChrW(34) + "includes/tweaks/gamebackgroundoverlay/enable.styles", "//include " + ChrW(34) + "includes/tweaks/gamebackgroundoverlay/enable.styles")
                contenidoConfig = contenidoConfig.Replace("//include " + ChrW(34) + "includes/tweaks/gamebackgroundoverlay/disable.styles", "include " + ChrW(34) + "includes/tweaks/gamebackgroundoverlay/disable.styles")
            End If

            File.WriteAllText(carpetaFichero + "\config.ini", contenidoConfig)
        End If

    End Sub

    Public Sub Threshold(carpetaFichero As String)

        If File.Exists(carpetaFichero + "\config.ini") Then
            Dim contenidoConfig As String = File.ReadAllText(carpetaFichero + "\config.ini")

            If Not ApplicationData.Current.LocalSettings.Values("ThresholdColor") = Nothing Then
                contenidoConfig = contenidoConfig.Replace("include " + ChrW(34) + "resource/styles/colors/cobalt.styles", "//include " + ChrW(34) + "resource/styles/colors/cobalt.styles")
                contenidoConfig = contenidoConfig.Replace("//include " + ChrW(34) + "resource/styles/colors/" + ApplicationData.Current.LocalSettings.Values("ThresholdColor").ToLower + ".styles", "include " + ChrW(34) + "resource/styles/colors/" + ApplicationData.Current.LocalSettings.Values("ThresholdColor").ToLower + ".styles")
            End If

            If ApplicationData.Current.LocalSettings.Values("Outlines") = "Yes" Then
                contenidoConfig = contenidoConfig.Replace("include " + ChrW(34) + "resource/styles/colors/no_outline.styles", "//include " + ChrW(34) + "resource/styles/colors/no_outline.styles")
            End If

            If ApplicationData.Current.LocalSettings.Values("Colored Titlebar") = "Yes" Then
                contenidoConfig = contenidoConfig.Replace("include " + ChrW(34) + "resource/styles/colors/titlebar_black.styles", "//include " + ChrW(34) + "resource/styles/colors/titlebar_black.styles")
            End If

            File.WriteAllText(carpetaFichero + "\config.ini", contenidoConfig)
        End If

    End Sub

End Module
