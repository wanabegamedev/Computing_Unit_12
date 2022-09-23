using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectManagmentSoftware
{
     static class FileSave
    {

       static public void SaveLoop()
        {




            Save();
        }


       static async void Save()
        {
            using (FileStream fs = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) +  Project.projectName +".pm", FileMode.OpenOrCreate))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write(Project.startDate.ToString());
                    bw.Write(Project.endDate.ToString());
                    bw.Write(Project.cards.ToString());
                    bw.Write(Project.projectName);

                    


                    MessageBox.Show("Save Done");

                    

                    
                }


            }

                await Task.Delay(5000);
                Save();

        }

    }
}
