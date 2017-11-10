using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAP
{
    class Profile
    {
        private int id;
        private string type;
        private string name;
        private Dictionary<string,int> skills;

        public Profile(string type, string name, Dictionary<string, int> skills)
        {
            id = 1;
            this.type = type;
            this.name = name;
            this.skills = skills;
        }

        // Access attributes
        public int Id { get { return id; } }
        public string Type { get { return type; } }
        public string Name { get { return name; } }
        public Dictionary<string, int> Skills { get { return skills; } }

        public void AddSkillValue(string skill, int value)
        {

        }


    }
}
