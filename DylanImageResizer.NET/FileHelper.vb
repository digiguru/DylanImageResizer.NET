Imports System.IO
Imports System.Web

Public Class FileHelper
    Public Shared Function MapPath(file As String) As String
        Return HttpContext.Current.Server.MapPath(file)
    End Function
    Public Shared Function IsWebServer(path As String) As Boolean
        Return path.StartsWith("http://") Or path.StartsWith("https://")
    End Function
    Public Shared Function IsFromLocalPath(path As String) As Boolean
        Return path.Contains(":") And Not IsWebServer(path)
    End Function
    Public Shared Function GetNewImagePath(ByVal imagePath As String, maximumImageSize As IImageSize) As String
        Dim justFileName As String = Path.GetFileNameWithoutExtension(imagePath)
        Dim newFileName As String = String.Concat(justFileName, "-", maximumImageSize.Width, "x", maximumImageSize.Height)
        If FileHelper.IsWebServer(imagePath) Then
            imagePath = MapPath(Path.GetFileName(imagePath).Replace(justFileName, newFileName))
        ElseIf FileHelper.IsFromLocalPath(imagePath) Then
            imagePath = imagePath.Replace(justFileName, newFileName)
        Else
            imagePath = MapPath(imagePath).Replace(justFileName, newFileName)
        End If
        Return imagePath
    End Function
    Public Shared Function MapURL(ByVal Path As String) As String
        If Not HttpContext.Current Is Nothing Then
            Dim AppPath As String = HttpContext.Current.Server.MapPath("~")
            Dim replacePath As String = Path.Replace(AppPath, "")
            Dim url As String = String.Format("~/{0}", replacePath.Replace("\", "/"))
            Return url
        Else
            Throw New NotSupportedException("This function can only be called from a website")
        End If
        Return String.Empty
    End Function

End Class
