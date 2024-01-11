using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBraid.BraidEili.Actions;

public class AApplyTempBrittle : CardAction
{
    public bool IsRandom;
    public override void Begin(G g, State s, Combat c)
    {
    }
    public override List<Tooltip> GetTooltips(State s)
    {
        return new List<Tooltip>()
        {
        //    (Tooltip) new TTGlossary(ModEntry.Instance.A?.Head ?? throw new Exception("Missing ACobraField_Glossary"), Array.Empty<object>())
        };
    }

    public override Icon? GetIcon(State s) => new Icon?(new Icon(ModEntry.Instance.AApplyTempBrittle_Icon.Sprite, new int?(), Colors.textMain));
}