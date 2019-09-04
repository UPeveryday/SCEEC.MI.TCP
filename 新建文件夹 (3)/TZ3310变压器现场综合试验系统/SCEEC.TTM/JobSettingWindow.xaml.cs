using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SCEEC.MI.TZ3310;
using SCEEC.Data;
using System.Data;
using System.Threading;

namespace SCEEC.TTM
{
    /// <summary>
    /// JobSettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class JobSettingWindow : Window
    {
        public string jobName = "";
        private bool inited = false;
        private Transformer currentTransformer;

        private bool changed = false;
        private bool closeConfirmed = false;

        public JobSettingWindow(string transformerSerialNo, string jobName = "")
        {
            InitializeComponent();
            currentTransformer = WorkingSets.local.getTransformer(transformerSerialNo);
            TransformerSerialNoTextBox.Text = currentTransformer.SerialNo;

            jobName = jobName.Trim();

            this.jobName = jobName;

            if (jobName.Length > 0) JobRefresh(transformerSerialNo, jobName);

            if (currentTransformer.WindingNum != 3)
            {
                LVWindingDCInsulationCheckBox.IsChecked = false;
                LVWindingCapacitanceCheckBox.IsChecked = false;
                LVWindingDCResistanceCheckBox.IsChecked = false;
                LVWindingDCInsulationCheckBox.Visibility = Visibility.Collapsed;
                LVWindingCapacitanceCheckBox.Visibility = Visibility.Collapsed;
                LVWindingDCResistanceCheckBox.Visibility = Visibility.Collapsed;
            }

            if ((!currentTransformer.Bushing.HVContained) && (!currentTransformer.Bushing.MVContained))
            {
                BushingListBoxItem.Visibility = Visibility.Collapsed;
                BushingDCInsulationCheckBox.IsChecked = false;
                BushingCapacitanceCheckBox.IsChecked = false;
            }

            if (currentTransformer.OLTC.Contained == false)
            {
                OLTCListBoxItem.Visibility = Visibility.Collapsed;
                OLTCCheckBox.IsChecked = false;
                OLTCDCResistanceCheckBox.IsChecked = false;
                OLTCSwitchingCheckBox.IsChecked = false;
                OLTCRangeTextBox.Text = currentTransformer.OLTC.TapNum.ToString();
            }

            inited = true;

            JobChanged();

            changed = false;

            JobNameTextBox.Focus();
        }

        private void JobChanged(object sender = null, RoutedEventArgs e = null)
        {
            ProgressEnd();
            if (!inited) return;
            if (HVWindingDCInsulationCheckBox == null) return;
            if (MVWindingDCInsulationCheckBox == null) return;
            if (LVWindingDCInsulationCheckBox == null) return;
            if (HVWindingCapacitanceCheckBox == null) return;
            if (MVWindingCapacitanceCheckBox == null) return;
            if (LVWindingCapacitanceCheckBox == null) return;
            if (HVWindingDCResistanceCheckBox == null) return;
            if (MVWindingDCResistanceCheckBox == null) return;
            if (LVWindingDCResistanceCheckBox == null) return;
            if (BushingDCInsulationCheckBox == null) return;
            if (BushingCapacitanceCheckBox == null) return;
            if (OLTCCheckBox == null) return;
            if (OLTCRangeTextBox == null) return;
            if (OLTCDCResistanceCheckBox == null) return;
            if (OLTCSwitchingCheckBox == null) return;
            if (WorkList == null) return;
            if (DCInsulationTestVoltageComboBox == null) return;
            if (DCInsulationResThTextBox == null) return;
            if (DCInsulationARTextBox == null) return;
            if (CapacitanceTestVoltageComboBox == null) return;
            if (DCResistanceCurrentComboBox == null) return;
            if (BushingDCInsulationTestVoltageComboBox == null) return;
            if (BushingCapacitanceTestVoltageComboBox == null) return;
            changed = true;

            JobList jobList = new JobList();

            jobList.Transformer = currentTransformer;

            jobList.DCInsulation.HVEnabled = (HVWindingDCInsulationCheckBox.IsChecked == true);
            jobList.DCInsulation.MVEnabled = (MVWindingDCInsulationCheckBox.IsChecked == true);
            jobList.DCInsulation.LVEnabled = (LVWindingDCInsulationCheckBox.IsChecked == true);

            jobList.Capacitance.HVEnabled = (HVWindingCapacitanceCheckBox.IsChecked == true);
            jobList.Capacitance.MVEnabled = (MVWindingCapacitanceCheckBox.IsChecked == true);
            jobList.Capacitance.LVEnabled = (LVWindingCapacitanceCheckBox.IsChecked == true);

            jobList.DCResistance.HVEnabled = (HVWindingDCResistanceCheckBox.IsChecked == true);
            jobList.DCResistance.MVEnabled = (MVWindingDCResistanceCheckBox.IsChecked == true);
            jobList.DCResistance.LVEnabled = (LVWindingDCResistanceCheckBox.IsChecked == true);

            jobList.Bushing.DCInsulation = (BushingDCInsulationCheckBox.IsChecked == true);
            jobList.Bushing.Capacitance = (BushingCapacitanceCheckBox.IsChecked == true);

            jobList.OLTC.Range = int.Parse(OLTCRangeTextBox.Text);
            jobList.OLTC.DCResistance = (OLTCDCResistanceCheckBox.IsChecked == true);
            jobList.OLTC.SwitchingCharacter = (OLTCSwitchingCheckBox.IsChecked == true);

            var miList = Translator.JobList2MeasurementItems(jobList);

            //UseUpanDoWork.LocalUsb.Reinitialize();
            //foreach (var mi in miList)
            //{
            //    UseUpanDoWork.LocalUsb.StartBuiltTestData(mi, jobList);
            //}
            //UpanForWriteToFile.writefile("D:\\Mission", "List.lis", jobList);


            List<string> miListString = new List<string>();
            foreach (var mi in miList)
            {
                miListString.Add(mi.Description);
            }
            WorkList.ItemsSource = miListString;
        }

        private void JobConfire(object sender = null, RoutedEventArgs e = null)
        {
            if (!inited) return;
            if (HVWindingDCInsulationCheckBox == null) return;
            if (MVWindingDCInsulationCheckBox == null) return;
            if (LVWindingDCInsulationCheckBox == null) return;
            if (HVWindingCapacitanceCheckBox == null) return;
            if (MVWindingCapacitanceCheckBox == null) return;
            if (LVWindingCapacitanceCheckBox == null) return;
            if (HVWindingDCResistanceCheckBox == null) return;
            if (MVWindingDCResistanceCheckBox == null) return;
            if (LVWindingDCResistanceCheckBox == null) return;
            if (BushingDCInsulationCheckBox == null) return;
            if (BushingCapacitanceCheckBox == null) return;
            if (OLTCCheckBox == null) return;
            if (OLTCRangeTextBox == null) return;
            if (OLTCDCResistanceCheckBox == null) return;
            if (OLTCSwitchingCheckBox == null) return;
            if (WorkList == null) return;
            if (DCInsulationTestVoltageComboBox == null) return;
            if (DCInsulationResThTextBox == null) return;
            if (DCInsulationARTextBox == null) return;
            if (CapacitanceTestVoltageComboBox == null) return;
            if (DCResistanceCurrentComboBox == null) return;
            if (BushingDCInsulationTestVoltageComboBox == null) return;
            if (BushingCapacitanceTestVoltageComboBox == null) return;
            changed = true;

            JobList jobList = new JobList();

            jobList.Transformer = currentTransformer;

            jobList.DCInsulation.HVEnabled = (HVWindingDCInsulationCheckBox.IsChecked == true);
            jobList.DCInsulation.MVEnabled = (MVWindingDCInsulationCheckBox.IsChecked == true);
            jobList.DCInsulation.LVEnabled = (LVWindingDCInsulationCheckBox.IsChecked == true);

            jobList.Capacitance.HVEnabled = (HVWindingCapacitanceCheckBox.IsChecked == true);
            jobList.Capacitance.MVEnabled = (MVWindingCapacitanceCheckBox.IsChecked == true);
            jobList.Capacitance.LVEnabled = (LVWindingCapacitanceCheckBox.IsChecked == true);

            jobList.DCResistance.HVEnabled = (HVWindingDCResistanceCheckBox.IsChecked == true);
            jobList.DCResistance.MVEnabled = (MVWindingDCResistanceCheckBox.IsChecked == true);
            jobList.DCResistance.LVEnabled = (LVWindingDCResistanceCheckBox.IsChecked == true);

            jobList.Bushing.DCInsulation = (BushingDCInsulationCheckBox.IsChecked == true);
            jobList.Bushing.Capacitance = (BushingCapacitanceCheckBox.IsChecked == true);

            jobList.OLTC.Range = int.Parse(OLTCRangeTextBox.Text);
            jobList.OLTC.DCResistance = (OLTCDCResistanceCheckBox.IsChecked == true);
            jobList.OLTC.SwitchingCharacter = (OLTCSwitchingCheckBox.IsChecked == true);

            var miList = Translator.JobList2MeasurementItems(jobList);
            UseUpanDoWork.LocalUsb.Reinitialize();


            foreach (var mi in miList)
            {
                UseUpanDoWork.LocalUsb.StartBuiltTestData(mi, jobList);
            }
            //自动检索Upan
#if true
            String[] drives = Environment.GetLogicalDrives();
            UpanForWriteToFile.writefile(drives[drives.Length - 1], "List.lis", jobList, miList);
#endif
            // UpanForWriteToFile.writefile("D:\\", "List.lis", jobList, miList);





            List<string> miListString = new List<string>();
            foreach (var mi in miList)
            {
                miListString.Add(mi.Description);
            }
            WorkList.ItemsSource = miListString;
        }

        private void JobRefresh(string TransformerSerialNo, string JobName)
        {
            if (JobName == string.Empty) return;
            JobList job = WorkingSets.local.getJob(TransformerSerialNo, JobName);
            JobNameTextBox.Text = job.Name;
            WindingDCInsulationCheckBox.IsChecked = job.DCInsulation.Enabled;
            HVWindingDCInsulationCheckBox.IsChecked = job.DCInsulation.HVEnabled;
            MVWindingDCInsulationCheckBox.IsChecked = job.DCInsulation.MVEnabled;
            LVWindingDCInsulationCheckBox.IsChecked = job.DCInsulation.LVEnabled;
            WindingCapacitanceCheckBox.IsChecked = job.Capacitance.Enabled;
            HVWindingCapacitanceCheckBox.IsChecked = job.Capacitance.HVEnabled;
            MVWindingCapacitanceCheckBox.IsChecked = job.Capacitance.MVEnabled;
            LVWindingCapacitanceCheckBox.IsChecked = job.Capacitance.LVEnabled;
            WindingDCResistanceCheckBox.IsChecked = job.DCResistance.Enabled;
            HVWindingDCResistanceCheckBox.IsChecked = job.DCResistance.HVEnabled;
            MVWindingDCResistanceCheckBox.IsChecked = job.DCResistance.MVEnabled;
            LVWindingDCResistanceCheckBox.IsChecked = job.DCResistance.LVEnabled;
            BushingDCInsulationCheckBox.IsChecked = job.Bushing.DCInsulation;
            BushingCapacitanceCheckBox.IsChecked = job.Bushing.Capacitance;
            OLTCCheckBox.IsChecked = job.OLTC.Enabled;
            OLTCRangeTextBox.Text = job.OLTC.Range.ToString();
            OLTCDCResistanceCheckBox.IsChecked = job.OLTC.DCResistance;
            OLTCSwitchingCheckBox.IsChecked = job.OLTC.SwitchingCharacter;

            DCInsulationTestVoltageComboBox.SelectedIndex = DCInsulationTestVoltageComboBox.Items.Count - 1;
            while ((job.Parameter.DCInsulationVoltage < SCEEC.Numerics.NumericsConverter.Text2Value(DCInsulationTestVoltageComboBox.Text).value) && (DCInsulationTestVoltageComboBox.SelectedIndex > 0))
            {
                DCInsulationTestVoltageComboBox.SelectedIndex--;
            }

            DCInsulationResThTextBox.Text = ((int)(job.Parameter.DCInsulationResistance + 0.5)).ToString();
            DCInsulationARTextBox.Text = job.Parameter.DCInsulationAbsorptionRatio.ToString("F1");

            CapacitanceTestVoltageComboBox.SelectedIndex = CapacitanceTestVoltageComboBox.Items.Count - 1;
            while ((job.Parameter.CapacitanceVoltage < SCEEC.Numerics.NumericsConverter.Text2Value(CapacitanceTestVoltageComboBox.Text).value) && (CapacitanceTestVoltageComboBox.SelectedIndex > 0))
            {
                CapacitanceTestVoltageComboBox.SelectedIndex--;
            }

            DCResistanceCurrentComboBox.SelectedIndex = DCResistanceCurrentComboBox.Items.Count - 1;
            while ((job.Parameter.DCResistanceCurrent < SCEEC.Numerics.NumericsConverter.Text2Value(DCResistanceCurrentComboBox.Text).value) && (DCResistanceCurrentComboBox.SelectedIndex > 0))
            {
                DCResistanceCurrentComboBox.SelectedIndex--;
            }

            BushingDCInsulationTestVoltageComboBox.SelectedIndex = BushingDCInsulationTestVoltageComboBox.Items.Count - 1;
            while ((job.Parameter.BushingDCInsulationVoltage < SCEEC.Numerics.NumericsConverter.Text2Value(BushingDCInsulationTestVoltageComboBox.Text).value) && (BushingDCInsulationTestVoltageComboBox.SelectedIndex > 0))
            {
                BushingDCInsulationTestVoltageComboBox.SelectedIndex--;
            }

            BushingCapacitanceTestVoltageComboBox.SelectedIndex = BushingCapacitanceTestVoltageComboBox.Items.Count - 1;
            while ((job.Parameter.BushingCapacitanceVoltage < SCEEC.Numerics.NumericsConverter.Text2Value(BushingCapacitanceTestVoltageComboBox.Text).value) && (BushingCapacitanceTestVoltageComboBox.SelectedIndex > 0))
            {
                BushingCapacitanceTestVoltageComboBox.SelectedIndex--;
            }
        }

        private bool saveJob()
        {
            JobNameTextBox.Text = JobNameTextBox.Text.Trim();
            if (JobNameTextBox.Text.Length == 0)
            {
                MessageBox.Show("请输入任务单名称!", "任务单管理器", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (!checkOLTCRange()) return false;
            TransformerSerialNoTextBox.Text = TransformerSerialNoTextBox.Text.Trim();
            JobNameTextBox.Text = JobNameTextBox.Text.Trim();
            DataRow[] rows = WorkingSets.local.Jobs.Select("TransformerSerialNo = '" + TransformerSerialNoTextBox.Text + "' and JobName = '" + JobNameTextBox.Text + "'");
            DataRow r;
            if (rows.Length > 0)
                r = rows[0];
            else
                r = WorkingSets.local.Jobs.NewRow();
            WorkingSets.local.saveJobs();
            r["TransformerSerialNo"] = TransformerSerialNoTextBox.Text;
            r["JobName"] = JobNameTextBox.Text;
            r["WindingDCInsulation"] = WindingDCInsulationCheckBox.IsChecked;
            r["HVWindingDCInsulation"] = HVWindingDCInsulationCheckBox.IsChecked;
            r["MVWindingDCInsulation"] = MVWindingDCInsulationCheckBox.IsChecked;
            r["LVWindingDCInsulation"] = LVWindingDCInsulationCheckBox.IsChecked;
            r["WindingCapacitance"] = WindingCapacitanceCheckBox.IsChecked;
            r["HVWindingCapacitance"] = HVWindingCapacitanceCheckBox.IsChecked;
            r["MVWindingCapacitance"] = MVWindingCapacitanceCheckBox.IsChecked;
            r["LVWindingCapacitance"] = LVWindingCapacitanceCheckBox.IsChecked;
            r["WindingDCResistance"] = WindingDCResistanceCheckBox.IsChecked;
            r["HVWindingDCResistance"] = HVWindingDCResistanceCheckBox.IsChecked;
            r["MVWindingDCResistance"] = MVWindingDCResistanceCheckBox.IsChecked;
            r["LVWindingDCResistance"] = LVWindingDCResistanceCheckBox.IsChecked;
            r["BushingDCInsulation"] = BushingDCInsulationCheckBox.IsChecked;
            r["BushingCapacitance"] = BushingCapacitanceCheckBox.IsChecked;
            r["OLTC"] = OLTCCheckBox.IsChecked;
            r["OLTCRangeTextBox"] = int.Parse(OLTCRangeTextBox.Text);
            r["OLTCDCResistance"] = OLTCDCResistanceCheckBox.IsChecked;
            r["OLTCSwitching"] = OLTCSwitchingCheckBox.IsChecked;
            r["dci_voltage"] = SCEEC.Numerics.NumericsConverter.Text2Value(DCInsulationTestVoltageComboBox.Text).value;
            r["dci_resistance"] = double.Parse(DCInsulationResThTextBox.Text);
            r["dci_ar"] = double.Parse(DCInsulationARTextBox.Text);
            r["cap_voltage"] = (int)(SCEEC.Numerics.NumericsConverter.Text2Value(CapacitanceTestVoltageComboBox.Text).value);
            r["dcr_current"] = (int)SCEEC.Numerics.NumericsConverter.Text2Value(DCResistanceCurrentComboBox.Text).value;
            r["bushing_cap_voltage"] = (int)(SCEEC.Numerics.NumericsConverter.Text2Value(BushingCapacitanceTestVoltageComboBox.Text).value);
            r["bushing_dci_voltage"] = SCEEC.Numerics.NumericsConverter.Text2Value(BushingDCInsulationTestVoltageComboBox.Text).value;
            if (rows.Length > 0) r.EndEdit();
            else WorkingSets.local.Jobs.Rows.Add(r);
            return WorkingSets.local.saveJobs();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void MinimumButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaximumButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                maximumButtonImage.Source = new BitmapImage(new Uri("Resources/maximum.png", UriKind.Relative));
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                maximumButtonImage.Source = new BitmapImage(new Uri("Resources/maximum2.png", UriKind.Relative));
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OLTCCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (OLTCRangeTextBox == null) return;
            if (OLTCDCResistanceCheckBox == null) return;
            if (OLTCSwitchingCheckBox == null) return;
            OLTCRangeWrapPanel.Visibility = Visibility.Visible;
            OLTCDCResistanceCheckBox.IsChecked = true;
            OLTCDCResistanceCheckBox.Visibility = Visibility.Visible;
            OLTCSwitchingCheckBox.IsChecked = true;
            OLTCSwitchingCheckBox.Visibility = Visibility.Visible;
        }

        private void OLTCCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (OLTCRangeTextBox == null) return;
            if (OLTCDCResistanceCheckBox == null) return;
            if (OLTCSwitchingCheckBox == null) return;
            OLTCRangeWrapPanel.Visibility = Visibility.Collapsed;
            OLTCDCResistanceCheckBox.IsChecked = false;
            OLTCDCResistanceCheckBox.Visibility = Visibility.Collapsed;
            OLTCSwitchingCheckBox.IsChecked = false;
            OLTCSwitchingCheckBox.Visibility = Visibility.Collapsed;
        }

        private bool checkOLTCRange()
        {
            int TapNum = (currentTransformer.OLTC.TapNum - 1) / 2;
            if (TapNum == 0) return true;
            if (!Microsoft.VisualBasic.Information.IsNumeric(OLTCRangeTextBox.Text))
            {
                MessageBox.Show("请输入试验分接数，应为1至" + TapNum + "的整数", "任务单管理器", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                OLTCRangeTextBox.Focus();
                return false;
            }
            if (double.Parse(OLTCRangeTextBox.Text) < 1)
            {
                MessageBox.Show("请输入试验分接数，应为1至" + TapNum + "的整数", "任务单管理器", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                OLTCRangeTextBox.Focus();
                return false;
            }
            if (double.Parse(OLTCRangeTextBox.Text) > TapNum)
            {
                MessageBox.Show("请输入试验分接数，应为1至" + TapNum + "的整数", "任务单管理器", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                OLTCRangeTextBox.Focus();
                return false;
            }
            if (OLTCRangeTextBox.Text.IndexOf(".") > -1)
            {
                MessageBox.Show("请输入试验分接数，应为1至" + TapNum + "的整数", "任务单管理器", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                OLTCRangeTextBox.Focus();
                return false;
            }
            if (OLTCRangeTextBox.Text.IndexOf(",") > -1)
            {
                MessageBox.Show("请输入试验分接数，应为1至" + TapNum + "的整数", "任务单管理器", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                OLTCRangeTextBox.Focus();
                return false;
            }
            if (OLTCRangeTextBox.Text.IndexOf("-") > -1)
            {
                MessageBox.Show("请输入试验分接数，应为1至" + TapNum + "的整数", "任务单管理器", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                OLTCRangeTextBox.Focus();
                return false;
            }
            if (OLTCRangeTextBox.Text.IndexOf("+") > -1)
            {
                MessageBox.Show("请输入试验分接数，应为1至" + TapNum + "的整数", "任务单管理器", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                OLTCRangeTextBox.Focus();
                return false;
            }
            return true;
        }

        private void OLTCRangeTextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            JobChanged();
        }

        private void OLTCRangeTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Microsoft.VisualBasic.Information.IsNumeric(e.Text))
                e.Handled = true;
            else
                e.Handled = false;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void WindingDCInsulationCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (HVWindingDCInsulationCheckBox == null) return;
            if (MVWindingDCInsulationCheckBox == null) return;
            if (LVWindingDCInsulationCheckBox == null) return;
            HVWindingDCInsulationCheckBox.IsChecked = true;
            MVWindingDCInsulationCheckBox.IsChecked = true;
            LVWindingDCInsulationCheckBox.IsChecked = true;
            HVWindingDCInsulationCheckBox.Visibility = Visibility.Visible;
            MVWindingDCInsulationCheckBox.Visibility = Visibility.Visible;
            if (currentTransformer.WindingNum == 3)
                LVWindingDCInsulationCheckBox.Visibility = Visibility.Visible;
            else
                LVWindingDCInsulationCheckBox.Visibility = Visibility.Collapsed;
        }

        private void WindingDCInsulationCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (HVWindingDCInsulationCheckBox == null) return;
            if (MVWindingDCInsulationCheckBox == null) return;
            if (LVWindingDCInsulationCheckBox == null) return;
            HVWindingDCInsulationCheckBox.IsChecked = false;
            MVWindingDCInsulationCheckBox.IsChecked = false;
            LVWindingDCInsulationCheckBox.IsChecked = false;
            HVWindingDCInsulationCheckBox.Visibility = Visibility.Collapsed;
            MVWindingDCInsulationCheckBox.Visibility = Visibility.Collapsed;
            LVWindingDCInsulationCheckBox.Visibility = Visibility.Collapsed;
        }

        private void WindingCapacitanceCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (HVWindingCapacitanceCheckBox == null) return;
            if (MVWindingCapacitanceCheckBox == null) return;
            if (LVWindingCapacitanceCheckBox == null) return;
            HVWindingCapacitanceCheckBox.IsChecked = true;
            MVWindingCapacitanceCheckBox.IsChecked = true;
            LVWindingCapacitanceCheckBox.IsChecked = true;
            HVWindingCapacitanceCheckBox.Visibility = Visibility.Visible;
            MVWindingCapacitanceCheckBox.Visibility = Visibility.Visible;
            if (currentTransformer.WindingNum == 3)
                LVWindingCapacitanceCheckBox.Visibility = Visibility.Visible;
            else
                LVWindingCapacitanceCheckBox.Visibility = Visibility.Collapsed;
        }

        private void WindingCapacitanceCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (HVWindingCapacitanceCheckBox == null) return;
            if (MVWindingCapacitanceCheckBox == null) return;
            if (LVWindingCapacitanceCheckBox == null) return;
            HVWindingCapacitanceCheckBox.IsChecked = false;
            MVWindingCapacitanceCheckBox.IsChecked = false;
            LVWindingCapacitanceCheckBox.IsChecked = false;
            HVWindingCapacitanceCheckBox.Visibility = Visibility.Collapsed;
            MVWindingCapacitanceCheckBox.Visibility = Visibility.Collapsed;
            LVWindingCapacitanceCheckBox.Visibility = Visibility.Collapsed;
        }

        private void WindingDCResistanceCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (HVWindingDCResistanceCheckBox == null) return;
            if (MVWindingDCResistanceCheckBox == null) return;
            if (LVWindingDCResistanceCheckBox == null) return;
            HVWindingDCResistanceCheckBox.IsChecked = true;
            MVWindingDCResistanceCheckBox.IsChecked = true;
            LVWindingDCResistanceCheckBox.IsChecked = true;
            HVWindingDCResistanceCheckBox.Visibility = Visibility.Visible;
            MVWindingDCResistanceCheckBox.Visibility = Visibility.Visible;
            LVWindingDCResistanceCheckBox.Visibility = Visibility.Visible;
            if (currentTransformer.WindingNum == 3)
                LVWindingDCResistanceCheckBox.Visibility = Visibility.Visible;
            else
                LVWindingDCResistanceCheckBox.Visibility = Visibility.Collapsed;
        }

        private void WindingDCResistanceCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (HVWindingDCResistanceCheckBox == null) return;
            if (MVWindingDCResistanceCheckBox == null) return;
            if (LVWindingDCResistanceCheckBox == null) return;
            HVWindingDCResistanceCheckBox.IsChecked = false;
            MVWindingDCResistanceCheckBox.IsChecked = false;
            LVWindingDCResistanceCheckBox.IsChecked = false;
            HVWindingDCResistanceCheckBox.Visibility = Visibility.Collapsed;
            MVWindingDCResistanceCheckBox.Visibility = Visibility.Collapsed;
            LVWindingDCResistanceCheckBox.Visibility = Visibility.Collapsed;
        }

        private void closeWithConfirm()
        {
            if (saveJob())
            {
                jobName = JobNameTextBox.Text;
                closeConfirmed = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("工作单保存错误", "工作单管理器", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (changed)
            {
                if (!closeConfirmed)
                {
                    switch (MessageBox.Show("变压器信息已发生更改，是否进行保存?", "位置管理器", MessageBoxButton.YesNoCancel, MessageBoxImage.Question))
                    {
                        case MessageBoxResult.Yes:
                            closeWithConfirm();
                            return;
                        case MessageBoxResult.No:
                            return;
                        case MessageBoxResult.Cancel:
                            e.Cancel = true;
                            return;
                    }
                }
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            //JobConfire();
            closeWithConfirm();
        }

        private void UpanButton_Click(object sender, RoutedEventArgs e)
        {
            JobConfire();
            ProgressBegin();

        }
        private void ProgressBegin()
        {

            Thread thread = new Thread(new ThreadStart(() =>
            {
                for (int i = 0; i <= 100; i++)
                {
                    this.LaunchProgressBar.Dispatcher.BeginInvoke((ThreadStart)delegate { this.LaunchProgressBar.Value = i; });
                    this.Dispatcher.BeginInvoke((Action)delegate ()
                    {
                        IsMessage.Text = i.ToString() + " %";
                    });
                    Thread.Sleep(20);
                    if (i == 100)
                    {
                        this.Dispatcher.BeginInvoke((Action)delegate ()
                        {
                            IsMessage.Text = "100%";
                        });

                    }
                }

            }));
            thread.Start();

        }
        private void ProgressEnd()
        {
            Thread thread = new Thread(new ThreadStart(() =>
            {
                if(LaunchProgressBar!=null)
                {
                    this.LaunchProgressBar.Dispatcher.BeginInvoke((ThreadStart)delegate { this.LaunchProgressBar.Value = 0; });
                    this.Dispatcher.BeginInvoke((Action)delegate ()
                    {
                        if (IsMessage != null)
                            IsMessage.Text = "";
                    });
                }
            }));
            thread.Start();
        }

        private void DataInsertButton_Click(object sender, RoutedEventArgs e)
        {
            //string[] needpath = Environment.GetLogicalDrives();
            //System.Diagnostics.Process.Start(needpath[needpath.Length-1]);
            //UseUpanDoWork.LocalUsb.ScanDirInfo();

            string[] tn = UseUpanDoWork.LocalUsb.GetTaskName();
            TaskUpan tk = new TaskUpan(tn);
            if (tn != null)
                tk.ShowDialog();
            else
            {
                DataInsertButton.Content = "未找到文件";
            }
        }
    }
}
