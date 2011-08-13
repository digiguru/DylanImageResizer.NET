Public Interface IImageResizeableMultipleSizes
    Property ResizedImageList As IEnumerable(Of IResizedImage)
    Property ImagePath As String
    Property ImageSizes As IEnumerable(Of IImageSize)
End Interface