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
            using (FileStream fs = new FileStream(@"C:\Users\oline\Desktop\ProjectManagmentFiles\" + Project.projectName + ".pm", FileMode.OpenOrCreate))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write(Project.startDate.ToString() + "," + Project.endDate.ToString() + "," + Project.projectName);
                    
                    

                    SaveCards();

                    MessageBox.Show("Save Done");




                }


            }

            await Task.Delay(5000);
            Save();

        }


        static void SaveCards()
        {

            using (FileStream fs = new FileStream(@"C:\Users\oline\Desktop\ProjectManagmentFiles\" + Project.projectName + " cards" + ".pm", FileMode.OpenOrCreate))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    foreach (Card card in Project.cards)
                    {
                        bw.Write(card.GetTitle() + ",");
                        bw.Write(card.GetDescription() + ",");
                        bw.Write(card.GetStartDate() + ",");
                        bw.Write(card.GetEndDate() + ",");
                        bw.Write(card.GetNotificationEnabled() + ",");
                        bw.Write(card.GetState() + ",");
                        bw.Write(Environment.NewLine);

                        MessageBox.Show("Save Done");
                    }

                    fs.Close();

                }
            }
        }
    }
}