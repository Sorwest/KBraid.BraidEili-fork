using Nickel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBraid.BraidEili;

internal interface IModdedCard
{
    static abstract void Register(IModHelper helper);

    float ActionRenderingSpacing
        => 1;
}