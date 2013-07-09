unpager
=======

Image processing and transformation tool.

This project now is in its early stage. 
It has its main goal in enabling printed matherial scanning with portative tools. 
There is basically three scenarios for using future unpager software.

First one is optimistic:
Hubert is a historian working in the archive. 
He has to process a whole lot of unique material which would be much easier in digital. 
He takes his phone and browses the whole matherial: reports, letters, notes etc. just in sight of phones camera. 
Then he presses the magic "Do it all" button and has a digital copy of all of it on his phone. 
Probably synchronized with his desktop, I don't know, it's a different story.


The second one is realistic:
Alison goes to the library to work on her project. 
She finds a book quite relative to the topic, but according to this library policies she can't take it home for thorough study. 
Let's assume the book content is in public domain anyways. 
She takes her camera and turns all the pages before it. 
Then at home she loads raw matherial on her desktop, sets some projection points, edits some polynomials, removes some lighting points, - 20 minutes a book would be enough effort - and then she has a digital copy of the book on her desktop.

And the third one is pessimistic:
Alex wants to share one of his vintage books with the public. 
He sets it up in a holder, puts his camera on a tripod and shoots the books' pages consequently. 
Then on the desktop he carefully sets up all the input for every book part and maybe for some distinct pages separately. 
It takes him about an hour. And a couple minutes for processing. Finally he has a set of pictures he still has to process with some third party tools like djvumaker or something.

Right now the current goal is in enabling third scenario with a single image processing tool. 
The very basics of it, the algorithmical playground, is already written in C# to allow rapid prototyping along with algorithms being implemented in C-like language. 
The tool itself would be probably in C++ with Qt to ensure portability and performance.
