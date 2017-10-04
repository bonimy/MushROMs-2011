# MushROMs (2011)
An archive of MushROMs' design strategy from 2011

**This program currently doesn't run and is for archive purposes only. If you want to work on this, then that's alright too.**

## About
This is the earliest software construction I had for MushROMs back when it was just supposed to be an editor for Super Mario All-Stars. The project was abandoned because I was using unmanaged DLLs to draw on windows forms. This would cause design-time errors in the Visual Studio IDE if the DLL was not present. Additionally, I realized it wasn't a good idea to put application code in form classes (for the design reasons mentioned earlier).

This project made it pretty far as functionality. It had a working palette editor, GFX viewer, and Map16 viewer (designed to work like Lunar Magic at the time). It could also draw levels somewhat.

The base path for the game files is currently hardcoded, so new users won't get much utility without it. I'm merely hosting this code on GitHub because it has a lot of useful knowledge in it. If someone wants to take it and make something out of it, then that's fine too.
