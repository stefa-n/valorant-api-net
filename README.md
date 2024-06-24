## valorant-api-net
Unofficial, in-game Valorant API Wrapper for .NET / C#

![](https://img.shields.io/github/stars/stefa-n/valorant-api-net?style=plastic) ![GitHub last commit](https://img.shields.io/github/last-commit/stefa-n/valorant-api-net?style=plastic)

### Installation
Build the project using any configuration (Debug / Release); Copy the .nupkg file from the build folder to the location of your local Nuget source folder. You can add one by going to %appdata%\NuGet\NuGet.Config and adding a key to a folder, e.g.
```
<packageSources>
	...
	<add key="MyLocalSharedSource" value="Path/To/Folder" protocolVersion="3" />
</packageSources>
```
After that, you can go to your IDE and install the package from there by searching for "Valorant" from the "MyLocalSharedSource" source.

### Usage
After the installation, you can immediately start using it by typing `Valorant.` and letting the IDE autocomplete. The API handler takes care of taking info from the Lockfile, encoding the password, making the requests, so everything becomes as simple as calling a method.
You can check out an example project [here](https://github.com/stefa-n/valorant_voice_lock_in).
