unpager
=======

Image processing and transformation tool.

This project now is in its research stage. 
It is mainly oriented on printed matherial scans and pictures enchancing.

The very basics of it, the algorithmical playground, is being written in C# to allow rapid prototyping along with algorithms being implemented in C-like language. The tool itself would be probably in C++ with Qt to ensure portability and performance.

For now I have few features working:

![screenshot](/screenshots/before_proj.png "Before projection") → ![screenshot](/screenshots/after_proj.png "After projection")

![screenshot](/screenshots/before_flat.png "Before projection") → ![screenshot](/screenshots/after_flat.png "After flattening")

![screenshot](/screenshots/before_light.png "Before relighting") → ![screenshot](/screenshots/after_light.png "After relighting")

![screenshot](/screenshots/before_darn.png "Before darning") → ![screenshot](/screenshots/after_darn.png "After darning")

I also have a GIMP script for darning: https://github.com/akalenuk/darning
