Public Class EditorColores

    Public Property Nombre As String
    Public Property Colores As List(Of String)

    Public Sub New(ByVal nombre As String, ByVal colores As List(Of String))
        Me.Nombre = nombre
        Me.Colores = colores
    End Sub

End Class
