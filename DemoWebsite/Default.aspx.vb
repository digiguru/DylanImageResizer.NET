
Public Class _Default
    Inherits System.Web.UI.Page
    Private Function RenderStyle(offset As System.Drawing.Point)
        Return "margin-left:" & offset.X & "px;margin-Top: " & offset.Y & "px"
    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim resizer As New ImageResizer.ImageResizer

        With resizer
            Dim original As String = "/example.jpg"
            Dim resized As ImageResizer.IImageResizeableImageObject

            Dim resizable As New ImageResizer.ImageResizeableImageObject
            resizable.ResizableImageOptions = New ImageResizer.ResizableImageOptions _
                                    With {
                                        .InputPath = original,
                                        .MaximumImageSize = _
                                            New ImageResizer.ImageSize _
                                                With { _
                                                        .Height = 125, _
                                                        .Width = 125 _
                                                    } _
                                            }


            resized = .ResizeImage(resizable)

            imgOriginal.ImageUrl = original
            imgResized.ImageUrl = ImageResizer.FileHelper.MapURL(resized.ResizedImage.OutputPath)
            imgResized.Height = resized.ResizedImage.OutputSize.Height
            imgResized.Width = resized.ResizedImage.OutputSize.Width
            imgResized.Attributes.Add("style", RenderStyle(resized.ResizedImage.OffsetCenter))

        End With

        With resizer
            Dim originalFile As String = "/chrome.jpg"
            Dim resized As ImageResizer.IImageResizeableImageObject

            Dim resizable As New ImageResizer.ImageResizeableImageObject
            resizable.ResizableImageOptions = New ImageResizer.ResizableImageOptions _
                                    With {
                                        .InputPath = MapPath(originalFile),
                                        .MaximumImageSize = _
                                            New ImageResizer.ImageSize _
                                                With { _
                                                        .Height = 125, _
                                                        .Width = 125 _
                                                    } _
                                            }


            resized = .ResizeImage(resizable)

            imgOriginalFile.ImageUrl = originalFile
            imgResizedFile.ImageUrl = ImageResizer.FileHelper.MapURL(resized.ResizedImage.OutputPath)
            imgResizedFile.Height = resized.ResizedImage.OutputSize.Height
            imgResizedFile.Width = resized.ResizedImage.OutputSize.Width
            imgResizedFile.Attributes.Add("style", RenderStyle(resized.ResizedImage.OffsetCenter))

        End With

        With resizer
            Dim originalWeb As String = "http://upload.wikimedia.org/wikipedia/en/b/bc/Wiki.png"
            Dim resized As ImageResizer.IImageResizeableImageObject

            Dim resizable As New ImageResizer.ImageResizeableImageObject
            resizable.ResizableImageOptions = New ImageResizer.ResizableImageOptions _
                                    With {
                                        .InputPath = originalWeb,
                                        .MaximumImageSize = _
                                            New ImageResizer.ImageSize _
                                                With { _
                                                        .Height = 125, _
                                                        .Width = 125 _
                                                    } _
                                            }


            resized = .ResizeImage(resizable)

            imgOriginalWeb.ImageUrl = originalWeb
            imgResizedWeb.ImageUrl = ImageResizer.FileHelper.MapURL(resized.ResizedImage.OutputPath)
            imgResizedWeb.Height = resized.ResizedImage.OutputSize.Height
            imgResizedWeb.Width = resized.ResizedImage.OutputSize.Width
            imgResizedWeb.Attributes.Add("style", RenderStyle(resized.ResizedImage.OffsetCenter))

        End With

    End Sub
    
End Class