using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhiteFlame.WhiteFlameCode.Abstracts;

namespace WhiteFlame.WhiteFlameCode.Powers;

public sealed class SepiaVeinPower : WhiteFlamePowerTemplate
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel card, bool causedByEthereal)
    {
        var handCards = PileType.Hand.GetPile(base.Owner.Player).Cards.ToList();
        if (handCards.Count == 0)
            return;

        Flash();

        for (int i = 0; i < base.Amount; i++)
        {
            var target = handCards[Random.Shared.Next(handCards.Count)];
            target.EnergyCost.AddThisTurn(-1);
        }
    }
}
