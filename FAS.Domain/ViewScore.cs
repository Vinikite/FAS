using FAS.Core;
using System.Collections.Generic;

namespace FAS.Domain
{
    public class ViewScore : AppEntity
    {
        public ViewScore()
        {
            Scores = new HashSet<Score>();
        }

        public string Name { get; set; }
        public virtual ICollection<Score> Scores { get; set; }
    }
}
