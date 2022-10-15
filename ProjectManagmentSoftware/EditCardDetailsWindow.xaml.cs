using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace ProjectManagmentSoftware
{
    /// <summary>
    /// Interaction logic for EditCardDetailsWindow.xaml
    /// </summary>
    public partial class EditCardDetailsWindow : Window
    {
        Card selectedCard;
        Button selectedButton;
        public EditCardDetailsWindow(Card selectedCard, Button selectedButton = null)
        {
            InitializeComponent();
            this.selectedCard = selectedCard;
            this.selectedButton = selectedButton;
        }

       


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StartDatePicker.DisplayDateStart = Project.startDate;
            StartDatePicker.DisplayDateEnd = Project.endDate;
            EndDatePicker.DisplayDateStart = Project.startDate;
            EndDatePicker.DisplayDateEnd = Project.endDate;
            

            //loads card details
            TitleTextBox.Text = selectedCard.GetTitle();
            DescriptionTextBox.Text = selectedCard.GetDescription();
            StartDatePicker.SelectedDate = selectedCard.GetStartDate();
            EndDatePicker.SelectedDate = selectedCard.GetEndDate();
            NotificationEnabledCheck.IsChecked = selectedCard.GetNotificationEnabled();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


            //used to check the state of the tickbox
            bool tempNotificationChecked;
            if (NotificationEnabledCheck.IsChecked == true)
            {
                tempNotificationChecked = true;
            }
            else
            {
                tempNotificationChecked = false;
            }


            //updates card's details
            selectedCard.SetTitle(TitleTextBox.Text);
            selectedCard.SetDescription(DescriptionTextBox.Text);
            selectedCard.SetStartDate(Convert.ToDateTime(StartDatePicker.Text));
            selectedCard.SetEndDate(Convert.ToDateTime(EndDatePicker.Text));
            selectedCard.SetNotificationEnabled(tempNotificationChecked);

            if (selectedButton != null)
            {
                selectedButton.Content = selectedCard.GetTitle();
            }
            

            //closes the window
            this.Close();
        }
    }
}
