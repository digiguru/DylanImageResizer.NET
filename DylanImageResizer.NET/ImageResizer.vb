﻿Imports System.Drawing
Imports System.IO
Imports System.Net

Public Class ImageResizer(Of T As IImageResizeableMultipleSizes)

    Public Function ResizeImage(ByVal objectWithImageList As IEnumerable(Of T), ByVal resizeOptionList As IEnumerable(Of IImageSize)) As IEnumerable(Of T)
        For Each image As IImageResizeableMultipleSizes In objectWithImageList
            Dim resizedImagesList As New Generic.List(Of IResizedImage)
            For Each imgSize As IImageSize In resizeOptionList
                Dim imgObject As New ResizableImageOptions() With {.InputPath = image.ImagePath, .MaximumImageSize = imgSize}
                Dim newPath As String = FileHelper.GetNewImagePath(imgObject)
                With imgObject
                    If Not File.Exists(newPath) Then
                        Dim imageURL As String = .InputPath
                        If FileHelper.IsWebServer(imageURL) Then
                            resizedImagesList.Add(ResizeFromInternet(imageURL, newPath, .MaximumImageSize))
                        ElseIf FileHelper.IsFromLocalPath(imageURL) Then
                            resizedImagesList.Add(ResizeFromFilePath(imageURL, newPath, .MaximumImageSize))
                        Else
                            resizedImagesList.Add(ResizeFromLocalServer(imageURL, newPath, .MaximumImageSize))
                        End If
                    Else
                        resizedImagesList.Add(GetCachedImage(newPath, .MaximumImageSize))
                    End If
                End With
            Next
            image.ResizedImageList = resizedImagesList
        Next
        Return objectWithImageList
    End Function
    Private Function GetCachedImage(newPath As String, maximumImageSize As IImageSize) As IResizedImage
        Dim NewResizedImage As New ResizedImage
        Dim newImageSize As IImageSize
        Using imageObj As System.Drawing.Image = System.Drawing.Image.FromFile(newPath)
            newImageSize = New ImageSize With {.Width = imageObj.Width, .Height = imageObj.Height}
        End Using

        With NewResizedImage
            .OutputPath = newPath
            .OutputSize = newImageSize
            .OffsetCenter = CenteredImageOffset(newImageSize, maximumImageSize)
        End With
        Return NewResizedImage
    End Function
    Private Function ResizeFromLocalServer(ByVal imageURL As String, _
                                           ByVal newPath As String, _
                                           ByVal maximumImageSize As IImageSize) As IResizedImage
        Dim originalPath As String = String.Empty
        originalPath = FileHelper.MapPath(imageURL)
        Return ResizeFromFilePath(originalPath, newPath, maximumImageSize)
    End Function

    Private Function ResizeFromFilePath(ByVal imageOnDisk As String, _
                                       ByVal newPath As String, _
                                       ByVal maximumImageSize As IImageSize) As IResizedImage
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
                                        ByVal maximumImageSize As IImageSize) As IResizedImage
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
    Private Function CalculateLargestThumbnailFromCenter(ByVal oldImageSize As IImageSize, ByVal maximumImageSize As IImageSize) As ThumbnailArea
        Dim ThumbnailArea As New ThumbnailArea With { _
            .Width = oldImageSize.Width, .Height = oldImageSize.Height, _
            .OffsetX = 0, .OffsetY = 0 _
        }
        If (CDec(oldImageSize.Width) / CDec(oldImageSize.Height)) > (CDec(maximumImageSize.Width) / CDec(maximumImageSize.Height)) Then
            Dim ratio As Decimal = oldImageSize.Height / CDec(maximumImageSize.Height)
            ThumbnailArea.Width = CInt((maximumImageSize.Width * ratio))
            ThumbnailArea.OffsetX = CDec((oldImageSize.Width - ThumbnailArea.Width) / 2)
        Else
            Dim ratio As Decimal = oldImageSize.Width / CDec(maximumImageSize.Width)
            ThumbnailArea.Height = CInt((maximumImageSize.Height * ratio))
            ThumbnailArea.OffsetY = CDec((oldImageSize.Height - ThumbnailArea.Height) / 2)
        End If

        Return ThumbnailArea
    End Function

    Private Function CalaculateSizeAndResizeImage(ByRef imageObj As System.Drawing.Image, _
                                           ByVal savePath As String, _
                                           ByVal format As System.Drawing.Imaging.ImageFormat, _
                                           ByVal maximumImageSize As IImageSize
                                          ) As IResizedImage
        Dim NewResizedImage As New ResizedImage
        'Dim NewImage As New ImageResizeableImageObject

        Dim oldImageSize As New ImageSize With {.Width = imageObj.Width, .Height = imageObj.Height}
        'Pre-processing, do we want to shrink the image? grow the image? Is it going to be cropped or the aspect ratio retained?
        Dim thumbnailArea As ThumbnailArea = CalculateLargestThumbnailFromCenter(oldImageSize, maximumImageSize)

        Dim theOriginalImageIsBiggerThanResize As Boolean = (maximumImageSize.Width < oldImageSize.Width Or maximumImageSize.Height < oldImageSize.Height)
        If theOriginalImageIsBiggerThanResize Then
            Dim newImageSize As IImageSize = CalculateImageSize(thumbnailArea, maximumImageSize)
            'If you set this after the resize, you'll find that "maximum Image size" has magically changed
            ResizeImageToThumbnail(imageObj, savePath, format, thumbnailArea, newImageSize)
            With NewResizedImage
                .OutputSize = maximumImageSize 'newImageSize
                .OffsetCenter = CenteredImageOffset(thumbnailArea, maximumImageSize)
                .OutputPath = savePath
            End With
        End If
        Return NewResizedImage
    End Function
    Public Function CenteredImageOffset(ByVal actualSize As IImageSize, ByVal maximumSize As IImageSize) As IImageSize

        Dim pointX As Integer = CInt((maximumSize.Width - actualSize.Width) / 2)
        Dim pointY As Integer = CInt((maximumSize.Height - actualSize.Height) / 2)

        Return New ImageSize With {.Width = pointX, .Height = pointY}
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
    Public Sub ResizeImageToThumbnail(ByRef imageObj As System.Drawing.Image, _
                            ByVal savePath As String, _
                            ByVal format As System.Drawing.Imaging.ImageFormat, _
                            ByVal thumbnailArea As ThumbnailArea, _
                            ByVal newImageSize As IImageSize _
                           )
        Using bitmap As New Bitmap(newImageSize.Width, newImageSize.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb)
            bitmap.SetResolution(imageObj.HorizontalResolution, imageObj.VerticalResolution)
            Using oGraphic As Graphics = Graphics.FromImage(bitmap)
                oGraphic.Clear(Color.White)
                oGraphic.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                'Do we want to add any processing in the image resize? Perhaps add a water mark?
                oGraphic.DrawImage(imageObj, New Rectangle(0, 0, newImageSize.Width, newImageSize.Height), New Rectangle(CInt(thumbnailArea.OffsetX), CInt(thumbnailArea.OffsetY), thumbnailArea.Width, thumbnailArea.Height), GraphicsUnit.Pixel)

            End Using
            bitmap.Save(savePath, format)
        End Using
    End Sub

End Class
