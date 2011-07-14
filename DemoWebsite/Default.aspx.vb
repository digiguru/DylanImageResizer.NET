
Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim original As String = "/example.jpg"
        Dim resized As String = String.Empty
        
        Dim resizer As New ImageResizer.ImageResizer
        resized = resizer.ResizeFromFilePath(MapPath(original), New ImageResizer.ImageSize With {.Height = 120, .Width = 120})

        imgOriginal.ImageUrl = original
        imgResized.ImageUrl = MapURL(resized)


    End Sub
    Public Function MapURL(ByVal Path As String) As String
        Dim AppPath As String = Server.MapPath("~")
        Dim replacePath As String = Path.Replace(AppPath, "")
        Dim url As String = String.Format("~/{0}", replacePath.Replace("\", "/"))
        Return url
    End Function
End Class