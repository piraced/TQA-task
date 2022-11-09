using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TQA_task
{
    internal static class FileIO
    {

        public static string[][] ReadData( string fileName)
        {
            string[][] data = new string[3][];
            if (Microsoft.VisualBasic.FileIO.FileSystem.FileExists(fileName))
            {
                // honestlly, this is mainly code I found on stackoverflow when looking for an easy way to read .csv files
                var parser = new Microsoft.VisualBasic.FileIO.TextFieldParser(fileName);
                parser.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited;
                parser.SetDelimiters(new string[] { "," });


                for(int i = 0; i < 3; i++)
                {
                    data[i] = parser.ReadFields();
                }
            }
            else using (FileStream fs = File.Create(fileName));
            return data;
        }

        //method for adding new variables to the input file
        public static void AppendData(int lineNo, string entry, string fileName)
        {
            string[] fileLine = new string[3];
            fileLine = File.ReadAllLines(fileName);
            if(fileLine.Length == 0)
            {
                fileLine = new string[3];
            }
            //if line is empty simply add the new variable
            if ((fileLine[lineNo] == null) || (fileLine[lineNo] == ""))
            {
                fileLine[lineNo] = entry;
            }
            //otherwise append a comma and the variable to the end
            else fileLine[lineNo] = fileLine[lineNo] + "," + entry;
            File.WriteAllLines(fileName, fileLine);
        }
    }
}
