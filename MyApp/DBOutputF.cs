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
    public partial class DBOutputF : Form
    {
        public static DateTime start;
        public DBOutputF()
        {
            InitializeComponent();
        }

        private void DBOutputF_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dBDataSet2.PersonalData". При необходимости она может быть перемещена или удалена.
            this.personalDataTableAdapter.Fill(this.dBDataSet2.PersonalData);
            DateTime finish = DateTime.Now;
            TimeSpan performance = finish - start;
            MessageBox.Show("Время выполнения", performance.ToString());

        }
    }
}
