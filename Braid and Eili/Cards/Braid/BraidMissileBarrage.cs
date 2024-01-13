using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace KBraid.BraidEili.Cards;
public class BraidMissileBarrage : Card, IModdedCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("MissileBarrage", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.BraidDeck.Deck,
                rarity = Rarity.rare,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "MissileBarrage", "name"]).Localize
        });
    }
    public override string Name() => "Missile Barrage";
    public override CardData GetData(State state)
    {
        CardData data = new CardData();
        data.cost = upgrade == Upgrade.A ? 2 : 3;
        data.exhaust = true;
        data.art = new Spr?(StableSpr.cards_Scattershot);
        return data;
    }

    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = new();
        switch (upgrade)
        {
            case Upgrade.None:
                List<CardAction> cardActionList1 = new List<CardAction>()
                {
                    new ASpawn()
                    {
                        thing = new Missile()
                        {
                            targetPlayer = false
                        }
                    },
                    new AMove()
                    {
                        dir = -1,
                        targetPlayer = true
                    },
                    new ASpawn()
                    {
                        thing = new Missile()
                        {
                            targetPlayer = false
                        }
                    },
                    new AMove()
                    {
                        dir = -1,
                        targetPlayer = true
                    },
                    new ASpawn()
                    {
                        thing = new Missile()
                        {
                            targetPlayer = false
                        }
                    }
                };
                actions = cardActionList1;
                break;
            case Upgrade.A:
                List<CardAction> cardActionList2 = new List<CardAction>()
                {
                    new ASpawn()
                    {
                        thing = new Missile()
                        {
                            targetPlayer = false
                        }
                    },
                    new AMove()
                    {
                        dir = -1,
                        targetPlayer = true
                    },
                    new ASpawn()
                    {
                        thing = new Missile()
                        {
                            targetPlayer = false
                        }
                    },
                    new AMove()
                    {
                        dir = -1,
                        targetPlayer = true
                    },
                    new ASpawn()
                    {
                        thing = new Missile()
                        {
                            targetPlayer = false
                        }
                    }
                };
                actions = cardActionList2;
                break;
            case Upgrade.B:
                List<CardAction> cardActionList3 = new List<CardAction>()
                {
                    new ASpawn()
                    {
                        thing = new Missile()
                        {
                            targetPlayer = false,
                            missileType = MissileType.heavy
                        }
                    },
                    new AMove()
                    {
                        dir = -1,
                        targetPlayer = true
                    },
                    new ASpawn()
                    {
                        thing = new Missile()
                        {
                            targetPlayer = false,
                            missileType = MissileType.heavy
                        }
                    },
                    new AMove()
                    {
                        dir = -1,
                        targetPlayer = true
                    },
                    new ASpawn()
                    {
                        thing = new Missile()
                        {
                            targetPlayer = false,
                            missileType = MissileType.heavy
                        }
                    }
                };
                actions = cardActionList3;
                break;
        }
        return actions;
    }
}
