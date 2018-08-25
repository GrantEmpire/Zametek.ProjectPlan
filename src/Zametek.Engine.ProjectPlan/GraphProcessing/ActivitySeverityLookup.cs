﻿using System;
using System.Collections.Generic;
using System.Linq;
using Zametek.Common.Project;

namespace Zametek.Engine.ProjectPlan
{
    public class ActivitySeverityLookup
    {
        #region Fields

        private readonly IList<ActivitySeverityDto> m_ActivitySeverityDtos;

        #endregion

        #region Ctors

        public ActivitySeverityLookup(IEnumerable<ActivitySeverityDto> activitySeverityDtos)
        {
            if (activitySeverityDtos == null)
            {
                throw new ArgumentNullException(nameof(activitySeverityDtos));
            }
            m_ActivitySeverityDtos = activitySeverityDtos.OrderBy(x => x.SlackLimit).ToList();
        }

        #endregion

        #region Public Methods

        public double FindSlackCriticalityWeight(int? totalSlack)
        {
            if (!totalSlack.HasValue)
            {
                return 1.0;
            }
            int totalSlackValue = totalSlack.GetValueOrDefault();
            foreach (ActivitySeverityDto activitySeverityDto in m_ActivitySeverityDtos)
            {
                if (totalSlackValue <= activitySeverityDto.SlackLimit)
                {
                    return activitySeverityDto.CriticalityWeight;
                }
            }
            return 1.0;
        }

        public double CriticalCriticalityWeight()
        {
            return m_ActivitySeverityDtos.Aggregate((i1, i2) => i1.SlackLimit < i2.SlackLimit ? i1 : i2).CriticalityWeight;
        }

        public double FindSlackFibonacciWeight(int? totalSlack)
        {
            if (!totalSlack.HasValue)
            {
                return 1.0;
            }
            int totalSlackValue = totalSlack.GetValueOrDefault();
            foreach (ActivitySeverityDto activitySeverityDto in m_ActivitySeverityDtos)
            {
                if (totalSlackValue <= activitySeverityDto.SlackLimit)
                {
                    return activitySeverityDto.FibonacciWeight;
                }
            }
            return 1.0;
        }

        public double CriticalFibonacciWeight()
        {
            return m_ActivitySeverityDtos.Aggregate((i1, i2) => i1.SlackLimit < i2.SlackLimit ? i1 : i2).FibonacciWeight;
        }

        #endregion
    }
}
