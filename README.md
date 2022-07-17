# Zeepkist-LevelEditorTrails

A mod that draws trails in the level editor for the runs that you've played. Helps with guesstimating where your next block(s) should go!

## Installation
1. Extract the `BepInEx_x64_5.4.19.0.zip` (that's the one this mod is developed on) into your Zeepkist install directory
2. Start the game once
3. Copy the `net.tnrd.zeepkist.leveleditortrails.dll` into the `BepInEx/plugins` folder that can be found in the Zeepkist install directory

## How to use
1. Simply test a level in the level editor

## Configuration
There are some things that can be configured. The configuration file can be found at `BepInEx/config` named `net.tnrd.zeepkist.leveleditortrails.cfg`. You can either edit this manually with a text editor, or use a plugin like [BepInExConfigManager](https://github.com/sinai-dev/BepInExConfigManager)

| Key              	| Default value 	| Description                                      	|
|------------------	|---------------	|--------------------------------------------------	|
| levelOfDetail    	| 5             	| Min = 1 / Max = 10. The higher the more detailed 	|
| maxRecordings    	| 5             	| How many trails should we keep                   	|
| lineWidth        	| 0.5           	| Min = 0.1 / Max = 2. The higher the thicker      	|
| toggleVisibility 	| F7            	| Toggles the visibility of the lines              	|
| removeAllLines   	| F8            	| Pressing this will remove all lines              	|
