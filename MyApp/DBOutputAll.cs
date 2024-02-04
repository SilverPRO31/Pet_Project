using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyApp
{
    public partial class DBOutputAll : Form
    {
        public DBOutputAll()
        {
            InitializeComponent();
        }

        private void DBOutput_Load(object sender, EventArgs e)
        {
            this.personalDataTableAdapter.Fill(this.dBDataSet.PersonalData);
            int rowCount = dataGridView1.RowCount;
            DateTime currentDate = DateTime.Now;
            for (int i = 0; i < rowCount; i++)
            {

                object cellValue = dataGridView1[2, i].Value;
                if (cellValue != null && !string.IsNullOrEmpty(cellValue.ToString()))
                {
                    DateTime birthday = DateTime.Parse(cellValue.ToString());
                    int age = currentDate.Year - birthday.Year;
                    if (currentDate < birthday.AddYears(age))
                    {
                        age--;
                    }
                    dataGridView1.Rows[i].Cells[4].Value = age;
                }
                }

            }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
