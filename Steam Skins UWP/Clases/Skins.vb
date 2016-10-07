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

    Public _botonDescarga As Button

    Public Property botonDescarga As Button
        Get
            Return _botonDescarga
        End Get
        Set(ByVal value As Button)
            _botonDescarga = value
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

    Public Sub New(ByVal titulo As String, ByVal enlace As Uri, ByVal botonDescarga As Button, ByVal textBlockInforme As TextBlock, ByVal progressInforme As ProgressRing)
        _titulo = titulo
        _enlace = enlace
        _botonDescarga = botonDescarga
        _textBlockInforme = textBlockInforme
        _progressInforme = progressInforme
    End Sub
End Class
