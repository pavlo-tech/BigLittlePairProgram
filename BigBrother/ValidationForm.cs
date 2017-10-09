using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BigBrother
{

    //for version #2, this will help to validate the correct name inputs
    public partial class ValidationForm : Form
    {
        List<BrotherObject> _Brothers;
        List<PledgeObject> _Pledges;
        int RedCeiling, YellowCeiling;

        public ValidationForm()
        {
            InitializeComponent();

            int RedCeiling = 1;
            int YellowCeiling = 2;
        }

        public List<BrotherObject> Brothers { get => _Brothers;}
        public List<PledgeObject> Pledges { get => _Pledges;}

        public void DiplayValidationPage(List<BrotherObject> Brothers, List<PledgeObject> Pledges)
        {
            ShowDialog();// opens form

            Action SetGridData = new Action(() =>
            {

                //init lists
                this._Brothers = Brothers;
                this._Pledges = Pledges;

                //set table columns & headers
                BrothersDataGridView.ColumnCount = 8;
                BrothersDataGridView.Columns[0].HeaderText = "Brother Name:";
                BrothersDataGridView.Columns[1].HeaderText = "Brother Contact Info:";
                BrothersDataGridView.Columns[2].HeaderText = "Brother Roll Number:";
                BrothersDataGridView.Columns[3].HeaderText = "Little Choice #1:";
                BrothersDataGridView.Columns[4].HeaderText = "Little Choice #2:";
                BrothersDataGridView.Columns[5].HeaderText = "Little Choice #3:";
                BrothersDataGridView.Columns[6].HeaderText = "Little Choice #4:";
                BrothersDataGridView.Columns[7].HeaderText = "Little Choice #5:";

                //add brother info to Data Grid
                foreach (BrotherObject b in this._Brothers)
                {
                    string[] row = new string[8];

                    row[0] = b.FullName;
                    row[1] = b.ContactInfo;
                    row[2] = b.RollNumber + "";

                    //adds little choices to row
                    for (int i = 0; i < b.LittleChoices.Count; i++)
                    {
                        if (b.LittleChoices[i] != null)
                        {
                            row[3 + i] = b.LittleChoices[i];
                        }
                        else
                        {
                            row[3 + i] = "";
                        }
                    }
                }

                //set table columns and headers
                PledgesDataGridView.ColumnCount = 7;
                PledgesDataGridView.Columns[0].HeaderText = "Pledge Name:";
                PledgesDataGridView.Columns[1].HeaderText = "Pledge Contact Info:";
                PledgesDataGridView.Columns[2].HeaderText = "Big Choice #1:";
                PledgesDataGridView.Columns[3].HeaderText = "Big Choice #2:";
                PledgesDataGridView.Columns[4].HeaderText = "Big Choice #3:";
                PledgesDataGridView.Columns[5].HeaderText = "Big Choice #4:";
                PledgesDataGridView.Columns[6].HeaderText = "Big Choice #5:";

                //add pledge info to grid
                foreach (PledgeObject p in this._Pledges)
                {
                    string[] row = new string[7];
                    row[0] = p.FullName;
                    row[1] = p.ContactInfo;

                    //adds big choices to row
                    for (int i = 0; i < p.BigChoices.Count; i++)
                    {
                        if (p.BigChoices[i] != null)
                        {
                            row[2 + i] = p.BigChoices[i];
                        }
                        else
                        {
                            row[2 + i] = "";
                        }
                    }
                }
            });

            if (InvokeRequired)
            {
                Invoke(SetGridData);
            }
            else
            {
                SetGridData();
            }
        }

        private void FinalizeEditsButton_Click(object sender, EventArgs e)
        {
            Action SaveChanges = new Action(() =>
            {
                _Brothers = new List<BrotherObject>();
                foreach (DataGridViewRow brotherRow in BrothersDataGridView.Rows)
                {
                    _Brothers.Add(new BrotherObject(brotherRow));
                }

                _Pledges = new List<PledgeObject>();
                foreach (DataGridViewRow pledgeRow in PledgesDataGridView.Rows)
                {
                    _Pledges.Add(new PledgeObject(pledgeRow));
                }
            });

            if (InvokeRequired)
            {
                Invoke(SaveChanges);
            }
            else
            {
                SaveChanges();
            }
        }

        private void RunValidationButton_Click(object sender, EventArgs e)
        {
            Action HighlightOutliers = new Action(() =>
            {
                //counts occurances of each brother name in pledge grid
                foreach (DataGridViewRow brother in BrothersDataGridView.Rows)
                {
                    int count = 0;
                    foreach (DataGridViewRow pledgeRow in PledgesDataGridView.Rows)
                    {
                        foreach (DataGridViewCell cell in pledgeRow.Cells)
                        {
                            if ((string)cell.Value == (string)brother.Cells[0].Value)
                            {
                                count++;
                            }
                        }
                    }
                    //if there are less occurances than the yellow threshold, we paint the box
                    if (count <= YellowCeiling)
                    {
                        if (count <= RedCeiling)//if there are less occurances than the yellow threshold, we paint the box
                        {
                            brother.Cells[0].Style.BackColor = Color.Red;
                        }
                        else
                        {
                            brother.Cells[0].Style.BackColor = Color.Yellow;
                        }
                    }
                }


                //counts occurances of each pledge name in brother grid
                foreach (DataGridViewRow pledge in PledgesDataGridView.Rows)
                {
                    int count = 0;
                    foreach (DataGridViewRow brotherRow in BrothersDataGridView.Rows)
                    {
                        foreach (DataGridViewCell cell in brotherRow.Cells)
                        {
                            if ((string)cell.Value == (string)pledge.Cells[0].Value)
                            {
                                count++;
                            }
                        }
                    }
                    //if there are less occurances than the yellow threshold, we paint the box
                    if (count <= YellowCeiling)
                    {
                        if (count <= RedCeiling)//if there are less occurances than the red threshold, we paint the box red
                        {
                            pledge.Cells[0].Style.BackColor = Color.Red;
                        }
                        else
                        {
                            pledge.Cells[0].Style.BackColor = Color.Yellow;
                        }
                    }
                }

                //sees if a pledge put down a brother that is not in the list
                foreach (DataGridViewRow LittleChoices in BrothersDataGridView.Rows)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        int count = 0;

                        foreach (DataGridViewRow pledge in PledgesDataGridView.Rows)
                        {
                            if ((string)pledge.Cells[0].Value == (string)LittleChoices.Cells[3 + i].Value)
                            {
                                count++;
                            }
                        }

                        if (count == 0)
                        {
                            LittleChoices.Cells[3 + i].Style.BackColor = Color.Red;
                        }
                    }
                }

                //sees if a brother put down a pledge that is not in pledges grid
                foreach (DataGridViewRow BigChoices in PledgesDataGridView.Rows)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        int count = 0;

                        foreach (DataGridViewRow brother in BrothersDataGridView.Rows)
                        {
                            if ((string)brother.Cells[0].Value == (string)BigChoices.Cells[2 + i].Value)
                            {
                                count++;
                            }
                        }

                        if (count == 0)
                        {
                            BigChoices.Cells[2 + i].Style.BackColor = Color.Red;
                        }
                    }
                }
            });

            if (InvokeRequired)
            {
                Invoke(HighlightOutliers);
            }
            else
            {
                HighlightOutliers();
            }
        }
    }
}
