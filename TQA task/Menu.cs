using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TQA_task.FileIO;

namespace TQA_task
{
    internal static class Menu
    {
        //checks to see if the program has enough data to make a band
        public static void BootProgram(string fileName)
        {
            string[][] data = ReadData(fileName);
            if ((data[0] == null) || (data[1] == null) || (data[2] == null) || (data[2].Length < 4))
            {
                FillMinimumData(fileName);
            }
            MainMenu(fileName);
        }

        //the main menu
        public static void MainMenu(string fileName)
        {
            // arbitrary value so input would be assigned
            char input = 'q';

            Band? band = null;

            while (input != 'x')
            {
                Console.Clear();
                Console.WriteLine("1. create/generate band");
                Console.WriteLine("2. display the count of adjectives, nouns and names available");
                Console.WriteLine("3. display the last created/generated band");
                Console.WriteLine("4. add new adjectives, nouns and/or names");
                Console.WriteLine("x. close the program");

                input = Console.ReadKey().KeyChar;

                switch (input)
                {
                    case '1':
                        band = CreateBand(fileName);
                        break;
                    case '2':
                        DisplayCount(fileName);
                        break;
                    case '3':
                        if(band != null)
                        {
                            Console.Clear();
                            band.PrintBandInfo();
                            Console.WriteLine("");
                            Console.WriteLine("press any key to return to the main menu");
                            Console.ReadKey();
                        }
                        break;
                    case '4':
                        AddNewData(fileName);
                        break;
                    case 'x':
                        Environment.Exit(0);
                        break;

                }
            }
            
        }

        // displays the amount of adjectives, nouns and names in the input file
        private static void DisplayCount(string fileName)
        {
            string[][] data = ReadData(fileName);

                Console.Clear();
                Console.WriteLine("adjectives: " + data[0].Length);
                Console.WriteLine("nouns:      " + data[1].Length);
                Console.WriteLine("names:      " + data[2].Length);
                Console.WriteLine("");
                Console.WriteLine("press any key to return to main menu");

            Console.ReadKey();
        }

        //menu for both randomly and manually generating a band
        private static Band? CreateBand(string fileName)
        {
            // arbitrary value so input would be assigned
            char input = 'q';

            string bandInfo = ""; // holds 1 band variable until it is properly added to the band object at the end of each loop
            string variableText = "";
            int row = 0; // which row of input file will be used 0 - adjectives, 1 - nouns, 2 - names
            Random random = new Random();
            Band band = new Band();

            string[][] data = ReadData(fileName);

            //1 loop for each variable in the band:
            //0 - adjective, 1 - noun, 2-5 band members names
            for (int i = 0; i < 6; i++)
            {

                switch (i)
                {
                    case 0:
                        variableText = "adjective";
                        row = 0;
                        break;
                    case 1:
                        variableText = "noun";
                        row = 1;
                        break;
                    case 2:
                        variableText = "vocalists name";
                        row = 2;
                        break;
                    case 3:
                        variableText = "drummers name";
                        row = 2;
                        break;
                    case 4:
                        variableText = "bass guitarists name";
                        row = 2;
                        break;
                    case 5:
                        variableText = "electric guitarists name";
                        row = 2;
                        break;
                }

                //keep reprinting this bit of menu if the bands variable was not selected
                //( wrong input or the user went back to this menu instead of selecting an option in SelectChoice )
                while (bandInfo == "")
                {
                    Console.Clear();
                    Console.WriteLine("1. manually select the bands " + variableText);
                    Console.WriteLine("2. randomly select the bands " + variableText);
                    Console.WriteLine("x. back to main menu");

                    input = Console.ReadKey().KeyChar;

                    switch (input)
                    {
                        case '1':
                            bandInfo = SelectChoice(data, row, band);
                            break;
                        case '2':
                            bandInfo = SelectRandom(data, row, band, random);
                            break;
                        case 'x':
                            return null;
                    }
                }

                if (i == 0)
                {
                    band.SetAdjective(bandInfo);
                }
                else if(i == 1)
                {
                    band.SetNoun(bandInfo);
                }
                else if(i > 1)
                {
                    band.AddBandMember(bandInfo);
                }
                bandInfo = "";
            }
            // a repeat of the menu option "display the last created/generated band"
            Console.Clear ();
            band.PrintBandInfo();
            Console.WriteLine("");
            Console.WriteLine("press any key to return to the main menu");
            Console.ReadKey();

            return band;
        }

        // randomly chooses an option for band generation
        private static string SelectRandom(string[][] data, int row, Band band, Random random)
        {
            // arbitrary value so input would be assigned
            char input = 'q';
            string variableText;
            bool loop = true;
            if(row == 0)
            {
                variableText = "adjective";
            }
            else if (row == 1)
            {
                variableText = "noun";
            }
            else
            {
                variableText = "name";
            }

            while (true)
            {
                string bandInfo = data[row][random.Next(0, data[row].Length)];
                //if generating a name keep randomly selecting a new name until it is not a duplicate
                if (row == 2)
                {
                    while (band.IsInBand(bandInfo))
                    {
                        bandInfo = data[row][random.Next(0, data[row].Length)];
                    }
                }

                Console.Clear();
                Console.WriteLine("generated " + variableText + ": " + bandInfo);
                Console.WriteLine("");
                Console.WriteLine("1. keep this " + variableText);
                Console.WriteLine("press any other key to generate a different " + variableText);
                input = Console.ReadKey().KeyChar;

                switch (input)
                {
                    case '1':
                        return bandInfo;
                    case '2':
                        break;
                }
            }
        }
        //lists available options for a part of band generation and lets the user choose one them
        private static string SelectChoice(string[][] data, int row, Band band)
        {
            string input = "";

            while (input != "x")
            {
                Console.Clear();
                Console.WriteLine("   available choices:");
                for(int i = 1; i <= data[row].Length; i++)
                {
                    //when selecting band member names skip the names already selected for other roles
                    while((row == 2) && (i <= data[row].Length) && (band.IsInBand(data[row][i - 1])))
                    {
                        i++;
                    }
                    // skip writing if iterator has gone above the max
                    if(i <= data[row].Length)
                    {
                        Console.WriteLine(i + ". " + data[row][i - 1]);
                    }
                }
                Console.WriteLine("x. back to previous menu");

                input = Console.ReadLine();

                if(input == "x") { return ""; }
                if(int.TryParse(input, out int answer) && (answer > 0) && (answer <= data[row].Length) && (!band.IsInBand(data[row][answer - 1])))
                {
                    return data[row][answer-1];
                }
            }

            return "";
        }

        //a menu for adding new data to the input file
        private static void AddNewData(string fileName)
        {
            // arbitrary value so input would be assigned
            char input = 'q';

            string inputString = "";
            string[][] data = ReadData(fileName);

            while (input != 'x')
            {
                Console.Clear();
                Console.WriteLine("1. add an adjective");
                Console.WriteLine("2. add a noun");
                Console.WriteLine("3. add a name");
                Console.WriteLine("x. back to main menu");

                input = Console.ReadKey().KeyChar;

                switch (input)
                {
                    case '1':
                        Console.Clear();
                        Console.WriteLine("enter the new adjective");
                        inputString = Console.ReadLine().Replace(",", "").Trim();
                        if (!data[0].Contains(inputString))
                        {
                            AppendData(0, inputString, fileName);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("given adjective already exists in the data");
                            Console.WriteLine("press any key to continue");
                            Console.ReadKey();
                        }
                        break;
                    case '2':
                        Console.Clear();
                        Console.WriteLine("enter the new noun");
                        inputString = Console.ReadLine().Replace(",", "").Trim();
                        if (!data[1].Contains(inputString))
                        {
                            AppendData(1, inputString, fileName);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("given noun already exists in the data");
                            Console.WriteLine("press any key to continue");
                            Console.ReadKey();
                        }
                        break;
                    case '3':
                        Console.Clear();
                        Console.WriteLine("enter the new name");
                        inputString = Console.ReadLine().Replace(",", "").Trim();
                        if (!data[2].Contains(inputString))
                        {
                            AppendData(2, inputString, fileName);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("given name already exists in the data");
                            Console.WriteLine("press any key to continue");
                            Console.ReadKey();
                        }
                        break;
                    case 'x':
                        break;

                }
            }
        }
        // method that asks the user to fill in the minimum amount of data to generate the band and saves that data to the input file
        private static void FillMinimumData(string fileName)
        {    
            string temp ;
            string[][] data = ReadData(fileName);

            Console.Clear();
            Console.WriteLine("data.csv file is missing or doesn't contain enough information to create a band");
            Console.WriteLine("please add the following information:");

            // if there are no adjectives
            if (data[0] == null)
            {
                Console.WriteLine("an adjective:");
                temp = Console.ReadLine().Replace(",", "").Trim();
                AppendData(0, temp, fileName);
            }
            //if there are no nouns
            if (data[1] == null)
            {
                Console.WriteLine("a noun:");
                temp = Console.ReadLine().Replace(",", "").Trim();
                AppendData(1, temp, fileName);
                }
            // if there are no/not enough names (less than 4)
            if ((data[2] == null) || (data[2].Length < 4))
            {
                int namesNeeded;
                if (data[2] == null) { namesNeeded = 4; } 
                else namesNeeded = 4 - data[2].GetLength(0); 
                Console.WriteLine(namesNeeded + " full names:");
                for(int i = 1; i <= namesNeeded; i++)
                {
                    data = ReadData(fileName);

                    Console.WriteLine("Name " + i + ":");
                    temp = Console.ReadLine().Replace(",", "").Trim();
                    //check to see if an entered name is a duplicate of an existing one
                    if ((data[2] == null) || (!data[2].Contains(temp)))
                    {
                        AppendData(2, temp, fileName);
                    }
                    else
                    {
                        Console.WriteLine("entered name already exists");
                        i--;
                    }
                    }
                }
        }

    }
}
