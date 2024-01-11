using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBraid.BraidEili;
internal sealed class TempShieldNextTurnManager : IStatusLogicHook
{
    public TempShieldNextTurnManager()
    {
        //ModEntry.Instance.KokoroApi.RegisterStatusLogicHook(this, 0);
    }
}