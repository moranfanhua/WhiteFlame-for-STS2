using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhiteFlame.WhiteFlameCode.Abstracts;

namespace WhiteFlame.WhiteFlameCode.Cards;

public class SepiaCorrode() : WhiteFlameCardTemplate(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var selected = (await CardSelectCmd.FromHand(
            prefs: new CardSelectorPrefs(CardSelectorPrefs.ExhaustSelectionPrompt, 1),
            context: choiceContext,
            player: base.Owner,
            filter: null,
            source: this)).FirstOrDefault();

        if (selected == null)
            return;

        // 为自动打出创建复制品（原牌将被消耗）
        var copy = selected.CreateClone();
        await CardCmd.Exhaust(choiceContext, selected);
        await CardCmd.AutoPlay(choiceContext, copy, null);

        if (base.IsUpgraded)
        {
            var handCopy = selected.CreateClone();
            await CardPileCmd.AddGeneratedCardToCombat(handCopy, PileType.Hand, base.Owner);
        }
    }

}
