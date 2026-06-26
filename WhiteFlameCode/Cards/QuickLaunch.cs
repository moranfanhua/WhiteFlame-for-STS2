using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhiteFlame.WhiteFlameCode.Abstracts;

namespace WhiteFlame.WhiteFlameCode.Cards;

public class QuickLaunch() : WhiteFlameCardTemplate(0, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var handCards = PileType.Hand.GetPile(base.Owner).Cards.ToList();

        foreach (var card in handCards)
        {
            await CardCmd.Exhaust(choiceContext, card);
        }

        int drawCount;
        if (base.IsUpgraded)
        {
            drawCount = CardPile.MaxCardsInHand - base.Owner.PlayerCombatState.Hand.Cards.Count;
        }
        else
        {
            drawCount = handCards.Count(c => c != this);
        }

        if (drawCount > 0)
        {
            await CardPileCmd.Draw(choiceContext, drawCount, base.Owner);
        }

        await PlayerCmd.GainEnergy(3m, base.Owner);
    }

}
