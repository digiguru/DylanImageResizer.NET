
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

        Dim resizer As New ImageResizer.ImageResizer(Of ImageResizer.ImageResiazableImageObjectMultipleSizes)

        With resizer
            Dim original As String = "/example.jpg"

            Dim resizable1 As New ImageResizer.ImageResiazableImageObjectMultipleSizes
            With resizable1
                .ImageSizes = lstSizes
                .ImagePath = original
            End With

            Dim originalFile As String = "/chrome.jpg"

            Dim resizable2 As New ImageResizer.ImageResiazableImageObjectMultipleSizes
            With resizable2
                .ImageSizes = lstSizes
                .ImagePath = MapPath(originalFile)
            End With

            Dim originalWeb As String = "http://upload.wikimedia.org/wikipedia/en/b/bc/Wiki.png"

            Dim resizable3 As New ImageResizer.ImageResiazableImageObjectMultipleSizes
            With resizable3
                .ImageSizes = lstSizes
                .ImagePath = originalWeb
            End With
            Dim imageList As New Generic.List(Of ImageResizer.ImageResiazableImageObjectMultipleSizes)
            imageList.Add(resizable1)
            imageList.Add(resizable2)
            imageList.Add(resizable3)

            imageList = .ResizeImage(imageList, lstSizes)

            imgOriginal.ImageUrl = original
            imgOriginalFile.ImageUrl = originalFile
            imgOriginalWeb.ImageUrl = originalWeb

            With imageList(0)
                imgResized1.ImageUrl = ImageResizer.FileHelper.MapURL(.ImagePath)
                With .ResizedImageList(0)
                    imgResized1.ImageUrl = ImageResizer.FileHelper.MapURL(.OutputPath)
                    With .OutputSize
                        imgResized1.Height = .Height
                        imgResized1.Width = .Width
                    End With
                    imgResized1.Attributes.Add("style", RenderStyle(.OffsetCenter))
                End With
                imgResized2.ImageUrl = ImageResizer.FileHelper.MapURL(.ImagePath)
                With .ResizedImageList(1)
                    imgResized2.ImageUrl = ImageResizer.FileHelper.MapURL(.OutputPath)
                    With .OutputSize
                        imgResized2.Height = .Height
                        imgResized2.Width = .Width
                    End With
                    imgResized2.Attributes.Add("style", RenderStyle(.OffsetCenter))
                End With
            End With
            With imageList(1)
                imgResizedFile1.ImageUrl = ImageResizer.FileHelper.MapURL(.ImagePath)
                With .ResizedImageList(0)
                    imgResizedFile1.ImageUrl = ImageResizer.FileHelper.MapURL(.OutputPath)
                    With .OutputSize
                        imgResizedFile1.Height = .Height
                        imgResizedFile1.Width = .Width
                    End With
                    imgResizedFile1.Attributes.Add("style", RenderStyle(.OffsetCenter))
                End With
                imgResizedFile2.ImageUrl = ImageResizer.FileHelper.MapURL(.ImagePath)
                With .ResizedImageList(1)
                    imgResizedFile2.ImageUrl = ImageResizer.FileHelper.MapURL(.OutputPath)
                    With .OutputSize
                        imgResizedFile2.Height = .Height
                        imgResizedFile2.Width = .Width
                    End With
                    imgResizedFile2.Attributes.Add("style", RenderStyle(.OffsetCenter))
                End With
            End With
            With imageList(2)
                imgResizedWeb1.ImageUrl = ImageResizer.FileHelper.MapURL(.ImagePath)
                With .ResizedImageList(0)
                    imgResizedWeb1.ImageUrl = ImageResizer.FileHelper.MapURL(.OutputPath)
                    With .OutputSize
                        imgResizedWeb1.Height = .Height
                        imgResizedWeb1.Width = .Width
                    End With
                    imgResizedWeb1.Attributes.Add("style", RenderStyle(.OffsetCenter))
                End With
                With .ResizedImageList(1)
                    imgResizedWeb2.ImageUrl = ImageResizer.FileHelper.MapURL(.OutputPath)
                    With .OutputSize
                        imgResizedWeb2.Height = .Height
                        imgResizedWeb2.Width = .Width
                    End With
                    imgResizedFile2.Attributes.Add("style", RenderStyle(.OffsetCenter))
                End With
            End With
        End With
    End Sub
    
End Class