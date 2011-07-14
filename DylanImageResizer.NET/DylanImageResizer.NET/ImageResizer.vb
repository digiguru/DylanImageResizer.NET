Imports System.Drawing
Imports System.IO

Public Class ImageResizer

    Public Function ResizeFromLocalServer(ByVal imageOnDisk As String, ByVal maximumImageSize As IImageSize) As String
        Dim newPath As String = GetNewImagePath(imageOnDisk, maximumImageSize)
        If File.Exists(imageOnDisk) Then
            If Not File.Exists(newPath) Then
                Using imageObj As System.Drawing.Image = System.Drawing.Image.FromFile(imageOnDisk)
                    ResizeImage(imageObj, newPath, Imaging.ImageFormat.Png, maximumImageSize)
                End Using
                'Do we want to do anything with the new image? Perhas upload it to a CDN?
            End If
        End If
        Return newPath
    End Function

    Private Function GetNewImagePath(ByVal imageOnDisk As String, maximumImageSize As IImageSize) As String
        Dim justFileName As String = Path.GetFileName(imageOnDisk)
        imageOnDisk.Replace(justFileName, String.Concat(justFileName, "-", maximumImageSize.Width, "x", maximumImageSize.Height))
        Return imageOnDisk
    End Function

    Private Sub ResizeImage(ByRef imageObj As Image, _
                           ByVal savePath As String, _
                           ByVal format As Imaging.ImageFormat, _
                           ByVal newImageSize As IImageSize _
                          )
        Using bitmap As New Bitmap(newImageSize.Width, newImageSize.Height, Imaging.PixelFormat.Format24bppRgb)
            bitmap.SetResolution(imageObj.HorizontalResolution, imageObj.VerticalResolution)
            Using oGraphic As Graphics = Graphics.FromImage(bitmap)
                oGraphic.Clear(Color.White)
                oGraphic.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                'Do we want to add any processing in the image resize? Perhaps add a water mark?
                oGraphic.DrawImage(imageObj, New Rectangle(0, 0, newImageSize.Width, newImageSize.Height), New Rectangle(0, 0, imageObj.Width, imageObj.Height), GraphicsUnit.Pixel)
            End Using
            bitmap.Save(savePath, format)
        End Using
    End Sub

End Class
