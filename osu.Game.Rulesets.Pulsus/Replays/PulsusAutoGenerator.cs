// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Beatmaps;
using osu.Game.Rulesets.Pulsus.Objects;
using osu.Game.Rulesets.Replays;

namespace osu.Game.Rulesets.Pulsus.Replays
{
    public class PulsusAutoGenerator : AutoGenerator<PulsusReplayFrame>
    {
        public new Beatmap<PulsusHitObject> Beatmap => (Beatmap<PulsusHitObject>)base.Beatmap;

        public PulsusAutoGenerator(IBeatmap beatmap)
            : base(beatmap)
        {
        }

        protected override void GenerateFrames()
        {
            Frames.Add(new PulsusReplayFrame());

            foreach (PulsusHitObject hitObject in Beatmap.HitObjects)
            {
                Frames.Add(new PulsusReplayFrame
                {
                    Time = hitObject.StartTime,
                    Position = hitObject.Position,
                });
            }
        }
    }
}
