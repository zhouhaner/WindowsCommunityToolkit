﻿// ******************************************************************
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THE CODE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH
// THE CODE OR THE USE OR OTHER DEALINGS IN THE CODE.
// ******************************************************************

using System.Collections.Generic;
using Microsoft.Toolkit.Uwp.UI.Controls.Lottie.Animation.Keyframe;
using Microsoft.Toolkit.Uwp.UI.Controls.Lottie.Value;

namespace Microsoft.Toolkit.Uwp.UI.Controls.Lottie.Model.Animatable
{
    internal class AnimatableScaleValue : BaseAnimatableValue<ScaleXy, ScaleXy>
    {
        internal AnimatableScaleValue()
            : this(new ScaleXy())
        {
        }

        public AnimatableScaleValue(ScaleXy value)
            : base(value)
        {
        }

        public AnimatableScaleValue(List<Keyframe<ScaleXy>> keyframes)
            : base(keyframes)
        {
        }

        public override IBaseKeyframeAnimation<ScaleXy, ScaleXy> CreateAnimation()
        {
            return new ScaleKeyframeAnimation(Keyframes);
        }
    }
}