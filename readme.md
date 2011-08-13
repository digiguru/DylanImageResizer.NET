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

### c/#
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

+ Give options on weather the image should be cropped or resized smaller than the box
+ Allow multiple images to be resized at once.

Release History
---------------
### Version 2.2.0.0

Multiple image resizes

### Version 2.1.0.0

Refactored code in preparation for the next build where images can be resized in multiple ways.

### Version 2.0.0.0

*Breaking change* - No Image Resize with just path, now we output extra information

### Version 1.0.0.2

Allows resizeing from a local computer, an image on the local server or a remote image from the web

### Version 1.0.0.1

Constrain the proportions of the image that is being resized

### Version 1.0.0.0

Allow users to resize the images just by selecting a URL on the server, and selecting the height and width.

