using System;
using System.Collections.Generic;

namespace Common.Circle
{
    public class CircleCounter
    {
        private readonly List<WhiteCircleController> _whiteCirclesOnScene = new List<WhiteCircleController>();
        private readonly List<BlackCircleController> _blackCirclesOnScene = new List<BlackCircleController>();
        
        public IReadOnlyList<WhiteCircleController> WhiteCirclesOnScene => _whiteCirclesOnScene;
        public IReadOnlyList<BlackCircleController> BlackCirclesOnScene => _blackCirclesOnScene;

        public void Add(ACircleController circleController)
        {
            switch (circleController)
            {
                case null:
                    throw new NullReferenceException();
                case WhiteCircleController whiteCircle:
                    _whiteCirclesOnScene.Add(whiteCircle);
                    break;
                case BlackCircleController blackCircle:
                    _blackCirclesOnScene.Add(blackCircle);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(circleController));
            }
        }

        public void Clear(CircleType circleType)
        {
            switch (circleType)
            {
                case CircleType.White:
                    _whiteCirclesOnScene.Clear();
                    break;
                case CircleType.Black:
                    _blackCirclesOnScene.Clear();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(circleType), circleType, null);
            }
        }
    }
}