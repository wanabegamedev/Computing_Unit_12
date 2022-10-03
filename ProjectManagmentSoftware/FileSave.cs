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

        static public void ExitSave()
        {
            Save();
            SaveCards();
            
        }


        static async void Save()
        {
            using (FileStream fs = new FileStream(@"C:\Users\oline\Desktop\ProjectManagmentFiles\" + Project.projectName + ".pm", FileMode.OpenOrCreate))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write(Project.startDate.ToString() + "," + Project.endDate.ToString() + "," + Project.projectName);
                    
                    

                    SaveCards();






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
                        bw.Write(card.GetTitle() + "," + card.GetDescription() + "," + card.GetStartDate() + "," + card.GetEndDate() + "," + card.GetNotificationEnabled() + "," + card.GetState() + "**");




                    }

                    

                    fs.Close();

                }
            }
        }
    }
}