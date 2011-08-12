Imports System.Drawing
Imports System.IO
Imports System.Net

Public Class ImageResizer

    Public Function ResizeImage(ByVal imageURL As String, ByVal maximumImageSize As IImageSize) As IImageResizeable
        Dim newPath As String = FileHelper.GetNewImagePath(imageURL, maximumImageSize)
        If Not File.Exists(newPath) Then
            If FileHelper.IsWebServer(imageURL) Then
                Return ResizeFromInternet(imageURL, newPath, maximumImageSize)
            ElseIf FileHelper.IsFromLocalPath(imageURL) Then
                Return ResizeFromFilePath(imageURL, newPath, maximumImageSize)
            Else
                Return ResizeFromLocalServer(imageURL, newPath, maximumImageSize)
            End If
        Else
            Return GetCachedImage(newPath, maximumImageSize)
        End If
        Return Nothing
    End Function
    Private Function GetCachedImage(newPath As String, maximumImageSize As IImageSize) As IImageResizeable
        Dim NewImage As New ImageResizable
        Dim newImageSize As IImageSize
        Using imageObj As System.Drawing.Image = System.Drawing.Image.FromFile(newPath)
            newImageSize = New ImageSize With {.Width = imageObj.Width, .Height = imageObj.Height}
        End Using
        With NewImage
            .Path = newPath
            .OutputSize = newImageSize
            .OffsetCenter = CenteredImageOffset(newImageSize, maximumImageSize)
        End With
        Return NewImage
    End Function
    Private Function ResizeFromLocalServer(ByVal imageURL As String, _
                                           ByVal newPath As String, _
                                           ByVal maximumImageSize As IImageSize) As IImageResizeable
        Dim originalPath As String = String.Empty
        originalPath = FileHelper.MapPath(imageURL)
        Return ResizeFromFilePath(originalPath, newPath, maximumImageSize)
    End Function

    Private Function ResizeFromFilePath(ByVal imageOnDisk As String, _
                                       ByVal newPath As String, _
                                       ByVal maximumImageSize As IImageSize) As IImageResizeable
        If File.Exists(imageOnDisk) Then
            Using imageObj As System.Drawing.Image = System.Drawing.Image.FromFile(imageOnDisk)
                Return CalaculateSizeAndResizeImage(imageObj, newPath, Imaging.ImageFormat.Jpeg, maximumImageSize)
            End Using

            'Do we want to do anything with the new image? Perhaps upload it to a CDN?
        End If
        Return Nothing
    End Function
    Private Function ResizeFromInternet(ByVal imageURL As String, _
                                        ByVal newPath As String, _
                                        ByVal maximumImageSize As IImageSize) As IImageResizeable
        Dim objRequest As WebRequest
        objRequest = WebRequest.Create(New Uri(imageURL))
        objRequest.Timeout = 10000
        CType(objRequest, HttpWebRequest).UserAgent = "(compatible; http://digiguru.net)"
        Using objResponse As WebResponse = objRequest.GetResponse()
            Using objStreamReceive As Stream = objResponse.GetResponseStream()
                Using imageObj As System.Drawing.Image = System.Drawing.Image.FromStream(objStreamReceive)
                    Return CalaculateSizeAndResizeImage(imageObj, newPath, Imaging.ImageFormat.Jpeg, maximumImageSize)
                End Using
            End Using
        End Using
    End Function
    Private Function CalaculateSizeAndResizeImage(ByRef imageObj As System.Drawing.Image, _
                                       ByVal savePath As String, _
                                       ByVal format As System.Drawing.Imaging.ImageFormat, _
                                       ByVal maximumImageSize As IImageSize
                                      ) As IImageResizeable
        Dim NewImage As New ImageResizable

        Dim oldImageSize As New ImageSize With {.Width = imageObj.Width, .Height = imageObj.Height}
        'Pre-processing, do we want to shrink the image? grow the image? Is it going to be cropped or the aspect ratio retained?
        Dim theOriginalImageIsBiggerThanResize As Boolean = (maximumImageSize.Width < oldImageSize.Width Or maximumImageSize.Height < oldImageSize.Height)
        If theOriginalImageIsBiggerThanResize Then
            Dim newImageSize As IImageSize = CalculateImageSize(oldImageSize, maximumImageSize)
            'If you set this after the resize, you'll find that "maximum Image size" has magically changed
            With NewImage
                .OutputSize = newImageSize
                .OffsetCenter = CenteredImageOffset(newImageSize, maximumImageSize)
            End With
            ResizeImage(imageObj, savePath, format, oldImageSize, newImageSize)
            With NewImage
                .Path = savePath
            End With
        End If
        Return NewImage
    End Function
    Public Function CenteredImageOffset(ByVal actualSize As IImageSize, ByVal maximumSize As IImageSize) As System.Drawing.Point

        Dim pointX As Integer = CInt((maximumSize.Width - actualSize.Width) / 2)
        Dim pointY As Integer = CInt((maximumSize.Height - actualSize.Height) / 2)

        Return New System.Drawing.Point(pointX, pointY)
    End Function

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
