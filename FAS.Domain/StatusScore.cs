﻿using FAS.Core;
using System.Collections.Generic;

namespace FAS.Domain
{
    public class StatusScore : AppEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Score> Scores { get; set; }
    }
}
