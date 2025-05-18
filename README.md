# ModeratorTools

A new iteration of the old AdminTools plugin. Contains various tools to assist staff.

This plugin is built using my command system, allowing for the usage of custom player selectors in nearly all commands.

> [!TIP]
> Check the [wiki](https://github.com/Axwabo/ModeratorTools/wiki) for more information.

# Installation

1. Install [Axwabo.CommandSystem](https://github.com/Axwabo/CommandSystem)
    - Follow the guide in the README to install Axwabo.Helpers and Harmony
2. Download the `ModeratorTools.dll` file from the [releases page](https://github.com/Axwabo/ModeratorTools/releases)
3. Place the file in the `plugins` folder:
    - Windows: `%appdata%\SCP Secret Laboratory\LabAPI\plugins\<port>`
    - Linux: `.config/SCP Secret Laboratory/LabAPI/plugins/<port>`
4. Restart the server

# Permissions

Check out the permission table [here](https://github.com/Axwabo/ModeratorTools/wiki/Permissions)

By default, the plugin uses string-based permissions provided by LabAPI.
To switch to using base-game permissions, set `vanilla_permissions` to `true` in the plugin's configuration.

You'll have to configure LabAPI permissions for staff to be able to use commands.
The permissions configuration is located at:

- Windows: `%appdata%\SCP Secret Laboratory\LabAPI\configs\permissions.yml`
- Linux: `.config/SCP Secret Laboratory/LabAPI/configs/permissions.yml`

> [!CAUTION]
> Wildcards should only be given to groups with maximum trust, such as the owner.

To allow a group to use all ModeratorTools permissions, add the entry `mt.*`
