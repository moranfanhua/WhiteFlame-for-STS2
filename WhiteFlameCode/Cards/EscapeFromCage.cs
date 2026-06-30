using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using STS2RitsuLib.Keywords;
using STS2RitsuLib.Models.Capabilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhiteFlame.WhiteFlameCode.Abstracts;
using WhiteFlame.WhiteFlameCode.Capabilities;
using WhiteFlame.WhiteFlameCode.Cards.Keywords;

namespace WhiteFlame.WhiteFlameCode.Cards;

public class EscapeFromCage() : WhiteFlameCardTemplate(1, CardType.Skill, CardRarity.Common, TargetType.Self), IModelCapabilitySource
{
    public void BuildDefaultCapabilities(ModelCapabilityList capabilities)
    {
        capabilities.Add<ImmortalCardCapability>();
    }

    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(4)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust, WhiteFlameKeywords.Immortal.GetModCardKeyword()];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner);

        var selected = (await CardSelectCmd.FromHand(prefs: new CardSelectorPrefs(CardSelectorPrefs.ExhaustSelectionPrompt, 2), context: choiceContext, player: base.Owner, filter: null, source: this)).FirstOrDefault();
        if (selected != null)
        {
            await CardCmd.Exhaust(choiceContext, selected);
        }
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Cards.UpgradeValueBy(1m);
    }
}
