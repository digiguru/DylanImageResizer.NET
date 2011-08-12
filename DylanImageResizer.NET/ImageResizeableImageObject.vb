Public Class ImageResizeableImageObject
    Implements IImageResizeableImageObject
    Private _resiazableImageObject As IResizableImageOptions
    Public Property ResizableImageOptions As IResizableImageOptions Implements IImageResizeableImageObject.ResizableImageOptions
        Get
            Return _resiazableImageObject
        End Get
        Set(ByVal value As IResizableImageOptions)
            _resiazableImageObject = value
        End Set
    End Property
    Private _resizedImage As IResizedImage
    Public Property ResizedImage As IResizedImage Implements IImageResizeableImageObject.ResizedImage
        Get
            Return _resizedImage
        End Get
        Set(ByVal value As IResizedImage)
            _resizedImage = value
        End Set
    End Property
End Class
