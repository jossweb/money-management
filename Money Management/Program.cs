namespace Money_Management
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
    static class CreateEntities
    {
        public static Panel HomePanel()
        {
            Panel homePanel = new Panel();
            homePanel.Size = new Size(500, 600);
            homePanel.Location = new Point(0, 0);

            homePanel.BackColor = Color.Green;

            return homePanel;
        }
    }
    static class Style
    {
        public static void SetStyleForm(Form form)
        {
            form.BackColor = Color.White;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Text = "Money Management";
            form.ClientSize = new Size(600, 500);
        }
    }
}