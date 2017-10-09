using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;
using System.Threading;
using System.IO;

namespace BigBrother
{
    public partial class MainForm : Form
    {

        List<PledgeObject> Pledges;
        List<BrotherObject> Brothers;
        List<Match> Unmatched, Matched, LastYear, BFMatch;

        string prepath;

        public MainForm()
        {

            InitializeComponent();

            prepath = @"C:\Users\Pavlo\Desktop\BigL_ittle\";

            PledgeCSVTextBox.Text = prepath;
            BrotherCSVTextBox.Text = prepath;

            GenerateButton.Click += (o, e) =>
            {
                Pledges = new List<PledgeObject>();
                Brothers = new List<BrotherObject>();
                Matched = new List<Match>();
                Unmatched = new List<Match>();


                try
                {
                    ProcessPledgesCSV(PledgeCSVTextBox.Text);
                    ProcessBrothersCSV(BrotherCSVTextBox.Text);

                    GenerateUnmatchedList();
                    GenerateMatchedList();
                    //ForceMinimumSocialRank();
                    //GenerateLastYearsValues();
                    //DisplayMatches(LastYear, dataGridView2);
                    DisplayMatches(/*Matched, dataGridView1*/);
                }
                catch (Exception ex)
                {
                    if (ex.GetType() == typeof(IndexOutOfRangeException))
                    {
                        MessageBox.Show("Either I didnt run enough tests, or you forgot to tag the correct column headers in the csvs!"+
                            " Remember: name = \"FN\", Roll number = \"ROLL\", choice 1 = \"CH1\", and choice 5 = \"CH5\"");
                    }
                }

            };

        }

        void ProcessPledgesCSV(string pathToPledgeCSV)
        {
            TextFieldParser CSVReader = new TextFieldParser(
                //prepath + "Pledges.csv");
                pathToPledgeCSV);

            //we separate strings by commas
            CSVReader.TextFieldType = FieldType.Delimited;
            CSVReader.SetDelimiters(",");

            List<string> headers = new List<string>(CSVReader.ReadFields());
            while (!CSVReader.EndOfData)
            {
                //each row is assumed to be the data for a pledge
                Pledges.Add( new PledgeObject( CSVReader.ReadFields(), headers ) );
            }
        }

        void ProcessBrothersCSV(string pathToBrotherCSV)
        {
            TextFieldParser CSVReader = new TextFieldParser(//prepath + "Brothers.csv");
                pathToBrotherCSV);

            //we separate strings by commas
            CSVReader.TextFieldType = FieldType.Delimited;
            CSVReader.SetDelimiters(",");

            List<string> headers = new List<string>(CSVReader.ReadFields());
            while (!CSVReader.EndOfData)
            {
                //each row is assumed to be the data for a pledge
                Brothers.Add( new BrotherObject( CSVReader.ReadFields(), headers ) );
            }
        }

        void GenerateUnmatchedList()
        {
            Unmatched = new List<Match>();

            foreach (PledgeObject pledge in Pledges)
            {
                foreach (string BigName in pledge.BigChoices)
                {
                    BrotherObject BigChoice = Brothers.Find( big => big.FullName == BigName );

                    if (BigChoice != null)
                    {
                        Unmatched.Add(new Match(pledge, BigChoice));
                    }
                }
            }

            foreach (BrotherObject brother in Brothers)
            {
                foreach (string LittleName in brother.LittleChoices)
                {
                    PledgeObject LittleChoice = Pledges.Find(little => little.FullName == LittleName);

                    if (LittleChoice != null)
                    {
                        Match m = new Match(brother, LittleChoice);
                        if (Unmatched.Find(mat => mat.Brother.FullName == brother.FullName && mat.Pledge.FullName == LittleChoice.FullName) == null)
                        {
                            Unmatched.Add(m);
                        }
                    }
                }
            }
        }

        void GenerateMatchedList()
        {
            Unmatched.Sort();
            Matched = new List<Match>();

            while (Unmatched.Count > 0)
            {
                Match CurrentMatch = Unmatched[0];

                Matched.Add( Unmatched[0]);

                Unmatched.RemoveAll(
                    match => match.Pledge.FullName == CurrentMatch.Pledge.FullName 
                        || match.Brother.FullName == CurrentMatch.Brother.FullName 
                );
            }

            //add unmatched pledges to match w/ Tag UNMATCHED
            foreach (PledgeObject p in Pledges)
            {
                //if the pledge isnt in Matched, we add it as an UNMATCHED pledge
                if (Matched.Find(match => match.Pledge.FullName == p.FullName) == null)
                {
                    Matched.Add(Match.Unmatched(p));
                }
            }
        }

        /// <summary>
        /// shows finalized matches in a data grid
        /// </summary>
        /// <param name="MatchList"></param>
        /// <param name="dataGridView1"></param>
        void DisplayMatches(/*List<Match> MatchList, DataGridView dataGridView1*/)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.ColumnCount = 3;
            dataGridView1.Columns[0].HeaderText = "Big:";
            dataGridView1.Columns[1].HeaderText = "Little:";
            dataGridView1.Columns[2].HeaderText = "Match Rank:";

            int AvgRank = 0;
            foreach (Match m in Matched)
            {
                AvgRank += m.matchRating;

                string[] row ={ m.Brother.FullName, m.Pledge.FullName, "" + m.matchRating };
                dataGridView1.Rows.Add(row);
            }
            //AvgRank /= MatchList.Count;
            string[] AverageRank = { "", "Average Ranking: ", "" + ((double)AvgRank)/((double)Matched.Count)};
            dataGridView1.Rows.Insert(0, AverageRank);

            //write list of unmatched brothers to datagrid2
            dataGridView2.Columns.Clear();
            dataGridView2.ColumnCount = 6;
            dataGridView2.Columns[0].HeaderText = "Remaining Brothers:";
            dataGridView2.Columns[1].HeaderText = "Choice 1: ";
            dataGridView2.Columns[2].HeaderText = "Choice 2: ";
            dataGridView2.Columns[3].HeaderText = "Choice 3: ";
            dataGridView2.Columns[4].HeaderText = "Choice 4: ";
            dataGridView2.Columns[5].HeaderText = "Choice 5: ";
            foreach (BrotherObject b in Brothers)
            {
                if (Matched.Exists(m => m.Brother.FullName == b.FullName) == false)
                {
                    string[] row = new string[6];
                    row[0] = b.FullName;
                    for (int i = 0; i < 5; i++)
                    {
                        row[i + 1] = (b.LittleChoices[i] != null) ? b.LittleChoices[i] : "";
                    }
                    dataGridView2.Rows.Add(row);
                }
            }
        }

        void WriteOutputToFile(List<Match> matchList, double average)
        {
            string line;
            double max = 10;

            string path = Directory.GetCurrentDirectory() + @"\BigBrotherOutput.txt";
            StreamReader reader = new StreamReader(path);

            if ((line = reader.ReadLine()) != null)
            {
                max = double.Parse(line);
            }
            reader.Close();

            if (max > average)
            {
                StringBuilder toWrite = new StringBuilder();
                toWrite.Append(average.ToString());

                toWrite.Append("\r\nLittle\tBig\tRating");
                foreach (Match m in matchList)
                {
                    toWrite.Append("\r\n");
                    toWrite.Append(m.Pledge.FullName);
                    toWrite.Append("\t");
                    toWrite.Append(m.Brother.FullName);
                    toWrite.Append("\t");
                    toWrite.Append(m.matchRating);
                }
                File.WriteAllText(path, toWrite.ToString());
            }
        }

        /// <summary>
        /// Everthing below this line is experimental stuff that either doesnt work, or doesnt work efficiently enough to use,
        /// feel free to play around with it, if you can come up with an algorithm to get an overall best average, let me know
        /// </summary>



        /// IDEA (new brute force implementation):
        /// Recursive threads with locked "best try" list and average. 
        /// to eliminate non-promising paths, we can compute a "predicted average" including
        /// what we have thus far as well as an average of best choice matches 
        /// for each remaining pledge. If that "predicted average" is worse 
        /// than our current "best try", we return a bad MatchList. We can limit the execution 
        /// time further by seeding it with the results determined from the algorithm used above
        void ForceMinimumSocialRank()
        {
            Matched = new List<Match>();
            //Unmatched.Sort();
            //we automatically match all perfect (0 matchRank) matches
            //while (Unmatched.Find(perfMatch => perfMatch.matchRating == 0) != null)
            //{

            //    Match toAdd = Unmatched.Find(perfMatch => perfMatch.matchRating == 0);
            //    Matched.Add(toAdd);
            //    Unmatched.RemoveAll(
            //        match =>
            //            match.Pledge.FullName == toAdd.Pledge.FullName
            //            ||
            //            match.Brother.FullName == toAdd.Brother.FullName
            //        );
            //}

            //while (Unmatched.Count > 0)
            //{
            //    Match toAdd = WorstBestMatch();
            //    if (toAdd.Brother == null) return;
            //    Matched.Add(toAdd);

            //    Unmatched.RemoveAll(
            //        match => match.Pledge.FullName == toAdd.Pledge.FullName
            //            || match.Brother.FullName == toAdd.Brother.FullName
            //    );
            //}

            
            //an attempt to speed things up by matching the worst ranked pledges first, i do not reccomend this
            for (int i = 0; i < 3; i++)
            {
                Match m = WorstBestMatch();
                Matched.Add(m);
                Unmatched.RemoveAll(
                    match =>
                        match.Pledge.FullName == m.Pledge.FullName
                        ||
                        match.Brother.FullName == m.Brother.FullName
                    );
            }
            Matched.Sort();
            Matched = new List<Match>( RecursiveBruteForce(Matched, Unmatched));
            //Console.WriteLine("Best Average: " + BestAverageRank);
        }

        List<Match> RecursiveBruteForce(List<Match> MatchedList, List<Match> UnMatchedList)
        {
            if (UnMatchedList.Count == 0)//base case
            {
                //return AverageMatchRank(MatchedList);
                return MatchedList;
            }
            else
            {
                int WorstMatch = WorstBestRank(UnMatchedList);

                double BestAverageRank = 10;
                List<Match> AllTimeBest = new List<Match>();

//                List<Thread> taskList = new List<Thread>();
                foreach (Match m in UnMatchedList)
                {
//                    taskList.Add(new Thread(() =>{

                        //if (m.matchRating <= WorstMatch){
                        //create a copy of unmatched list
                        List<Match> recurUnMatch = new List<Match>();
                        foreach (Match rm in UnMatchedList) recurUnMatch.Add(rm);

                        //create copy of matched list
                        List<Match> recurMatch = new List<Match>();
                        foreach (Match rm in MatchedList) recurMatch.Add(rm);

                        recurMatch.Add(m);

                        recurUnMatch.RemoveAll(
                            match =>
                                match.Pledge.FullName == m.Pledge.FullName
                                ||
                                match.Brother.FullName == m.Brother.FullName
                            );
                        List<Match> tempBest = RecursiveBruteForce(recurMatch, recurUnMatch);

                        double avg = AverageMatchRank(tempBest);
                        lock (this)// i dont know if this is correct locking
                        {
                            if (avg < BestAverageRank && tempBest.Count == Pledges.Count)
                            {
                                BestAverageRank = avg;
                                AllTimeBest = new List<Match>(tempBest);
                                WriteOutputToFile(AllTimeBest, BestAverageRank);
                            }
                        }
                  //  }));
                }
                //foreach (Thread t in taskList)
                //{
                //    t.IsBackground = true;
                //    t.Start();
                //}
                //foreach (Thread t in taskList)
                //{
                //    if (t != null && t.IsAlive)
                //    {
                //        t.Join();
                //    }
                //}
                return AllTimeBest;
            }
        }
        double AverageMatchRank(List<Match> MatchList)
        {
            double average = 0;
            foreach (Match m in MatchList)
            {
                average += m.matchRating;
            }
            return average /= MatchList.Count;
        }



        /// <summary>
        /// find's the best possible match ranking for the pledge with the worst match ranking
        /// the idea is that this should be our worst possible match value
        /// </summary>
        /// <returns></returns>
        int WorstBestRank(List<Match> Unmatched)
        {
            int OverallWorstRank = 0;
            foreach (PledgeObject p in Pledges)
            {

                //find best rank for each remaining pledge
                if (Unmatched.Find(pledge => pledge.Pledge.FullName == p.FullName) != null)
                {
                    int PledgesBestRank = 10;
                    foreach (Match m in Unmatched)
                    {
                        if (m.Pledge.FullName == p.FullName) //find pledge in unmatched
                        {
                            if (PledgesBestRank > m.matchRating) //if the current rank is better, 
                            {
                                PledgesBestRank = m.matchRating;
                            }
                        }
                    }

                    //the worst rank cannot be closer to 0 than any pledge's best
                    if (OverallWorstRank < PledgesBestRank)
                    {
                        OverallWorstRank = PledgesBestRank;
                    }
                }
            }
            return OverallWorstRank;
        }

        Match WorstBestMatch()
        {
            Match OverallWorstBestMatch = new Match(false);
            foreach (PledgeObject p in Pledges)
            {

                //find best rank for each remaining pledge
                if (Unmatched.Find(match => match.Pledge.FullName == p.FullName) != null)
                {
                    Match PledgesBestMatch = new Match(true);
                    foreach (Match m in Unmatched)
                    {
                        if (m.Pledge.FullName == p.FullName)
                        {
                            if (PledgesBestMatch.matchRating > m.matchRating)
                            {
                                PledgesBestMatch = m;
                            }
                        }
                    }

                    //the worst best match rank cannot be closer to 0 than any pledge's best
                    if (OverallWorstBestMatch.matchRating < PledgesBestMatch.matchRating)
                    {
                        OverallWorstBestMatch = PledgesBestMatch;
                    }
                }
            }
            return OverallWorstBestMatch;
        }


            void GenerateLastYearsValues()
            {
            LastYear = new List<Match>();
            TextFieldParser CSVReader = new TextFieldParser(prepath + "LastYear.csv");

            //we separate strings by commas
            CSVReader.TextFieldType = FieldType.Delimited;
            CSVReader.SetDelimiters(",");

            PledgeObject p;
            BrotherObject b;
            string[] currLine;

            //CSVReader.ReadFields();
            while (!CSVReader.EndOfData)
            {
                currLine = CSVReader.ReadFields();
                p = Pledges.Find(pledge => pledge.FullName == currLine[0].Replace(" ", "").ToUpper());
                b = Brothers.Find(brother => brother.FullName == currLine[1].Replace(" ", "").ToUpper());
                if(p!= null && b != null)
                LastYear.Add(new Match(p, b));
            }
        }
    }
}


//remove all repeats where
//Unmatched.RemoveAll
//(
//    m => m.matchRating > 3 && 
//    (
//        Unmatched.FindAll
//            (
//                mat => mat.Pledge.FullName == m.Pledge.FullName
//            ).Count > 1
//    )
//);
//BFMatch = new List<Match>();

//for (int i = 0; i < Unmatched.Count; i++)
//{

//}


//List<Match> 

//double AverageRating(List<Match> matches)
//{
//    int values = 0;
//    foreach (Match m in matches)
//    {
//        values += m.matchRating;
//    }
//    return ((double)values) / ((double)matches.Count);
//}