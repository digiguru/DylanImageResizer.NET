Public Class ImageResizable
    Implements IImageResizeable

    Private _outputSize As IImageSize
    Public Property OutputSize As IImageSize Implements IImageResizeable.OutputSize
        Get
            Return _outputSize
        End Get
        Set(ByVal value As IImageSize)
            _outputSize = value
        End Set
    End Property

    Private _path As String
    Public Property Path As String Implements IImageResizeable.Path
        Get
            Return _path
        End Get
        Set(ByVal value As String)
            _path = value
        End Set
    End Property
    Private _offsetCenter As System.Drawing.Point
    Public Property OffsetCenter As System.Drawing.Point Implements IImageResizeable.OffsetCenter
        Get
            Return _offsetCenter
        End Get
        Set(ByVal value As System.Drawing.Point)
            _offsetCenter = value
        End Set
    End Property
End Class
