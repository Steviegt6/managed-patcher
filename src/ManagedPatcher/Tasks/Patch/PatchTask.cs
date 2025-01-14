﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ManagedPatcher.Config;
using ManagedPatcher.Utilities;
using Spectre.Console;

namespace ManagedPatcher.Tasks.Patch
{
    public class PatchTask : PatcherTask<PatchArguments>
    {
        public override async Task ExecuteAsync(PatchArguments args)
        {
            ConfigFile config = args.Config;

            foreach ((string? name, string[]? paths) in config.Patches)
            {
                if (args.Patches.Count != 0 && !args.Patches.Contains(name))
                    continue;
                
                if (paths.Length != 3)
                    throw new InvalidOperationException("Cannot perform patch task when three paths are not provided.");

                string directory = paths[0];
                string patches = paths[1];
                string original = paths[2];
                
                AnsiConsole.MarkupLine($"[gray]Executing patch task \"{name}\": {patches} -> {directory} (base: {original})[/]");

                DirectoryInfo dir = new(directory);
                DirectoryInfo origDir = new(original);
                
                // Reset the files for proper patching.
                InOutUtils.CopyDirectory(origDir, dir, true);

                await DirectoryPatcher.PatchDirectories(
                    new DirectoryInfo(patches),
                    new DirectoryInfo(directory)
                );
                
                AnsiConsole.MarkupLine($"[gray]Finished patch task \"{name}\"[/]");
            }
            
            await Task.CompletedTask;
        }
    }
}