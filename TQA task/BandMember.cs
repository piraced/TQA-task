using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TQA_task
{
    internal class BandMember
    {
        int role; // 0= Vocalist, 1=Drummer, 2=Bass guitarist, 3=Electric guitarist
        string name;
        public string GetName() { return name; }
        public BandMember(int role, string name) 
        { 
            this.role = role;
            this.name = name; 
        }
    }
}
