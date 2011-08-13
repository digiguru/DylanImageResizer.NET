
Public Class _Default
    Inherits System.Web.UI.Page
    Private Function RenderStyle(offset As System.Drawing.Point)
        Return "margin-left:" & offset.X & "px;margin-Top: " & offset.Y & "px"
    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim lstSizes As New Generic.List(Of ImageResizer.ImageSize)
        Dim imageSize1 As New ImageResizer.ImageSize
        With imageSize1
            .Height = 111
            .Width = 111
        End With
        Dim imageSize2 As New ImageResizer.ImageSize
        With imageSize2
            .Height = 40
            .Width = 40
        End With
        lstSizes.Add(imageSize1)
        lstSizes.Add(imageSize2)


        Dim resizer As New ImageResizer.ImageResizer

        With resizer
            Dim original As String = "/example.jpg"
            Dim resized As ImageResizer.IImageResizeableMultipleSizes

            Dim resizable As New ImageResizer.ImageResiazableImageObjectMultipleSizes
            With resizable
                .ImageSizes = lstSizes
                .ImagePath = original
            End With

            resized = .ResizeImage(resizable)

            imgOriginal.ImageUrl = original
            With resized.ResizedImageList(0)
                imgResized1.ImageUrl = ImageResizer.FileHelper.MapURL(.OutputPath)
                imgResized1.Height = .OutputSize.Height
                imgResized1.Width = .OutputSize.Width
                imgResized1.Attributes.Add("style", RenderStyle(.OffsetCenter))
            End With
            With resized.ResizedImageList(1)
                imgResized2.ImageUrl = ImageResizer.FileHelper.MapURL(.OutputPath)
                imgResized2.Height = .OutputSize.Height
                imgResized2.Width = .OutputSize.Width
                imgResized2.Attributes.Add("style", RenderStyle(.OffsetCenter))
            End With

        End With

        With resizer
            Dim originalFile As String = "/chrome.jpg"
            Dim resized As ImageResizer.IImageResizeableMultipleSizes

            Dim resizable As New ImageResizer.ImageResiazableImageObjectMultipleSizes
            With resizable
                .ImageSizes = lstSizes
                .ImagePath = MapPath(originalFile)
            End With

            resized = .ResizeImage(resizable)

            imgOriginalFile.ImageUrl = originalFile
            With resized.ResizedImageList(0)
                imgResizedFile1.ImageUrl = ImageResizer.FileHelper.MapURL(.OutputPath)
                imgResizedFile1.Height = .OutputSize.Height
                imgResizedFile1.Width = .OutputSize.Width
                imgResizedFile1.Attributes.Add("style", RenderStyle(.OffsetCenter))
            End With
            With resized.ResizedImageList(1)
                imgResizedFile2.ImageUrl = ImageResizer.FileHelper.MapURL(.OutputPath)
                imgResizedFile2.Height = .OutputSize.Height
                imgResizedFile2.Width = .OutputSize.Width
                imgResizedFile2.Attributes.Add("style", RenderStyle(.OffsetCenter))
            End With

        End With

        With resizer
            Dim originalWeb As String = "http://upload.wikimedia.org/wikipedia/en/b/bc/Wiki.png"
            Dim resized As ImageResizer.IImageResizeableMultipleSizes

            Dim resizable As New ImageResizer.ImageResiazableImageObjectMultipleSizes
            With resizable
                .ImageSizes = lstSizes
                .ImagePath = originalWeb
            End With

            resized = .ResizeImage(resizable)

            imgOriginalWeb.ImageUrl = originalWeb
            With resized.ResizedImageList(0)

                imgResizedWeb1.ImageUrl = ImageResizer.FileHelper.MapURL(.OutputPath)
                imgResizedWeb1.Height = .OutputSize.Height
                imgResizedWeb1.Width = .OutputSize.Width
                imgResizedWeb1.Attributes.Add("style", RenderStyle(.OffsetCenter))
            End With
            With resized.ResizedImageList(1)
                imgResizedWeb2.ImageUrl = ImageResizer.FileHelper.MapURL(.OutputPath)
                imgResizedWeb2.Height = .OutputSize.Height
                imgResizedWeb2.Width = .OutputSize.Width
                imgResizedWeb2.Attributes.Add("style", RenderStyle(.OffsetCenter))
            End With

        End With

    End Sub
    
End Class