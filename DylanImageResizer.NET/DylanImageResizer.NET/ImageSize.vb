Public Class ImageSize
    Implements IImageSize
    Private _height As Integer
    Public Property Height As Integer Implements IImageSize.Height
        Get
            Return _height
        End Get
        Set(ByVal value As Integer)
            _height = value
        End Set
    End Property
    Private _width As Integer
    Public Property Width As Integer Implements IImageSize.Width
        Get
            Return _width
        End Get
        Set(ByVal value As Integer)
            _width = value
        End Set
    End Property
End Class
