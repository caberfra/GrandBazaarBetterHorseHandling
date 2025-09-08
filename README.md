# Story of Seasons BepInEx Plugin Tutorial

This example plugin demonstrates how to create a simple plugin for BepInEx IL2CPP.  
All the plugin does is remove all stamina from player actions. The code is fully commented to help you understand how it
works.

---

## Setup

### Prerequisites

- **.NET 6.0 SDK** or later
- **A C# IDE** (e.g., Visual Studio, JetBrains Rider, etc.)
- [BepInEx IL2CPP Plugin Template](https://docs.bepinex.dev/master/articles/dev_guide/plugin_tutorial/1_setup.html) (
  optional, but highly recommended)
- **dnSpy** (to inspect game assemblies and find methods to hook into(and see classes, enums, variables etc.))
- **BepInEx** (installed in your game folder and run at least once, see the [BepInEx
  documentation](https://docs.bepinex.dev/master/articles/user_guide/installation/unity_il2cpp.html) for instructions). You will need it for the decompiled DLL's.


You can fork this repository and use it as a starting point, or create a new project with the BepInEx IL2CPP plugin
template.

I recommended starting from scratch with the BepInEx template, as it sets up the plugin info for you and helps you learn how to add assemblies.



### Notes

- **Decompiling Game Code**:  
  The actual function code will not be readable in dnSpy due to it being compiled in il2CPP. You will need to use [Ghidra](https://github.com/NationalSecurityAgency/ghidra/releases) to decompile the code.  
  Note that the decompiled code will be in C, so if you are unfamiliar with C, it may be hard to understand.
  <br><br>
- **Adding Assemblies**(this allows you to access game classes and methods):  
  To add assemblies(only Rider and Visual Studio are covered here):
    1. Rider - Right-click on the Dependencies option in the Explorer tab and select **"Reference... -> Add From.."**
       and select where your games DLL's are
    2. Visual Studio - Right-click on the Dependencies option in the Solution Explorer and select **"Add Project
       Reference... -> Browse..."**.
    3. Browse to the DLL you want to add (these will be in the `BepInEx/interop` folder, but I recommend moving them to another folder outside the game).
    4. Select the DLL and click OK. (You can check if it was added by expanding the Dependencies and looking in assemblies(In Rider under .NET 6.0 -> Assemblies))
<br>
  The `.csproj` file also contains comments for clarification on what they add.
<br><br>
---
## Build
- **Building the Plugin**:  
  To build the plugin, just build the project in your IDE. The output DLL will be located in the `bin/Debug/net6.0` or
  `bin/Release/net6.0` folder of your project, depending on your build configuration. 
<br><br>You can then copy this DLL to the `BepInEx/plugins` folder of your game(The DLL will be named, ex.`yourPLUGIN_GUID.DLL`).<br>Only move that DLL, nothing else.
<br><br>There is also a post-build event in the `.csproj` file that will copy the DLL to the `BepInEx/plugins` folder of your game automatically when you build the project. You will need to change the path to your game folder in the `.csproj` file, though. It is commented where that is in the file.

---

## Need Help?

If you need help, you can join the Story of Seasons Modding Discord.  
There is a tutorial available there that explains how to set up a plugin from scratch using the BepInEx IL2CPP template.
