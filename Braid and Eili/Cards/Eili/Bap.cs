
using System.Collections.Generic;

#nullable enable
[CardMeta(deck = Deck.eili, rarity = Rarity.common, upgradesTo = new Upgrade[] {Upgrade.A, Upgrade.B})]
public class Bap : Card
{
  public override string Name() => "Bap";

  public override CardData GetData(State state)
  {
    return new CardData()
    {
      cost = 0,
      art = new Spr?(Spr.cards_colorless),
    };
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
        actions = cardActionList1;
        break;
      case Upgrade.A:
        List<CardAction> cardActionList2 = new List<CardAction>();
        cardActionList2.Add((CardAction) new AAttack()
        {
          damage = this.GetDmg(s, 1),
          damage = this.GetDmg(s, 1),
        });
        actions = cardActionList2;
        break;
      case Upgrade.B:
        List<CardAction> cardActionList3 = new List<CardAction>();
        cardActionList3.Add((CardAction) new AAttack()
        {
          damage = this.GetDmg(s, 1),
          stunEnemy = true
        });
        cardActionList3.Add((CardAction) new AStatus()
        {
          status = Status.stunCharge,
          statusAmount = 1,
          targetPlayer = true
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
