﻿Public Class ResizedImage
    Implements IResizedImage
    Private _outputSize As IImageSize
    Public Property OutputSize As IImageSize Implements IResizedImage.OutputSize
        Get
            Return _outputSize
        End Get
        Set(ByVal value As IImageSize)
            _outputSize = value
        End Set
    End Property

    Private _outputpath As String
    Public Property OutputPath As String Implements IResizedImage.OutputPath
        Get
            Return _outputpath
        End Get
        Set(ByVal value As String)
            _outputpath = value
        End Set
    End Property
    Private _offsetCenter As IImageSize
    Public Property OffsetCenter As IImageSize Implements IResizedImage.OffsetCenter
        Get
            Return _offsetCenter
        End Get
        Set(ByVal value As IImageSize)
            _offsetCenter = value
        End Set
    End Property

End Class
