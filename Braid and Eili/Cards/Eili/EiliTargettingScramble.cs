using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace KBraid.BraidEili.Cards;
public class EiliTargettingScramble : Card, IModdedCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("TargettingScramble", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.EiliDeck.Deck,
                rarity = Rarity.rare,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "TargettingScramble", "name"]).Localize
        });
    }
    public override string Name() => "Targetting Scramble";

    public override CardData GetData(State state)
    {
        CardData data = new CardData();
        data.cost = 2;
        data.exhaust = upgrade == Upgrade.A ? false : true;
        data.retain = upgrade == Upgrade.B ? true : false;
        data.art = ModEntry.Instance.BasicBackground.Sprite;
        return data;
    }

    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = new()
        {
            new ADroneFlip()
        };
        return actions;
    }
}