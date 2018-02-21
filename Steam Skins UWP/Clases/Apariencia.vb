Public Class Apariencia

    Public Property Titulo As String
    Public Property EnlaceDescarga As String
    Public Property CarpetaDescarga As String

    Public Property DonacionPaypal As String
    Public Property DonacionPatreon As String
    Public Property WebCreador As String

    Public Property Opcion1 As String
    Public Property OpcionListado1 As List(Of String)
    Public Property Opcion2 As String
    Public Property OpcionListado2 As List(Of String)
    Public Property Opcion3 As String
    Public Property OpcionListado3 As List(Of String)
    Public Property Opcion4 As String
    Public Property OpcionListado4 As List(Of String)

    Public Property Imagen1 As String
    Public Property Imagen2 As String
    Public Property Imagen3 As String
    Public Property Imagen4 As String

    Public Sub New(ByVal titulo As String, ByVal enlaceDescarga As String, ByVal carpetaDescarga As String,
                   ByVal donacionPaypal As String, ByVal donacionPatreon As String, ByVal webCreador As String,
                   ByVal opcion1 As String, ByVal opcionListado1 As List(Of String), ByVal opcion2 As String, ByVal opcionListado2 As List(Of String),
                   ByVal opcion3 As String, ByVal opcionListado3 As List(Of String), ByVal opcion4 As String, ByVal opcionListado4 As List(Of String),
                   ByVal imagen1 As String, ByVal imagen2 As String, ByVal imagen3 As String, ByVal imagen4 As String)
        Me.Titulo = titulo
        Me.EnlaceDescarga = enlaceDescarga
        Me.CarpetaDescarga = carpetaDescarga

        Me.DonacionPaypal = donacionPaypal
        Me.DonacionPatreon = donacionPatreon
        Me.WebCreador = webCreador

        Me.Opcion1 = opcion1
        Me.OpcionListado1 = opcionListado1
        Me.Opcion2 = opcion2
        Me.OpcionListado2 = opcionListado2
        Me.Opcion3 = opcion3
        Me.OpcionListado3 = opcionListado3
        Me.Opcion4 = opcion4
        Me.OpcionListado4 = opcionListado4

        Me.Imagen1 = imagen1
        Me.Imagen2 = imagen2
        Me.Imagen3 = imagen3
        Me.Imagen4 = imagen4
    End Sub

End Class
