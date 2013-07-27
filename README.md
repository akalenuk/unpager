unpager
=======

Image processing and transformation tool.

This project now is in its early stage. 
It has its main goal in enabling printed matherial scanning with portative tools. 

The very basics of it, the algorithmical playground, is being written in C# to allow rapid prototyping along with algorithms being implemented in C-like language. The tool itself would be probably in C++ with Qt to ensure portability and performance.

For now I have few features working:

![screenshot](/screenshots/before_proj.phg "Before projection") ![screenshot](/screenshots/after_proj.phg "After projection")

![screenshot](/screenshots/before_flat.phg "Before projection") ![screenshot](/screenshots/after_flat.phg "After flattening")

![screenshot](/screenshots/before_light.phg "Before relighting") ![screenshot](/screenshots/after_light.phg "After relighting")

![screenshot](/screenshots/before_darn.phg "Before darning") ![screenshot](/screenshots/after_darning.phg "After darning")

There will be three scenarios for future unpager development regading user experience.

First one is *optimistic*:
Hubert is a historian working in the archive. 
He has to process a lot of unique matherial which would be much easier in digital. 
He takes his phone and browses the whole matherial: reports, letters, notes etc. just in sight of phones camera. 
Then he presses the magic "Do it all" button and has a digital copy of all of it on his phone. 
Probably synchronized with his desktop, I don't know, it's another story.


The second one is *realistic*:
Alison goes to the library to work on her project. 
She finds a book quite relative to the topic, but according to this library policies she can't take it home for thorough study. 
Let's assume the book content is in public domain anyways. 
She takes her camera and turns all the pages before it. 
Then, at home, she loads raw matherial on her desktop, sets some projection points, edits some polynomials, removes some lighting points, - 20 minutes a book would be enough effort - and then she has a digital copy of the book on her desktop.

And the third one is *pessimistic*:
Alex wants to share one of his vintage books with the public. 
He sets it up in a holder, puts his camera on a tripod and shoots the books' pages consequently. 
Then on the desktop he carefully sets up all the input for every book part and maybe for some distinct pages separately. 
It takes him about an hour or two. And a couple minutes of machine time for processing. Finally he has a set of pictures he still has to process with some third party tools like djvumaker or something.

Right now the current goal is in enabling third scenario with a single image processing tool. Pictures in - pages out.

