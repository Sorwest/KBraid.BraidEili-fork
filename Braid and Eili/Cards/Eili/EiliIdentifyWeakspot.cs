using Nickel;
using System.Collections.Generic;
using System.Reflection;
using KBraid.BraidEili.Actions;

namespace KBraid.BraidEili.Cards;
public class EiliIdentifyWeakspot : Card, IModdedCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("IdentifyWeakspot", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.EiliDeck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "IdentifyWeakspot", "name"]).Localize
        });
    }
    public override string Name() => "IdentifyWeakspot";

    public override CardData GetData(State state)
    {
        CardData data = new CardData();
        data.cost = upgrade == Upgrade.B ? 1 : 0;
        data.exhaust = upgrade == Upgrade.B ? false : true;
        data.art = ModEntry.Instance.BasicBackground.Sprite;
        return data;
    }

    public override List<CardAction> GetActions(State s, Combat c)
    {
        Upgrade upgrade = this.upgrade;
        List<CardAction> actions = new()
        {
            new AApplyTempBrittle()
            {
                IsRandom = true,
            }
        };
        return actions;
    }
}