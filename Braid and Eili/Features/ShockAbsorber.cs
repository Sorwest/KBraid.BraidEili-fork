using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBraid.BraidEili;
internal sealed class ShockAbsorberManager : IStatusLogicHook
{
    public ShockAbsorberManager()
    {
        //ModEntry.Instance.KokoroApi.RegisterStatusLogicHook(this, 0);
    }
}