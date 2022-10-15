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
            using (FileStream fs = new FileStream(Properties.Settings.Default.ProjectFolderPath + @"\" + Project.projectName + ".pm", FileMode.OpenOrCreate))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    //using ASCII for breaks between bits of data to remove the need for some validation
                    bw.Write(Project.startDate.ToString() + ((char)140).ToString() + Project.endDate.ToString() + ((char)140).ToString() + Project.projectName);
                    
                    

                    SaveCards();
                }


            }

            await Task.Delay(5000);
            Save();

        }


        static void SaveCards()
        {

            using (FileStream fs = new FileStream (Properties.Settings.Default.ProjectFolderPath + @"\" + Project.projectName + " cards" + ".pm", FileMode.OpenOrCreate))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    foreach (Card card in Project.cards)
                    {
                        //using ASCII for breaks between bits of data to remove the need for some validation
                        bw.Write(card.GetTitle() + ((char)140).ToString() + card.GetDescription() + ((char)140).ToString() + card.GetStartDate() + ((char)140).ToString() + card.GetEndDate() + ((char)140).ToString() + card.GetNotificationEnabled() + ((char)140).ToString() + card.GetState() + ((char)141).ToString());




                    }

                    

                    fs.Close();

                }
            }
        }
    }
}