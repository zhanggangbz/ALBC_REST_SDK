using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SDK_Test
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //创建一个打开文件式的对话框  
            OpenFileDialog ofd = new OpenFileDialog();
            //设置这个对话框的起始打开路径  
            ofd.InitialDirectory = @"D:\";
            //设置打开的文件的类型，注意过滤器的语法  
            ofd.Filter = "所有文件|*.*|PNG图片|*.png|JPG图片|*.jpg|BMP图片|*.bmp";
            //调用ShowDialog()方法显示该对话框，该方法的返回值代表用户是否点击了确定按钮  
            if (ofd.ShowDialog() == true)
            {
                ALBC_REST_SDK.ALBCClient _client = new ALBC_REST_SDK.ALBCClient("", "", "");
                _client.UpLoadFileBlock(ofd.FileName);
            }
            else
            {
                MessageBox.Show("没有选择图片");
            }
        }
    }
}
