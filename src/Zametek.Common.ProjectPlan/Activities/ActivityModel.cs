﻿using System;
using System.Collections.Generic;
using Zametek.Maths.Graphs;

namespace Zametek.Common.ProjectPlan
{
    [Serializable]
    public class ActivityModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "DTO property")]
        public List<int> TargetResources { get; set; }

        public LogicalOperator TargetResourceOperator { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "DTO property")]
        public List<int> AllocatedToResources { get; set; }

        public bool CanBeRemoved { get; set; }

        public bool HasNoCost { get; set; }

        public int Duration { get; set; }

        public int? FreeSlack { get; set; }

        public int? TotalSlack { get; set; }

        public int? EarliestStartTime { get; set; }

        public int? LatestStartTime { get; set; }

        public int? EarliestFinishTime { get; set; }

        public int? LatestFinishTime { get; set; }

        public int? MinimumFreeSlack { get; set; }

        public int? MinimumEarliestStartTime { get; set; }

        public DateTime? MinimumEarliestStartDateTime { get; set; }

        public int? MaximumLatestFinishTime { get; set; }

        public DateTime? MaximumLatestFinishDateTime { get; set; }
    }
}
