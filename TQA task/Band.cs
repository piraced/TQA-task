using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TQA_task
{
    internal class Band
    {
        string adjective;
        string noun;
        BandMember[] bandMember;
        int bandMemberCount;


        public Band()
        {
            this.adjective = "";
            this.noun = "";
            this.bandMemberCount = 0; //Im using this since bandMember.Length returns 4 as that what is initilized to
                                      //while I need to check how many many elements of the array are populated. Im probably missing an easier way

            this.bandMember = new BandMember[4];
        }

        public void AddBandMember(string name)
        {
            bandMember[bandMemberCount] = new BandMember(bandMemberCount, name);
            bandMemberCount++;
        }

        // returns true if the given name matches the name of a band member
        public bool IsInBand(string name)
        {
            for(int i = 0; i < bandMemberCount; i++)
            {
                if(bandMember[i].GetName() == name)
                {
                    return true;
                }
            }
            return false;
        }

        public void PrintBandInfo()
        {
            Console.WriteLine("band name: " + adjective + " " + noun);
            Console.WriteLine("");
            Console.WriteLine("members:");
            Console.WriteLine("");
            Console.WriteLine("vocalist:           " + bandMember[0].GetName());
            Console.WriteLine("drummer:            " + bandMember[1].GetName());
            Console.WriteLine("bass guitarist:     " + bandMember[2].GetName());
            Console.WriteLine("electric guitarist: " + bandMember[3].GetName());

        }

        public void SetAdjective(string adjective) { this.adjective = adjective; }
        public void SetNoun(string noun) { this.noun = noun; }

    }
}
