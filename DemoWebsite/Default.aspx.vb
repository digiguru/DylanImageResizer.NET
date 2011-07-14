
Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim resizer As New ImageResizer.ImageResizer

        With resizer
            Dim original As String = "/example.jpg"
            Dim resized As String = String.Empty

            resized = .ResizeImage(original, New ImageResizer.ImageSize With {.Height = 105, .Width = 105})

            imgOriginal.ImageUrl = original
            imgResized.ImageUrl = ImageResizer.FileHelper.MapURL(resized)
        End With

        With resizer
            Dim originalFile As String = "/chrome.jpg"
            Dim resizedFile As String = String.Empty

            resizedFile = .ResizeImage(MapPath(originalFile), New ImageResizer.ImageSize With {.Height = 105, .Width = 105})

            imgOriginalFile.ImageUrl = originalFile
            imgResizedFile.ImageUrl = ImageResizer.FileHelper.MapURL(resizedFile)
        End With

        With resizer
            Dim originalWeb As String = "https://gs1.wac.edgecastcdn.net/80460E/assets/images/modules/dashboard/bootcamp/octocat_create.png"
            Dim resizedWeb As String = String.Empty

            resizedWeb = .ResizeImage(originalWeb, New ImageResizer.ImageSize With {.Height = 105, .Width = 105})

            imgOriginalWeb.ImageUrl = originalWeb
            imgResizedWeb.ImageUrl = ImageResizer.FileHelper.MapURL(resizedWeb)
        End With

    End Sub
    
End Class