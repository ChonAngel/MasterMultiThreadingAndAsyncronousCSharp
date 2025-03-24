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

        private void button1_Click(object sender, EventArgs e)
        {
            //ShowMessage("First Message", 2000);
            Thread thread = new Thread(() => ShowMessage("First Message", 2000));
            thread.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //ShowMessage("Second Message", 3000);
            Thread thread = new Thread(() => ShowMessage("Second Message", 3000));
            thread.Start();
        }

        //private void ShowMessage(string message, int delay)
        //{
        //    Thread.Sleep(delay);
        //    lblMessage.Text = message;
        //}

        private void ShowMessage(string message, int delay)
        {
            Thread.Sleep(delay);
            if (lblMessage.InvokeRequired)
            {
                lblMessage.Invoke(new Action(() => lblMessage.Text = message));
            }
            else
            {
                lblMessage.Text = message;
            }
        }

    }
}
