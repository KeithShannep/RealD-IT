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
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace RealD_IT
{    
    public partial class MainWindow : Window
    {
        //Hold Attachment paths
        List<string> myAttachmentPaths;

        public MainWindow()
        {
            InitializeComponent();
            myAttachmentPaths = new List<string>();
        }

        //Email helpdesk Button.
        private void Submit_Click_1(object sender, RoutedEventArgs e)
        {
            {
                //Get Text from Rich textbox
                TextRange Issuetext = new TextRange(IssueBox.Document.ContentStart, IssueBox.Document.ContentEnd);

                string allIssText = Issuetext.Text;                              

                TextRange ResText = new TextRange(ResolutionBox.Document.ContentStart, ResolutionBox.Document.ContentEnd);

                string allResText = ResText.Text;


                try
                {
                    //Message to show blank fields

                    if (NameBox.Text == "")
                    {
                        MessageBox.Show("Please enter Name.");
                        return;
                    }
                    else if (LocationBox.Text == "")
                    {
                        MessageBox.Show("Please select a location.");
                        return;
                    }

                    else if (CategoryBox.Text == "")
                    {
                        MessageBox.Show("Please choose a category.");
                        return;
                    }
                    else if (DepartmentBox.Text == "")
                    {
                        MessageBox.Show("Please select a department");
                        return;
                    }
                    else if (Issuetext.Text == "")
                    {
                        MessageBox.Show("Please describe the problem you are having.");
                        return;
                    }

                    // Create the Outlook application.
                    Outlook.Application oApp = new Outlook.Application();

                    // Create a new mail item.
                    Outlook.MailItem oMsg = (Outlook.MailItem)oApp.CreateItem(Outlook.OlItemType.olMailItem);

                    //Add Attachment from Listbox
                    if (AttachmentBox.Items != null)
                    {
                        foreach (string fileLoc in myAttachmentPaths)
                        {
                            //attach the file
                            Outlook.Attachment oAttach = oMsg.Attachments.Add(fileLoc);
                        }
                    }

                    //Subject line            
                    oMsg.Subject = " " + this.LocationBox.Text + "-" + this.CategoryBox.Text + "-" + this.PriorityBox.Text;

                    //Add the recipient
                    Outlook.Recipients oRecips = (Outlook.Recipients)oMsg.Recipients;

                    Outlook.Recipient oRecip = (Outlook.Recipient)oRecips.Add("helpdesk@reald.com");


                    //Dpartments CC
                    // If AR Finance is selected in departments 

                    if (this.DepartmentBox.SelectedIndex == 0)
                    {
                        Outlook.Recipient CC1 = (Outlook.Recipient)oRecips.Add("areges@reald.com");
                        Outlook.Recipient CC2 = (Outlook.Recipient)oRecips.Add("ltorgeson@reald.com");
                    }


                    //Body of the email             
                    oMsg.HTMLBody =
                        "<p><font color=white>@</font><Strong>Category=</strong>" + this.CategoryBox.Text +
                        "<br />" +
                        "<p><font color=white>@</font><Strong>Priority=</strong>" + this.PriorityBox.Text +
                        "<br />" +
                        "<p><font color=white>@</font><Strong>Status=</strong>" + this.StatusBox.Text +
                        "<br />" +
                        "<p><font color=white>@</font><Strong>Resolution=</strong>" + ResText.Text +
                        "<br />" +
                        "<br />" +
                        "<Strong> Neme:</strong>" + this.NameBox.Text +
                        "<br />" +
                        "<Strong> Location:</strong>" + this.LocationBox.Text +
                        "<br />" +
                        "<Strong> Issue:</strong>" + Issuetext.Text +
                        "<br />" +                       
                        "<Strong> Resolution:</strong>" + ResText.Text;


                    // If Autonomy/MASS500/Filesite is selected in category ARKUS
                    if (this.CategoryBox.SelectedIndex == 2 | this.CategoryBox.SelectedIndex == 4 | this.CategoryBox.SelectedIndex == 9)
                    {
                        Outlook.Recipient CC3 = (Outlook.Recipient)oRecips.Add("Arkus@reald.com");
                        Outlook.Recipient CC4 = (Outlook.Recipient)oRecips.Add("mweinberg@reald.com");
                        Outlook.Recipient CC5 = (Outlook.Recipient)oRecips.Add("nkameron@reald.com");

                        oMsg.HTMLBody =
                        "<p><font color=white>@</font><Strong>Category=</strong>" + this.CategoryBox.Text +
                        "<br />" +
                        "<p><font color=white>@</font><Strong>Priority=</strong>" + this.PriorityBox.Text +
                        "<br />" +
                        "<p><font color=white>@</font><Strong>Status=</strong>" + this.StatusBox.Text +
                        "<br />" +
                        "<p><font color=white>@</font><Strong>Resolution=</strong>" + ResText.Text +
                        "<br />" +
                        "<p><font color=white>@</font><Strong>Owner=</strong>Unassigned" +
                        "<br />" +
                        "<br />" +
                        "<Strong> Neme:</strong>" + this.NameBox.Text +
                        "<br />" +
                        "<Strong> Location:</strong>" + this.LocationBox.Text +
                        "<br />" +
                        "<Strong> Issue:</strong>" + Issuetext.Text +
                        "<br />" +                        
                        "<Strong> Resolution:</strong>" + ResText.Text;
                    }

                    // If AD Change/AD Password/Security is selected in category Nick Kameron
                    if (this.CategoryBox.SelectedIndex == 0 | this.CategoryBox.SelectedIndex == 1 | this.CategoryBox.SelectedIndex == 15)
                    {
                        Outlook.Recipient CC6 = (Outlook.Recipient)oRecips.Add("nkameron@reald.com");

                        oMsg.HTMLBody =
                        "<p><font color=white>@</font><Strong>Category=</strong>" + this.CategoryBox.Text +
                        "<br />" +
                        "<p><font color=white>@</font><Strong>Priority=</strong>" + this.PriorityBox.Text +
                        "<br />" +
                        "<p><font color=white>@</font><Strong>Status=</strong>" + this.StatusBox.Text +
                        "<br />" +
                        "<<p><font color=white>@</font><Strong>Owner=</strong>Nick Kameron" +
                        "<br />" +
                        "<br />" +
                        "<Strong> Neme:</strong>" + this.NameBox.Text +
                        "<br />" +
                        "<Strong> Location:</strong>" + this.LocationBox.Text +
                        "<br />" +
                         "<Strong> Issue:</strong>" + Issuetext.Text;
                        
                    }

                    //Resolves all recipients
                    oMsg.Recipients.ResolveAll();

                    // Send.
                    oMsg.Send();

                    // Clean up.
                    oRecip = null;
                    oRecips = null;
                    oMsg = null;
                    oApp = null;

                    // display submitted box
                    MessageBox.Show("Your ticket has been submitted!");

                    Close();
                }//end of try block
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        //Drag and drop for attachment box
        private void AttachmentBox_Drop(object sender, DragEventArgs e)
        {
            string[] DropPath = e.Data.GetData(DataFormats.FileDrop, true) as string[];
            foreach (string dropfilepath in DropPath)
            {
                ListBoxItem listboxitem = new ListBoxItem();
                if (System.IO.Path.GetExtension(dropfilepath).Contains("."))
                {
                    myAttachmentPaths.Add(System.IO.Path.GetFullPath(dropfilepath));
                    listboxitem.Content = System.IO.Path.GetFileNameWithoutExtension(dropfilepath);
                    listboxitem.ToolTip = DropPath;
                    AttachmentBox.Items.Add(listboxitem);
                }
            }
        }   

        //Delete item from Attachment listbox
        private void MainWindow_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 20; i++)
            {
                AttachmentBox.Items.Add(new Random().Next().ToString());
            }
        }

        //Delete item from Attachment listbox
        private void AttachmentBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back | e.Key == Key.Delete)
            {
                AttachmentBox.Items.RemoveAt(AttachmentBox.SelectedIndex);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //Attachment click box
        private void Attachmentbutton_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem listboxitem = new ListBoxItem();
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                myAttachmentPaths.Add(System.IO.Path.GetFullPath(filename));
                listboxitem.Content = System.IO.Path.GetFileNameWithoutExtension(filename);
                AttachmentBox.Items.Add(listboxitem);
            }
        }
    }
}
