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
        static List<string> projectCardFile;
        
        
        static List<string> projectDetails;
        static List<Button> createdButtons;





        static public List<Button> LoadProjects()
        {
            createdButtons = new List<Button>();
          
            projectCardFile = Directory.GetFiles(@"C:\Users\oline\Desktop\ProjectManagmentFiles\").ToList();

            
            //check for project card files
            
            
            foreach (string file in projectCardFile)
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

        static public void LoadCards()
        {
            List<Card> tempCards = new List<Card>();

            if (!File.Exists(@"C:\Users\oline\Desktop\ProjectManagmentFiles\" + Project.projectName + " cards" + ".pm"))
            {
                MessageBox.Show("Error: No cards found for project");
                return;
            }

            using (FileStream fs = new FileStream(@"C:\Users\oline\Desktop\ProjectManagmentFiles\" + Project.projectName + " cards" + ".pm", FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    string cardString = "";

                    //validation for broken card save files 
                    try
                    {
                       cardString = br.ReadString();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error: Issue reading cards");
                        return;
                        
                    }

                    

                    string[] cards = cardString.Split("**");

                    
                    



                    for (int i = 0; i < cards.Length - 1; i++)
                    {
                        //compiles card info ino new card
                       tempCards.Add(CompileCard(cards[i]));
                    }


                    br.Close();
                }

                fs.Close();


                //sets project's cards to the cards loaded from the file 
                Project.cards = tempCards;
            }







        }

        static Card CompileCard(string card)
        {
            //card data order:
            //title 0
            //description 1
            //start date 2
            //end date 3
            //notificationEnabled 4
            //state 5


            string[] details = card.Split(",");

            Card tempCard = new Card(details[0], Convert.ToDateTime(details[2]), Convert.ToDateTime(details[3]), details[1], Convert.ToBoolean(details[4]));

            object newState;

            //sets card strate in kanban board
            Enum.TryParse(typeof(State), details[5], out newState);


            tempCard.SetState((State)newState);

                
            return tempCard;


        }
    }
}
