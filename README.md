unpager
=======

A desktop app that undoes unwanted perspective, unbends pages, and cleans up stains.

The maths behind all the transformations are explained in my book [Geometry for Programmers](https://www.manning.com/books/geometry-for-programmers).

And also partially on [Words and Buttons Online](https://wordsandbuttons.online/):
- [Interactive guide to homogeneous coordinates](https://wordsandbuttons.online/interactive_guide_to_homogeneous_coordinates.html)
- [Bi-watever transformations](https://wordsandbuttons.online/biwhatever_transformations.html)
- [Image darning](https://wordsandbuttons.online/image_darning.html)


Undoing the perspective
-----------------------
![screenshot](/screenshots/before_proj.png "Before projection") → ![screenshot](/screenshots/after_proj.png "After projection")

Unbending a page
----------------
![screenshot](/screenshots/before_flat.png "Before unbending") → ![screenshot](/screenshots/after_flat.png "After flattening")

Cleaning up
-----------
![screenshot](https://github.com/akalenuk/darning/blob/master/screenshots/E_before.png?raw=true "Before darning") → ![screenshot](https://github.com/akalenuk/darning/blob/master/screenshots/E_after.png?raw=true "After darning")

What now?
---------
The project is abandoned. I tried to extract any useful math from it to [reimplement it in C++](https://github.com/akalenuk/unpager_cpp_libs), and some code was later reused in other programs. But as a desktop application, it is done. 

The code is unlicensed though, so you can pick it up and reuse in your own work.

If you want to try the app, here's the release: https://github.com/akalenuk/unpager/releases/tag/0.1

I also have a GIMP script for darning (that's what the cleaning method is called): https://github.com/akalenuk/darning

And also an Android application for undoing perspective: https://github.com/akalenuk/Docam
