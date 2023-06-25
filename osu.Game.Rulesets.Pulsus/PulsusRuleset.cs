// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Input.Bindings;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Difficulty;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Pulsus.Beatmaps;
using osu.Game.Rulesets.Pulsus.Mods;
using osu.Game.Rulesets.Pulsus.UI;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Pulsus
{
    public class PulsusRuleset : Ruleset
    {
        public override string Description => "Pulsus like game";

        public override DrawableRuleset CreateDrawableRulesetWith(IBeatmap beatmap, IReadOnlyList<Mod>? mods = null)
        {
            if (mods == null) throw new ArgumentNullException(nameof(mods));
            return new DrawablePulsusRuleset(this, beatmap, mods);
        }

        public override IBeatmapConverter CreateBeatmapConverter(IBeatmap beatmap) =>
            new PulsusBeatmapConverter(beatmap, this);

        public override DifficultyCalculator CreateDifficultyCalculator(IWorkingBeatmap beatmap) =>
            new PulsusDifficultyCalculator(RulesetInfo, beatmap);

        public override IEnumerable<Mod> GetModsFor(ModType type)
        {
            switch (type)
            {
                case ModType.Automation:
                    return new[] { new PulsusModAutoplay() };

                default:
                    return Array.Empty<Mod>();
            }
        }

        public override string ShortName => "pulsus";

        public override IEnumerable<KeyBinding> GetDefaultKeyBindings(int variant = 0) => new[]
        {
            new KeyBinding(InputKey.Keypad1, PulsusAction.Button1),
            new KeyBinding(InputKey.Keypad2, PulsusAction.Button2),
            new KeyBinding(InputKey.Keypad3, PulsusAction.Button3),
            new KeyBinding(InputKey.Keypad4, PulsusAction.Button4),
            new KeyBinding(InputKey.Keypad5, PulsusAction.Button5),
            new KeyBinding(InputKey.Keypad6, PulsusAction.Button6),
            new KeyBinding(InputKey.Keypad7, PulsusAction.Button7),
            new KeyBinding(InputKey.Keypad8, PulsusAction.Button8),
            new KeyBinding(InputKey.Keypad9, PulsusAction.Button9),
        };

        public override Drawable CreateIcon() => new PulsusRulesetIcon(this);

        // Leave this line intact. It will bake the correct version into the ruleset on each build/release.
        public override string RulesetAPIVersionSupported => CURRENT_RULESET_API_VERSION;
    }
}
