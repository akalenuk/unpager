unpager
=======

A desktop app that undoes unwanted perspective, unbends pages, and cleans ups pictures.

The maths behind all the transformations is explained in my book [Geometry for Programmers](https://www.manning.com/books/geometry-for-programmers).

Undoing the perspective
-----------------------
![screenshot](/screenshots/before_proj.png "Before projection") → ![screenshot](/screenshots/after_proj.png "After projection")

Unbending a page
----------------
![screenshot](/screenshots/before_flat.png "Before unbending") → ![screenshot](/screenshots/after_flat.png "After flattening")

Cleaning up
-----------
![screenshot](/screenshots/before_darn.png "Before darning") → ![screenshot](/screenshots/after_darn.png "After darning")

What now?
---------
**As for July 2016**, the project is abandoned. I tried to extract any useful math from it to reimplement in c++, and some code was reused in python programs. But as a desktop application it is done. 

The code is unlicensed though, so you can pick it up and reuse in your own work.

If you want to try the app, here's the release: https://github.com/akalenuk/unpager/releases/tag/0.1

I also have a GIMP script for darning: https://github.com/akalenuk/darning

And also Android application for projections: https://github.com/akalenuk/Docam
