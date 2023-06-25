// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Input;
using osu.Game.Beatmaps;
using osu.Game.Input.Handlers;
using osu.Game.Replays;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Pulsus.Objects;
using osu.Game.Rulesets.Pulsus.Objects.Drawables;
using osu.Game.Rulesets.Pulsus.Replays;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Pulsus.UI
{
    [Cached]
    public partial class DrawablePulsusRuleset : DrawableRuleset<PulsusHitObject>
    {
        public DrawablePulsusRuleset(PulsusRuleset ruleset, IBeatmap beatmap, IReadOnlyList<Mod>? mods = null)
            : base(ruleset, beatmap, mods)
        {
        }

        public override PlayfieldAdjustmentContainer CreatePlayfieldAdjustmentContainer() => new PulsusPlayfieldAdjustmentContainer();

        protected override Playfield CreatePlayfield() => new PulsusPlayfield();

        protected override ReplayInputHandler CreateReplayInputHandler(Replay replay) => new PulsusFramedReplayInputHandler(replay);

        public override DrawableHitObject<PulsusHitObject> CreateDrawableRepresentation(PulsusHitObject h) => new DrawablePulsusHitObject(h);

        protected override PassThroughInputManager CreateInputManager() => new PulsusInputManager(Ruleset.RulesetInfo);
    }
}
