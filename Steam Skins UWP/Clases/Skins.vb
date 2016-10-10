Public Class Skins

    Public _titulo As String

    Public Property titulo As String
        Get
            Return _titulo
        End Get
        Set(ByVal value As String)
            _titulo = value
        End Set
    End Property

    Public _enlace As Uri

    Public Property enlace As Uri
        Get
            Return _enlace
        End Get
        Set(ByVal value As Uri)
            _enlace = value
        End Set
    End Property

    Public _textBlockInforme As TextBlock

    Public Property textBlockInforme As TextBlock
        Get
            Return _textBlockInforme
        End Get
        Set(ByVal value As TextBlock)
            _textBlockInforme = value
        End Set
    End Property

    Public _progressInforme As ProgressRing

    Public Property progressInforme As ProgressRing
        Get
            Return _progressInforme
        End Get
        Set(ByVal value As ProgressRing)
            _progressInforme = value
        End Set
    End Property

    Public _opciones As List(Of String)

    Public Property opciones As List(Of String)
        Get
            Return _opciones
        End Get
        Set(ByVal value As List(Of String))
            _opciones = value
        End Set
    End Property

    Public _opcionesGrid As Grid

    Public Property opcionesGrid As Grid
        Get
            Return _opcionesGrid
        End Get
        Set(ByVal value As Grid)
            _opcionesGrid = value
        End Set
    End Property

    Public Sub New(ByVal titulo As String, ByVal enlace As Uri, ByVal textBlockInforme As TextBlock, ByVal progressInforme As ProgressRing, ByVal opciones As List(Of String), ByVal opcionesGrid As Grid)
        _titulo = titulo
        _enlace = enlace
        _textBlockInforme = textBlockInforme
        _progressInforme = progressInforme
        _opciones = opciones
        _opcionesGrid = opcionesGrid
    End Sub
End Class
