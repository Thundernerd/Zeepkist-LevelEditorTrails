# Zeepkist-LevelEditorTrails

A mod that draws trails in the level editor for the runs that you've played. Helps with guesstimating where your next block(s) should go!

## Installation

### Installing BepInEx

Before installing LevelEditorTrails, you need to have BepInEx installed. The easiest way to install BepInEx for Zeepkist is using one of these community tools:

- **Windows**: [Modkist Revamped](https://github.com/donderjoekel/ModkistRevamped) - A modloader for Zeepkist using mod.io with a user-friendly interface
- **Linux**: [zeeper](https://codeberg.org/Vulpesx/zeeper) - A command-line tool for managing Zeepkist mods and installing BepInEx

Alternatively, you can manually install BepInEx by following the [official BepInEx documentation](https://docs.bepinex.dev/articles/user_guide/installation/index.html).

### Installing LevelEditorTrails

You can install LevelEditorTrails using the same tools mentioned above for installing BepInEx:

- **Windows**: Use [Modkist Revamped](https://github.com/donderjoekel/ModkistRevamped) to install LevelEditorTrails through its mod browser
- **Linux**: Use [zeeper](https://codeberg.org/Vulpesx/zeeper) to install LevelEditorTrails via command line

Alternatively, you can install LevelEditorTrails manually:

1. Download the latest LevelEditorTrails release
2. Place the `net.tnrd.zeepkist.leveleditortrails.dll` file in your `BepInEx/plugins` directory
3. Launch Zeepkist - LevelEditorTrails will load automatically

## How to use

Once installed, LevelEditorTrails automatically records your runs when you test a level in the level editor:

1. Open the level editor in Zeepkist
2. Test your level by clicking the test button or pressing the test key
3. The mod will automatically record your trail as you play
4. After finishing your test run, return to the editor to see your trail visualized
5. Multiple test runs will create multiple trails, helping you see different paths and optimize your level design

### Controls

- **F7**: Toggle visibility of all trails on/off
- **F6**: Toggle visibility of time markers on/off
- **F8**: Remove all recorded trails

## Configuration

LevelEditorTrails can be customized through its configuration file. The configuration file is located at `BepInEx/config/LevelEditorTrails.cfg`.

You can edit this file manually with any text editor, or access the settings in-game through Zeep mod settings for a more user-friendly interface.

### Configuration Options

#### General Settings

| Key                   	| Default value 	| Description                                      	|
|-----------------------	|---------------	|--------------------------------------------------	|
| Maximum Recordings    	| 5             	| Maximum number of trails to keep in memory. Older trails will be removed when this limit is reached	|
| Trail Width           	| 0.5           	| Min = 0.1 / Max = 2. The thickness of the trail lines. Higher values create thicker lines	|
| Draw Time Markers     	| true          	| Whether to draw time markers along the trails	|
| Time Marker Step      	| 1.0           	| The time in seconds between every time marker	|

#### Keyboard Shortcuts

| Key                   	| Default value 	| Description                                      	|
|-----------------------	|---------------	|--------------------------------------------------	|
| Toggle Line Visibility	| F7            	| Keyboard key to toggle the visibility of all trails on/off	|
| Toggle Time Marker Visibility | F6       	| Keyboard key to toggle the visibility of time markers on/off	|
| Remove All Lines      	| F8            	| Keyboard key to remove all recorded trails	|

#### Color Settings

| Key                   	| Default value 	| Description                                      	|
|-----------------------	|---------------	|--------------------------------------------------	|
| Color Mode            	| Gradient      	| The color mode for trails. Options: `Single` (all trails same color) or `Gradient` (color transitions from newest to oldest trail)	|
| Color                  	| White         	| The color used when Color Mode is set to `Single`	|
| Start Color            	| White         	| The color for the newest trail when Color Mode is set to `Gradient`	|
| End Color              	| Black         	| The color for the oldest trail when Color Mode is set to `Gradient`	|
