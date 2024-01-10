
using System.Collections.Generic;

#nullable enable
[CardMeta(deck = Deck.braid, rarity = Rarity.common, upgradesTo = new Upgrade[] {Upgrade.A, Upgrade.B})]
public class Pummel : Card
{
  public override string Name() => "Pummel";

  public override CardData GetData(State state)
  {
    CardData data = new CardData();
    data.cost = 2;
    data.art = new Spr?(Spr.cards_Scattershot);
    Upgrade upgrade = this.upgrade;
    bool flag;
    switch (upgrade)
    {
      case Upgrade.None:
        flag = false;
        break;
      case Upgrade.A:
        flag = false;
        break;
      case Upgrade.B:
        flag = true;
        break;
      default:
        // ISSUE: reference to a compiler-generated method
        \u003CPrivateImplementationDetails\u003E.ThrowSwitchExpressionException((object) upgrade);
        break;
    }
    data.infinite = flag;
    return data;
  }

  public override List<CardAction> GetActions(State s, Combat c)
  {
    Upgrade upgrade = this.upgrade;
    List<CardAction> actions;
    switch (upgrade)
    {
      case Upgrade.None:
        List<CardAction> cardActionList1 = new List<CardAction>();
        cardActionList1.Add((CardAction) new AAttack()
        {
          damage = this.GetDmg(s, 1),
        });
        cardActionList1.Add((CardAction) new AAttack()
        {
          damage = this.GetDmg(s, 1),
        });
        cardActionList1.Add((CardAction) new AAttack()
        {
          damage = this.GetDmg(s, 1),
        });
        actions = cardActionList1;
        break;
      case Upgrade.A:
        List<CardAction> cardActionList2 = new List<CardAction>();
        cardActionList2.Add((CardAction) new AAttack()
        {
          damage = this.GetDmg(s, 1),
          piercing = true
        });
        cardActionList2.Add((CardAction) new AAttack()
        {
          damage = this.GetDmg(s, 1),
          piercing = true
        });
        cardActionList2.Add((CardAction) new AAttack()
        {
          damage = this.GetDmg(s, 1),
          piercing = true
        });
        actions = cardActionList2;
        break;
      case Upgrade.B:
        List<CardAction> cardActionList3 = new List<CardAction>();
        cardActionList3.Add((CardAction) new AAttack()
        {
          damage = this.GetDmg(s, 4),
        });
        actions = cardActionList3;
        break;
      default:
        // ISSUE: reference to a compiler-generated method
        \u003CPrivateImplementationDetails\u003E.ThrowSwitchExpressionException((object) upgrade);
        break;
    }
    return actions;
  }
}
