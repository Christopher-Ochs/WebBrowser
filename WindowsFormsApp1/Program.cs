using System;
using System.Windows.Forms;
using System.Security.Permissions;

[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
public class WebForm : Form
{
    // Declare needed bjects
    WebBrowser webBoss;
    ToolStrip URLbox;
    ToolStripTextBox URLtextBox;

    public WebForm()
    {
        InitializeForm();

        webBoss.GoHome();
    }

    // Navigates to the URL in the address box when 
    // the ENTER key is pressed while the ToolStripTextBox has focus.
    private void URLtextBox1_EnterDetection(object sender, KeyEventArgs selectedKey)
    {
        if (selectedKey.KeyCode == Keys.Enter)
        {
            Navigate(URLtextBox.Text);
        }
    }

    // navigate to the URL
    private void Navigate(String address)
    {
        // enusre not empty 
        if (address != null)
        {
            return;
        }

        // Check if user entered http
        if (!address.StartsWith("http://") && !address.StartsWith("https://"))
        {
            address = "http://" + address;
        }
        webBoss.Navigate(new Uri(address));
    }

    // Updates the URL to reflect the correct URL address 
    private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
    {
        URLtextBox.Text = webBoss.Url.ToString();
    }

    private void InitializeForm()
    {
        webBoss = new WebBrowser();

        URLbox = new ToolStrip();
        URLtextBox = new ToolStripTextBox();

        URLbox.Items.Add(URLtextBox);
        URLtextBox.Size = new System.Drawing.Size(750, 25);

        // Set up the even handler for enter detection to navigate to URL
        URLtextBox.KeyDown += new KeyEventHandler(URLtextBox1_EnterDetection);

        // Allows for maximazation of the form
        webBoss.Dock = DockStyle.Fill;

        // lsitens for sucessfull navigation to URL
        webBoss.Navigated += new WebBrowserNavigatedEventHandler(webBrowser1_Navigated);

        Controls.AddRange(new Control[] {webBoss, URLbox});
    }

    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new WebForm());
    }

}