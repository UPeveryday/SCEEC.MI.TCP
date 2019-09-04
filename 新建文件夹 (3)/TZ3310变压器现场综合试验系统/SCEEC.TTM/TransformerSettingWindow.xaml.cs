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
using System.Data;

namespace SCEEC.TTM
{
    /// <summary>
    /// TransformerSettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TransformerSettingWindow : Window
    {

        bool changed = false;
        bool closeConfirmed = false;
        string originSerialNo = string.Empty;
        public string serialno = string.Empty;

        public void TransformerInfoInitial(string serialNo)
        {
            if (serialNo == string.Empty)
            {
                Title = "新变压器";
                return;
            }
            serialNo = serialNo.Trim();
            SerialNoTextBox.Text = serialNo;
            originSerialNo = serialNo;
            DataRow[] rows = WorkingSets.local.Transformers.Select("serialno = '" + serialNo + "'");
            if (rows.Length < 1)
            {
                Title = "新变压器";
                return;
            }
            DataRow r = rows[0];
            locationComboBox.SelectedIndex = locationComboBox.Items.IndexOf((string)r["location"]);
            ApparatusIDTextBox.Text = (string)r["apparatusid"];
            ManufacturerTextBox.Text = (string)r["manufacturer"];
            ProductionYearTextBox.Text = (string)r["productionyear"];
            AssetSystemCodeTextBox.Text = (string)r["assetsystemcode"];
            PhaseComboBox.SelectedIndex = ((int)r["phases"] == 3) ? 1 : 0;
            WindingNumComboBox.SelectedIndex = ((int)r["windings"] == 3) ? 1 : 0;
            RatingFrequencyComboBox.SelectedIndex = ((int)r["ratedfrequency"] == 50) ? 0 : 1;
            HvWindingConfigComboBox.SelectedIndex = (int)r["windingconfig_hv"];
            MvWindingConfigComboBox.SelectedIndex = (int)r["windingconfig_mv"];
            MvWindingLabelComboBox.SelectedIndex = (int)r["windingconfig_mv_label"];
            LvWindingConfigComboBox.SelectedIndex = (int)r["windingconfig_lv"];
            LvWindingLabelComboBox.SelectedIndex = (int)r["windingconfig_lv_label"];
            HvVoltageRatingTextBox.Text = ((double)r["voltageratinghv"]).ToString();
            MvVoltageRatingTextBox.Text = ((double)r["voltageratingmv"]).ToString();
            LvVoltageRatingTextBox.Text = ((double)r["voltageratinglv"]).ToString();
            HvPowerRatingTextBox.Text = ((double)r["powerratinghv"]).ToString();
            MvPowerRatingTextBox.Text = ((double)r["powerratingmv"]).ToString();
            LvPowerRatingTextBox.Text = ((double)r["powerratinglv"]).ToString();
            HvBushingCheckBox.IsChecked = (bool)r["bushing_hv_enabled"];
            MvBushingCheckBox.IsChecked = (bool)r["bushing_mv_enabled"];
            OLTCCheckBox.IsChecked = (((int)r["oltc_tapnum"]) > -1);
            if (OLTCCheckBox.IsChecked == true)
            {
                OLTCWindingComboBox.SelectedIndex = (int)r["oltc_winding"];
                OLTCTapNumComboBox.SelectedIndex = (int)r["oltc_tapnum"];
                OLTCStepComboBox.SelectedIndex = (int)r["oltc_step"];
                OLTCTapMainNumTextBox.Text = ((int)r["oltc_tapmainnum"]).ToString();
                OLTCSerialNoTextBox.Text = (string)r["oltc_serialno"];
                OLTCModelTypeTextBox.Text = (string)r["oltc_modeltype"];
                OLTCManufacturerTextBox.Text = (string)r["oltc_manufacturer"];
                OLTCProductionYearTextBox.Text = (string)r["oltcproductionyear"];
            }
        }

        private bool reviewTable()
        {
            bool passed;
            double outValue;
            SerialNoTextBox.Text = SerialNoTextBox.Text.Trim();
            if (SerialNoTextBox.Text == string.Empty)
            {
                MessageBox.Show("请输入变压器出厂序号!", "变压器管理器", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                SerialNoTextBox.Focus();
                return false;
            }
            if (locationComboBox.SelectedIndex < 0)
            {
                MessageBox.Show("请选择变压器位置!", "变压器管理器", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                locationComboBox.Focus();
                return false;
            }
            passed = double.TryParse(HvVoltageRatingTextBox.Text, out outValue);
            if ((!passed) || (outValue <= 0.0))
            {
                MessageBox.Show("额定电压需要是正数!", "数据错误", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                HvVoltageRatingTextBox.Focus();
                return false;
            }
            passed = double.TryParse(MvVoltageRatingTextBox.Text, out outValue);
            if ((!passed) || (outValue <= 0.0))
            {
                MessageBox.Show("额定电压需要是正数!", "数据错误", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                MvVoltageRatingTextBox.Focus();
                return false;
            }
            if (WindingNumComboBox.SelectedIndex > 0)
            {
                passed = double.TryParse(LvVoltageRatingTextBox.Text, out outValue);
                if ((!passed) || (outValue <= 0.0))
                {
                    MessageBox.Show("额定电压需要是正数!", "数据错误", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    LvVoltageRatingTextBox.Focus();
                    return false;
                }
            }
            passed = double.TryParse(HvPowerRatingTextBox.Text, out outValue);
            if ((!passed) || (outValue <= 0.0))
            {
                MessageBox.Show("额定容量需要是正数!", "数据错误", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                HvPowerRatingTextBox.Focus();
                return false;
            }
            passed = double.TryParse(MvPowerRatingTextBox.Text, out outValue);
            if ((!passed) || (outValue <= 0.0))
            {
                MessageBox.Show("额定容量需要是正数!", "数据错误", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                MvPowerRatingTextBox.Focus();
                return false;
            }
            passed = double.TryParse(HvPowerRatingTextBox.Text, out outValue);
            if ((!passed) || (outValue <= 0.0))
            {
                MessageBox.Show("额定容量需要是正数!", "数据错误", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                LvPowerRatingTextBox.Focus();
                return false;
            }
            return true;
        }

        private bool saveTable()
        {
            if (!reviewTable()) return false;
            DataRow[] rows = WorkingSets.local.Transformers.Select("serialno = '" + SerialNoTextBox.Text.Trim() + "'");
            DataRow r = WorkingSets.local.Transformers.NewRow();
            if (rows.Length > 0)
                r = rows[0];
            else
                r = WorkingSets.local.Transformers.NewRow();
            bool previewSave =  WorkingSets.local.saveTransformer();
            r["serialno"] = SerialNoTextBox.Text;
            r["location"] = (string)locationComboBox.SelectedItem;
            r["apparatusid"] = ApparatusIDTextBox.Text;
            r["manufacturer"] = ManufacturerTextBox.Text;
            r["productionyear"] = ProductionYearTextBox.Text;
            r["assetsystemcode"] = AssetSystemCodeTextBox.Text;
            r["phases"] = (PhaseComboBox.SelectedIndex == 1) ? 3 : 2;
            r["windings"] = (WindingNumComboBox.SelectedIndex == 1) ? 3 : 2;
            r["ratedfrequency"] = (RatingFrequencyComboBox.SelectedIndex == 0) ? 50 : 60;
            r["windingconfig_hv"] = HvWindingConfigComboBox.SelectedIndex;
            r["windingconfig_mv"] = MvWindingConfigComboBox.SelectedIndex;
            r["windingconfig_mv_label"] = MvWindingLabelComboBox.SelectedIndex;
            r["windingconfig_lv"] = LvWindingConfigComboBox.SelectedIndex;
            r["windingconfig_lv_label"] = LvWindingLabelComboBox.SelectedIndex;
            r["voltageratinghv"] = double.Parse(HvVoltageRatingTextBox.Text);
            r["voltageratingmv"] = MvVoltageRatingTextBox.Text;
            r["voltageratinglv"] = LvVoltageRatingTextBox.Text;
            r["powerratinghv"] = HvPowerRatingTextBox.Text;
            r["powerratingmv"] = MvPowerRatingTextBox.Text;
            r["powerratinglv"] = LvPowerRatingTextBox.Text;
            r["bushing_hv_enabled"] = HvBushingCheckBox.IsChecked;
            r["bushing_mv_enabled"] = MvBushingCheckBox.IsChecked;
            if (OLTCCheckBox.IsChecked == true)
            {
                r["oltc_winding"] = OLTCWindingComboBox.SelectedIndex;
                r["oltc_tapmainnum"] = int.Parse(OLTCTapMainNumTextBox.Text);
                r["oltc_tapnum"] = OLTCTapNumComboBox.SelectedIndex;
                r["oltc_step"] = OLTCStepComboBox.SelectedIndex;
                r["oltc_serialno"] = OLTCSerialNoTextBox.Text;
                r["oltc_modeltype"] = OLTCModelTypeTextBox.Text;
                r["oltc_manufacturer"] = OLTCManufacturerTextBox.Text;
                r["oltcproductionyear"] = OLTCProductionYearTextBox.Text;
            }
            else
            {
                r["oltc_winding"] = 0;
                r["oltc_tapmainnum"] = 0;
                r["oltc_tapnum"] = -1;
                r["oltc_serialno"] = string.Empty;
                r["oltc_modeltype"] = string.Empty;
                r["oltc_manufacturer"] = string.Empty;
                r["oltcproductionyear"] = string.Empty;
            }
            r["id"] = 1;
            if (rows.Length > 0)  r.EndEdit();
            else WorkingSets.local.Transformers.Rows.Add(r);
            return WorkingSets.local.saveTransformer();
        }

        public TransformerSettingWindow(string serialNo = "")
        {
            InitializeComponent();
            locationComboBox.ItemsSource = WorkingSets.local.getLocationName();
            locationComboBox.SelectedIndex = 0;
            TransformerInfoInitial(serialNo);
            changed = false;
            this.serialno = serialNo;
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

        private void WindingNumComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LvPowerRatingLabel == null) return;
            if (LvPowerRatingTextBox == null) return;
            if (LvVoltageRatingLabel == null) return;
            if (LvVoltageRatingTextBox == null) return;
            if (LvWindingConfigComboBox == null) return;
            if (LvWindingLabelComboBox == null) return;
            changed = true;
            if (WindingNumComboBox.SelectedIndex == 1)
            {
                LvPowerRatingLabel.Visibility = Visibility.Visible;
                LvPowerRatingTextBox.Text = "1";
                LvPowerRatingTextBox.Visibility = Visibility.Visible;
                LvVoltageRatingLabel.Visibility = Visibility.Visible;
                LvVoltageRatingTextBox.Text = "35";
                LvVoltageRatingTextBox.Visibility = Visibility.Visible;
                if (LvWindingConfigComboBox.IsEnabled) LvWindingConfigComboBox.SelectedIndex = 2;
                LvWindingConfigComboBox.Visibility = Visibility.Visible;
                if (LvWindingLabelComboBox.IsEnabled) LvWindingLabelComboBox.SelectedIndex = 11;
                LvWindingLabelComboBox.Visibility = Visibility.Visible;
                LVOLTCComboBoxItem.Visibility = Visibility.Visible;
            }
            else
            {
                LvPowerRatingLabel.Visibility = Visibility.Collapsed;
                LvPowerRatingTextBox.Text = "0";
                LvPowerRatingTextBox.Visibility = Visibility.Collapsed;
                LvVoltageRatingLabel.Visibility = Visibility.Collapsed;
                LvVoltageRatingTextBox.Text = "0";
                LvVoltageRatingTextBox.Visibility = Visibility.Collapsed;
                LvWindingConfigComboBox.SelectedIndex = -1;
                LvWindingConfigComboBox.Visibility = Visibility.Collapsed;
                LvWindingLabelComboBox.SelectedIndex = -1;
                LvWindingLabelComboBox.Visibility = Visibility.Collapsed;
                LVOLTCComboBoxItem.Visibility = Visibility.Collapsed;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        private void closeWithConfirm()
        {
            if (saveTable())
            {
                serialno = SerialNoTextBox.Text;
                closeConfirmed = true;
                this.Close();
            }
        }

        private void TextChanged(object sender, object e)
        {
            changed = true;
        }

        private void PhaseComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HvWindingConfigComboBox == null) return;
            if (MvWindingConfigComboBox == null) return;
            if (MvWindingConfigComboBox == null) return;
            if (LvWindingConfigComboBox == null) return;
            if (LvWindingLabelComboBox == null) return;
            changed = true;
            if (PhaseComboBox.SelectedIndex == 1)
            {
                HvWindingConfigComboBox.SelectedIndex = 0;
                MvWindingConfigComboBox.SelectedIndex = 0;
                MvWindingLabelComboBox.SelectedIndex = 0;
                LvWindingConfigComboBox.SelectedIndex = 2;
                LvWindingLabelComboBox.SelectedIndex = 11;
                HvWindingConfigComboBox.IsEnabled = true;
                MvWindingConfigComboBox.IsEnabled = true;
                MvWindingLabelComboBox.IsEnabled = true;
                LvWindingConfigComboBox.IsEnabled = true;
                LvWindingLabelComboBox.IsEnabled = true;
            }
            else
            {
                HvWindingConfigComboBox.SelectedIndex = -1;
                MvWindingConfigComboBox.SelectedIndex = -1;
                MvWindingLabelComboBox.SelectedIndex = -1;
                LvWindingConfigComboBox.SelectedIndex = -1;
                LvWindingLabelComboBox.SelectedIndex = -1;
                HvWindingConfigComboBox.IsEnabled = false;
                MvWindingConfigComboBox.IsEnabled = false;
                MvWindingLabelComboBox.IsEnabled = false;
                LvWindingConfigComboBox.IsEnabled = false;
                LvWindingLabelComboBox.IsEnabled = false;
            }
        }

        private void OLTCCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            OLTCMainTapWrapPanel.Visibility = Visibility.Visible;
            OLTCGrid.Visibility = Visibility.Visible;
            changed = true;
        }

        private void OLTCCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            OLTCMainTapWrapPanel.Visibility = Visibility.Collapsed;
            OLTCGrid.Visibility = Visibility.Collapsed;
            changed = true;
        }

        private void SerialNoTextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            SerialNoTextBox.Text = SerialNoTextBox.Text.Trim();
            string SerialNo = SerialNoTextBox.Text;
            DataRow[] rows = WorkingSets.local.Transformers.Select("serialno = '" + SerialNo + "'");
            if (rows.Length > 0)
            {
                if ((originSerialNo != string.Empty) &&(originSerialNo != SerialNo))
                {
                    MessageBox.Show("该变压器出厂序号已存在，请修改出厂序号!", "变压器出厂序号重复", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    SerialNoTextBox.Focus();
                    return;
                }
                if (originSerialNo != SerialNo)
                    switch (MessageBox.Show("该变压器出厂序号已存在，是否对该变压器信息进行修改", "变压器出厂序号重复", MessageBoxButton.YesNo, MessageBoxImage.Exclamation))
                    {
                        case MessageBoxResult.Yes:
                            SerialNoTextBox.Focus();
                            return;
                        default:
                            SerialNoTextBox.Focus();
                            return;
                    }
            }
        }

        private void OLTCTapNumComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            changed = true;
            if (OLTCTapNumComboBox == null) return;
            if (OLTCStepComboBox == null) return;
            switch (((ComboBoxItem)(OLTCTapNumComboBox.SelectedItem)).Content.ToString())
            {
                case "1":
                    OLTCStepComboBox.SelectedIndex = 6;
                    break;
                case "2":
                    OLTCStepComboBox.SelectedIndex = 5;
                    break;
                case "4":
                    OLTCStepComboBox.SelectedIndex = 4;
                    break;
                case "5":
                    OLTCStepComboBox.SelectedIndex = 3;
                    break;
                case "8":
                    OLTCStepComboBox.SelectedIndex = 2;
                    break;
                case "10":
                    OLTCStepComboBox.SelectedIndex = 1;
                    break;
            }
        }

        private void LossPanelButton_Click(object sender, RoutedEventArgs e)
        {
            changed = true;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            closeWithConfirm();
        }

        private void OLTCTapMainNumTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Microsoft.VisualBasic.Information.IsNumeric(e.Text))
                e.Handled = true;
            else
                e.Handled = false;
        }

        private void OLTCTapMainNumTextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (!Microsoft.VisualBasic.Information.IsNumeric(OLTCTapMainNumTextBox.Text))
            {
                MessageBox.Show("请输入变压器主分接数，应为1或3。", "变压器管理器", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                OLTCTapMainNumTextBox.Focus();
                return;
            }
            if (OLTCTapMainNumTextBox.Text.IndexOf('.') > -1)
            {
                MessageBox.Show("请输入变压器主分接数，应为1或3。", "变压器管理器", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                OLTCTapMainNumTextBox.Focus();
                return;
            }
            int OLTCTapMainNum = int.Parse(OLTCTapMainNumTextBox.Text);
            if ((OLTCTapMainNum != 1) && (OLTCTapMainNum != 3))
            {
                MessageBox.Show("请输入变压器主分接数，应为1或3。", "变压器管理器", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                OLTCTapMainNumTextBox.Focus();
                return;
            }
        }
    }
}
