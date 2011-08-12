Public Class ResizableImageOptions
    Implements IResizableImageOptions


    Private _inputPath As String
    Public Property InputPath As String Implements IResizableImageOptions.InputPath
        Get
            Return _inputPath
        End Get
        Set(ByVal value As String)
            _inputPath = value
        End Set
    End Property

    Private _maxImageSize As IImageSize
    Public Property MaximumImageSize As IImageSize Implements IResizableImageOptions.MaximumImageSize
        Get
            Return _maxImageSize
        End Get
        Set(ByVal value As IImageSize)
            _maxImageSize = value
        End Set
    End Property
End Class
