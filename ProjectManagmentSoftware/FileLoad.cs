using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Windows;
using System.Windows;
using System.Windows.Controls;

namespace ProjectManagmentSoftware
{
    static class FileLoad
    {
        static List<string> files;
        
        
        static List<string> projectDetails;
        static List<Button> createdButtons;





        static public List<Button> LoadProjects()
        {
            createdButtons = new List<Button>();
          
            files = Directory.GetFiles(@"C:\Users\oline\Desktop\ProjectManagmentFiles\").ToList();

            
            //check for project card files
            
            
            foreach (string file in files)
            {

                //check for project's card file
                if (file.Contains("cards"))
                {
                    continue;
                }
                


                projectDetails = new List<string>();

                var tempButton = new Button();
                using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        
                        string fileString = br.ReadString();

                        //gets all project details in file

                        string[] details = fileString.Split(",");


                        foreach (string word in details)
                        {
                            projectDetails.Add(word);
                        }
                 

                        br.Close();
                    }

                    fs.Close();
                }



                    //data order:
                    //start date
                    //end date
                    //project name

                    //adds details to button tag
                    tempButton.Tag = projectDetails;

                    tempButton.Content = projectDetails[2];

                    createdButtons.Add(tempButton);

                


            }

            return createdButtons;

            
        }
    }
}
