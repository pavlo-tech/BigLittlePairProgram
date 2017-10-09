using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BigBrother
{
    public class PledgeObject
    {
        public string FullName;
        public string ContactInfo;
        public List<string> BigChoices;

        public PledgeObject(string[] CSVRow, List<string> topRow)
        {
            int NamePos = topRow.FindIndex(s => s.Contains("FN"));
            int Brother0Pos = topRow.FindIndex(s => s.Contains("CH1"));
            int Brother4Pos = topRow.FindIndex(s => s.Contains("CH5"));

            //int NamePos         = 1;
            //int Brother0Pos     = 2;
            //int Brother4Pos     = 6;


            FullName =
                CSVRow[NamePos].Replace(" ", "").ToUpper();

            BigChoices = new List<string>(5);
            for (int i = Brother0Pos; i <= Brother4Pos; i++)
            {
                if (CSVRow[i] != null)
                {
                    BigChoices.Add(CSVRow[i].Replace(" ", "").ToUpper());
                }
            }
        }

        public PledgeObject(DataGridViewRow row)
        {
            FullName = (string)row.Cells[0].Value;
            ContactInfo = (string)row.Cells[1].Value;
            BigChoices.Add(((string)row.Cells[2].Value).Replace(" ","").ToUpper());
            BigChoices.Add(((string)row.Cells[3].Value).Replace(" ", "").ToUpper());
            BigChoices.Add(((string)row.Cells[4].Value).Replace(" ", "").ToUpper());
            BigChoices.Add(((string)row.Cells[5].Value).Replace(" ", "").ToUpper());
            BigChoices.Add(((string)row.Cells[6].Value).Replace(" ", "").ToUpper());
        }
    }

    public class BrotherObject
    {
        public string FullName;
        public int RollNumber;
        public string ContactInfo;
        public List<string> LittleChoices;

        public BrotherObject(string[] CSVRow, List<string> topRow)
        {
            // int NamePos         = 1;
            //int RollNumberPos   = 2;
            //int PhoneNumberPos  = 3;
            //int Pledge0Pos      = 4;
            //int Pledge4Pos      = 8;

            int NamePos = topRow.FindIndex(s => s.Contains("FN"));
            int RollNumberPos = topRow.FindIndex(s => s.Contains("ROLL"));
            int Pledge0Pos = topRow.FindIndex(s => s.Contains("CH1"));
            int Pledge4Pos = topRow.FindIndex(s => s.Contains("CH5"));

            //format: "PAVLOTRIANTAFYLLIDES"
            FullName = CSVRow[NamePos].Replace(" ", "").Replace("'","").ToUpper();

            RollNumber = int.Parse(CSVRow[RollNumberPos].Replace(" ", ""));

            //ContactInfo = CSVRow[PhoneNumberPos].Replace(" ", "");

            LittleChoices = new List<string>(5);
            for (int i = Pledge0Pos; i <= Pledge4Pos; i++)
            {
                if (CSVRow[i] != null)
                {
                    LittleChoices.Add(CSVRow[i].Replace(" ", "").ToUpper());
                }
            }
        }

        public BrotherObject(DataGridViewRow row)
        {
            FullName = (string)row.Cells[0].Value;
            ContactInfo = (string)row.Cells[1].Value;
            RollNumber = int.Parse((string)row.Cells[2].Value);//convert to tryparse
            LittleChoices.Add(((string)row.Cells[3].Value).Replace(" ", "").ToUpper());
            LittleChoices.Add(((string)row.Cells[4].Value).Replace(" ", "").ToUpper());
            LittleChoices.Add(((string)row.Cells[5].Value).Replace(" ", "").ToUpper());
            LittleChoices.Add(((string)row.Cells[6].Value).Replace(" ", "").ToUpper());
            LittleChoices.Add(((string)row.Cells[7].Value).Replace(" ", "").ToUpper());
        }

        public static BrotherObject Unmatched()
        {
            string[] headers = { "date", "FN", "ROLL", "CONTACT", "CH1", "2", "3", "4", "CH5" };
            string[] unmatchedrow   = { "", "UNMATCHED", "69", "911", "pledge0", "pledge1", "pledge2", "pledge3", "pledge4" };

            return new BrotherObject(unmatchedrow, new List<string>(headers));
        }
    }

    public class Match : IComparable
    {
        public PledgeObject Pledge;
        public BrotherObject Brother;
        public int matchRating;

        public Match(PledgeObject p, BrotherObject b)
        {
            Pledge = p;
            Brother = b;

            //we assume that brother b is in pledge p's biglist

            if (Brother.LittleChoices.Contains(Pledge.FullName))
            {
                //if brother put this little on their list, matchrating is the sum of the index of both names
                matchRating = Pledge.BigChoices.IndexOf(Brother.FullName) + Brother.LittleChoices.IndexOf(Pledge.FullName);
            }
            else
            {
                //if brother did not put little on their list, matchrating is the index of 
                matchRating = Pledge.BigChoices.IndexOf(Brother.FullName) + 5;
            }
        }

        public Match(BrotherObject b, PledgeObject p)
        {
            Pledge = p;
            Brother = b;

            

            if (Pledge.BigChoices.Contains(Brother.FullName))
            {
                //if brother put this little on their list, matchrating is the sum of the index of both names
                matchRating = Brother.LittleChoices.IndexOf(Pledge.FullName) + Pledge.BigChoices.IndexOf(Brother.FullName);
            }
            else
            {
                //if brother did not put little on their list, matchrating is the index of 
                matchRating = Brother.LittleChoices.IndexOf(Pledge.FullName) + 5;
            }
        }
        public Match(bool isBest)
        {
            this.Brother = null;
            this.Pledge = null;
            this.matchRating = (isBest)? 10 : 0;
        }


        public int CompareTo(object obj)
        {
            Match other = (Match)obj;

            //if matches are equal and the same brother we give to the brother's higher choice
            if (this.matchRating == other.matchRating && this.Brother.RollNumber == other.Brother.RollNumber)
            {
                return this.Brother.LittleChoices.FindIndex(pledge => pledge == this.Pledge.FullName) - other.Brother.LittleChoices.FindIndex(pledge => pledge == other.Pledge.FullName);

            }
            else if (this.matchRating == other.matchRating)
            {
                //if other is lower roll number, it comes first so (+) is returned
                return  this.Brother.RollNumber - other.Brother.RollNumber;
            }
            else 
            {
                //if other has lower match rating, it comes first so (+) is returned
                return this.matchRating - other.matchRating;
            }
        }

        public Match() { }

        public static Match Unmatched(PledgeObject p)
        {
            Match ToReturn = new Match();
            ToReturn.Pledge = p;
            ToReturn.Brother = BrotherObject.Unmatched();
            ToReturn.matchRating = 10;
            return ToReturn;
        }
    }
}
