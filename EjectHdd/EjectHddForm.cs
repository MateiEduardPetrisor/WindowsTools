using System;
using System.Windows.Forms;

namespace EjectHdd
{
    public partial class EjectHddForm : Form
    {
        private DiskUtils DiskUtilsObj;

        public EjectHddForm()
        {
            InitializeComponent();
        }

        private void ButtonOnline_Click(object sender, EventArgs e)
        {
            this.DiskUtilsObj.DiskOperation(Convert.ToInt32(this.ComboBox_DiskNumber.SelectedItem), "Online");
        }

        private void ButtonOffline_Click(object sender, EventArgs e)
        {
            this.DiskUtilsObj.DiskOperation(Convert.ToInt32(this.ComboBox_DiskNumber.SelectedItem), "Offline");
        }

        private void EjectHdd_Load(object sender, EventArgs e)
        {
            this.ComboBox_DiskNumber.SelectedIndex = 1;
            this.DiskUtilsObj = new DiskUtils();
        }
    }
}