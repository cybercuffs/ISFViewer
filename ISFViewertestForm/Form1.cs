using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Microsoft.Ink;
using Microsoft.Win32;
using System.IO;
using System.Windows.Ink;



namespace ISFViewertestForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Select the source file
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "ISF files (*.isf)|*.isf";
            openFileDialog1.Title = "Browse an ISF file to be converted";
            DialogResult result = openFileDialog1.ShowDialog();
            

            if (result == DialogResult.OK)
            {

                // Converting ISF file to stream fs
                FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open);
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, (int)bytes.Length);

                // Loading Ink from 
                InkOverlay mInkOverlay = new InkOverlay();
                mInkOverlay.Ink.Load(bytes);
                fs.Write(bytes, 0, bytes.Length);

                // Saving Ink in GIF format inside a byte array;
                byte[] gif = mInkOverlay.Ink.Save(PersistenceFormat.Gif);
                fs.Write(gif, 0, gif.Length);

                // Dialog box to save the converted GIF file to the desired location
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Gif Image|*.gif";
                saveFileDialog1.Title = "Save an Image File";
                saveFileDialog1.ShowDialog();

                // Writing bytes to the file
                System.IO.File.WriteAllBytes(saveFileDialog1.FileName, gif);

                //Print Done! when the processing is completed
                label2.Text = "Done!";

            }               
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Select a ISF source folder
            FolderBrowserDialog openFolderDialog1 = new FolderBrowserDialog();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFolderDialog1.Description = "Browse an ISF folder";
            DialogResult result = openFolderDialog1.ShowDialog();

            // Select destination folder
            FolderBrowserDialog saveFolderDialog1 = new FolderBrowserDialog();
            saveFolderDialog1.Description = "Browse destination folder";
            DialogResult output = saveFolderDialog1.ShowDialog();
           

            if (result == DialogResult.OK)
            {
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(openFolderDialog1.SelectedPath);
                int count = dir.GetFiles().Length;
                          
                if (count != 0)
                {
                    foreach (string file in Directory.EnumerateFiles(openFolderDialog1.SelectedPath, "*.isf"))
                    {

                        //String myfilename = Path.GetFileName(file);

                        // Converting ISF file to stream fs
                        FileStream fs = new FileStream(file, FileMode.Open);
                        byte[] bytes = new byte[fs.Length];
                        fs.Read(bytes, 0, (int)bytes.Length);

                        // Loading Ink from 
                        InkOverlay mInkOverlay = new InkOverlay();
                        mInkOverlay.Ink.Load(bytes);
                        fs.Write(bytes, 0, bytes.Length);

                        // Saving Ink in GIF format inside a byte array;
                        byte[] gif = mInkOverlay.Ink.Save(PersistenceFormat.Gif);
                        fs.Write(gif, 0, gif.Length);

                        //Saving files with the same name as ISF and location specified in the dialog box of destination folder
                        var tempfilenameandlocation = Path.Combine(saveFolderDialog1.SelectedPath, Path.GetFileNameWithoutExtension(file));
                        String ext = ".gif";
                        System.IO.File.WriteAllBytes(tempfilenameandlocation + ext, gif);

                        //Print Done! when the processing is completed
                        label2.Text = "Done!";
                    }
                }              
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
