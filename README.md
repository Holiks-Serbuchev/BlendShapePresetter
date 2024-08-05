# BlendShapePresetter
 BlendShapePresetter is a special tool for Unity designed to simplify working with blendshapes.
## How install project:
To get started with this tool, download the latest version of the tool in the [releases](https://github.com/Holiks-Serbuchev/BlendShapePresetter/releases/tag/Release).

## User Manual:
The first time a user logs into the BlendShapePresetter tool. He sees the interface for creating a preset of blendshapes:
  * The field for selecting the object from which the blendshapes will be copied;
  * A list of meshes that have blendshapes;
  * There are also several buttons:
    * There is an "Auto Mode" button on the top right, it is responsible for quickly switching between modes;
    * The "Save" button is responsible for saving the preset of the blendshapes to a json file. By default, the file is saved to a folder along the path "C:\Users\UserName\Documents\BlendShapePresetter";
    * The "Add Row" button adds an empty field to the list of meshes. If auto mode is enabled, the button will not appear;
    * The "Remove" button is in the list where you can select the meshes that will be recorded in the preset. It is responsible for deleting a certain field in the list of meshes if this mesh is not needed. To make the button appear, you need to turn off auto mode.
   
![alt text](https://raw.githubusercontent.com/Holiks-Serbuchev/BlendShapePresetter/master/Create.png)
   
When the user goes to the "View" tab, he sees the interface for viewing saved presets. The interface for viewing saved presets includes:
* The field for selecting the object to which you want to copy the blendshapes from the file;
* There are also several buttons:
    * The "Copy to the model" button is responsible for copying the preset of the blendshapes to the selected object. This button appears after selecting an object;
    * The "Delete" button is responsible for deleting the preset along this path "C:\Users\UserName\Documents\BlendShapePresetter". After deleting, the list will be updated immediately.

![alt text](https://raw.githubusercontent.com/Holiks-Serbuchev/BlendShapePresetter/master/View.png)

When the user goes to the "Settings" tab, he sees the settings interface. The settings interface includes:
* The field that shows the path to the files;
* Rules section:
    * The "Auto detect objects with blendshapes" rule allows you to automatically detect all meshes with blendshapes on an object;
    * The "Consider empty values" rule allows you to take into account the weight of the blendshapes, which are equal to zero, at the stage of saving.
* The "Github Repository" button allows you to quickly navigate to the source of this tool. 

![alt text](https://raw.githubusercontent.com/Holiks-Serbuchev/BlendShapePresetter/master/Settings.png)
