Imports System.IO

Public Class FileHelper

    Public Shared Function GetNewImagePath(ByVal imageOnDisk As String, maximumImageSize As IImageSize) As String
        Dim justFileName As String = Path.GetFileNameWithoutExtension(imageOnDisk)
        imageOnDisk = imageOnDisk.Replace(justFileName, String.Concat(justFileName, "-", maximumImageSize.Width, "x", maximumImageSize.Height))
        Return imageOnDisk
    End Function
    
End Class
