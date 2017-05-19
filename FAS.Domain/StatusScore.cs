using FAS.Core;
using System.Collections.Generic;

namespace FAS.Domain
{
    public class StatusScore : AppEntity
    {
        public StatusScore()
        {
            Scores = new HashSet<Score>();
        }
        public StatusScore(string Name)
        {
            this.Name = Name;
        }
        public string Name { get; set; }
        public virtual ICollection<Score> Scores { get; set; }
    }
}
