Imports System.Drawing
Imports System.IO

Public Class ImageResizer

    Public Function ResizeFromFilePath(ByVal imageOnDisk As String, ByVal maximumImageSize As IImageSize) As String
        Dim newPath As String = FileHelper.GetNewImagePath(imageOnDisk, maximumImageSize)
        If File.Exists(imageOnDisk) Then
            If Not File.Exists(newPath) Then
                Using imageObj As System.Drawing.Image = System.Drawing.Image.FromFile(imageOnDisk)
                    CalaculateSizeAndResizeImage(imageObj, newPath, Imaging.ImageFormat.Jpeg, maximumImageSize)
                End Using
                'Do we want to do anything with the new image? Perhas upload it to a CDN?
            End If
        End If
        Return newPath
    End Function
    Private Sub CalaculateSizeAndResizeImage(ByRef imageObj As System.Drawing.Image, _
                                       ByVal savePath As String, _
                                       ByVal format As System.Drawing.Imaging.ImageFormat, _
                                       ByVal maximumImageSize As IImageSize
                                      )
        Dim oldImageSize As New ImageSize With {.Width = imageObj.Width, .Height = imageObj.Height}
        'Pre-processing, do we want to shrink the image? grow the image? Is it going to be cropped or the aspect ratio retained?
        Dim theOriginalImageIsBiggerThanResize As Boolean = (maximumImageSize.Width < oldImageSize.Width Or maximumImageSize.Height < oldImageSize.Height)
        If theOriginalImageIsBiggerThanResize Then
            Dim newImageSize As IImageSize = CalculateImageSize(oldImageSize, maximumImageSize)
            ResizeImage(imageObj, savePath, format, oldImageSize, newImageSize)
        End If

    End Sub
    Private Function CalculateImageSize(ByVal originalSize As IImageSize, _
                                     ByVal targetSize As IImageSize) As IImageSize
        Dim newSize As IImageSize = targetSize
        If (CDec(originalSize.Width) / CDec(originalSize.Height)) > (CDec(targetSize.Width) / CDec(targetSize.Height)) Then
            Dim ratio As Decimal = CDec(targetSize.Width) / originalSize.Width
            newSize.Height = CInt((originalSize.Height * ratio))
        Else
            Dim ratio As Decimal = CDec(targetSize.Height) / originalSize.Height
            newSize.Width = CInt((originalSize.Width * ratio))
        End If
        Return newSize
    End Function
    Private Sub ResizeImage(ByRef imageObj As Image, _
                           ByVal savePath As String, _
                           ByVal format As Imaging.ImageFormat, _
                           ByVal originalImageSize As IImageSize, _
                           ByVal newImageSize As IImageSize _
                          )
        Using bitmap As New Bitmap(newImageSize.Width, newImageSize.Height, Imaging.PixelFormat.Format24bppRgb)
            bitmap.SetResolution(imageObj.HorizontalResolution, imageObj.VerticalResolution)
            Using oGraphic As Graphics = Graphics.FromImage(bitmap)
                oGraphic.Clear(Color.White)
                oGraphic.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                'Do we want to add any processing in the image resize? Perhaps add a water mark?
                oGraphic.DrawImage(imageObj, New Rectangle(0, 0, newImageSize.Width, newImageSize.Height), New Rectangle(0, 0, originalImageSize.Width, originalImageSize.Height), GraphicsUnit.Pixel)
            End Using
            bitmap.Save(savePath, format)
        End Using
    End Sub

End Class
