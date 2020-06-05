# MoatTekSoundboard
A soundboard meant for annoying other people in the TeamSpeak server.
Intended to be used alongside [VB Audio Virtual Cable](https://www.vb-audio.com/Cable/), and supports up to 2 output devices.

# Setting Up
1) Build the project, and create a folder called "SoundFiles" in the same directory.
2) Add your desired mp3 files to the folder.

## Custom Button Colours
Lots of grey buttons are boring, so now you can customise the colours. Just add a hex code (excluding the #) and an underscore to the beginning of the file. The hex code and the underscore will not show on on the button. Example below:

FF0000_ThisWillBeRed.mp3

# Known issues
- Adding too many mp3 files will cause the buttons to misbehave
- Pressing the buttons and changing audio devices too quickly will cause the program to crash.
- Pressing a button while another is playing will stop the current playing sound without playing the new one.
- Output devices bar at the bottom looks ugly

# To do
- Add volume control.
- Search for other file types (such as WAV etc).
- sizing options for everything.

---

# Changelog

# V1.2
- Made text on buttons slightly smaller.

---

# V1.1
- Added ability to colour different sound clips.

---

# V1
- Basic soundboard made.
