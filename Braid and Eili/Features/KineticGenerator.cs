﻿using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBraid.BraidEili;
internal sealed class KineticGeneratorManager : IStatusLogicHook
{
    public KineticGeneratorManager()
    {
        //ModEntry.Instance.KokoroApi.RegisterStatusLogicHook(this, 0);
    }
}