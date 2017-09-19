Public Class Apariencia

    Public Property Titulo As String
    Public Property Enlace As Uri
    Public Property Informe As TextBlock
    Public Property Progreso As ProgressRing
    Public Property Opciones As List(Of String)
    Public Property OpcionesSp As StackPanel

    Public Sub New(ByVal titulo As String, ByVal enlace As Uri, ByVal informe As TextBlock,
                   ByVal progreso As ProgressRing, ByVal opciones As List(Of String),
                   ByVal opcionesSp As StackPanel)
        Me.Titulo = titulo
        Me.Enlace = enlace
        Me.Informe = informe
        Me.Progreso = progreso
        Me.Opciones = opciones
        Me.OpcionesSp = opcionesSp
    End Sub
End Class
