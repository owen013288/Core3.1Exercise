using System.Collections.Generic;

namespace CoreExercise.ViewModel
{
    public class AICorpViewModel
    {
        public string Website { get; set; }
        public List<Branch> Branches { get; set; }
    }

    public class Branch
    {
        public string Location { get; set; }
        public string Name { get; set; }
        public string Tel { get; set; }
    }
}
