Public Class Skins

    Public Property Titulo As String
    Public Property Enlace As Uri
    Public Property TextBlockInforme As TextBlock
    Public Property ProgressInforme As ProgressRing
    Public Property Opciones As List(Of String)
    Public Property OpcionesGrid As Grid

    Public Sub New(ByVal titulo As String, ByVal enlace As Uri, ByVal textBlockInforme As TextBlock,
                   ByVal progressInforme As ProgressRing, ByVal opciones As List(Of String),
                   ByVal opcionesGrid As Grid)
        Me.titulo = titulo
        Me.enlace = enlace
        Me.textBlockInforme = textBlockInforme
        Me.progressInforme = progressInforme
        Me.Opciones = opciones
        Me.OpcionesGrid = opcionesGrid
    End Sub
End Class
