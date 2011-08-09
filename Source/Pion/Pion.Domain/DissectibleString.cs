using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pion.Domain
{
    internal sealed class DissectibleString
    {
        StringMarker _currentMarking;
        StringMarker _endMarking;
        readonly string _originalValue;
        StringMarker _startMarking;

        const int MARKER_NOT_FOUND = -1;

        public DissectibleString(string originalValue)
        {
            _originalValue = originalValue;

            _currentMarking = new StringMarker(0, string.Empty);
        }

        public string GetMarkedRange()
        {
            string rangeIncludingMarkings = _originalValue.Substring(_startMarking.Position, _endMarking.Position - _startMarking.Position + _endMarking.Length);

            SlicableString slicey = new SlicableString(rangeIncludingMarkings);
            string rangeWithoutMarkings = slicey.Slice(_startMarking.Value, _endMarking.Value);

            return rangeWithoutMarkings;
        }

        public void MoveBackwardsTo(string marking)
        {
            string valueBeforeCurrentPosition = _originalValue.Substring(0, _currentMarking.Position);

            int markerPosition = valueBeforeCurrentPosition.LastIndexOf(marking);

            if (markerPosition == MARKER_NOT_FOUND)
            {
                throw new StringMarkingNotFoundException(marking);
            }

            _currentMarking = new StringMarker(markerPosition, marking);
        }

        public void MoveForwardTo(string marking)
        {
            string valueAfterCurrentPosition = _originalValue.Substring(_currentMarking.Position + _currentMarking.Length);

            int markerPosition = valueAfterCurrentPosition.IndexOf(marking);

            if (markerPosition == MARKER_NOT_FOUND)
            {
                throw new StringMarkingNotFoundException(marking);
            }

            _currentMarking = new StringMarker(markerPosition + _currentMarking.Position + _currentMarking.Length, marking);
        }

        public void SetEndMarker()
        {
            _endMarking = _currentMarking;
        }

        public void SetStartMarker()
        {
            _startMarking = _currentMarking;
        }
    }
}
