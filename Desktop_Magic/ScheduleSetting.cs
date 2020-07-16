using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DM
{
    public partial class ScheduleSetting : Form
    {
        public ScheduleSetting()
        {
            InitializeComponent();
            LoadSetting();
        }

        private void LoadSetting()
        {
            if (File.Exists(Application.StartupPath + "\\schedule"))
            {
                Schedule schedule = new Schedule();
                StreamReader streamReader = new StreamReader(Application.StartupPath + "\\schedule");
                numericUpDownHMonF.Value = schedule.startTimeH = Int32.Parse(streamReader.ReadLine());
                numericUpDownMMonF.Value = schedule.startTimeM = Int32.Parse(streamReader.ReadLine());
                numericUpDownHMonT.Value = schedule.endTimeH = Int32.Parse(streamReader.ReadLine());
                numericUpDownMMonT.Value = schedule.endTimeM = Int32.Parse(streamReader.ReadLine());
                checkBoxMonday.Checked = Boolean.Parse(streamReader.ReadLine());
                checkBoxTuesday.Checked = Boolean.Parse(streamReader.ReadLine());
                checkBoxWednesday.Checked = Boolean.Parse(streamReader.ReadLine());
                checkBoxThursday.Checked = Boolean.Parse(streamReader.ReadLine());
                checkBoxFriday.Checked = Boolean.Parse(streamReader.ReadLine());
                checkBoxSaturday.Checked = Boolean.Parse(streamReader.ReadLine());
                checkBoxSunday.Checked = Boolean.Parse(streamReader.ReadLine());
                schedule.weekDays = new WeekDay(checkBoxMonday.Checked, checkBoxTuesday.Checked, checkBoxWednesday.Checked, checkBoxThursday.Checked, checkBoxFriday.Checked, checkBoxSaturday.Checked, checkBoxSunday.Checked);

                streamReader.Close();
            }

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Schedule schedule = new Schedule();
            schedule.startTimeH = Decimal.ToInt32(numericUpDownHMonF.Value);
            schedule.startTimeM = Decimal.ToInt32(numericUpDownMMonF.Value);
            schedule.endTimeH = Decimal.ToInt32(numericUpDownHMonT.Value);
            schedule.endTimeM = Decimal.ToInt32(numericUpDownMMonT.Value);
            schedule.weekDays = new WeekDay(checkBoxMonday.Checked, checkBoxTuesday.Checked, checkBoxWednesday.Checked, checkBoxThursday.Checked, checkBoxFriday.Checked, checkBoxSaturday.Checked, checkBoxSunday.Checked);

            System.IO.File.WriteAllText(Application.StartupPath + "\\schedule", String.Empty);
            StreamWriter streamWriter = new StreamWriter(Application.StartupPath + "\\schedule");
            streamWriter.WriteLine(schedule.startTimeH);
            streamWriter.WriteLine(schedule.startTimeM);
            streamWriter.WriteLine(schedule.endTimeH);
            streamWriter.WriteLine(schedule.endTimeM);
            streamWriter.WriteLine(schedule.weekDays.mon);
            streamWriter.WriteLine(schedule.weekDays.tue);
            streamWriter.WriteLine(schedule.weekDays.wed);
            streamWriter.WriteLine(schedule.weekDays.thu);
            streamWriter.WriteLine(schedule.weekDays.fri);
            streamWriter.WriteLine(schedule.weekDays.sat);
            streamWriter.WriteLine(schedule.weekDays.sun);
            streamWriter.Close();
            
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
