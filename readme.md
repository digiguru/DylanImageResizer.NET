DylanImageResizer.NET
=====================

A Simple image resizer for .NET
-------------------------------

Resizeing images in asp.net can be a little over complicated.

It should be like this...

### VB
    ImageResizer.ImageResizer resizer = new ImageResizer.ImageResizer();
    resizer.ResizeFromLocalServer(MapPath("/example.jpg"), new ImageResizer.ImageSize {
        Height = 30,
        Width = 30
    });

### c\#
    Dim resizer As New ImageResizer.ImageResizer()
    resizer.ResizeFromLocalServer(MapPath("/example.jpg"), New ImageResizer.ImageSize() With { _
        .Height = 30, _
        .Width = 30 _
    })

Goal
----

The goal of this project is to make it really easy for users to resize images in their .NET applications

Future Plans
------------

+ Don't distort the image when resizing, keep the original proportions.
+ Allow multiple images to be resized at once.

Release History
---------------

### Version 1.0

Allow users to resize the images just by selecting a URL on the server, and selecting the height and width.

