﻿using System.Threading.Tasks;
using CliFx.Attributes;
using ManagedPatcher.Config;

namespace ManagedPatcher.Commands
{
    [Command("patch")]
    public class PatchCommand : BaseCommand
    {
        public override ValueTask ExecuteAsync(ConfigFile config)
        {
            throw new System.NotImplementedException();
        }
    }
}