using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRAP_TEST_GRAPHIQUE
{
        class Profile
        {
            private int id;
            private string type;
            private string name;
            protected Dictionary<string, int> skills;

            public Profile(int id, string type, string name, Dictionary<string, int> skills)
            {
                this.id = id;
                this.type = type;
                this.name = name;
                this.skills = skills;
            }

            // Access attributes
            public int Id { get { return id; } }
            public string Type { get { return type; } }
            public string Name { get { return name; } }
            public Dictionary<string, int> Skills { get { return skills; } }



        }
    }
