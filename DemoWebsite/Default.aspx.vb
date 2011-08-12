
Public Class _Default
    Inherits System.Web.UI.Page
    Private Function RenderStyle(offset As System.Drawing.Point)
        Return "margin-left:" & offset.X & "px;margin-Top: " & offset.Y & "px"
    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim resizer As New ImageResizer.ImageResizer

        With resizer
            Dim original As String = "/example.jpg"
            Dim resized As ImageResizer.IImageResizeable

            resized = .ResizeImage(original, New ImageResizer.ImageSize With {.Height = 105, .Width = 105})

            imgOriginal.ImageUrl = original
            imgResized.ImageUrl = ImageResizer.FileHelper.MapURL(resized.Path)
            imgResized.Height = resized.OutputSize.Height
            imgResized.Width = resized.OutputSize.Width
            imgResized.Attributes.Add("style", RenderStyle(resized.OffsetCenter))

        End With

        With resizer
            Dim originalFile As String = "/chrome.jpg"
            Dim resizedFile As ImageResizer.IImageResizeable

            resizedFile = .ResizeImage(MapPath(originalFile), New ImageResizer.ImageSize With {.Height = 105, .Width = 105})

            imgOriginalFile.ImageUrl = originalFile
            imgResizedFile.ImageUrl = ImageResizer.FileHelper.MapURL(resizedFile.Path)
            imgResizedFile.Height = resizedFile.OutputSize.Height
            imgResizedFile.Width = resizedFile.OutputSize.Width
            imgResized.Attributes.Add("style", RenderStyle(resizedFile.OffsetCenter))

        End With

        With resizer
            Dim originalWeb As String = "http://upload.wikimedia.org/wikipedia/en/b/bc/Wiki.png"
            Dim resizedWeb As ImageResizer.IImageResizeable

            resizedWeb = .ResizeImage(originalWeb, New ImageResizer.ImageSize With {.Height = 105, .Width = 105})

            imgOriginalWeb.ImageUrl = originalWeb
            imgResizedWeb.ImageUrl = ImageResizer.FileHelper.MapURL(resizedWeb.Path)
            imgResizedFile.Height = resizedWeb.OutputSize.Height
            imgResizedFile.Width = resizedWeb.OutputSize.Width
            imgResized.Attributes.Add("style", RenderStyle(resizedWeb.OffsetCenter))

        End With

    End Sub
    
End Class