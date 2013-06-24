using System;
using System.Collections.Generic;
using System.Text;

namespace hammergo.Tracking
{
    public interface ITrackable
    {
        TrackingInfo TrackingState { get; set; }

    }
}
