Public Class Apariencia

    Public Property Titulo As String
    Public Property EnlaceDescarga As String
    Public Property CarpetaDescarga As String

    Public Property Github As String

    Public Property Imagen1 As String
    Public Property Imagen2 As String
    Public Property Imagen3 As String

    Public Sub New(ByVal titulo As String, ByVal enlaceDescarga As String, ByVal carpetaDescarga As String,
                   ByVal github As String, ByVal imagen1 As String, ByVal imagen2 As String, ByVal imagen3 As String)
        Me.Titulo = titulo
        Me.EnlaceDescarga = enlaceDescarga
        Me.CarpetaDescarga = carpetaDescarga

        Me.Github = github

        Me.Imagen1 = imagen1
        Me.Imagen2 = imagen2
        Me.Imagen3 = imagen3
    End Sub

End Class
