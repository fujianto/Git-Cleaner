using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using xDialog;
using System.IO;


namespace GitCleaner
{
    public partial class MainForm : MetroForm
    {
        public MainForm()
        {
            InitializeComponent();
            lblInfo.Text = "";
        }


        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            string folderPath = "";
            FolderBrowserDialog fbdGit = new FolderBrowserDialog();
            DialogResult dr = fbdGit.ShowDialog();

            if (dr == DialogResult.OK)
            {
                try
                {
                    folderPath = fbdGit.SelectedPath;
                    string[] fileTarget = Directory.GetFiles(@folderPath, ".git*", SearchOption.AllDirectories);
                    var di = new DirectoryInfo(@folderPath + "\\.git");

                    //Delete all files started with .git
                    for (int i = 0; i < fileTarget.Length; i++)
                    {
                        File.Delete(fileTarget[i]);
                        Console.WriteLine(fileTarget[i] + " Deleted");
                    }

                    Console.WriteLine("All *.git* files deleted");

                    //Delete .git directory
                    foreach (var file in di.GetFiles("*", SearchOption.AllDirectories))
                    {
                        file.Attributes &= ~FileAttributes.ReadOnly;
                    }

                    Console.WriteLine("All hidden attribute stripped");

                    System.IO.Directory.Delete(@folderPath + "\\.git", true);
                    MetroFramework.MetroMessageBox.Show(this, "All .git Removed", "Successs", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MsgBox.Show(ex.Message, "Exception", MsgBox.Buttons.OK, MsgBox.Icon.Error);
                }
 
            }
        }

 
    }
}
