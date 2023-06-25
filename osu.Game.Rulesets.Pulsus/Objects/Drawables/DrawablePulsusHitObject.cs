﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input.Bindings;
using osu.Framework.Input.Events;
using osu.Game.Audio;
using osu.Game.Beatmaps.ControlPoints;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Scoring;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Pulsus.Objects.Drawables
{
    public partial class DrawablePulsusHitObject : DrawableHitObject<PulsusHitObject>
    {
        private const double time_preempt = 600;
        private const double time_fadein = 400;

        public override bool HandlePositionalInput => true;

        public HitReceptor? HitArea { get; private set; }
        public int PositionIndex;
        public PulsusAction BindAction;

        public DrawablePulsusHitObject(PulsusHitObject hitObject)
            : base(hitObject)
        {
            Size = new Vector2(80);

            Origin = Anchor.Centre;
            Position = hitObject.Position;
            PositionIndex = hitObject.PositionIndex;
            BindAction = getPulsusAction(PositionIndex);
        }

        private PulsusAction getPulsusAction(int positionIndex)
        {
            return (PulsusAction)positionIndex - 1;
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            AddInternal(new Sprite
            {
                RelativeSizeAxes = Axes.Both,
                Texture = textures.Get("coin"),
            });
            AddRangeInternal(new Drawable[]
            {
                HitArea = new HitReceptor
                {
                    Hit = () =>
                    {
                        if (AllJudged)
                            return false;

                        UpdateResult(true);
                        return true;
                    },
                }
            });
        }

        public override IEnumerable<HitSampleInfo> GetSamples() => new[]
        {
            new HitSampleInfo(HitSampleInfo.HIT_NORMAL, SampleControlPoint.DEFAULT_BANK)
        };

        protected override void CheckForResult(bool userTriggered, double timeOffset)
        {
            if (HitArea == null)
                return;

            if (HitArea.HitAction == BindAction && timeOffset > -500)
            {
                ApplyResult(r => r.Type = HitResult.Perfect);
                return;
            }

            if (timeOffset >= 0)
                ApplyResult(r => r.Type = HitResult.Miss);
        }

        protected override double InitialLifetimeOffset => time_preempt;

        protected override void UpdateInitialTransforms() => this.FadeInFromZero(time_fadein);

        protected override void UpdateHitStateTransforms(ArmedState state)
        {
            switch (state)
            {
                case ArmedState.Hit:
                    this.ScaleTo(5, 1500, Easing.OutQuint).FadeOut(1500, Easing.OutQuint).Expire();
                    break;

                case ArmedState.Miss:
                    const double duration = 1000;

                    this.ScaleTo(0.8f, duration, Easing.OutQuint);
                    this.MoveToOffset(new Vector2(0, 10), duration, Easing.In);
                    this.FadeColour(Color4.Red.Opacity(0.5f), duration / 2, Easing.OutQuint).Then().FadeOut(duration / 2, Easing.InQuint).Expire();
                    break;
            }
        }

        public partial class HitReceptor : CompositeDrawable, IKeyBindingHandler<PulsusAction>
        {
            public PulsusAction? HitAction;

            public Func<bool>? Hit;

            public bool OnPressed(KeyBindingPressEvent<PulsusAction> e)
            {
                Console.WriteLine(e.Action);

                if (!(Hit?.Invoke() ?? false))
                    return false;

                HitAction = e.Action;
                return true;
            }

            public void OnReleased(KeyBindingReleaseEvent<PulsusAction> e)
            {
            }
        }
    }
}