#Path of Vision (pov).

Contents:
- what pov does.
- how pov work.
- virus total cheks.

:star: Program allows you to "move" the icon of buffs or debuffs (not yet ready) to any position on the screen (or second monitor).
Strong beta alpha gamma atomic.

![7ly8lx](https://github.com/RandomNameQ/path-of-vision/assets/125605136/cf434d86-9e4b-4ec6-bf98-4bcb2c1ca109)
![7ly9wy](https://github.com/RandomNameQ/path-of-vision/assets/125605136/1d5c5d0c-2a77-4669-9048-ffa4c814912d)

:grey_exclamation: ~Works with all buffs (requires configuration).
:grey_exclamation: Track any buffs in a convenient place for the eyes.

~~Debuff logger ~~~ stores information about what debuffs were imposed on us, a description and a picture.
~~Debuff detector ~~~ the same mechanics as buff tracking.
~~Anti scam blessing~~ shows the total amount of transferred and received currency, protection against replacement of essences
Not working yet.

##How it work.

1. Pov collects all the images from the "Icons\Buff" folder, finds the center of the image and collects ~10 pixels from left to right.

2. After creates a screenshot of the upper part of the screen, approximately 10%.
3. Tries to find the icons of sumons, brands, flasks and other buffs.
3.1. If it does, it finds the center of the image and collects ~10 pixels from left to right.
4. Tries to find icons from 1 point by matching pixels from 1 and 3.1 points.
5. If it finds, then it shows.

6. Repeats step 2-5 every 0.1 seconds.



##:poop::poop::poop::poop:Virus check.

https://www.virustotal.com/gui/file/92fb88c92a172345c4f5a4673d2a1199830dcdb5951bfccf79bc1ba3306497ab
sourceCodeVirusCheck.zip - all files

https://www.virustotal.com/gui/file/115690af0ad8529082620f46aac8c985e9802668848dc2aee8800ba6f27a5eed
Poe show buff.dll

https://www.virustotal.com/gui/file/115690af0ad8529082620f46aac8c985e9802668848dc2aee8800ba6f27a5eed
pathOfVision.zip 

It can be virus or false positive. Dont know. Use at your own risk :)



