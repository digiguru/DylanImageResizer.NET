Public Class ImageResiazableImageObjectMultipleSizes
    Implements IImageResizeableMultipleSizes


    Private _imageSizes As IEnumerable(Of IImageSize)
    Public Property ImageSizes As IEnumerable(Of IImageSize) Implements IImageResizeableMultipleSizes.ImageSizes
        Get
            Return _imageSizes
        End Get
        Set(ByVal value As System.Collections.Generic.IEnumerable(Of IImageSize))
            _imageSizes = value
        End Set
    End Property

    Private _resizedImageList As IEnumerable(Of IResizedImage)
    Public Property ResizedImageList As IEnumerable(Of IResizedImage) Implements IImageResizeableMultipleSizes.ResizedImageList
        Get
            Return _resizedImageList
        End Get
        Set(ByVal value As IEnumerable(Of IResizedImage))
            _resizedImageList = value
        End Set
    End Property
    Private _imagePath As String
    Public Property ImagePath As String Implements IImageResizeableMultipleSizes.ImagePath
        Get
            Return _imagePath
        End Get
        Set(ByVal value As String)
            _imagePath = value
        End Set
    End Property
End Class
