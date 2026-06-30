using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using STS2RitsuLib.Keywords;
using STS2RitsuLib.Models.Capabilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhiteFlame.WhiteFlameCode.Abstracts;
using WhiteFlame.WhiteFlameCode.Capabilities;
using WhiteFlame.WhiteFlameCode.Cards.Keywords;

namespace WhiteFlame.WhiteFlameCode.Cards;

public class TideOfSepia() : WhiteFlameCardTemplate(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self), IModelCapabilitySource
{
    public void BuildDefaultCapabilities(ModelCapabilityList capabilities)
    {
        capabilities.Add<ImmortalCardCapability>();
    }

    public override IEnumerable<CardKeyword> CanonicalKeywords => [WhiteFlameKeywords.Immortal.GetModCardKeyword()];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        int cnt = CardPile.MaxCardsInHand - CardPile.GetCards(base.Owner, PileType.Hand).Count();
        var cards = CardFactory.GetForCombat(base.Owner, base.Owner.Character.CardPool.GetUnlockedCards(base.Owner.UnlockState, base.Owner.RunState.CardMultiplayerConstraint), cnt, base.Owner.RunState.Rng.CombatCardGeneration).ToList();

        if (base.IsUpgraded)
        {
            CardCmd.Upgrade(cards, CardPreviewStyle.None);
        }

        await CardPileCmd.AddGeneratedCardsToCombat(cards, PileType.Hand, base.Owner);
    }
}
