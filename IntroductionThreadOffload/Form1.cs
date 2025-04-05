using System.Threading.Tasks;

namespace IntroductionThreadOffload
{
    // Offload long running task to another thread Concept
    // We call worker thread and does not block main thread (Not block UI from the user)
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    //ShowMessage("First Message", 2000);
        //    Thread thread = new Thread(() => ShowMessage("First Message", 2000));
        //    thread.Start();
        //}

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    //ShowMessage("Second Message", 3000);
        //    Thread thread = new Thread(() => ShowMessage("Second Message", 3000));
        //    thread.Start();
        //}

        ////private void ShowMessage(string message, int delay)
        ////{
        ////    Thread.Sleep(delay);
        ////    lblMessage.Text = message;
        ////}

        //private void ShowMessage(string message, int delay)
        //{
        //    Thread.Sleep(delay);

        //    //// lblMessage maintain by UI thread(Own Thread)
        //    //lblMessage.Text = message;

        //    //// Call the delegate within the thread that the label was created in
        //    //lblMessage.Invoke(() => lblMessage.Text = message);

        //    // If the current thread is not the UI thread, then we need to invoke the delegate otherwise we can directly update the label
        //    if (lblMessage.InvokeRequired)
        //    {
        //        lblMessage.Invoke(new Action(() => lblMessage.Text = message));
        //    }
        //    else
        //    {
        //        lblMessage.Text = message;
        //    }
        //}

        private async void button1_Click(object sender, EventArgs e)
        {
            await ShowMessageAsync("First Message", 3000);
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await ShowMessageAsync("Second Message", 2000);
        }

        private async Task ShowMessageAsync(string message, int delay)
        {
            await Task.Delay(delay);
            
            // await keyword make the continuation run on the same context on the UI thread
            lblMessage.Text = message;

        }

    }
}
